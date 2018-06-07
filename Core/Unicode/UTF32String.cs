using System;
using System.Collections;
using System.Text;

namespace Echo.Services.Encoding.Unicode
{
	/// <summary>
	/// Summary description for UnicodeString.
	/// </summary>
	public class UTF32String
	{
		//---------------------------------------------------------------------
		// Static data members
		//---------------------------------------------------------------------
		public static char			HIGH_SURROGATE_LLIMIT	= (char)0xD800;
		public static char			HIGH_SURROGATE_HLIMIT	= (char)0xDBFF;
		public static char			LOW_SURROGATE_LLIMIT	= (char)0xDC00;
		public static char			LOW_SURROGATE_HLIMIT	= (char)0xDFFF;
		public static int[]			WHITE_SPACES			= new int[]
									{
										0x0009, 0x000B, 0x000C, 
										0x0020, 0x00A0, 0x1680, 
										0x2000, 0x2001, 0x2002, 
										0x2003, 0x2004, 0x2005, 
										0x2006, 0x2007, 0x2008, 
										0x2009, 0x200A, 0x200B, 
										0x202F, 0x205F, 0x3000
									};



		//---------------------------------------------------------------------
		// Data members
		//---------------------------------------------------------------------
		private int[]				m_internal = null;

		//---------------------------------------------------------------------
		// Constructors
		//---------------------------------------------------------------------
		public UTF32String()
		{
			m_internal = new int[0];
		}

		public UTF32String(int[] pInput)
		{
			if (null != pInput)
				m_internal = (int[])pInput.Clone();
			else
				m_internal = new int[0];
		}

		public UTF32String(char[] pInput)
		{
			m_internal = UTF32String.GetInts(pInput);
		}

		public UTF32String(string pInput)
		{
			m_internal	= UTF32String.GetInts(pInput);
		}

		public UTF32String(UTF32String pUTF32)
		{
			if (null != pUTF32)
				m_internal = (int[])pUTF32.Ints.Clone();
			else
				m_internal = new int[0];
		}

		//---------------------------------------------------------------------
		// Properties
		//---------------------------------------------------------------------
		public int[] Ints
		{
			get { return m_internal;}
		}

		public int Length
		{
			get { return null == m_internal ? 0 : m_internal.Length; }
		}

		public int this[int index]
		{
			get { return this.IntAt(index); }
			set { this.SetAt(index, value);	}
		}

		//---------------------------------------------------------------------
		// Public methods
		//---------------------------------------------------------------------
		public int IntAt(int pIndex)
		{
			// the boundary will be checked automatically
			return m_internal[pIndex];
		}

		public void SetAt(int pIndex, int pValue)
		{
			if (null == m_internal)
				return ;

			// set the value
			// the boundary will be checked automatically
			m_internal[pIndex] = pValue;

			// Done
			return ;
		}

		public UTF32String Insert(int pIndex, UTF32String pInsert)
		{
			if (null == pInsert)
				return this.Insert(pIndex, (int[])null);

			return this.Insert(pIndex, pInsert.Ints);
		}

		public UTF32String Insert(int pIndex, int[] pInsert)
		{
			int[] newArray = null;

			if (null == pInsert)
				return new UTF32String(this);

			// check boundary
			if ((0 > pIndex) || (pIndex > this.Length))
				throw new ArgumentOutOfRangeException("index");

			newArray = new int[this.Length + pInsert.Length];

			// copy the first part
			Array.Copy (this.Ints,		0,		newArray, 0,						pIndex);
			Array.Copy (pInsert,		0,		newArray, pIndex,					pInsert.Length);
			Array.Copy (this.Ints,		pIndex, newArray, pIndex + pInsert.Length,	this.Length - pIndex);

			lock(m_internal)
			{	
				m_internal = (int[])newArray.Clone();
			}

			// create theh new instance
			return new UTF32String(newArray);
		}

		public UTF32String Remove(int pIndex)
		{
			int[] newArray = null;

			// check boundary
			if ((0 > pIndex) || (this.Length <= pIndex))
				throw new ArgumentOutOfRangeException("index");

			newArray = new int[this.Length - 1];
			
			// Copy the first part
			Array.Copy (this.Ints,	0, newArray, 0, pIndex);

			// last one?
			if (pIndex < (this.Length -1))
				Array.Copy (this.Ints,  pIndex + 1, newArray, pIndex,	this.Length - pIndex - 1);

			lock(m_internal)
			{	
				m_internal = (int[])newArray.Clone();
			}

			return new UTF32String(newArray);
		}

