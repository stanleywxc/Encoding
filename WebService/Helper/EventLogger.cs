using System;
using System.Text;
using System.Diagnostics;

namespace Echo.Services.Encoding.WebService.Helper
{
	public class EventLogger
	{
		public enum LogType
		{
			Error	= EventLogEntryType.Error,
			Failure = EventLogEntryType.FailureAudit,
			Info	= EventLogEntryType.Information,
			Success	= EventLogEntryType.SuccessAudit,
			Warning	= EventLogEntryType.Warning
		};

		//---------------------------------------------------------------------
		// Static Data members
		//---------------------------------------------------------------------
		private static EventLog m_logger = null;

		//---------------------------------------------------------------------
		// Static Public methods
		//---------------------------------------------------------------------
		public static void Log(string pSource, LogType pType, params string[] pMessages)
		{
			StringBuilder buffer = new StringBuilder();

			// Initialize it if necessary
			Initialize(pSource);

			if (null != pMessages)
			{
				for (int index = 0; index < pMessages.Length; index++)
				{
					if (null != pMessages[index])
						buffer.AppendFormat("{0}\r\n", pMessages[index]);
				}
			}

			// set the source and log it.
			m_logger.Source = pSource;
			m_logger.WriteEntry(buffer.ToString(), (EventLogEntryType)pType);

			return ;
		}

		public static void Initialize(string pSource)
		{
			// Already initialized?
			if (null != m_logger)
				return ;

			// check if the log source exists.
			if(!EventLog.SourceExists(pSource))
				EventLog.CreateEventSource(pSource, "Application");

			// Create an EventLog instance and assign its source.
			m_logger		= new EventLog();
			m_logger.Source = pSource;
		}
	}
}