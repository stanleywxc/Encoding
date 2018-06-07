using System;
using System.Text;

namespace Echo.Services.Encoding.Encoding
{
	/// <summary>
	/// Summary description for Base32.
	/// </summary>
	public class Base32
	{
		//---------------------------------------------------------------------
		// Static data member
		//---------------------------------------------------------------------
		private static string		m_mapTable = "abcdefghijklmnopqrstuvwxyz234567";

		//---------------------------------------------------------------------
		// Constructors
		//---------------------------------------------------------------------
		private Base32()
		{
		}
	
		//---------------------------------------------------------------------
		// public Static Methods
		//---------------------------------------------------------------------
		public static string Encode(byte[] pBytes)
		{
			long			buffer	= 0;
			StringBuilder	encoded	= null;

			// Initializes
			encoded = new StringBuilder();

			// loop for bytes for encoding
			for(int index = 0; index != pBytes.Length; )
			{
				// first move 5 bytes into a long
				buffer <<= 8;
				buffer |= ((long)pBytes[index] & 0x00000000000000FF);

				// check if we have 5 bytes already
				if(++index % 5 == 0)
				{
					encoded.Append(Base32.Map(((int)(buffer >> 35)) & 0x001F));
					encoded.Append(Base32.Map(((int)(buffer >> 30)) & 0x001F));
					encoded.Append(Base32.Map(((int)(buffer >> 25)) & 0x001F));
					encoded.Append(Base32.Map(((int)(buffer >> 20)) & 0x001F));
					encoded.Append(Base32.Map(((int)(buffer >> 15)) & 0x001F));
					encoded.Append(Base32.Map(((int)(buffer >> 10)) & 0x001F));
					encoded.Append(Base32.Map(((int)(buffer >>  5)) & 0x001F));
					encoded.Append(Base32.Map(((int)buffer)         & 0x001F));
		
					// clear buffer
					buffer = 0;
				}
			}

			// check if the leftover bytes
			switch(pBytes.Length % 5)
			{
				case 1:
					buffer <<= 2;
					encoded.Append(Base32.Map(((int)(buffer >> 5)) & 0x001F));
					encoded.Append(Base32.Map(((int)buffer)        & 0x001F));
					break;
				case 2:
					buffer <<= 4;
					encoded.Append(Base32.Map(((int)(buffer >> 15)) & 0x001F));
					encoded.Append(Base32.Map(((int)(buffer >> 10)) & 0x001F));
					encoded.Append(Base32.Map(((int)(buffer >>  5)) & 0x001F));
					encoded.Append(Base32.Map(((int)buffer)         & 0x001F));
					break;
				case 3:
					buffer <<= 1;
					encoded.Append(Base32.Map(((int)(buffer >> 20)) & 0x001F));
					encoded.Append(Base32.Map(((int)(buffer >> 15)) & 0x001F));
					encoded.Append(Base32.Map(((int)(buffer >> 10)) & 0x001F));
					encoded.Append(Base32.Map(((int)(buffer >>  5)) & 0x001F));
					encoded.Append(Base32.Map(((int)buffer)         & 0x001F));
					break;
				case 4:
					buffer <<= 3;
					encoded.Append(Base32.Map(((int)(buffer >> 30)) & 0x001F));
					encoded.Append(Base32.Map(((int)(buffer >> 25)) & 0x001F));
					encoded.Append(Base32.Map(((int)(buffer >> 20)) & 0x001F));
					encoded.Append(Base32.Map(((int)(buffer >> 15)) & 0x001F));
					encoded.Append(Base32.Map(((int)(buffer >> 10)) & 0x001F));
					encoded.Append(Base32.Map(((int)(buffer >>  5)) & 0x001F));
					encoded.Append(Base32.Map(((int)buffer)         & 0x001F));
					break;
			}

			return encoded.ToString();
		}

		public static byte[] Decode(string pEncoded)
		{
			int		offset	= 0;
			long	buffer	= 0;
			byte[]	decoded	= null;

			// Initializes
			offset	= 0;
			buffer	= 0;
			decoded = new byte[pEncoded.Length * 5 / 8];

			// for every char, combine them into a long
			for (int index = 0; index != pEncoded.Length;)
			{
				// first decode the value into a long buffer
				buffer <<= 5;
				buffer  |= (((long)Base32.Unmap(pEncoded[index])) & 0x00000000000000FF);

				if ((++index % 8) == 0)
				{
					decoded[offset++] = (byte)((buffer >> 32)& 0x00000000000000FF);
					decoded[offset++] = (byte)((buffer >> 24)& 0x00000000000000FF);
					decoded[offset++] = (byte)((buffer >> 16)& 0x00000000000000FF);
					decoded[offset++] = (byte)((buffer >> 8) & 0x00000000000000FF);
					decoded[offset++] = (byte)(buffer		 & 0x00000000000000FF);

					//reset
					buffer	 = 0;
				}
			}

			switch(pEncoded.Length % 8)
			{
				case 2:
					decoded[offset] = (byte)((buffer >> 2) & 0x00000000000000FF);
					break;
				case 4:
					decoded[offset++] = (byte)((buffer >> 12) & 0x00000000000000FF);
					decoded[offset++] = (byte)((buffer >> 4)  & 0x00000000000000FF);
					break;
				case 5:
					decoded[offset++] = (byte)((buffer >> 17) & 0x00000000000000FF);
					decoded[offset++] = (byte)((buffer >>  9) & 0x00000000000000FF);
					decoded[offset++] = (byte)((buffer >>  1) & 0x00000000000000FF);					
					break;
				case 7:
					decoded[offset++] = (byte)((buffer >> 27) & 0x00000000000000FF);
					decoded[offset++] = (byte)((buffer >> 19) & 0x00000000000000FF);
					decoded[offset++] = (byte)((buffer >> 11) & 0x00000000000000FF);
					decoded[offset++] = (byte)((buffer >>  3) & 0x00000000000000FF);
					break;
			}

			// Done
			return decoded;
		}

		//---------------------------------------------------------------------
		// private static methods
		//---------------------------------------------------------------------
		private static char Map(int pValue)
		{
			return (char)m_mapTable[pValue];
		}

		private static int Unmap(char pChar)
		{
			return m_mapTable.IndexOf(pChar);
		}
	}
}
