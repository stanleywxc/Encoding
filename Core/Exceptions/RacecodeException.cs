using System;

namespace Echo.Services.Encoding.Exceptions
{
	/// <summary>
	/// Summary description for Std3RuleCodePointException.
	/// </summary>
	public class RacecodeException : ACEException
	{
		public RacecodeException() : base()
		{
		}

		public RacecodeException(string pMessage) : base(pMessage)
		{
		}
	}
}
