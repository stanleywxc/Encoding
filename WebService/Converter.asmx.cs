using System;
using System.Collections;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Web;
using System.Web.Services;

// Local Imports
using Echo.Core.Text.Encoding;

using Echo.Services.Encoding.WebService.Helper;

namespace Echo.Services.Encoding.WebService.Convert
{
	/// <summary>
	/// Summary description for Service1.
	/// </summary>
	[WebService(Namespace="http://name-services.com/webservice/encoding/")]
	public class Converter : System.Web.Services.WebService
	{
		//---------------------------------------------------------------------
		// Data members
		//---------------------------------------------------------------------
		private const	string			EVENT_SOURCE	= "Encoding WebService";
		private static	IACEConverter	m_raceConverter	= null;
		private static	IACEConverter	m_punyConverter	= null;
		private static	Hashtable		m_tldCache		= null;

		static Converter()
		{
			// Initialize
			Initialize();
		}

		public Converter()
		{
			//CODEGEN: This call is required by the ASP.NET Web Services Designer
			InitializeComponent();
		}

		#region Component Designer generated code
		
		//Required by the Web Services Designer 
		private IContainer components = null;
				
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if(disposing && components != null)
			{
				components.Dispose();
			}
			base.Dispose(disposing);		
		}
		
		#endregion


		[WebMethod]
		public string NativeToPunycode(string domain)
		{
			string			sld			= null;
			string			tld			= null;
			string			converted	= null;
			DomainParser	parser		= null;

			if (null == domain)
				return null;

			try
			{
				parser	= new DomainParser(m_tldCache);

				// parse the domain.
				this.Parse(parser, domain, ref sld, ref tld);

				// convert it
				converted = m_punyConverter.Encode(sld);
				if (null != tld)
					converted += "." + tld;
			}
			catch(Exception e)
			{
				converted = null;
				EventLogger.Log(EVENT_SOURCE, EventLogger.LogType.Error, e.ToString());
			}

			// Done.
			return converted;
		}

		[WebMethod]
		public string NativeToRace(string domain)
		{
			string			sld			= null;
			string			tld			= null;
			string			converted	= null;
			DomainParser	parser		= null;

			if (null == domain)
				return null;
			
			try
			{
				parser	= new DomainParser(m_tldCache);

				// parse the domain.
				this.Parse(parser, domain, ref sld, ref tld);

				// convert it
				converted = m_raceConverter.Encode(sld);
				if (null != tld)
					converted += "." + tld;
			}
			catch(Exception e)
			{
				converted = null;
				EventLogger.Log(EVENT_SOURCE, EventLogger.LogType.Error, e.ToString());
			}

			// Done.
			return converted;
		}

		[WebMethod]
		public string PunycodeToNative(string domain)
		{
			string			sld			= null;
			string			tld			= null;
			string			converted	= null;
			DomainParser	parser		= null;

			if (null == domain)
				return null;
			
			try
			{
				parser = new DomainParser(m_tldCache);

				// parse the domain.
				this.Parse(parser, domain, ref sld, ref tld);

				// convert it
				converted = m_punyConverter.Decode(sld);
				if (null != tld)
					converted += "." + tld;
			}
			catch(Exception e)
			{
				converted = null;
				EventLogger.Log(EVENT_SOURCE, EventLogger.LogType.Error, e.ToString());
			}

			// Done.
			return converted;
		}

		[WebMethod]
		public string RaceToNative(string domain)
		{
			string			sld			= null;
			string			tld			= null;
			string			converted	= null;
			DomainParser	parser		= null;

			if (null == domain)
				return null;

			
			try
			{
				parser	= new DomainParser(m_tldCache);
				// parse the domain.
				this.Parse(parser, domain, ref sld, ref tld);

				// convert it
				converted = m_raceConverter.Decode(sld);
				if (null != tld)
					converted += "." + tld;
			}
			catch(Exception e)
			{
				converted = null;
				EventLogger.Log(EVENT_SOURCE, EventLogger.LogType.Error, e.ToString());
			}

			// Done.
			return converted;
		}
			