		public UTF32String TrimStart()
		{
			int		start		= 0;
			int[]	newArray	= null;

			// valid?
			if (0 == this.Length)
				return new UTF32String();

			// find the first non-whitespace
			while((start < this.Length) && this.IsInArray(WHITE_SPACES, this[start]))
			{
				start++;
			}

			//  create a new array
			newArray = new int[this.Length - start];

			// copy the reset
			Array.Copy(this.Ints, start, newArray, 0, this.Length - start);

			// reset itself
			lock(m_internal)
			{
				m_internal = (int[])newArray.Clone();
			}

			// return the new String
			return new UTF32String(newArray);
		}

		public UTF32String TrimEnd()
		{
			int		end			= 0;
			int[]	newArray	= null;

			// valid?
			if (0 == this.Length)
				return new UTF32String();

			end = this.Length - 1;
			// find the first non-whitespace
			while((end >= 0) && this.IsInArray(WHITE_SPACES, this[end]))
			{
				end--;
			}

			//  create a new array
			newArray = new int[end + 1];

			// copy the reset
			Array.Copy(this.Ints, 0, newArray, 0, end + 1);

			// reset itself
			lock(m_internal)
			{
				m_internal = (int[])newArray.Clone();
			}

			// return the new String
			return new UTF32String(newArray);
		}

		public UTF32String Trim()
		{
			int		start		= 0;
			int		end			= 0;
			int[]	newArray	= null;

			// valid?
			if (0 == this.Length)
				return new UTF32String();

			// Initializes
			start	= 0;
			end		= this.Length - 1;

			// find the first non-whitespace
			while((start < this.Length) && this.IsInArray(WHITE_SPACES, this[start]))
			{
				start++;
			}

			// find the last non-whitspace
			while((end >= 0) && this.IsInArray(WHITE_SPACES, this[end]))
			{
				end--;
			}

			// allocate a new memory
			newArray = new int[end - start + 1];

			// Copy the data over
			Array.Copy(this.Ints, start, newArray, 0, end - start + 1);

			// reset itself
			lock(m_internal)
			{
				m_internal = (int[])newArray.Clone();
			}

			return new UTF32String(newArray);
		}

		public UTF32String Append(int pChar)
		{
			int[] newArray = null;

			newArray = new int[this.Length + 1];

			// Copy the original over
			Array.Copy(this.Ints, 0, newArray, 0, this.Length);
			
			// append it
			newArray[this.Length] = pChar;

			lock(m_internal)
			{	
				m_internal = (int[])newArray.Clone();
			}
			
			// create a new instance
			return new UTF32String(newArray);
		}

		public UTF32String Append(int[] pChars)
		{
			int[] newArray = null;

			// null append?
			if (null == pChars)
				pChars = new int[0];

			newArray = new int[this.Length + pChars.Length];
			
			// copy the original over
			Array.Copy(this.Ints, 0, newArray, 0, this.Length);
			
			// append it
			Array.Copy(pChars, 0, newArray, this.Length, pChars.Length);

			lock(m_internal)
			{	
				m_internal = (int[])newArray.Clone();
			}

			return new UTF32String(newArray);
		}

		public UTF32String Append(char[] pChars)
		{
			return this.Append(UTF32String.GetInts(pChars));
		}

		public UTF32String Append(string pUTF16)
		{
			// valid?
			if (null == pUTF16)
				return new UTF32String(this.Ints);

			// create a new instance
			return this.Append(pUTF16.ToCharArray());
		}

		public UTF32String Append(UTF32String pUTF32)
		{
			// valid?
			if (null == pUTF32)
				return new UTF32String(this.Ints);

			// create a new instance
			return this.Append(pUTF32.Ints);
		}

		public UTF32String[] Split(int[] pSeperators)
		{
			int				c			= 0;
			int[]			eArray		= null;
			ArrayList		entryArray	= null;
			ArrayList		entry		= null;
			UTF32String[]	utfArray	= null;

			entry		= new ArrayList();
			entryArray	= new ArrayList();
			
			// for each char in the array
			for (int index = 0; index < this.Length; index++)
			{
				c = this[index];
				if (this.IsInArray(pSeperators, c))
				{
					eArray = new int[entry.Count];
					entry.CopyTo(0, eArray, 0, entry.Count);
					
					// add the entry array
					entryArray.Add(new UTF32String(eArray));

					// clear it
					entry.Clear();
				}
				else
					entry.Add (c);
			}

			// left over?
			if (0 != entry.Count)
			{
				eArray = new int[entry.Count];
				entry.CopyTo(0, eArray, 0, entry.Count);

				// add the entry array
				entryArray.Add(new UTF32String(eArray));
			}

			utfArray = new UTF32String[entryArray.Count];
	
			// copy it to the array.
			entryArray.CopyTo(0, utfArray, 0, entryArray.Count);

			// Done
			return utfArray;
		}

