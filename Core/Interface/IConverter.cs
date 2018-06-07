using System;

// Local Imports
using Echo.Services.Encoding.Unicode;

namespace Echo.Services.Encoding.Interface
{
	/// <summary>
	/// Summary description for IEncoder.
	/// </summary>
	public interface IConverter
	{
		//---------------------------------------------------------------------
		// Properties
		//---------------------------------------------------------------------
		string			Prefix
		{
			get ;
		}

		//---------------------------------------------------------------------
		// Methods
		//---------------------------------------------------------------------
		string			Encode				(UTF32String	pSource, bool[] pCaseFlag);
		UTF32String		Decode				(string			pSource, bool[] pCaseFlag);
		bool			Validate			(UTF32String	pSource, IEncodingOption pOption);
		bool			IsBeginWithPrefix	(UTF32String	pSource);
	}
}
