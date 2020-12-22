using System;

using efundraising.Configuration;

namespace efundraising.eReportWeb.Components.Server
{
	/// <summary>
	/// Summary description for Config.
	/// </summary>
	public class Config
	{
		private Config() {}

		// Is application External or Internal
		// External is the limited version of the report for the partners, internal for eFundraising
		public static bool IsExternal
		{
			get { return (ApplicationSettings.GetConfig()["Reports.External", "isExternal"].ToLower() == "true"); }
		}
	}
}
