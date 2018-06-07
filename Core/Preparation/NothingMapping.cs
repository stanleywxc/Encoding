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
	internal class NothingMapping
	{
		//---------------------------------------------------------------------
		// Data Members
		//---------------------------------------------------------------------
		private static Hashtable	m_map		= null;
		private static Table		m_table		= null;

		static NothingMapping()
		{
			m_map	= new Hashtable(new CaseInsensitiveHashCodeProvider(), new CaseInsensitiveComparer());
			m_table	= new Table();

			// load tables
			m_table.LoadTable(m_map);
		}

		public bool IsMapNothing(int pKey)
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
					"00AD; ; Map to nothing",
					"034F; ; Map to nothing",
					"1806; ; Map to nothing",
					"180B; ; Map to nothing",
					"180C; ; Map to nothing",
					"180D; ; Map to nothing",
					"200B; ; Map to nothing",
					"200C; ; Map to nothing",
					"200D; ; Map to nothing",
					"2060; ; Map to nothing",
					"FE00; ; Map to nothing",
					"FE01; ; Map to nothing",
					"FE02; ; Map to nothing",
					"FE03; ; Map to nothing",
					"FE04; ; Map to nothing",
					"FE05; ; Map to nothing",
					"FE06; ; Map to nothing",
					"FE07; ; Map to nothing",
					"FE08; ; Map to nothing",
					"FE09; ; Map to nothing",
					"FE0A; ; Map to nothing",
					"FE0B; ; Map to nothing",
					"FE0C; ; Map to nothing",
					"FE0D; ; Map to nothing",
					"FE0E; ; Map to nothing",
					"FE0F; ; Map to nothing",
					"FEFF; ; Map to nothing"
				};
			}

			public void LoadTable(Hashtable pMap)
			{
				int			intVal	= 0;
				string		line	= null;
				string[]	parts	= null;

				try
				{
					// valid?
					if (null == pMap)
						return ;

					for (int index = 0; index < this.Map.Length; index++)
					{
						line = StringUtil.Normalize(this.Map[index]);
						if (null == line)
							continue;

						// split it
						parts = line.Split(';');
						if (null == parts)
							continue;

						parts[0] = StringUtil.Normalize(parts[0]);
						if (null == parts[0])
							continue;

						try
						{
							intVal			= int.Parse(parts[0], NumberStyles.HexNumber);
							pMap[intVal]	= true;
						}
						catch(Exception)
						{
							continue;
						}
					}
				}
				catch(Exception){}
				
				return ;
			}
		}
	}
}
