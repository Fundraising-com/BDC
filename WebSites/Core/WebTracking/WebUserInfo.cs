using System;

namespace GA.BDC.Core.WebTracking
{
	/// <summary>
	/// Summary description for WebUserInfo.
	/// </summary>
    [Serializable]
   public class WebUserInfo : GA.BDC.Core.BusinessBase.BusinessBase
   {
		private string _sessionID;
		private string _referrer;
		private string _browserName;
		private string _browserLanguage;
		private string _browserVersion;
		private string _platform;
		private string _ipAddress;

		public WebUserInfo(string sessionID, string referrer, string broswerName, string browserLanguage,
							string browserVersion, string platform, string ipAddress)
		{
			_sessionID = sessionID;
			_referrer = referrer;
			_browserName = broswerName;
			_browserLanguage = browserLanguage;
			_browserVersion = browserVersion;
			_platform = platform;
			_ipAddress = ipAddress;
		}

		public static WebUserInfo GetWebUserPackage(System.Web.HttpRequest request,
			System.Web.SessionState.HttpSessionState session) {
			WebUserInfo webPackage;

			string sessionId = session.SessionID;
			string referrer = null;

			if(request.UrlReferrer != null)
				referrer = request.UrlReferrer.AbsoluteUri;
			else
				referrer = "";

			string browserName = request.Browser.Browser;
			string browserLanguage = "en-US";
			string browserVersion = request.Browser.Version;
			string platform = request.Browser.Platform;
			string ipAddress = request.UserHostAddress;

			webPackage = new WebUserInfo(sessionId, referrer, browserName, browserLanguage, browserVersion,
				platform, ipAddress);

			return webPackage;
		}

		#region Properties
		public string SessionID {
			get { return _sessionID; }
			set { _sessionID = value; }
		}

		public string Referrer {
			get { return _referrer; }
			set { _referrer = value; }
		}

		public string BrowserName {
			get { return _browserName; }
			set { _browserName = value; }
		}

		public string BrowserVersion {
			get { return _browserVersion; }
			set { _browserVersion = value; }
		}

		public string Platform {
			get { return _platform; }
			set { _platform = value; }
		}

		public string IpAddress {
			get { return _ipAddress; }
			set { _ipAddress = value; }
		}


		#endregion

	}
}

