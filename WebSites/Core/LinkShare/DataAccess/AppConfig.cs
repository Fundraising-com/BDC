using System;
using GA.BDC.Core.Collections;
using GA.BDC.Core.Configuration;

namespace GA.BDC.Core.LinkShare.DataAccess
{
	/// <summary>
	/// Summary description for AppConfig.
	/// </summary>
	public class AppConfig
	{
		public AppConfig()
		{

		}

		public bool IsProduction 
		{
			get { return (ApplicationSettings.GetConfig()["LinkShare.Production", "isProduction"].ToLower() == "true"); }
		}

		public string ConnectionStringRelease
		{
			get 
			{
				return ApplicationSettings.GetConfig()["LinkShare.SqlConnection.Release", "connectionString"];
			}
		}

		public string ConnectionStringDebug
		{
			get 
			{
				return ApplicationSettings.GetConfig()["LinkShare.SqlConnection.Debug", "connectionString"];
			}
		}

		public string DataProviderDebug
		{
			get 
			{
				return ApplicationSettings.GetConfig()["LinkShare.SqlConnection.Debug", "provider"];
			}
		}

		public string DataProviderRelease
		{
			get 
			{
				return ApplicationSettings.GetConfig()["LinkShare.SqlConnection.Release", "provider"];
			}
		}
	}
}
