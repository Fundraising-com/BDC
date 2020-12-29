using System;

namespace GA.BDC.Core.WebTracking {
	/// <summary>
	/// Summary description for VisitorInfo.
	/// </summary>
    [Serializable]
   public class VisitorInfo : GA.BDC.Core.BusinessBase.BusinessBase
   {
		private int visitorLogID;
		private int availableWidth;
		private int availableHeight;
		private string ip;
		private string browserName;
		private string browserVersion;
		private string browserLanguage;
		private string platform;
		private string dns;
		private string referrer;
		private string countryCode;
		private string subDivisionCode;

		public VisitorInfo(VisitorLog vl) {
			visitorLogID = vl.VisitorLogID;
		}

		public void InsertIntoDatabase() {
			if(visitorLogID > 0) {
				DataAccess.WebTrackingDatabase dbo = new DataAccess.WebTrackingDatabase();
				dbo.InsertVisitorInfo(visitorLogID, availableWidth, availableHeight, ip,
					browserName, browserVersion, browserLanguage, platform, dns, referrer,
					countryCode, subDivisionCode);
			}
		}

		public int VisitorLogID {
			set { visitorLogID = value; }
			get { return visitorLogID; }
		}

		public int AvailableWidth {
			set { availableWidth = value; }
			get { return availableWidth; }
		}

		public int AvailableHeight {
			set { availableHeight = value; }
			get { return availableHeight; }
		}

		public string Ip {
			set { ip = value; }
			get { return ip; }
		}

		public string BrowserName {
			set { browserName = value; }
			get { return browserName; }
		}

		public string BrowserVersion {
			set { browserVersion = value; }
			get { return browserVersion; }
		}

		public string BrowserLanguage {
			set { browserLanguage = value; }
			get { return browserLanguage; }
		}

		public string Platform {
			set { platform = value; }
			get { return platform; }
		}

		public string Dns {
			set { dns = value; }
			get { return dns; }
		}

		public string Referrer {
			set { referrer = value; }
			get { return referrer; }
		}

		public string CountryCode {
			set { countryCode = value; }
			get { return countryCode; }
		}

		public string SubDivisionCode {
			set { subDivisionCode = value; }
			get { return subDivisionCode; }
		}

	}
}
