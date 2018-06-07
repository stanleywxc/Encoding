using System;
using System.Collections;

namespace Echo.Services.Encoding.WebService.Helper
{
	/// <summary>
	/// Summary description for Host.
	/// </summary>
	public class DomainParser
	{
		// Data members
		private string		m_sld;
		private string		m_tld;
		private Hashtable	m_tldCache;
		private Hashtable	m_exception;

		public DomainParser(Hashtable pTldCache)
		{
			m_sld		= null;
			m_tld		= null;
			m_tldCache	= pTldCache;
			m_exception = new Hashtable(new CaseInsensitiveHashCodeProvider(), new CaseInsensitiveComparer());
			
			// .name is an exception in sld.
			m_exception.Add("name", 3);
		}

		public string SLD
		{
			get	{	return m_sld;	}
			set	{	m_sld = value;	}
		}

		public string TLD
		{
			get	{	return m_tld;	}
			set {	m_tld = value;	}
		}

		public bool Parse(string pHost)
		{
			int			index	= 0;
			string		host	= null;
			string[]	labels	= null;

			// reset
			this.Reset();

			// trim it.
			pHost = pHost.Trim();

			// find the index of '.'
			index = pHost.IndexOf('.');

			// must has at least one '.' 
			// and not at the beginning.
			if (index <= 0)
				return false;

			// '.' can't be at the end.
			if (index == pHost.Length - 1)
				return false;

			// can't have any ".." togather
			if (pHost.IndexOf("..") >= 0)
				return false;

			// can't have more than 63 letters.
			if (pHost.Length > 63)
				return false;

			// check if it's tld itself
			if (m_tldCache.ContainsKey("." + pHost))
				return false;

			// parse each label to get host, sld, and tld.
			labels = pHost.Split('.');
			if ((null == labels) || (labels.Length <= 1))
				return false;
		
			// Let's start from the end of the label
			// to check tlds and slds
			for (index = labels.Length - 1; index >= 0; index--)
			{
				string tld = null;

				// validate each label.
				if (!validateLabel(labels[index]))
					return false;

				// set the tld to see if it's valid.
				tld = (m_tld != null) ? labels[index] + "." + m_tld : labels[index];

				// trying to find the tld until we fail.
				if (m_tldCache.ContainsKey("." + tld))
				{
					m_tld = tld;
				}
				else
				{
					// OK, the sld is in labels[index].
					break;
				}
			}

			// find tld?
			if (null == m_tld)
				return false;

			// exception?
			if (m_exception.ContainsKey(m_tld))
			{
				int required = (int)m_exception [m_tld];
				
				// check if having all required parts?
				if (labels.Length < required)
					return false;
			
				// get the sld for exceptions.
				for (int i = labels.Length - required; i <= index; i++)
				{
					if (i  > (labels.Length - required))
						m_sld += ".";

					m_sld += labels[i];
				}
		
				// position that host/subdomain part ends.
				index = labels.Length - required;
			}
			else
				m_sld = labels[index];
			
			// the rest is host/subdomain part
			for (int i = 0; i < index; i++)
			{
				if (i > 0)
					host += ".";
				
				// host/subdomain part.
				host += labels[i];
			}

			if (null != host)
				m_sld = host + "." + m_sld;

			return true;
		}
	
		public void Reset()
		{
			this.SLD	= null;
			this.TLD	= null;			
		}

		public static bool validateLabel(string pLabel)
		{
			int		index = 0;
			char[]	chars = null;

			// Initialize
			chars = pLabel.ToCharArray();
	
			// valid?
			if ((null == chars) || (chars.Length == 0))
				return false;

			// each label must start with a letter.
			// NOTE: Only allowed a letter in spec.
			//		 but a digit allowed in real world.
			if (!char.IsLetterOrDigit(chars[0]))
				return false;

			// each label must end with a letter or a digit.
			if (!char.IsLetterOrDigit(chars[chars.Length - 1]))
				return false;

			// for each char in label, only allow letter, digit, or '-'.
			for (index = 0; index < chars.Length; index++)
			{
				// check a-z, 0-9, and '-'.
				if (!char.IsLetterOrDigit(chars[index]) && (chars[index] != '-'))
					return false;
			}
		
			// Done
			return true;
		}
	}
}
