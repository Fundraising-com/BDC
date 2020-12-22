using System;
using System.Xml;

namespace efundraising.EFundraisingCRM {

	public class ARConsultant: EFundraisingCRMDataObject {

		private int aRConsultantID;
		private string name;
		private string email;
		private string phoneExt;
		private int isActive;
		private string ntLogin;


		public ARConsultant() : this(int.MinValue) { }
		public ARConsultant(int aRConsultantID) : this(aRConsultantID, null) { }
		public ARConsultant(int aRConsultantID, string name) : this(aRConsultantID, name, null) { }
		public ARConsultant(int aRConsultantID, string name, string email) : this(aRConsultantID, name, email, null) { }
		public ARConsultant(int aRConsultantID, string name, string email, string phoneExt) : this(aRConsultantID, name, email, phoneExt, int.MinValue) { }
		public ARConsultant(int aRConsultantID, string name, string email, string phoneExt, int isActive) : this(aRConsultantID, name, email, phoneExt, isActive, null) { }
		public ARConsultant(int aRConsultantID, string name, string email, string phoneExt, int isActive, string ntLogin) {
			this.aRConsultantID = aRConsultantID;
			this.name = name;
			this.email = email;
			this.phoneExt = phoneExt;
			this.isActive = isActive;
			this.ntLogin = ntLogin;
		}


		#region XML Methods

		#region Save XML
		public override string GenerateXML() {
			return "<ARConsultant>\r\n" +
			"	<ARConsultantID>" + aRConsultantID + "</ARConsultantID>\r\n" +
			"	<Name>" + System.Web.HttpUtility.HtmlEncode(name) + "</Name>\r\n" +
			"	<Email>" + System.Web.HttpUtility.HtmlEncode(email) + "</Email>\r\n" +
			"	<PhoneExt>" + System.Web.HttpUtility.HtmlEncode(phoneExt) + "</PhoneExt>\r\n" +
			"	<IsActive>" + isActive + "</IsActive>\r\n" +
			"	<NtLogin>" + System.Web.HttpUtility.HtmlEncode(ntLogin) + "</NtLogin>\r\n" +
			"</ARConsultant>\r\n";
		}
		#endregion

		#region Load Methods
		// loop through the xml and set the properties and child classes
		public override void Load(XmlNode childNodes) {
			foreach(XmlNode node in childNodes) {
				if(ToLowerCase(node.Name) == ToLowerCase("arConsultantId")) {
					SetXmlValue(ref aRConsultantID, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("name")) {
					SetXmlValue(ref name, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("email")) {
					SetXmlValue(ref email, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("phoneExt")) {
					SetXmlValue(ref phoneExt, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("isActive")) {
					SetXmlValue(ref isActive, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("ntLogin")) {
					SetXmlValue(ref ntLogin, node.InnerText);
				}
			}
		}
		#endregion

		#endregion

		#region Data Source Methods
		public static ARConsultant[] GetARConsultants() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetARConsultants();
		}

		public static ARConsultant GetARConsultantByID(int id) {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetARConsultantByID(id);
		}

		public int Insert() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.InsertARConsultant(this);
		}

		public int Update() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.UpdateARConsultant(this);
		}
		#endregion

		#region Properties
		public int ARConsultantID {
			set { aRConsultantID = value; }
			get { return aRConsultantID; }
		}

		public string Name {
			set { name = value; }
			get { return name; }
		}

		public string Email {
			set { email = value; }
			get { return email; }
		}

		public string PhoneExt {
			set { phoneExt = value; }
			get { return phoneExt; }
		}

		public int IsActive {
			set { isActive = value; }
			get { return isActive; }
		}

		public string NtLogin {
			set { ntLogin = value; }
			get { return ntLogin; }
		}

		#endregion
	}
}
