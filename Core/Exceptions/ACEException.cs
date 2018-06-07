using System;

namespace Echo.Services.Encoding.Exceptions
{
	/// <summary>
	/// Summary description for ACEException.
	/// </summary>
	public class ACEException : Exception
	{
		//---------------------------------------------------------------------
		// Constructors
		//---------------------------------------------------------------------
		public ACEException() : base()
		{
		}

		public ACEException(string pMessage) : base(pMessage)
		{
		}
	}
}