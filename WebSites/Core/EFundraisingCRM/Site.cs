using System;
using System.Xml;

namespace GA.BDC.Core.EFundraisingCRM {

	public class Site: EFundraisingCRMDataObject {

		private int siteId;
		private string siteTitle;
		private string siteContent;


		public Site() : this(int.MinValue) { }
		public Site(int siteId) : this(siteId, null) { }
		public Site(int siteId, string siteTitle) : this(siteId, siteTitle, null) { }
		public Site(int siteId, string siteTitle, string siteContent) {
			this.siteId = siteId;
			this.siteTitle = siteTitle;
			this.siteContent = siteContent;
		}


		#region XML Methods

		#region Save XML
		public override string GenerateXML() {
			return "<Site>\r\n" +
			"	<SiteId>" + siteId + "</SiteId>\r\n" +
			"	<SiteTitle>" + System.Web.HttpUtility.HtmlEncode(siteTitle) + "</SiteTitle>\r\n" +
			"	<SiteContent>" + System.Web.HttpUtility.HtmlEncode(siteContent) + "</SiteContent>\r\n" +
			"</Site>\r\n";
		}
		#endregion

		#region Load Methods
		// loop through the xml and set the properties and child classes
		public override void Load(XmlNode childNodes) {
			foreach(XmlNode node in childNodes) {
				if(ToLowerCase(node.Name) == ToLowerCase("siteId")) {
					SetXmlValue(ref siteId, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("siteTitle")) {
					SetXmlValue(ref siteTitle, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("siteContent")) {
					SetXmlValue(ref siteContent, node.InnerText);
				}
			}
		}
		#endregion

		#endregion

		#region Data Source Methods
		public static Site[] GetSites() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetSites();
		}

		public static Site GetSiteByID(int id) {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetSiteByID(id);
		}

		public int Insert() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.InsertSite(this);
		}

		public int Update() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.UpdateSite(this);
		}
		#endregion

		#region Properties
		public int SiteId {
			set { siteId = value; }
			get { return siteId; }
		}

		public string SiteTitle {
			set { siteTitle = value; }
			get { return siteTitle; }
		}

		public string SiteContent {
			set { siteContent = value; }
			get { return siteContent; }
		}

		#endregion
	}
}
