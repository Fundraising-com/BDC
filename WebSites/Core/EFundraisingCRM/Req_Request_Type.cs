using System;
using System.Xml;

namespace GA.BDC.Core.EFundraisingCRM {

	public class ReqRequestType: EFundraisingCRMDataObject {

		private int requestTypeID;
		private int languageId;
		private string description;


		public ReqRequestType() : this(int.MinValue) { }
		public ReqRequestType(int requestTypeID) : this(requestTypeID, int.MinValue) { }
		public ReqRequestType(int requestTypeID, int languageId) : this(requestTypeID, languageId, null) { }
		public ReqRequestType(int requestTypeID, int languageId, string description) {
			this.requestTypeID = requestTypeID;
			this.languageId = languageId;
			this.description = description;
		}


		#region XML Methods

		#region Save XML
		public override string GenerateXML() {
			return "<ReqRequestType>\r\n" +
			"	<RequestTypeID>" + requestTypeID + "</RequestTypeID>\r\n" +
			"	<LanguageId>" + languageId + "</LanguageId>\r\n" +
			"	<Description>" + System.Web.HttpUtility.HtmlEncode(description) + "</Description>\r\n" +
			"</ReqRequestType>\r\n";
		}
		#endregion

		#region Load Methods
		// loop through the xml and set the properties and child classes
		public override void Load(XmlNode childNodes) {
			foreach(XmlNode node in childNodes) {
				if(ToLowerCase(node.Name) == ToLowerCase("requestTypeId")) {
					SetXmlValue(ref requestTypeID, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("languageId")) {
					SetXmlValue(ref languageId, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("description")) {
					SetXmlValue(ref description, node.InnerText);
				}
			}
		}
		#endregion

		#endregion

		#region Data Source Methods
		public static ReqRequestType[] GetReqRequestTypes() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetReqRequestTypes();
		}

		public static ReqRequestType GetReqRequestTypeByID(int id) {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetReqRequestTypeByID(id);
		}

		public int Insert() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.InsertReqRequestType(this);
		}

		public int Update() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.UpdateReqRequestType(this);
		}
		#endregion

		#region Properties
		public int RequestTypeID {
			set { requestTypeID = value; }
			get { return requestTypeID; }
		}

		public int LanguageId {
			set { languageId = value; }
			get { return languageId; }
		}

		public string Description {
			set { description = value; }
			get { return description; }
		}

		#endregion
	}
}
