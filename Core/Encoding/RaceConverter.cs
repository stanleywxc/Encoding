using System;
using System.Text;

//Local Imports
using Echo.Services.Encoding.Exceptions;
using Echo.Services.Encoding.Helper;
using Echo.Services.Encoding.Interface;
using Echo.Services.Encoding.Unicode;

namespace Echo.Services.Encoding.Encoding
{
	internal enum Race
	{
		ZERO					= 0x00,
		DOUBLE_ESCAPE			= 0x0099,
		NULL_COMPRESSION_FLAG	= 0xD8,
		DOUBLE_F				= 0xFF,
		DOUBLE_9				= 0x99,
		MAX_COMPRESSION_SIZE	= 63
	};

	/// <summary>
	/// Summary description for RaceEncoder.
	/// </summary>
	internal class RaceConverter : Converter
	{
		//---------------------------------------------------------------------
		// Static Data members
		//---------------------------------------------------------------------
		public static string		ACE_PREFIX = "bq--";

		//---------------------------------------------------------------------
		// Properties
		//---------------------------------------------------------------------
		public override string Prefix
		{
			get { return ACE_PREFIX; }
		}

		//---------------------------------------------------------------------
		// Public methods
		//---------------------------------------------------------------------
		public override string Encode(UTF32String pSource, bool[] pCaseFlag)
		{
			string	source		= null;
			byte[]	compressed	= null;

			// get the UTF16 string
			source = pSource.ToUTF16();

			// compress
			compressed = this.Compress(source);
		
			// Base32 encoding
			return Base32.Encode(compressed);
		}

		public override UTF32String Decode (string pSource, bool[] pCaseFlag)
		{
			byte[]			source		 = null;
			string			decompressed = null;

			// valid?
			pSource = StringUtil.Normalize(pSource);
			if (null == pSource)
				throw new RacecodeException("Invalid input parameter(null)");

			// check if it starts with prefix
			if (pSource.StartsWith(this.Prefix))
				pSource = StringUtil.Normalize(pSource.Substring(this.Prefix.Length));

			// valid?
			if (null == pSource)
				throw new RacecodeException("Invalid input parameter(null)");

			// get bytes for source, then decompress
			source		 = Base32.Decode(pSource);
			decompressed = this.Decompress(source);
			
			/// get UTF32
			return new UTF32String(decompressed);
		}

		public override bool Validate(UTF32String pSource, IEncodingOption pOption)
		{
			return true;
		}


