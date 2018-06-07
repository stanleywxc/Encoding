using System;

// Local Imports
using Echo.Services.Encoding.Unicode;

namespace Echo.Services.Encoding.Helper
{
	/// <summary>
	/// Summary description for StringUtil.
	/// </summary>
	public class StringUtil
	{
		//---------------------------------------------------------------------
		// Static Public Methods
		//---------------------------------------------------------------------
		public static string Normalize(string pValue)
		{
			// Null?
			if (null == pValue)
				return null;

			// trim it
			pValue = pValue.Trim();

			// Null?
			if (null == pValue)
				return null;

			// empty?
			if (string.Empty == pValue)
				return null;

			// Normalized.
			return pValue;
		}

		public static UTF32String Normalize(UTF32String pValue)
		{
			// Null?
			if (null == pValue)
				return null;

			pValue = pValue.Trim();
			if (null == pValue)
				return null;

			// empty?
			if (0 == pValue.Length)
				return null;

			// Normalized
			return pValue;
		}
	}
}
