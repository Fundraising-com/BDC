using System;
using System.Xml;

namespace efundraising.EFundraisingCRM {

	public class EntryForm: EFundraisingCRMDataObject {

		private int entryFormID;
		private string entryFormDesc;
		private string masterTemplate;
		private string contentTemplate;
		private int webSiteID;


		public EntryForm() : this(int.MinValue) { }
		public EntryForm(int entryFormID) : this(entryFormID, null) { }
		public EntryForm(int entryFormID, string entryFormDesc) : this(entryFormID, entryFormDesc, null) { }
		public EntryForm(int entryFormID, string entryFormDesc, string masterTemplate) : this(entryFormID, entryFormDesc, masterTemplate, null) { }
		public EntryForm(int entryFormID, string entryFormDesc, string masterTemplate, string contentTemplate) : this(entryFormID, entryFormDesc, masterTemplate, contentTemplate, int.MinValue) { }
		public EntryForm(int entryFormID, string entryFormDesc, string masterTemplate, string contentTemplate, int webSiteID) {
			this.entryFormID = entryFormID;
			this.entryFormDesc = entryFormDesc;
			this.masterTemplate = masterTemplate;
			this.contentTemplate = contentTemplate;
			this.webSiteID = webSiteID;
		}


		#region XML Methods

		#region Save XML
		public override string GenerateXML() {
			return "<EntryForm>\r\n" +
			"	<EntryFormID>" + entryFormID + "</EntryFormID>\r\n" +
			"	<EntryFormDesc>" + System.Web.HttpUtility.HtmlEncode(entryFormDesc) + "</EntryFormDesc>\r\n" +
			"	<MasterTemplate>" + System.Web.HttpUtility.HtmlEncode(masterTemplate) + "</MasterTemplate>\r\n" +
			"	<ContentTemplate>" + System.Web.HttpUtility.HtmlEncode(contentTemplate) + "</ContentTemplate>\r\n" +
			"	<WebSiteID>" + webSiteID + "</WebSiteID>\r\n" +
			"</EntryForm>\r\n";
		}
		#endregion

		#region Load Methods
		// loop through the xml and set the properties and child classes
		public override void Load(XmlNode childNodes) {
			foreach(XmlNode node in childNodes) {
				if(ToLowerCase(node.Name) == ToLowerCase("entryFormId")) {
					SetXmlValue(ref entryFormID, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("entryFormDesc")) {
					SetXmlValue(ref entryFormDesc, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("masterTemplate")) {
					SetXmlValue(ref masterTemplate, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("contentTemplate")) {
					SetXmlValue(ref contentTemplate, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("webSiteId")) {
					SetXmlValue(ref webSiteID, node.InnerText);
				}
			}
		}
		#endregion

		#endregion

		#region Data Source Methods
		public static EntryForm[] GetEntryForms() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetEntryForms();
		}

		public static EntryForm GetEntryFormByID(int id) {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetEntryFormByID(id);
		}

		public int Insert() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.InsertEntryForm(this);
		}

		public int Update() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.UpdateEntryForm(this);
		}
		#endregion

		#region Properties
		public int EntryFormID {
			set { entryFormID = value; }
			get { return entryFormID; }
		}

		public string EntryFormDesc {
			set { entryFormDesc = value; }
			get { return entryFormDesc; }
		}

		public string MasterTemplate {
			set { masterTemplate = value; }
			get { return masterTemplate; }
		}

		public string ContentTemplate {
			set { contentTemplate = value; }
			get { return contentTemplate; }
		}

		public int WebSiteID {
			set { webSiteID = value; }
			get { return webSiteID; }
		}

		#endregion
	}
}
