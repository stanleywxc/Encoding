using System;

//Local Imports
using Echo.Services.Encoding.Interface;
using Echo.Services.Encoding.Unicode;

namespace Echo.Services.Encoding.Encoding
{
	/// <summary>
	/// Summary description for Converter.
	/// </summary>
	internal abstract class Converter : IConverter
	{
		//---------------------------------------------------------------------
		// Static data members
		//---------------------------------------------------------------------
		public static int			CHAR_HYPHEN			= 0x002D;
		public static int[]			SEPERATORS			= new int[4]{0x002E, 0x3002, 0xFF0E, 0xFF61};
		public static int			LABEL_MAX_LENGTH	= 63;

		//---------------------------------------------------------------------
		// Public methods
		//---------------------------------------------------------------------
		public bool IsBeginWithPrefix(UTF32String pSource)
		{
			if ((null == pSource) || (0 == pSource.Length))
				return false;

			// Done.
			return pSource.StartsWith(this.Prefix);
		}

		//---------------------------------------------------------------------
		// Static Methods
		//---------------------------------------------------------------------
		public static bool IsAscii(int c) 
		{
			return c < 0x80;
		}

		public static bool IsAllAscii(UTF32String pSource)
		{
			for (int index = 0; index < pSource.Length; index++)
			{
				if (pSource[index] > 0x7F)
					return false;
			}

			// Done
			return true;
		}

		public static bool IsAllDnsCompatible(UTF32String pSource)
		{
			for (int index = 0; index < pSource.Length; index++)
			{
				if (!IsCharDnsCompatible(pSource[index]))
					return false;
			}

			// Done
			return true;	
		}

		public static bool IsCharDnsCompatible(int pChar)
		{
			// high runner case
			if(pChar > 0x007A)
				return false;

			if	((pChar == 0x002D)						|| 
				(0x0030 <= pChar && pChar <= 0x0039)	||
				(0x0041 <= pChar && pChar <= 0x005A)	||
				(0x0061 <= pChar && pChar <= 0x007A))
			{
				return true;
			}

			// Done
			return false;
		}

		public static bool IsSeperator(int pChar)
		{
			for (int index = 0; index < SEPERATORS.Length; index++)
			{
				if (pChar == SEPERATORS[index])
					return true;
			}

			return false;
		}

		public static byte GetHighByte(char pChar) 
		{
			return (byte)((pChar & 0xff00) >> 8);
		}

		public static byte GetLowByte(char pChar) 
		{
			return (byte)(pChar & 0x00ff);
		}

		//---------------------------------------------------------------------
		// Abstract properties
		//---------------------------------------------------------------------
		public abstract string Prefix
		{
			get ;
		}

		//---------------------------------------------------------------------
		// Abstract methods
		//---------------------------------------------------------------------
		public abstract string		Encode		(UTF32String pSource, bool[] pCaseFlag);
		public abstract UTF32String	Decode		(string	pSource, bool[] pCaseFlag);
		public abstract bool		Validate	(UTF32String pSource, IEncodingOption pOption);
	}
}
