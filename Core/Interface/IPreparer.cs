using System;

//Local Imports
using Echo.Services.Encoding.Unicode;

namespace Echo.Services.Encoding.Interface
{
	/// <summary>
	/// Summary description for IPreparer.
	/// </summary>
	public interface IPreparer
	{
		UTF32String Prepare		(UTF32String pSource, IEncodingOption pOption);
		UTF32String Unprepare	(UTF32String pSource, IEncodingOption pOption);
	}
}
