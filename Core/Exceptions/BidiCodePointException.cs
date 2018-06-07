using System;

namespace Echo.Services.Encoding.Exceptions
{
	/// <summary>
	/// Summary description for UnassignedCodePointException.
	/// </summary>
	public class BidiCodePointException : ACEException
	{
		//---------------------------------------------------------------------
		// Constructors
		//---------------------------------------------------------------------
		public BidiCodePointException() : base()
		{
		}

		public BidiCodePointException(string pMessage) : base(pMessage)
		{
		}
	}
}
