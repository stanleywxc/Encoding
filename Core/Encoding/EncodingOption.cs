using System;

// Local Imports
using Echo.Services.Encoding.Interface;

namespace Echo.Services.Encoding.Encoding
{
	/// <summary>
	/// Summary description for EncodingOption.
	/// </summary>
	public class EncodingOption : IEncodingOption
	{
		//---------------------------------------------------------------------
		// Static Data mambers
		//---------------------------------------------------------------------
		public static  long		ALLOW_UNASSIGNED	= 0x0000000000000001L;
		public static  long		USE_STD3_RULES		= 0x0000000000000002L;
		public static  long		USE_NORMALIZE		= 0x0000000000000004L;
		public static  long		CHECK_BIDI			= 0x0000000000000008L;
		public static  long		DECODE_DOUBLE_CHECK	= 0x0000000000000010L;
		public static  long		ALLOW_DECODE_FAIL	= 0x0000000000000020L;

		//---------------------------------------------------------------------
		// Data mambers
		//---------------------------------------------------------------------
		private	long			m_option			= 0;

		//---------------------------------------------------------------------
		// Constructors
		//---------------------------------------------------------------------
		public EncodingOption()
		{
			m_option = USE_NORMALIZE | CHECK_BIDI | DECODE_DOUBLE_CHECK | ALLOW_DECODE_FAIL;
		}

		//---------------------------------------------------------------------
		// Properties
		//---------------------------------------------------------------------	
		public long Option
		{
			get { return m_option;	}
		}

		//---------------------------------------------------------------------
		// Public methods
		//---------------------------------------------------------------------
		public void SetOptionOn(long pOption)
		{
			m_option |= pOption;
		}

		public void SetOptionOff(long pOption)
		{
			m_option &= ~pOption;
		}
		
		public bool IsOptionSet(long pOption)
		{
			return (this.Option & pOption) != 0;
		}
	}
}
