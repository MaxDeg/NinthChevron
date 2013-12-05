using System;
using System.Configuration;

namespace TestSandbox
{
	public static class AppSettings
	{
	}
	
	public static class ConnectionStrings
	{
		public static string LocalSqlServer { get { return ConfigurationManager.ConnectionStrings["LocalSqlServer"].ConnectionString; } }
		public static string LocalMySqlServer { get { return ConfigurationManager.ConnectionStrings["LocalMySqlServer"].ConnectionString; } }
		public static string T4Connection { get { return ConfigurationManager.ConnectionStrings["T4Connection"].ConnectionString; } }
	}
}