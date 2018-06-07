using System;

namespace Echo.Services.Encoding.Exceptions
{
	/// <summary>
	/// Summary description for Std3RuleCodePointException.
	/// </summary>
	public class PunycodeException : ACEException
	{
		public PunycodeException() : base()
		{
		}

		public PunycodeException(string pMessage) : base(pMessage)
		{
		}
	}
}
