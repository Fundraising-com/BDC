using System;
using GA.BDC.Core.Collections;
using GA.BDC.Core.Configuration;
using GA.BDC.Core.LinkShare.DataAccess;

namespace GA.BDC.Core.LinkShare
{
	/// <summary>
	/// Summary description for AppConfig.
	/// </summary>
	public class Config
	{
		public Config()
		{

		}

		public bool IsProduction 
		{
			get {return new AppConfig().IsProduction; }
		}

		public string ConnectionStringRelease
		{
			get {return new AppConfig().ConnectionStringRelease;}
		}

		public string ConnectionStringDebug
		{
			get {return new AppConfig().ConnectionStringDebug;}
		}

		public string DataProviderDebug
		{
			get {return new AppConfig().DataProviderDebug;}
		}

		public string DataProviderRelease
		{
			get {return new AppConfig().DataProviderRelease;}
		}
	}
}
