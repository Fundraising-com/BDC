using System;
using System.Xml;

namespace efundraising.EFundraisingCRM {

	public class CrmUsers: EFundraisingCRMDataObject {

		private string consultantID;
		private string userName;
		private string password;
		private int accessLevel;


		public CrmUsers() : this(null) { }
		public CrmUsers(string consultantID) : this(consultantID, null) { }
		public CrmUsers(string consultantID, string userName) : this(consultantID, userName, null) { }
		public CrmUsers(string consultantID, string userName, string password) : this(consultantID, userName, password, int.MinValue) { }
		public CrmUsers(string consultantID, string userName, string password, int accessLevel) {
			this.consultantID = consultantID;
			this.userName = userName;
			this.password = password;
			this.accessLevel = accessLevel;
		}


		#region XML Methods

		#region Save XML
		public override string GenerateXML() {
			return "<CrmUsers>\r\n" +
			"	<ConsultantID>" + System.Web.HttpUtility.HtmlEncode(consultantID) + "</ConsultantID>\r\n" +
			"	<UserName>" + System.Web.HttpUtility.HtmlEncode(userName) + "</UserName>\r\n" +
			"	<Password>" + System.Web.HttpUtility.HtmlEncode(password) + "</Password>\r\n" +
			"	<AccessLevel>" + accessLevel + "</AccessLevel>\r\n" +
			"</CrmUsers>\r\n";
		}
		#endregion

		#region Load Methods
		// loop through the xml and set the properties and child classes
		public override void Load(XmlNode childNodes) {
			foreach(XmlNode node in childNodes) {
				if(ToLowerCase(node.Name) == ToLowerCase("consultantId")) {
					SetXmlValue(ref consultantID, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("userName")) {
					SetXmlValue(ref userName, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("password")) {
					SetXmlValue(ref password, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("accessLevel")) {
					SetXmlValue(ref accessLevel, node.InnerText);
				}
			}
		}
		#endregion

		#endregion

		#region Data Source Methods
		public static CrmUsers[] GetCrmUserss() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetCrmUserss();
		}

		/*
		public static CrmUsers GetCrmUsersByID(int id) {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetCrmUsersByID(id);
		}

		public int Insert() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.InsertCrmUsers(this);
		}

		public int Update() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.UpdateCrmUsers(this);
		}*/
		#endregion

		#region Properties
		public string ConsultantID {
			set { consultantID = value; }
			get { return consultantID; }
		}

		public string UserName {
			set { userName = value; }
			get { return userName; }
		}

		public string Password {
			set { password = value; }
			get { return password; }
		}

		public int AccessLevel {
			set { accessLevel = value; }
			get { return accessLevel; }
		}

		#endregion
	}
}
