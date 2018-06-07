using System;

namespace Echo.Services.Encoding.Exceptions
{
	/// <summary>
	/// Summary description for Std3RuleCodePointException.
	/// </summary>
	public class Std3RuleCodePointException : ACEException
	{
		public Std3RuleCodePointException() : base()
		{
		}

		public Std3RuleCodePointException(string pMessage) : base(pMessage)
		{
		}
	}
}
