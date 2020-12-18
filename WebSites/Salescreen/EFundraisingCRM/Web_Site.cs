using System;
using System.Xml;

namespace efundraising.EFundraisingCRM {

	public class WebSite: EFundraisingCRMDataObject {

		private int webSiteId;
		private string webSiteName;


		public WebSite() : this(int.MinValue) { }
		public WebSite(int webSiteId) : this(webSiteId, null) { }
		public WebSite(int webSiteId, string webSiteName) {
			this.webSiteId = webSiteId;
			this.webSiteName = webSiteName;
		}


		#region XML Methods

		#region Save XML
		public override string GenerateXML() {
			return "<WebSite>\r\n" +
			"	<WebSiteId>" + webSiteId + "</WebSiteId>\r\n" +
			"	<WebSiteName>" + System.Web.HttpUtility.HtmlEncode(webSiteName) + "</WebSiteName>\r\n" +
			"</WebSite>\r\n";
		}
		#endregion

		#region Load Methods
		// loop through the xml and set the properties and child classes
		public override void Load(XmlNode childNodes) {
			foreach(XmlNode node in childNodes) {
				if(ToLowerCase(node.Name) == ToLowerCase("webSiteId")) {
					SetXmlValue(ref webSiteId, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("webSiteName")) {
					SetXmlValue(ref webSiteName, node.InnerText);
				}
			}
		}
		#endregion

		#endregion

		#region Data Source Methods
		public static WebSite[] GetWebSites() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetWebSites();
		}

		public static WebSite GetWebSiteByID(int id) {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetWebSiteByID(id);
		}

		public int Insert() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.InsertWebSite(this);
		}

		public int Update() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.UpdateWebSite(this);
		}
		#endregion

		#region Properties
		public int WebSiteId {
			set { webSiteId = value; }
			get { return webSiteId; }
		}

		public string WebSiteName {
			set { webSiteName = value; }
			get { return webSiteName; }
		}

		#endregion
	}
}