		public int IndexOf(int pChar)
		{
			int		index	= -1;
			bool	bFound	= false;

			// Loop each char to check matches from beginning
			for (index = 0; index < this.Length; index++)
			{
				bFound = (this[index] == pChar);
				if (bFound)
					break;
			}

			// Done.
			return bFound ? index : -1;
		}

		public int LastIndexOf(int pChar)
		{
			int		index	= -1;
			bool	bFound	= false;
			
			// Loop each char to check matches from the end.
			for (index = this.Length - 1; index >= 0; index--)
			{
				bFound = (this[index] == pChar);
				if (bFound)
					break;
			}

			return bFound ? index : -1;
		}

		public bool StartsWith(UTF32String pPattern)
		{
			// valid?
			if (null == pPattern)
				return false;

			// Done.
			return this.StartsWith(pPattern.Ints);
		}

		public bool StartsWith(string pPattern)
		{
			return this.StartsWith(UTF32String.GetInts(pPattern));
		}

		public bool StartsWith(int[] pPattern)
		{
			// valid?
			if (0 == this.Length)
				return false;

			if ((null == pPattern) || (0 == pPattern.Length))
				return false;

			if (pPattern.Length > this.Length)
				return false;

			for (int index = 0; index < pPattern.Length; index++)
			{
				if (this[index] != pPattern[index])
					return false;
			}

			// Done.
			return true;
		}

		public bool EndsWith(UTF32String pPattern)
		{
			// valid
			if (null == pPattern)
				return false;
	
			// Done
			return this.EndsWith(pPattern.Ints);
		}

		public bool EndsWith(string pPattern)
		{
			return this.EndsWith(UTF32String.GetInts(pPattern));
		}
		
		public bool EndsWith(int[] pPattern)
		{
			if (0 == this.Length)
				return false;

			// valid?
			if ((null == pPattern) || (0 == pPattern.Length))
				return false;

			if (pPattern.Length > this.Length)
				return false;

			for (int index = 0; index < pPattern.Length; index++)
			{
				if (pPattern [pPattern.Length - index - 1] != this[this.Length - index - 1])
					return false;
			}

			// Done.
			return true;
		}

		public string ToUTF16()
		{
			int				cPoint		= 0;
			char			hSurrogate	= (char)0;
			char			lSurrogate	= (char)0;
			StringBuilder	buffer		= null;

			// Initializes
			buffer = new StringBuilder();

			// for each char
			for (int index = 0; index < this.Length; index++)
			{
				cPoint = this[index];

				// need to split into surrogate?
				if ((cPoint & 0xf0000) != 0)
				{
					hSurrogate = UTF32String.HighSurrogate(cPoint);
					lSurrogate = UTF32String.LowSurrogate(cPoint);
				
					// append surrogate
					buffer.Append(hSurrogate);
					buffer.Append(lSurrogate);
				}
				else
					buffer.Append((char)cPoint);
			}
	
			// Done
			return buffer.ToString();
		}

		public override string ToString()
		{
			StringBuilder buffer = new StringBuilder();

			for (int index = 0; index < this.Length; index++)
				buffer.AppendFormat(", U{0:X8}", this[index]);

			return buffer.ToString();
		}

		//---------------------------------------------------------------------
		// Operators
		//---------------------------------------------------------------------
		public static implicit operator UTF32String (string pUTF16)
		{
			return (null == pUTF16) ? new UTF32String() : new UTF32String(pUTF16);
		}

		public static UTF32String operator + (UTF32String pUTF321, UTF32String pUTF322)
		{
			return new UTF32String(pUTF321).Append(pUTF322);
		}

		public static UTF32String operator + (UTF32String pUTF32, string pUTF16)
		{
			return new UTF32String(pUTF32).Append(pUTF16);
		}

