using System;

namespace Echo.Services.Encoding.Interface
{
	/// <summary>
	/// Summary description for IEncodingOption.
	/// </summary>
	public interface IEncodingOption
	{
		void SetOptionOn		(long pOption);
		void SetOptionOff		(long pOption);
		bool IsOptionSet		(long pOption);
	}
}
