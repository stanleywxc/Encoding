using System;
using System.Text;

//Local Imports
using Echo.Services.Encoding.Exceptions;
using Echo.Services.Encoding.Helper;
using Echo.Services.Encoding.Interface;
using Echo.Services.Encoding.Unicode;

namespace Echo.Services.Encoding.Encoding
{
	internal enum Punycode
	{
		BASE				= 36,
		T_MIN				= 1,
		T_MAX				= 26,
		SKEW				= 38,
		DAMP				= 700,
		INITIAL_BIAS		= 72,
		INITIAL_N			= 0x80,
		LOBASE				= 35,		// BASE - T_MIN,
		CUTOFF				= 468,		// (LOBASE * T_MAX) / 2,
		MAX_UNICODE			= 0x10FFFF,
		PUNYCODE_MAX_LENGTH	= 256,
		DELIMITER			= 0x002D
	};

	/// <summary>
	/// Summary description for PunyEncoder.
	/// </summary>
	internal class PunyConverter : Converter
	{
		//---------------------------------------------------------------------
		// Static Data members
		//---------------------------------------------------------------------
		public static string		ACE_PREFIX = "xn--";
		
		//---------------------------------------------------------------------
		// Properties
		//---------------------------------------------------------------------
		public override string Prefix
		{
			get { return ACE_PREFIX; }
		}

		public override string Encode(UTF32String pSource, bool[] pCaseFlag)
		{
			int				currentLargestCP	= 0;
			int				delta				= 0;
			int				cpsHandled			= 0;
			int				bias				= 0;
			int				nextLargerCP		= 0;
			int				currentDelta		= 0;
			int				currentBase			= 0;
			int				threshold			= 0;
			int				basicCPsCount		= 0;
			StringBuilder	buffer				= null;

			// valid?
			if ((null == pSource) || (0 == pSource.Length))
				throw new PunycodeException("Invalid input(null)");

			// Initializes
			buffer				= new StringBuilder();
			bias				= (int)Punycode.INITIAL_BIAS;
			currentLargestCP	= (int)Punycode.INITIAL_N;

			// add all the basic code points to the output string
			for (int i = 0; i < pSource.Length; i++) 
			{
				int inputChar = pSource[i];

				if (Converter.IsAscii(inputChar)) 
				{
					if (pCaseFlag != null) 
						inputChar = PunyConverter.EncodeBasic(inputChar, pCaseFlag[i]);

					// Add it to the output
					buffer.Append((char)inputChar);
				}
			}

			basicCPsCount	= buffer.Length;
			cpsHandled		= basicCPsCount;

			// need to append the delimiter?
			if (basicCPsCount > 0)
				buffer.Append((char) Punycode.DELIMITER);

			// Main encoding loop
			while (cpsHandled < pSource.Length) 
			{
				// All non-basic code points < n have been
				// handled already.  Find the next larger one:
				nextLargerCP = int.MaxValue;
				for (int index = 0; index < pSource.Length; ++index) 
				{
					int inputChar = pSource[index];

					// Get the next largest one.
					if (inputChar >= currentLargestCP && inputChar < nextLargerCP) 
						nextLargerCP = inputChar;
				}

				// Increase delta enough to advance the decoder's
				// <currentLargestCP, i> state to <nextLargerCP, 0>,
				// but guard against overflow:
				if ((nextLargerCP - currentLargestCP) > ((int.MaxValue - delta) / (cpsHandled + 1)))
					throw new PunycodeException("Punycode Encoding Overflow");

				delta			+= (nextLargerCP - currentLargestCP) * (cpsHandled + 1);
				currentLargestCP = nextLargerCP;

				for (int index = 0; index < pSource.Length;  ++index) 
				{
					int inputChar = pSource[index];

					if ((inputChar) < currentLargestCP && ++delta == 0) 
						throw new PunycodeException("Output too large");

					if (inputChar == currentLargestCP) 
					{
						for (currentDelta = delta, currentBase = (int)Punycode.BASE; ; currentBase += (int)Punycode.BASE) 
						{
							if (buffer.Length >= (int)Punycode.PUNYCODE_MAX_LENGTH)
								throw new PunycodeException("Output too long");

							// calculate the threshold
							if (currentBase <= bias)
								threshold = (int)Punycode.T_MIN;
							else 
								threshold = (currentBase >= (bias + (int)Punycode.T_MAX)) ? (int)Punycode.T_MAX : currentBase - bias;

							if (currentDelta < threshold) 
							{
								int outputDelta = currentDelta;

								// determine the current uppercase flag
								bool ucFlag = (pCaseFlag != null) ? ucFlag = pCaseFlag[index] : false;

								// encode the delta
								char encodedDelta = (char) PunyConverter.EncodeDigit(outputDelta, ucFlag);

								// append the encoded delta to output
								buffer.Append(encodedDelta);

								break;
							}
							else 
							{
								int		outputDelta;
								char	encodedDelta;
	
								outputDelta = threshold + (currentDelta - threshold) % ((int)Punycode.BASE - threshold);

								// encode the delta
								encodedDelta = (char) PunyConverter.EncodeDigit(outputDelta, false);

								// append the encoded delta to output
								buffer.Append(encodedDelta);

								// adjust the delta value for next iteration
								currentDelta = (currentDelta - threshold) / ((int)Punycode.BASE - threshold);
							}
						}

						// Adapt the bias:
						bias	= PunyConverter.Adapt(delta, cpsHandled + 1, cpsHandled == basicCPsCount);
						delta	= 0;

						++cpsHandled;
					}
				}

				++delta;
				++currentLargestCP;
			}

			if (buffer.Length > (int)Punycode.PUNYCODE_MAX_LENGTH)
				throw new PunycodeException(string.Format("Output too long: {0}", buffer.Length));

			// Done.
			return buffer.ToString();
		}