		//---------------------------------------------------------------------
		// Public Static Methods
		//---------------------------------------------------------------------
		public static UTF32String FromChars(char[] pChars)
		{
			int[] intArray = null;

			// valid?
			if (null != pChars)
				intArray = UTF32String.GetInts(pChars);

			// create a new instance
			return new UTF32String(intArray);
		}

		public static UTF32String FromBytes(byte[] pBytes)
		{			
			char[] charArray = null;
	
			// valid?
			if (null != pBytes)
				charArray = UnicodeEncoding.UTF8.GetChars(pBytes);

			// create a new instance
			return UTF32String.FromChars(charArray);
		}

		public static UTF32String FromUTF16(string pString)
		{
			char[]	charArray = null;

			if (null != pString)
				charArray = pString.ToCharArray();

			// create a new instance
			return UTF32String.FromChars(charArray);
		}

		public static bool IsSurrogate(char pChar)
		{
			// High surrogate
			if (UTF32String.IsHighSurrogate(pChar))
				return true;

			// Low Surrogate
			if (UTF32String.IsLowSurrogate(pChar))
				return true;
			
			// Not a surrogate
			return false;
		}

		public static bool IsHighSurrogate(char pChar)
		{
			return ((HIGH_SURROGATE_LLIMIT <= pChar) && (HIGH_SURROGATE_HLIMIT >= pChar));
		}

		public static bool IsLowSurrogate(char pChar)
		{
			return ((LOW_SURROGATE_LLIMIT <= pChar) && (LOW_SURROGATE_HLIMIT >= pChar));
		}

		public static int FromSurrogate(char pHigh, char pLow)
		{
			return 0x10000 + ((((int)pHigh) - HIGH_SURROGATE_LLIMIT) << 10) + (((int)pLow) - LOW_SURROGATE_LLIMIT);
		}

		public static char HighSurrogate(int pUTF32)
		{
			return (char)(((pUTF32 - 0x10000) >> 10) + HIGH_SURROGATE_LLIMIT);
		}

		public static char LowSurrogate(int pUTF32)
		{
			return (char)(((pUTF32 - 0x10000) & 0x3ff) + LOW_SURROGATE_LLIMIT);
		}

		public static int[] GetInts(string pUTF16)
		{
			// valid?
			if (null == pUTF16)
				return new int[0];

			// return it
			return UTF32String.GetInts(pUTF16.ToCharArray());
		}

		public static int[] GetInts(char[] pChars)
		{
			// valid?
			if (null == pChars)
				return new int[0];

			// return it
			return UTF32String.GetInts(pChars, 0, pChars.Length);
		}

		public static int[] GetInts(char[] pChars, int pIndex, int pLength)
		{
			char			c;
			char			l;
			int				cIndex		= 0;
			int[]			buffer		= null;
			int[]			intArray	= null;

			// valid?
			if ((null == pChars) || (0 == pChars.Length))
				return new int[0];

			// validates
			if ((0 > pIndex) || (pIndex > pChars.Length))
				pIndex = 0;

			if ((0 > pLength) || (pLength > pChars.Length))
				pLength = pChars.Length;

			// create a temp buffer
			cIndex	= 0;
			buffer	= new int[pChars.Length];

			// for each char in the inputs.
			for (int index = pIndex; index < pLength; index++)
			{
				c = pChars[index];

				// if it starts with a low surrogate pairs
				// ignore it
				if (UTF32String.IsLowSurrogate(c))
					continue;

				// check if it's a surrogate pair
				if (UTF32String.IsHighSurrogate(c))
				{
					// end of the array?
					if ((index + 1) < pLength)
					{
						l = pChars[++index];

						// check if it's a low surrogate pair
						if (UTF32String.IsLowSurrogate(l))
						{
							// convert it to the UTF32
							buffer[cIndex++] = UTF32String.FromSurrogate(c, l);
						}
					}
				}
				else
					buffer[cIndex++] = (int)c;
			}

			// create the internal buffer
			intArray = new int[cIndex];
		
			// Copy the data over
			Array.Copy (buffer, 0, intArray, 0, cIndex);

			return intArray;	
		}

		//---------------------------------------------------------------------
		// Private Methods
		//---------------------------------------------------------------------
		private bool IsInArray(int[] pArray, int pCheck)
		{
			bool bFound = false;
		
			if (null == pArray)
				return false;

			// Loop each char to check the match
			for (int index = 0; index < pArray.Length; index++)
			{
				bFound = (pArray[index] == pCheck); 
				if (bFound)
					break;
			}

			// found?
			return bFound;
		}
	}
}
