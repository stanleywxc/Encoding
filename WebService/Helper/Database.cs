using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading;

namespace Echo.Services.Encoding.WebService.Helper
{
	/// <summary>
	/// Summary description for Database.
	/// </summary>
	public class Database
	{
		// const
		private const string	QUERY_TLD		= "sp_DN_GetAllTLDs";

		// data members
		private string			m_dsn			= null;
		private SqlConnection	m_connection	= null;
		private SqlCommand		m_tldQuery		= null;

		public Database(string pDSN)
		{
			Initialize(pDSN);
		}

		//---------------------------------------------------------------------
		// Public Methods
		//---------------------------------------------------------------------
		public Hashtable GetTldCache()
		{
			Hashtable		cache		= null;
			SqlDataReader	dataReader	= null;

			// Initialize
			cache = new Hashtable(new CaseInsensitiveHashCodeProvider(), new CaseInsensitiveComparer());

			try
			{
				// Local connection object
				Monitor.Enter(m_connection);

				// check if the connection is open
				if (!IsConnectionOpen())
					OpenConnection();
				
				// execute the statement and get query result beck.
				dataReader = m_tldQuery.ExecuteReader();
				
				// read all the result back to Hashtable.
				while(dataReader.Read())
				{
					string tld = dataReader.GetString(0);
					if (null == tld)
						continue;

					// Add it to cache.
					cache.Add(tld, true);
				}
				
				// close reader.
				dataReader.Close();
			}
			finally
			{
				// close connection
				CloseConnection();

				// Release the lock.
				Monitor.Exit(m_connection);
			}
			
			// Done.
			return cache;
		}

		public bool OpenConnection()
		{
			if (!IsConnectionOpen())
				m_connection.Open();
			
			return true;
		}

		public void CloseConnection()
		{
			if (IsConnectionOpen())
				m_connection.Close();

			return ;
		}

		public bool IsConnectionOpen()
		{
			return ((null != m_connection) && (ConnectionState.Open == m_connection.State));
		}

		//---------------------------------------------------------------------
		// Private Methods
		//---------------------------------------------------------------------

		// create sql connection
		private SqlConnection createConnection()
		{
			SqlConnection	connection	= null;

			try
			{
				// try to create a connection
				connection = new SqlConnection(m_dsn);
			}
			catch(Exception e)
			{
				throw e;
			}

			// Done.
			return connection;
		}

		private void Initialize(string pDSN)
		{
			try
			{
				// create the objects.
				m_dsn		 = pDSN;
				m_connection = createConnection();
				m_tldQuery	 = new SqlCommand();

				// Initialize tld query statement
				m_tldQuery.CommandType	= CommandType.StoredProcedure;
				m_tldQuery.CommandText	= QUERY_TLD;
				m_tldQuery.Connection	= m_connection;				
			}
			catch(Exception e)
			{
				throw e;
			}
		}
	}
}
