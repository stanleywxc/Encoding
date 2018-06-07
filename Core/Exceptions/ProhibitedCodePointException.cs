using System;

namespace Echo.Services.Encoding.Exceptions
{
	/// <summary>
	/// Summary description for ProhibitedCodePointException.
	/// </summary>
	public class ProhibitedCodePointException : ACEException
	{
		//---------------------------------------------------------------------
		// Constructors
		//---------------------------------------------------------------------
		public ProhibitedCodePointException() : base()
		{
		}

		public ProhibitedCodePointException(string pMessage) : base(pMessage)
		{
		}
	}
}
