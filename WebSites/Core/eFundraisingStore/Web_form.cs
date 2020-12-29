using System;
using System.Xml;

namespace GA.BDC.Core.eFundraisingStore {

	public class WebForm: eFundraisingStoreDataObject {

		private int webFormId;
		private string webFormDesc;
		private int webFormTypeId;
		private int leadStatusId;
		private string storedProcToCall;
		private DateTime datestamp;


		public WebForm() : this(int.MinValue) { }
		public WebForm(int webFormId) : this(webFormId, null) { }
		public WebForm(int webFormId, string webFormDesc) : this(webFormId, webFormDesc, int.MinValue) { }
		public WebForm(int webFormId, string webFormDesc, int webFormTypeId) : this(webFormId, webFormDesc, webFormTypeId, int.MinValue) { }
		public WebForm(int webFormId, string webFormDesc, int webFormTypeId, int leadStatusId) : this(webFormId, webFormDesc, webFormTypeId, leadStatusId, null) { }
		public WebForm(int webFormId, string webFormDesc, int webFormTypeId, int leadStatusId, string storedProcToCall) : this(webFormId, webFormDesc, webFormTypeId, leadStatusId, storedProcToCall, DateTime.MinValue) { }
		public WebForm(int webFormId, string webFormDesc, int webFormTypeId, int leadStatusId, string storedProcToCall, DateTime datestamp) {
			this.webFormId = webFormId;
			this.webFormDesc = webFormDesc;
			this.webFormTypeId = webFormTypeId;
			this.leadStatusId = leadStatusId;
			this.storedProcToCall = storedProcToCall;
			this.datestamp = datestamp;
		}


		#region XML Methods

		#region Save XML
		public override string GenerateXML() {
			return "<WebForm>\r\n" +
			"	<WebFormId>" + webFormId + "</WebFormId>\r\n" +
			"	<WebFormDesc>" + System.Web.HttpUtility.HtmlEncode(webFormDesc) + "</WebFormDesc>\r\n" +
			"	<WebFormTypeId>" + webFormTypeId + "</WebFormTypeId>\r\n" +
			"	<LeadStatusId>" + leadStatusId + "</LeadStatusId>\r\n" +
			"	<StoredProcToCall>" + System.Web.HttpUtility.HtmlEncode(storedProcToCall) + "</StoredProcToCall>\r\n" +
			"	<Datestamp>" + datestamp + "</Datestamp>\r\n" +
			"</WebForm>\r\n";
		}
		#endregion

		#region Load Methods
		// loop through the xml and set the properties and child classes
		public override void Load(XmlNode childNodes) {
			foreach(XmlNode node in childNodes) {
				if(node.Name.ToLower() == "webFormId") {
					SetXmlValue(ref webFormId, node.InnerText);
				}
				if(node.Name.ToLower() == "webFormDesc") {
					SetXmlValue(ref webFormDesc, node.InnerText);
				}
				if(node.Name.ToLower() == "webFormTypeId") {
					SetXmlValue(ref webFormTypeId, node.InnerText);
				}
				if(node.Name.ToLower() == "leadStatusId") {
					SetXmlValue(ref leadStatusId, node.InnerText);
				}
				if(node.Name.ToLower() == "storedProcToCall") {
					SetXmlValue(ref storedProcToCall, node.InnerText);
				}
				if(node.Name.ToLower() == "datestamp") {
					SetXmlValue(ref datestamp, node.InnerText);
				}
			}
		}
		#endregion

		#endregion

		#region Data Source Methods
		public static WebForm[] GetWebForms() {
			DataAccess.eFundraisingStoreDatabase dbo = new DataAccess.eFundraisingStoreDatabase();
			return dbo.GetWebForms();
		}

		public static WebForm GetWebFormByID(int id) {
			DataAccess.eFundraisingStoreDatabase dbo = new DataAccess.eFundraisingStoreDatabase();
			return dbo.GetWebFormByID(id);
		}

		public int Insert() {
			DataAccess.eFundraisingStoreDatabase dbo = new DataAccess.eFundraisingStoreDatabase();
			return dbo.InsertWebForm(this);
		}

		public int Update() {
			DataAccess.eFundraisingStoreDatabase dbo = new DataAccess.eFundraisingStoreDatabase();
			return dbo.UpdateWebForm(this);
		}
		#endregion

		#region Properties
		public int WebFormId {
			set { webFormId = value; }
			get { return webFormId; }
		}

		public string WebFormDesc {
			set { webFormDesc = value; }
			get { return webFormDesc; }
		}

		public int WebFormTypeId {
			set { webFormTypeId = value; }
			get { return webFormTypeId; }
		}

		public int LeadStatusId {
			set { leadStatusId = value; }
			get { return leadStatusId; }
		}

		public string StoredProcToCall {
			set { storedProcToCall = value; }
			get { return storedProcToCall; }
		}

		public DateTime Datestamp {
			set { datestamp = value; }
			get { return datestamp; }
		}

		#endregion
	}
}
