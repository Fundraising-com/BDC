using System;
using efundraising.Collections;
using efundraising.Configuration;
using efundraising.RecaudarFondosWeb;
using efundraising.Web.UI.Tracking.Omniture;

namespace efundraising.RecaudarFondosWeb
{
	/// <summary>
	/// Summary description for RecaudarFondosWebPage.
	/// </summary>
	public class RecaudarFondosWebPage : System.Web.UI.Page
	{
		public RecaudarFondosWebPage()
		{

		}

		#region Omniture Tracking Methods
		
		private Components.Server.Omniture.RecaudarFondosOmniture _recaudarFondosOmnitureTracking = null;

		protected Components.Server.Omniture.RecaudarFondosOmniture recaudarFondosOmnitureTracking 
		{
			get 
			{
				if (_recaudarFondosOmnitureTracking == null)
					_recaudarFondosOmnitureTracking = new Components.Server.Omniture.RecaudarFondosOmniture (GetOmnitureJSFileName(this.Page.Request));
				return _recaudarFondosOmnitureTracking;
			}
		}
		
		protected override void OnPreRender(EventArgs e) 
		{
			if (_recaudarFondosOmnitureTracking != null && _recaudarFondosOmnitureTracking.GetPageName().Trim() != string.Empty && _recaudarFondosOmnitureTracking.JSFileName.Trim() != string.Empty )
				RegisterStartupScript ("OmnitureTracking", _recaudarFondosOmnitureTracking.FetchScriptBlock ());
			base.OnPreRender (e);	
		}

		public static string GetOmnitureJSFileName(System.Web.HttpRequest request)
		{
			string resourcePath = "Resources/Javascript/";
			string result = string.Empty;
			string sAppPath= request.ApplicationPath;
			if (!sAppPath.EndsWith("/"))
				sAppPath += "/";
			if (ApplicationSettings.GetConfig()["WebTracking.Omniture", "isProduction"].ToLower () == "true")
			{
				result = sAppPath + resourcePath + ApplicationSettings.GetConfig()["WebTracking.Omniture", "jsFileNameInProduction"];
			}
			else
			{
				result = sAppPath + resourcePath + ApplicationSettings.GetConfig()["WebTracking.Omniture", "jsFileNameInDevelopment"];
			}
			return result;
		}

		#endregion
	}
}
