using System;
using System.Text;
using System.Collections;

// Local Imports
using Echo.Services.Encoding.Exceptions;
using Echo.Services.Encoding.Helper;
using Echo.Services.Encoding.Interface;
using Echo.Services.Encoding.Unicode;

namespace Echo.Services.Encoding.Encoding
{
	/// <summary>
	/// Summary description for ACEConverter.
	/// </summary>
	internal class ACEConverter : IACEConverter
	{
		//---------------------------------------------------------------------
		// Data members
		//---------------------------------------------------------------------
		private IPreparer		m_preparer	= null;
		private IConverter		m_converter	= null;

		//---------------------------------------------------------------------
		// Constructors
		//---------------------------------------------------------------------
		public ACEConverter(IPreparer pPreparer, IConverter pConverter)
		{
			m_preparer	= pPreparer;
			m_converter	= pConverter;
		}

		//---------------------------------------------------------------------
		// Public methods
		//---------------------------------------------------------------------
		public string Encode(string pSource)
		{
			string				encoded		= null;
			StringBuilder		buffer		= null;
			EncodingOption		option		= null;
			UTF32String			source		= null;
			UTF32String			label		= null;
			UTF32String[]		labelArray	= null;
			

			// check if we have encoder or not
			if (null == m_converter)
				throw new ACEException("No ace converter defined");

			pSource = StringUtil.Normalize(pSource);
			if (null == pSource)
				throw new ACEException("Encoding error, invalid input(null, Encode)");

			// Initializes
			buffer		= new StringBuilder();
			source		= new UTF32String(pSource);
			option		= new EncodingOption();
			labelArray	= source.Split(Converter.SEPERATORS);

			// for each label do the encoding
			for (int index = 0; index < labelArray.Length; index++)
			{
				label = StringUtil.Normalize(labelArray[index]);
				if (null == label)
					throw new ACEException(string.Format("Encoding error, empty label: {0}", pSource.ToString()));

				// encode each label
				encoded = this.Encode(label, option);

				// append the encoded
				buffer.Append(encoded);
				if (index < (labelArray.Length - 1))
				{
					// Based on RFC3492, only allow FULL_STOP in
					// encoded string as seperator
					buffer.Append(".");
				}
			}

			// return the encoded string
			return buffer.ToString();
		}

		public string Decode (string pSource)
		{
			string				label		= null;
			string[]			labelArray	= null;
			StringBuilder		buffer		= null;
			EncodingOption		option		= null;
			UTF32String			decoded		= null;
			
			// check if we have encoder or not
			if (null == m_converter)
				return null;

			if (null == pSource)
				return null;

			// Initializes
			buffer		= new StringBuilder();
			option		= new EncodingOption();

			// SHOULD ONLY contain FULL_STOP as seperator(RFC3492)
			labelArray	= pSource.Split('.');	

			// for each label do the decoding
			for (int index = 0; index < labelArray.Length; index++)
			{
				label	= labelArray[index];
				decoded = StringUtil.Normalize(this.Decode(label, option));
				if (null == decoded)
					throw new ACEException(string.Format("Decoding error, empty label: {0}", pSource));
				

				// append the decoded
				buffer.Append(decoded.ToUTF16());
				if (index < (labelArray.Length - 1))
				{
					// Based on RFC3492, only allow FULL_STOP in
					// string as seperator after decoding
					buffer.Append(".");
				}
			}

			// return the decoded string
			return buffer.ToString();
		}

		//---------------------------------------------------------------------
		// Private methods
		//---------------------------------------------------------------------
		private string Encode(UTF32String pSource, IEncodingOption pOption)
		{
			bool		bAllAscii	= false;
			string		encoded		= null;
			UTF32String	prepared	= null;

			// Step #1: set the flag, all ascii?
			bAllAscii = Converter.IsAllAscii(pSource);

			// Step #2
			if (!bAllAscii)
			{
				// check if we need to prepare the string
				if (null != m_preparer)
					prepared = m_preparer.Prepare(pSource, pOption);
				else
					prepared = pSource;				
			}
			
			// Step #3: check if we need to apply the rules
			if (pOption.IsOptionSet(EncodingOption.USE_STD3_RULES))
			{
				// failed on Dns compatible?
				if (!Converter.IsAllDnsCompatible(prepared))
					throw new Std3RuleCodePointException(string.Format("The input does not conform to the STD 3 ASCII rules(DNS Compatible): {0}", prepared.ToString()));

				if (0 < prepared.Length)
				{
					// first char is hyphen?
					if (prepared[0] == Converter.CHAR_HYPHEN)
						throw new Std3RuleCodePointException(string.Format("The input does not conform to the STD 3 ASCII rules(Hyphen at the beginning): {0}", prepared.ToString()));

					// last char is hyphen?
					if (prepared[prepared.Length - 1] == Converter.CHAR_HYPHEN)
						throw new Std3RuleCodePointException(string.Format("The input does not conform to the STD 3 ASCII rules(Hyphen at the end): {0}", prepared.ToString()));
				}
			}

			//Step #4: check if it's all ascii already
			if (!bAllAscii)
			{
				// Step #5: check if it begin with the 'prefix'
				if (m_converter.IsBeginWithPrefix(prepared))
					throw new ACEException(string.Format("The input can't begin with an ACE prefix: {0}", pSource.ToString()));

				// Step #6:
				encoded = m_converter.Encode(prepared, new bool[prepared.Length]);

				//Step #7: insert the prefix
				encoded = encoded.Insert(0, m_converter.Prefix);
			}
			else
				encoded = pSource.ToUTF16();

			// Step #8
			if (encoded.Length > Converter.LABEL_MAX_LENGTH)
				throw new ACEException(string.Format("Encoded name too long: {0}", encoded.Length));

			// Done
			return encoded;
		}

		private UTF32String Decode(string pSource, IEncodingOption pOption)
		{
			string		check		= null;
			UTF32String decoded		= null;
			UTF32String	source		= null;

			try 
			{
				// Initializes
				source = new UTF32String(pSource);

				// Step #1-2
				if (!Converter.IsAllAscii(source) && (null != m_preparer))
					source = m_preparer.Prepare(source, pOption);

				// Step #3-5
				if (null != m_converter)
					decoded = m_converter.Decode(source.ToUTF16(), new bool[source.Length]);

				// Step #6-7
				if (pOption.IsOptionSet(EncodingOption.DECODE_DOUBLE_CHECK))
				{
					check = this.Encode(decoded, pOption);
					if (0 != string.Compare(check, pSource, true))
						throw new ACEException("Decoding round trip check failed");
				}
			} 
			catch (Exception e)
			{
				// Based on RFC3492, decode never fails.
				// check if we need to allow decode fail
				if (pOption.IsOptionSet(EncodingOption.ALLOW_DECODE_FAIL))
					throw e;

				decoded = new UTF32String(pSource);
			}

			// Step #8
			return decoded;
		}
	}
}