		public override UTF32String Decode (string pSource, bool[] pCaseFlag)
		{
			int			decodedVal		= 0;
			//int			out				= 0;
			int			opIndex			= 0;
			int			bias			= 0;
			int			basicCPsCount	= 0;
			int			index			= 0;
			int			oldIndex		= 0;
			int			weight			= 0;
			int			currentBase		= 0;
			int			delta			= 0;
			int			digit			= 0;
			int			threshold		= 0;
			UTF32String	decoded			= null;

			pSource = StringUtil.Normalize(pSource);
			if (null == pSource)
				throw new PunycodeException("Invalid input parameter(null)");

			// check if it starts with prefix
			if (pSource.StartsWith(this.Prefix))
				pSource = StringUtil.Normalize(pSource.Substring(this.Prefix.Length));

			// valid?
			if (null == pSource)
				throw new PunycodeException("Invalid input parameter(null)");

			// check if it's delimiter
			if (pSource[pSource.Length - 1] == (char)Punycode.DELIMITER)
				throw new PunycodeException("Invalid input format: string ends with delimiter");

			// Initializes
			opIndex		= 0;
			bias		= (int)Punycode.INITIAL_BIAS;
			decodedVal	= (int)Punycode.INITIAL_N;
			decoded		= new UTF32String();

			// Handle the basic code points:  Let b be the number of input code
			// points before the last delimiter, or 0 if there is none, then
			// copy the first b code points to the output.
			for (basicCPsCount = 0, index = 0; index < pSource.Length; index++)
			{
				if (PunyConverter.IsDelimiter(pSource[index])) 
					basicCPsCount = index;
			}

			// check string length
			if (basicCPsCount > (int)Punycode.PUNYCODE_MAX_LENGTH)
				throw new PunycodeException(string.Format("Input string too long: {0}", basicCPsCount));

			// For each char 
			for (index = 0; index < basicCPsCount; index++)
			{
				char inputChar = pSource[index];

				if (pCaseFlag != null) 
					pCaseFlag[index] = PunyConverter.IsFlagged(inputChar);

				// check if it's ascii
				if (!PunyConverter.IsAscii(inputChar)) 
					throw new PunycodeException(string.Format("Decoding error, bad char in input string: {0}", inputChar));

				decoded.Append((int)inputChar);
			}

			// Main decoding loop:  Start just after the last delimiter if any
			// basic code points were copied; start at the beginning otherwise.
			index = (basicCPsCount > 0) ? basicCPsCount + 1 : 0;
			while (index < pSource.Length) 
			{
				// index is the index of the next character to be consumed, and
				// out is the number of code points in the output array.
				// Decode a generalized variable-length integer into delta,
				// which gets added to i.  The overflow checking is easier
				// if we increase i as we go, then subtract off its starting
				// value at the end to obtain delta.
				oldIndex	= opIndex;
				weight		= 1;
				currentBase = (int)Punycode.BASE;

				for ( ; ; ) 
				{
					if (index >= pSource.Length)
						throw new PunycodeException("Bad input string for decoding");

					digit = PunyConverter.DecodeDigit(pSource[index++]);

					// valid?
					if (digit >= (int)Punycode.BASE) 
						throw new PunycodeException("Invalid input string for decoding");

					if (digit > (int.MaxValue - opIndex) / weight)
						throw new PunycodeException("Punycode decoding overflow");

					opIndex += digit * weight;

					// calculate the threshold
					if (currentBase <= bias)
						threshold = (int)Punycode.T_MIN;
					else
						threshold = ((currentBase - bias) >= (int)Punycode.T_MAX) ? (int)Punycode.T_MAX : (currentBase - bias);

					// Finished?
					if (digit < threshold)
						break;

					// check if weight is valid
					if (weight > (int.MaxValue / ((int)Punycode.BASE - threshold)))
						throw new PunycodeException(string.Format("Decoding overflow, Invalid weight: {0}", weight));

					// new weight
					weight		*= ((int)Punycode.BASE - threshold);
					currentBase += (int)Punycode.BASE;
				}

				// Adapt the bias
				delta	= (oldIndex == 0) ? opIndex / (int)Punycode.DAMP : (opIndex - oldIndex) >> 1;
				delta  += delta / (decoded.Length + 1);

				for (bias = 0;  delta > (int)Punycode.CUTOFF;  bias += (int)Punycode.BASE) 
					delta /= (int)Punycode.LOBASE;

				// new bias
				bias += ((int)Punycode.LOBASE + 1) * delta / (delta + (int)Punycode.SKEW);

				// opIndex was supposed to wrap around from decoded.length()+1 to 0,
				// incrementing n each time, so we'll fix that now:

				if (opIndex / (decoded.Length + 1) > (int.MaxValue - decodedVal))
					throw new PunycodeException(string.Format("Decooding overflow, invalid op index:{0}", opIndex));

				// calculate new index and value
				decodedVal	+= opIndex / (decoded.Length + 1);
				opIndex		%= (decoded.Length + 1);

				// valid?
				if (decoded.Length >= (int)Punycode.PUNYCODE_MAX_LENGTH)
					throw new PunycodeException(string.Format("Decoding overflow, decoded string too long:{0}", decoded.Length));

				// check case flag
				// Case of last character determines uppercase flag
				if (pCaseFlag != null) 
					pCaseFlag[opIndex] = PunyConverter.IsFlagged(pSource[index - 1]);

				// check if the number corresponds to a valid unicode character
				if (decodedVal > (int)Punycode.MAX_UNICODE) 
					throw new PunycodeException(string.Format("Decoding overflow, decoded code point out of range: U{0:X8}", decodedVal));

				// Insert decodedVal at position i of the output
				if (Converter.IsSeperator(decodedVal))
					throw new PunycodeException(string.Format("Decoding error, delimiter found: U{0:X8}", decodedVal));

				// add it to decoded output
				decoded.Insert(opIndex++, new int[1]{decodedVal});
			}

			// return the decoded
			return decoded;
		}

