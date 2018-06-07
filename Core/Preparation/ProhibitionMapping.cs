using System;
using System.Collections;
using System.Globalization;

// Local Imports
using Echo.Services.Encoding.Helper;

namespace Echo.Services.Encoding.Preparation
{
	/// <summary>
	/// Summary description for Unmapping.
	/// </summary>
	internal class ProhibitionMapping
	{
		//---------------------------------------------------------------------
		// Data Members
		//---------------------------------------------------------------------
		private static Hashtable	m_map		= null;
		private static Table		m_table		= null;

		static ProhibitionMapping()
		{
			m_map	= new Hashtable(new CaseInsensitiveHashCodeProvider(), new CaseInsensitiveComparer());
			m_table	= new Table();

			// load tables
			m_table.LoadTable(m_map);
		}

		public bool IsProhibited(int pKey)
		{
			bool flag = false;

			try
			{
				if (null == m_map)
					return false;

				flag = m_map.ContainsKey(pKey);
			}
			catch(Exception)
			{
				return false;
			}

			return flag;
		}


		private class Table
		{
			//---------------------------------------------------------------------
			// Data Members
			//---------------------------------------------------------------------
			private string[]	m_table	= null;

			//---------------------------------------------------------------------
			// Properties
			//---------------------------------------------------------------------
			public string[] Map
			{
				get { return m_table;	}
			}

			//---------------------------------------------------------------------
			// Constructors
			//---------------------------------------------------------------------
			public Table()
			{
				m_table = new string[]
				{
					"0020; SPACE",
					"00A0; NO-BREAK SPACE",
					"1680; OGHAM SPACE MARK",
					"2000; EN QUAD",
					"2001; EM QUAD",
					"2002; EN SPACE",
					"2003; EM SPACE",
					"2004; THREE-PER-EM SPACE",
					"2005; FOUR-PER-EM SPACE",
					"2006; SIX-PER-EM SPACE",
					"2007; FIGURE SPACE",
					"2008; PUNCTUATION SPACE",
					"2009; THIN SPACE",
					"200A; HAIR SPACE",
					"200B; ZERO WIDTH SPACE",
					"202F; NARROW NO-BREAK SPACE",
					"205F; MEDIUM MATHEMATICAL SPACE",
					"3000; IDEOGRAPHIC SPACE",
					"0000-001F; [CONTROL CHARACTERS]",
					"007F; DELETE",
					"0080-009F; [CONTROL CHARACTERS]",
					"06DD; ARABIC END OF AYAH",
					"070F; SYRIAC ABBREVIATION MARK",
					"180E; MONGOLIAN VOWEL SEPARATOR",
					"200C; ZERO WIDTH NON-JOINER",
					"200D; ZERO WIDTH JOINER",
					"2028; LINE SEPARATOR",
					"2029; PARAGRAPH SEPARATOR",
					"2060; WORD JOINER",
					"2061; FUNCTION APPLICATION",
					"2062; INVISIBLE TIMES",
					"2063; INVISIBLE SEPARATOR",
					"206A-206F; [CONTROL CHARACTERS]",
					"FEFF; ZERO WIDTH NO-BREAK SPACE",
					"FFF9-FFFC; [CONTROL CHARACTERS]",
					"1D173-1D17A; [MUSICAL CONTROL CHARACTERS]",
					"E000-F8FF; [PRIVATE USE, PLANE 0]",
					"F0000-FFFFD; [PRIVATE USE, PLANE 15]",
					"100000-10FFFD; [PRIVATE USE, PLANE 16]",
					"FDD0-FDEF; [NONCHARACTER CODE POINTS]",
					"FFFE-FFFF; [NONCHARACTER CODE POINTS]",
					"1FFFE-1FFFF; [NONCHARACTER CODE POINTS]",
					"2FFFE-2FFFF; [NONCHARACTER CODE POINTS]",
					"3FFFE-3FFFF; [NONCHARACTER CODE POINTS]",
					"4FFFE-4FFFF; [NONCHARACTER CODE POINTS]",
					"5FFFE-5FFFF; [NONCHARACTER CODE POINTS]",
					"6FFFE-6FFFF; [NONCHARACTER CODE POINTS]",
					"7FFFE-7FFFF; [NONCHARACTER CODE POINTS]",
					"8FFFE-8FFFF; [NONCHARACTER CODE POINTS]",
					"9FFFE-9FFFF; [NONCHARACTER CODE POINTS]",
					"AFFFE-AFFFF; [NONCHARACTER CODE POINTS]",
					"BFFFE-BFFFF; [NONCHARACTER CODE POINTS]",
					"CFFFE-CFFFF; [NONCHARACTER CODE POINTS]",
					"DFFFE-DFFFF; [NONCHARACTER CODE POINTS]",
					"EFFFE-EFFFF; [NONCHARACTER CODE POINTS]",
					"FFFFE-FFFFF; [NONCHARACTER CODE POINTS]",
					"10FFFE-10FFFF; [NONCHARACTER CODE POINTS]",
					"D800-DFFF; [SURROGATE CODES]",
					"FFF9; INTERLINEAR ANNOTATION ANCHOR",
					"FFFA; INTERLINEAR ANNOTATION SEPARATOR",
					"FFFB; INTERLINEAR ANNOTATION TERMINATOR",
					"FFFC; OBJECT REPLACEMENT CHARACTER",
					"FFFD; REPLACEMENT CHARACTER",
					"2FF0-2FFB; [IDEOGRAPHIC DESCRIPTION CHARACTERS]",
					"0340; COMBINING GRAVE TONE MARK",
					"0341; COMBINING ACUTE TONE MARK",
					"200E; LEFT-TO-RIGHT MARK",
					"200F; RIGHT-TO-LEFT MARK",
					"202A; LEFT-TO-RIGHT EMBEDDING",
					"202B; RIGHT-TO-LEFT EMBEDDING",
					"202C; POP DIRECTIONAL FORMATTING",
					"202D; LEFT-TO-RIGHT OVERRIDE",
					"202E; RIGHT-TO-LEFT OVERRIDE",
					"206A; INHIBIT SYMMETRIC SWAPPING",
					"206B; ACTIVATE SYMMETRIC SWAPPING",
					"206C; INHIBIT ARABIC FORM SHAPING",
					"206D; ACTIVATE ARABIC FORM SHAPING",
					"206E; NATIONAL DIGIT SHAPES",
					"206F; NOMINAL DIGIT SHAPES",
					"E0001; LANGUAGE TAG",
					"E0020-E007F; [TAGGING CHARACTERS]"
				};
			}

