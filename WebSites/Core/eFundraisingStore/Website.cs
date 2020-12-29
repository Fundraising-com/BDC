using System;
using System.Xml;

namespace GA.BDC.Core.eFundraisingStore {

	public class Website: eFundraisingStoreDataObject {

		private short websiteId;
		private int partnerId;
		private short webprojectId;
		private string websiteDns;


		public Website() : this(short.MinValue) { }
		public Website(short websiteId) : this(websiteId, int.MinValue) { }
		public Website(short websiteId, int partnerId) : this(websiteId, partnerId, short.MinValue) { }
		public Website(short websiteId, int partnerId, short webprojectId) : this(websiteId, partnerId, webprojectId, null) { }
		public Website(short websiteId, int partnerId, short webprojectId, string websiteDns) {
			this.websiteId = websiteId;
			this.partnerId = partnerId;
			this.webprojectId = webprojectId;
			this.websiteDns = websiteDns;
		}


		#region XML Methods

		#region Save XML
		public override string GenerateXML() {
			return "<Website>\r\n" +
			"	<WebsiteId>" + websiteId + "</WebsiteId>\r\n" +
			"	<PartnerId>" + partnerId + "</PartnerId>\r\n" +
			"	<WebprojectId>" + webprojectId + "</WebprojectId>\r\n" +
			"	<WebsiteDns>" + System.Web.HttpUtility.HtmlEncode(websiteDns) + "</WebsiteDns>\r\n" +
			"</Website>\r\n";
		}
		#endregion

		#region Load Methods
		// loop through the xml and set the properties and child classes
		public override void Load(XmlNode childNodes) {
			foreach(XmlNode node in childNodes) {
				if(node.Name.ToLower() == "websiteId") {
					SetXmlValue(ref websiteId, node.InnerText);
				}
				if(node.Name.ToLower() == "partnerId") {
					SetXmlValue(ref partnerId, node.InnerText);
				}
				if(node.Name.ToLower() == "webprojectId") {
					SetXmlValue(ref webprojectId, node.InnerText);
				}
				if(node.Name.ToLower() == "websiteDns") {
					SetXmlValue(ref websiteDns, node.InnerText);
				}
			}
		}
		#endregion

		#endregion

		#region Data Source Methods
		public static Website[] GetWebsites() {
			DataAccess.eFundraisingStoreDatabase dbo = new DataAccess.eFundraisingStoreDatabase();
			return dbo.GetWebsites();
		}

		public static Website GetWebsiteByID(int id) {
			DataAccess.eFundraisingStoreDatabase dbo = new DataAccess.eFundraisingStoreDatabase();
			return dbo.GetWebsiteByID(id);
		}

		public int Insert() {
			DataAccess.eFundraisingStoreDatabase dbo = new DataAccess.eFundraisingStoreDatabase();
			return dbo.InsertWebsite(this);
		}

		public int Update() {
			DataAccess.eFundraisingStoreDatabase dbo = new DataAccess.eFundraisingStoreDatabase();
			return dbo.UpdateWebsite(this);
		}
		#endregion

		#region Properties
		public short WebsiteId {
			set { websiteId = value; }
			get { return websiteId; }
		}

		public int PartnerId {
			set { partnerId = value; }
			get { return partnerId; }
		}

		public short WebprojectId {
			set { webprojectId = value; }
			get { return webprojectId; }
		}

		public string WebsiteDns {
			set { websiteDns = value; }
			get { return websiteDns; }
		}

		#endregion
	}
}