		public override bool Validate(UTF32String pSource, IEncodingOption pOption)
		{
			return true;
		}

		//---------------------------------------------------------------------
		// Private methods
		//---------------------------------------------------------------------
		private static bool IsDelimiter(int pCodePoint) 
		{
			return pCodePoint == (int)Punycode.DELIMITER;
		}

		private static bool IsFlagged(int pCodePoint) 
		{
			return pCodePoint - 65 < 26;
		}

		private static int DecodeDigit(char pDigit)
		{
			if (pDigit >= 0x30 && pDigit <= 0x39) 
				return pDigit - 0x16;
			else if (pDigit >= 0x41 && pDigit <= 0x5A)
				return pDigit - 0x41;
			else if (pDigit >= 0x61 && pDigit <= 0x7A) 
				return pDigit - 0x61;
			else
				throw new PunycodeException(string.Format("Invalid character: {0}\r\n", pDigit));
		}

		private static int EncodeDigit(int pDigit, bool pCaseFlag)
		{
			pDigit += 22;

			if (pCaseFlag) 
				pDigit -= 32;

			if (pDigit < 48) 
				pDigit += 75;

			return pDigit;
		}

		private static int EncodeBasic(int pBasicCodePoint, bool pCaseFlag) 
		{
			if (pBasicCodePoint - 97 < 26) 
				pBasicCodePoint -= 32;

			if (!pCaseFlag && (pBasicCodePoint - 65 < 26)) 
				pBasicCodePoint += 32;

			return pBasicCodePoint;
		}

		private static int Adapt(int pDelta, int pCodePointCount, bool pFirstTime) 
		{
			int	index = 0;

			pDelta  = pFirstTime ? (pDelta / (int)Punycode.DAMP) : (pDelta >> 1);
			pDelta += pDelta / pCodePointCount;

			for (index = 0;  pDelta > (int)Punycode.CUTOFF;  index += (int)Punycode.BASE) 
			{
				pDelta /= (int)Punycode.LOBASE;
			}

			return index + ((int)Punycode.LOBASE + 1) * pDelta / (pDelta + (int)Punycode.SKEW);
		}
	}
}