		[WebMethod]
		public string RaceToPunycode(string domain)
		{
			string			sld			= null;
			string			tld			= null;
			string			native		= null;
			string			converted	= null;
			DomainParser	parser		= null;

			if (null == domain)
				return null;

			
			try
			{
				parser	= new DomainParser(m_tldCache);
				// parse the domain.
				this.Parse(parser, domain, ref sld, ref tld);

				// convert it
				native		= m_raceConverter.Decode(sld);
				converted	= m_punyConverter.Encode(native);
				if (null != tld)
					converted += "." + tld;
			}
			catch(Exception e)
			{
				converted = null;
				EventLogger.Log(EVENT_SOURCE, EventLogger.LogType.Error, e.ToString());
			}

			// Done.
			return converted;
		}

		[WebMethod]
		public string PunycodeToRace(string domain)
		{
			string			sld			= null;
			string			tld			= null;
			string			native		= null;
			string			converted	= null;
			DomainParser	parser		= null;

			if (null == domain)
				return null;

			
			try
			{
				parser	= new DomainParser(m_tldCache);

				// parse the domain.
				this.Parse(parser, domain, ref sld, ref tld);

				// convert it
				native		= m_punyConverter.Decode(sld);
				converted	= m_raceConverter.Encode(native);
				if (null != tld)
					converted += "." + tld;
			}
			catch(Exception e)
			{
				converted = null;
				EventLogger.Log(EVENT_SOURCE, EventLogger.LogType.Error, e.ToString());
			}

			// Done.
			return converted;
		}

		//---------------------------------------------------------------------
		private static void Initialize()
		{
			string		dsn			= null;
			Database	database	= null;

			try
			{
				// create the converters
				m_raceConverter = ConverterFactory.Create(ConverterFactory.SCHEMA_RACE);
				m_punyConverter	= ConverterFactory.Create(ConverterFactory.SCHEMA_PUNYCODE);

				// Load the tld cache
				dsn			= ConfigurationSettings.AppSettings["dsn"];
				database	= new Database(dsn);
	
				// load the tld cache.
				m_tldCache	= database.GetTldCache();
			}
			catch(SqlException e)
			{
				// sql exception, load cache from local file
				LoadLocalCache();
				EventLogger.Log(EVENT_SOURCE, EventLogger.LogType.Error, "Load tld Cache from database failed\r\n" + e.ToString());
			}
		}


		//---------------------------------------------------------------------
		// Private methods.
		//---------------------------------------------------------------------
		private static void LoadLocalCache()
		{
			string				cacheFile	= null;
			string				line		= null;
			StreamReader		reader		= null;

			// Initialize
			m_tldCache	= new Hashtable(new CaseInsensitiveHashCodeProvider(), new CaseInsensitiveComparer());
			cacheFile	= ConfigurationSettings.AppSettings["cache"];

			try
			{
				// Create an instance of StreamReader to read from a file.
				// The using statement also closes the StreamReader.
				using (reader = new StreamReader(cacheFile)) 
				{
					// Read lines from the file until the end of 
					// the file is reached.
					while ((line = reader.ReadLine()) != null) 
					{
						line.Trim();
						m_tldCache.Add(line, true);
					}
				}
			}
			catch (Exception e) 
			{
				EventLogger.Log(EVENT_SOURCE, EventLogger.LogType.Error, "Load Local Cache Failed\r\n" + e.ToString());
			}
	
			// Done.
			return ;
		}

		private void Parse(DomainParser pParser, string pDomain, ref string pSld, ref string pTld)
		{
			try
			{
				if (!pParser.Parse(pDomain))
				{
					pSld = pDomain;
					pTld = null;
				}
				else
				{
					// get sld and tld
					pSld = pParser.SLD;
					pTld = pParser.TLD;
				}
			}
			catch(Exception)
			{
				pSld = pDomain;
				pTld = null;
			}

			// Done
			return ;
		}
	}
}
