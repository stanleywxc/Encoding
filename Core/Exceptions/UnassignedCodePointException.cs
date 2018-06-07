using System;

namespace Echo.Services.Encoding.Exceptions
{
	/// <summary>
	/// Summary description for UnassignedCodePointException.
	/// </summary>
	public class UnassignedCodePointException : ACEException
	{
		//---------------------------------------------------------------------
		// Constructors
		//---------------------------------------------------------------------
		public UnassignedCodePointException() : base()
		{
		}

		public UnassignedCodePointException(string pMessage) : base(pMessage)
		{
		}
	}
}
