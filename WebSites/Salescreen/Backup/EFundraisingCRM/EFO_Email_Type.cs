using System;
using System.Xml;

namespace efundraising.EFundraisingCRM {

	public class EFOEmailType: EFundraisingCRMDataObject {

		private int emailTypeID;
		private string body;
		private string description;


		public EFOEmailType() : this(int.MinValue) { }
		public EFOEmailType(int emailTypeID) : this(emailTypeID, null) { }
		public EFOEmailType(int emailTypeID, string body) : this(emailTypeID, body, null) { }
		public EFOEmailType(int emailTypeID, string body, string description) {
			this.emailTypeID = emailTypeID;
			this.body = body;
			this.description = description;
		}


		#region XML Methods

		#region Save XML
		public override string GenerateXML() {
			return "<EFOEmailType>\r\n" +
			"	<EmailTypeID>" + emailTypeID + "</EmailTypeID>\r\n" +
			"	<Body>" + System.Web.HttpUtility.HtmlEncode(body) + "</Body>\r\n" +
			"	<Description>" + System.Web.HttpUtility.HtmlEncode(description) + "</Description>\r\n" +
			"</EFOEmailType>\r\n";
		}
		#endregion

		#region Load Methods
		// loop through the xml and set the properties and child classes
		public override void Load(XmlNode childNodes) {
			foreach(XmlNode node in childNodes) {
				if(ToLowerCase(node.Name) == ToLowerCase("emailTypeId")) {
					SetXmlValue(ref emailTypeID, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("body")) {
					SetXmlValue(ref body, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("description")) {
					SetXmlValue(ref description, node.InnerText);
				}
			}
		}
		#endregion

		#endregion

		#region Data Source Methods
		public static EFOEmailType[] GetEFOEmailTypes() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetEFOEmailTypes();
		}

		public static EFOEmailType GetEFOEmailTypeByID(int id) {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetEFOEmailTypeByID(id);
		}

		public int Insert() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.InsertEFOEmailType(this);
		}

		public int Update() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.UpdateEFOEmailType(this);
		}
		#endregion

		#region Properties
		public int EmailTypeID {
			set { emailTypeID = value; }
			get { return emailTypeID; }
		}

		public string Body {
			set { body = value; }
			get { return body; }
		}

		public string Description {
			set { description = value; }
			get { return description; }
		}

		#endregion
	}
}
