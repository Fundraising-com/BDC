using System;
using System.Xml;

namespace GA.BDC.Core.eFundraisingStore {

	public class WebFormTypeDesc: eFundraisingStoreDataObject {

		private int webFormTypeId;
		private string cultureCode;
		private string description;


		public WebFormTypeDesc() : this(int.MinValue) { }
		public WebFormTypeDesc(int webFormTypeId) : this(webFormTypeId, null) { }
		public WebFormTypeDesc(int webFormTypeId, string cultureCode) : this(webFormTypeId, cultureCode, null) { }
		public WebFormTypeDesc(int webFormTypeId, string cultureCode, string description) {
			this.webFormTypeId = webFormTypeId;
			this.cultureCode = cultureCode;
			this.description = description;
		}


		#region XML Methods

		#region Save XML
		public override string GenerateXML() {
			return "<WebFormTypeDesc>\r\n" +
			"	<WebFormTypeId>" + webFormTypeId + "</WebFormTypeId>\r\n" +
			"	<CultureCode>" + System.Web.HttpUtility.HtmlEncode(cultureCode) + "</CultureCode>\r\n" +
			"	<Description>" + System.Web.HttpUtility.HtmlEncode(description) + "</Description>\r\n" +
			"</WebFormTypeDesc>\r\n";
		}
		#endregion

		#region Load Methods
		// loop through the xml and set the properties and child classes
		public override void Load(XmlNode childNodes) {
			foreach(XmlNode node in childNodes) {
				if(node.Name.ToLower() == "webFormTypeId") {
					SetXmlValue(ref webFormTypeId, node.InnerText);
				}
				if(node.Name.ToLower() == "cultureCode") {
					SetXmlValue(ref cultureCode, node.InnerText);
				}
				if(node.Name.ToLower() == "description") {
					SetXmlValue(ref description, node.InnerText);
				}
			}
		}
		#endregion

		#endregion

		#region Data Source Methods
		public static WebFormTypeDesc[] GetWebFormTypeDescs() {
			DataAccess.eFundraisingStoreDatabase dbo = new DataAccess.eFundraisingStoreDatabase();
			return dbo.GetWebFormTypeDescs();
		}

		public static WebFormTypeDesc GetWebFormTypeDescByID(int id) {
			DataAccess.eFundraisingStoreDatabase dbo = new DataAccess.eFundraisingStoreDatabase();
			return dbo.GetWebFormTypeDescByID(id);
		}

		public int Insert() {
			DataAccess.eFundraisingStoreDatabase dbo = new DataAccess.eFundraisingStoreDatabase();
			return dbo.InsertWebFormTypeDesc(this);
		}

		public int Update() {
			DataAccess.eFundraisingStoreDatabase dbo = new DataAccess.eFundraisingStoreDatabase();
			return dbo.UpdateWebFormTypeDesc(this);
		}
		#endregion

		#region Properties
		public int WebFormTypeId {
			set { webFormTypeId = value; }
			get { return webFormTypeId; }
		}

		public string CultureCode {
			set { cultureCode = value; }
			get { return cultureCode; }
		}

		public string Description {
			set { description = value; }
			get { return description; }
		}

		#endregion
	}
}
