using System;

// Local Imports
using Echo.Services.Encoding.Interface;
using Echo.Services.Encoding.Preparation;

namespace Echo.Services.Encoding.Encoding
{
	/// <summary>
	/// Summary description for CoverterFactory.
	/// </summary>
	public class ConverterFactory
	{
		//---------------------------------------------------------------------
		// Constants
		//---------------------------------------------------------------------
		public const string		SCHEMA_RACE		= "RACE";
		public const string		SCHEMA_PUNYCODE = "PUNYCODE";

		//---------------------------------------------------------------------
		// Static method
		//---------------------------------------------------------------------
		public static IACEConverter Create(string pSchema)
		{
			IACEConverter converter = null;

			try
			{
				// valid?
				if (null != pSchema)
					pSchema = pSchema.ToUpper();
				
				// create converter
				switch(pSchema)
				{
					case SCHEMA_RACE:
						converter = new ACEConverter (StringPrep.GetInstance(), new RaceConverter());
						break;
					case SCHEMA_PUNYCODE:
						converter = new ACEConverter (StringPrep.GetInstance(), new PunyConverter());
						break;
					default:
						converter = null;
						break;
				}
			}
			catch(Exception)
			{
				converter = null;
			}

			// Done
			return converter;
		}
	}
}