		// compress - The race algorithm contains two main steps.  The Utf16 pSource
		// data is passed into a compression which yields a sequence of bytes.  This
		// byte sequence is then encoded using Base32 to ensure that the data
		// is dns compatible.
		private byte[] Compress(string pSource)
		{
			byte	u1				= 0;
			byte	u2				= 0;
			byte	n1				= 0;
			byte	highByte		= 0;
			int		offset			= int.MinValue;
			bool	bCompressible	= false;
			byte[]	buffer			= null;
			byte[]	output			= null;

			// Input checking
			pSource = StringUtil.Normalize(pSource);
			if (null == pSource)
				throw new RacecodeException("Compress error, invalid source (null)");

			// Initializes
			buffer = new byte[(pSource.Length*2) + 1];

			// Should we compress? 
			u1				= (byte)Race.ZERO;
			bCompressible	= true;

			for (int index = 0; index < pSource.Length; index++) 
			{
				highByte = Converter.GetHighByte(pSource[index]);
				if (highByte != (byte)Race.ZERO) 
				{
					if (u1 == (byte)Race.ZERO) 
						u1 = highByte;
					else if (u1 != highByte)
					{
						bCompressible = false; 
						break;
					}
				}
			}

			// Compress the pSource
			offset = 0;
			try 
			{
				if (bCompressible) 
				{
					char c = (char)(u1 & 0x00ff);

					if (c >= 0x00d8 && c <= 0x00df)
						throw new RacecodeException(string.Format("Compress error, bad surrogate: {0}", c));

					buffer[offset++] = u1;
					for (int index = 0; index < pSource.Length; index++) 
					{
						// check double escape
						if (pSource[index] == (char)Race.DOUBLE_ESCAPE)
							throw new RacecodeException(string.Format("Compress error, double escape present: {0}", index));

						// check seperators
						if (Converter.IsSeperator((int)pSource[index])) 
							throw new RacecodeException(string.Format("Compress error, delimiter found: {0}", index));

						u2 = Converter.GetHighByte	(pSource[index]);
						n1 = Converter.GetLowByte	(pSource[index]);
						if (u2 == u1)
						{
							if (n1 != (byte)Race.DOUBLE_F) 
							{
								buffer[offset++] = n1;
							}
							else
							{
								buffer[offset++] = (byte)Race.DOUBLE_F;
								buffer[offset++] = (byte)Race.DOUBLE_9;
							}
						} 
						else 
						{
							buffer[offset++] = (byte)Race.DOUBLE_F;
							buffer[offset++] = n1;
						}
					}
				} 
				else 
				{
					buffer[offset++] = (byte)Race.NULL_COMPRESSION_FLAG;

					for (int index = 0; index < pSource.Length; index++) 
					{
						buffer[offset++] = Converter.GetHighByte(pSource[index]);
						buffer[offset++] = Converter.GetLowByte(pSource[index]);
					}
				}

				// need to create a new buffer?
				if (offset != buffer.Length) 
				{
					output = new byte[offset];

					// copy the result
					Array.Copy(buffer, 0, output, 0, offset);
				} 
				else
					output = buffer;
			} 
			catch (Exception e) 
			{
				throw new RacecodeException(string.Format("Compress error, exception: {0}", e.ToString()));
			}

			// Ensure compression does not exceed allowed length
			if (output.Length > (int)Race.MAX_COMPRESSION_SIZE)
				throw new RacecodeException(string.Format("Compress error, compressed result to long: {0}", output.Length));

			return output;
		}

	
		// decompress - Reverse the Race compression and return a sequence of Utf16
		// characters.
		private string Decompress(byte[] pSource)
		{
			byte			u1						= 0;
			byte			n1						= 0;
			byte			highByte				= 0;
			byte			lowByte					= 0;
			bool			bFoundInvalidDnsChar	= false;
			bool			bFoundUnescapedOctet	= false;
			bool			bFoundDoubleF			= false;
			bool			bCompressible			= false;
			char			delta					= (char)0;
			StringBuilder	buffer					= null;

			// Input checking
			if ((null == pSource) || (0 == pSource.Length))
				throw new RacecodeException("Decompress error, Invalid input (null)");

			// check compression length
			if (pSource.Length > (int)Race.MAX_COMPRESSION_SIZE)
				throw new RacecodeException(string.Format("Decompress error, input too long: {0}", pSource.Length));

			// Initializes
			bFoundInvalidDnsChar	= false;
			bFoundUnescapedOctet	= false;
			bFoundDoubleF			= false;
			bCompressible			= true;
			buffer					= new StringBuilder();

			try 
			{
				if (pSource.Length == 1)
					throw new RacecodeException("Decompress error, odd octet length");

				u1 = pSource[0];

				// No compression was done, so copy the remaining octets into the output.
				// Ensure that no Delimiters are found in the output.
				// Ensure that the output could not have been compressed.
				if (u1 == (byte)Race.NULL_COMPRESSION_FLAG) 
				{
					if (pSource.Length % 2 == 0) 
						throw new RacecodeException(string.Format("Decompress error, odd octet length: {0}", pSource.Length));

					u1 = (byte)Race.ZERO;

					for (int index = 1; index < pSource.Length; index += 2)
					{
						highByte	= pSource[index];
						lowByte		= pSource[index + 1];

						// form a char
						delta = (char)((highByte << 8) | (lowByte & 0x00ff));

						// check delimiter
						if (Converter.IsSeperator((int)delta))
							throw new RacecodeException(string.Format("Decompress error, delimiter found: {0}", delta));

						if (highByte != (byte)Race.ZERO) 
						{
							if (u1 == (byte)Race.ZERO) 
								u1 = highByte;
							else if (u1 != highByte) 
								bCompressible = false;
						
							bFoundInvalidDnsChar = true;
						} 
						else 
						{
							if (!Converter.IsCharDnsCompatible(delta)) 
								bFoundInvalidDnsChar = true;
						}

						// Put into buffer
						buffer.Append(delta);
					}

					// compressible
					if (bCompressible)
						throw new RacecodeException("Decompress error, unusual compressible");
				} 
				else 
				{
					char c = (char)(u1 & 0x00ff);

					if ((c >= 0x00d8) && (c <= 0x00df))
						throw new RacecodeException(string.Format("Compress error, bad surrogate: {0}", c));

					for (int index = 1; index < pSource.Length; index++)
					{
						n1 = pSource[index];
						if (!bFoundDoubleF) 
						{
							if (n1 == (byte)Race.DOUBLE_F) 
								bFoundDoubleF = true;
							else 
							{
								delta = (char)((u1 << 8) | (n1 & 0x00ff));

								if (Converter.IsSeperator((int)delta))
									throw new RacecodeException(string.Format("Decompress error, delimiter found(2): {0}", delta));

								if (delta == (char)Race.DOUBLE_ESCAPE)
									throw new RacecodeException("Decompress error, double escape found");

								if (! Converter.IsCharDnsCompatible(delta))
									bFoundInvalidDnsChar = true;

								bFoundUnescapedOctet = true;

								buffer.Append(delta);
							}
						}
						else 
						{
							bFoundDoubleF = false;
							if (n1 == (byte)Race.DOUBLE_9) 
							{
								delta = (char)((u1<<8) | (0x00ff));

								bFoundUnescapedOctet = true;
								bFoundInvalidDnsChar = true;
								buffer.Append(delta);
							} 
							else 
							{
								delta = (char)(n1 & 0x00ff);
								if (u1 == (byte)Race.ZERO) 
									throw new RacecodeException("Decompress error, unnecessary escape found");

								if (Converter.IsSeperator((int)delta))
									throw new RacecodeException(string.Format("Decompress error, delimiter found(3): {0}", delta));

								if (! Converter.IsCharDnsCompatible(delta))
									bFoundInvalidDnsChar = true;

								buffer.Append(delta);
							}
						}
					}

					if (bFoundDoubleF)
						throw new RacecodeException("Decompress error, trailing escape found");

					if (u1 != (byte)Race.ZERO && !bFoundUnescapedOctet)
						throw new RacecodeException("Decompress error, decode none escape octet");
				}

				if (! bFoundInvalidDnsChar)
					throw new RacecodeException("Decompress error, invalid dns char found");
			} 
			catch(ACEException ae)
			{
				throw ae;
			}
			catch (Exception e) 
			{
				throw new RacecodeException(string.Format("Decompress error, exception: {0}", e.ToString()));
			}

			// Done
			return buffer.ToString();
		}
	}
}