			public void LoadTable(Hashtable pMap)
			{
				bool		bLine	= true;
				int[]		intVal	= null;
				string		line	= null;
				string[]	parts	= null;
				
				try
				{
					for (int index = 0; index < this.Map.Length; index++)
					{
						// Next line
						line = StringUtil.Normalize(this.Map[index]);
						if (null == line)
							continue;

						// split the first part
						parts = line.Split(';');
						if (null == parts)
							continue;

						// valid?
						line = StringUtil.Normalize(parts[0]);
						if (null == line)
							continue;

						// parse it
						parts = line.Split('-');
						if (null == parts)
							continue;

						bLine	= true;
						intVal	= new int[2];
						for (int pIndex = 0; pIndex < 2; pIndex++)
						{
							// parse the value.
							try
							{
								line = StringUtil.Normalize(parts[pIndex]);
								if (null == line)
								{
									bLine = false;
									break;
								}

								// get the int value
								intVal[pIndex] = int.Parse(line, NumberStyles.HexNumber);
							}
							catch(Exception)
							{
								bLine = false;
								break;
							}
						}
		
						// valid line?
						if (false == bLine)
							continue;

						// set the range
						if (1 == parts.Length)
							intVal[1] = intVal[0];

						// put into hash table
						for (int vIndex = intVal[0]; vIndex <= intVal[1]; vIndex ++)
						{
							m_map[vIndex] = true;
						}
					}
				}
				catch(Exception){}
		
				return ;
			}
		}
	}
}
