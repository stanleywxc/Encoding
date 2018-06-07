using System;

using Echo.Services.Encoding.Unicode;

namespace Echo.Services.Encoding.Interface
{
	/// <summary>
	/// Summary description for IASCIICompatibleEncoding.
	/// </summary>
	public interface IACEConverter
	{
		//---------------------------------------------------------------------
		// methods
		//---------------------------------------------------------------------
		string	Encode	(string	pSource);
		string	Decode	(string	pSource);
	}
}
