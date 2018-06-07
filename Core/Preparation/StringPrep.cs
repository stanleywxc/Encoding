using System;

// Local Imports
using Echo.Services.Encoding.Encoding;
using Echo.Services.Encoding.Exceptions;
using Echo.Services.Encoding.Interface;
using Echo.Services.Encoding.Unicode;

namespace Echo.Services.Encoding.Preparation
{
	/// <summary>
	/// Summary description for NamePrep.
	/// </summary>
	public class StringPrep : IPreparer
	{
		//---------------------------------------------------------------------
		// Static Data members
		//---------------------------------------------------------------------
		private static	StringPrep	m_instance	= null;
		
		//---------------------------------------------------------------------
		// Data members
		//---------------------------------------------------------------------
		private BidirectionMapping			m_bidirectionMapping		= null;
		private NormalizedCaseMapping		m_normalizedCaseMapping		= null;
		private NothingMapping				m_nothingMapping			= null;
		private ProhibitionMapping			m_prohibitionMapping		= null;
		private UnassignedMapping			m_unassignedMapping			= null;
		private UnnormalizedCaseMapping		m_unnormalizedCaseMapping	= null;

		//---------------------------------------------------------------------
		// Constructors
		//---------------------------------------------------------------------
		private StringPrep()
		{
			m_bidirectionMapping		= new BidirectionMapping		();
			m_normalizedCaseMapping		= new NormalizedCaseMapping		();
			m_nothingMapping			= new NothingMapping			();
			m_prohibitionMapping		= new ProhibitionMapping		();
			m_unassignedMapping			= new UnassignedMapping			();
			m_unnormalizedCaseMapping	= new UnnormalizedCaseMapping	();
		}

		//---------------------------------------------------------------------
		// Public methods
		//---------------------------------------------------------------------
		public UTF32String Prepare(UTF32String pSource, IEncodingOption pOption)
		{
			UTF32String	output = null;

			if (null == pSource)
				return new UTF32String();

			// Based on RFC3454:
			// Step 1 & 2: Map & Normalization
			output = this.Map(pSource, pOption);
			
			// Step 2: Normalization
			output = this.Normalize(output, pOption);

			// Step 3: Prohibition
			output = this.Prohibit(output, pOption);

			// Step 4: Bidi
			output = this.Bidirection(output, pOption);

			// Done
			return output;
		}

		public UTF32String Unprepare(UTF32String pSource, IEncodingOption pOption)
		{
			// no implementation based on RFC3454. Reserved for furture extention.
			return pSource;
		}

		//---------------------------------------------------------------------
		// Private members
		//---------------------------------------------------------------------
		private UTF32String Map(UTF32String pSource, IEncodingOption pOption)
		{
			bool			bAllowUnassigned	= false;
			bool			bNormalize			= true;
			int				cPoint				= 0;
			int[]			mPoint				= null;
			UTF32String		mapped				= null;

			// Initializes
			mapped				= new UTF32String();
			bAllowUnassigned	= (pOption != null) && pOption.IsOptionSet(EncodingOption.ALLOW_UNASSIGNED);
			bNormalize			= (pOption != null) && pOption.IsOptionSet(EncodingOption.USE_NORMALIZE);
			
			// valid?
			if (null == pSource)
				return mapped;

			// for each char
			for (int index = 0; index < pSource.Length; index++)
			{
				// get code point
				cPoint = pSource[index];

				// check if it's unassigned
				if (bAllowUnassigned && m_unassignedMapping.IsUnassigned(cPoint))
					throw new UnassignedCodePointException(string.Format("Unassigned code point found: {0} in {1:X8}\r\n", cPoint, pSource.ToString()));

				// check if there is any map nothing
				if (m_nothingMapping.IsMapNothing(cPoint))
					continue;

				// check the map
				if (bNormalize)
					mPoint = m_normalizedCaseMapping.Mapping(cPoint);
				else
					mPoint = m_unnormalizedCaseMapping.Mapping(cPoint);
				
				// having mapping?
				if ((null == mPoint) || (0 == mPoint.Length))
					mPoint = new int [1]{cPoint};

				// add the mapping to the output
				for (int mIndex = 0; mIndex < mPoint.Length; mIndex++)
					mapped.Append(mPoint[mIndex]);
			}

			//Done
			return mapped;
		}

		private UTF32String Normalize(UTF32String pSource, IEncodingOption pOption)
		{
			return pSource;
		}

		private UTF32String Prohibit(UTF32String pSource, IEncodingOption pOption)
		{
			int	cPoint = 0;

			// valid?
			if (null == pSource)
				return pSource;

			// for each char
			for (int index = 0; index < pSource.Length; index++)
			{
				cPoint = pSource[index];
				
				// check if there is any prohibited code point
				if (m_prohibitionMapping.IsProhibited(cPoint))
					throw new ProhibitedCodePointException(string.Format("Prohibited code point found: {0} in {1:X8}\r\n", cPoint, pSource.ToString()));
			}

			return pSource;
		}

		private UTF32String Bidirection(UTF32String pSource, IEncodingOption pOption)
		{
			int			cPoint				= 0;
			bool		bLeftToRight		= false;
			bool		bRightToLeft		= false;
			Direction	LastCharDirection	= Direction.DIRECTION_NORM;
			Direction	FirstCharDirection	= Direction.DIRECTION_NORM;

			// check if we need to check bidi
			if (!pOption.IsOptionSet(EncodingOption.CHECK_BIDI))
				return pSource;

			// get the first char direction
			FirstCharDirection = m_bidirectionMapping.GetDirection(pSource[0]);

			// for each char, checking it's direction
			for (int index = 1; index < pSource.Length; index++)
			{
				cPoint				= pSource[index];
				LastCharDirection	= m_bidirectionMapping.GetDirection(cPoint);

				// check Left to right if necessary
				if (false == bLeftToRight)
					bLeftToRight = (LastCharDirection == Direction.DIRECTION_RIGHT);

				// check right to left if necesssary
				if (false == bRightToLeft)
					bLeftToRight = (LastCharDirection == Direction.DIRECTION_LEFT);
			}

			// Based on RFC3454 6.2, check if there are both right to left or left to right
			if (bLeftToRight && bRightToLeft)
				throw new BidiCodePointException(string.Format("Invalid bidi code point found[Can't have both 'RightToLeft' and 'LeftToRight' code point]:  {0} in {1:X8}\r\n", cPoint, pSource.ToString()));

			// Based on RFC3454 6.3, check if there are both right to left
			if (bRightToLeft && (FirstCharDirection != LastCharDirection))
				throw new BidiCodePointException(string.Format("Invalid bidi code point found[first char and last char of string MUST be both 'RightToLeft']:  {0} in {1:X8}\r\n", cPoint, pSource.ToString()));

			// Done.
			return pSource;
		}

		//---------------------------------------------------------------------
		// Static members
		//---------------------------------------------------------------------
		public static StringPrep GetInstance()
		{
			// check if the instance is created or not.
			// this is singleton method to create only one instance
			if (null == m_instance)
				m_instance = new StringPrep();

			// Done.
			return m_instance;
		}
	}
}
