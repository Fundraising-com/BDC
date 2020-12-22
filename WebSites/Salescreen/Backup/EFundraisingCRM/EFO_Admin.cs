using System;
using System.Xml;

namespace efundraising.EFundraisingCRM {

	public class EFOAdmin: EFundraisingCRMDataObject {

		private int adminID;
		private string uID;
		private string password;


		public EFOAdmin() : this(int.MinValue) { }
		public EFOAdmin(int adminID) : this(adminID, null) { }
		public EFOAdmin(int adminID, string uID) : this(adminID, uID, null) { }
		public EFOAdmin(int adminID, string uID, string password) {
			this.adminID = adminID;
			this.uID = uID;
			this.password = password;
		}


		#region XML Methods

		#region Save XML
		public override string GenerateXML() {
			return "<EFOAdmin>\r\n" +
			"	<AdminID>" + adminID + "</AdminID>\r\n" +
			"	<UID>" + System.Web.HttpUtility.HtmlEncode(uID) + "</UID>\r\n" +
			"	<Password>" + System.Web.HttpUtility.HtmlEncode(password) + "</Password>\r\n" +
			"</EFOAdmin>\r\n";
		}
		#endregion

		#region Load Methods
		// loop through the xml and set the properties and child classes
		public override void Load(XmlNode childNodes) {
			foreach(XmlNode node in childNodes) {
				if(ToLowerCase(node.Name) == ToLowerCase("adminId")) {
					SetXmlValue(ref adminID, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("uid")) {
					SetXmlValue(ref uID, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("password")) {
					SetXmlValue(ref password, node.InnerText);
				}
			}
		}
		#endregion

		#endregion

		#region Data Source Methods
		public static EFOAdmin[] GetEFOAdmins() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetEFOAdmins();
		}

		public static EFOAdmin GetEFOAdminByID(int id) {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetEFOAdminByID(id);
		}

		public int Insert() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.InsertEFOAdmin(this);
		}

		public int Update() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.UpdateEFOAdmin(this);
		}
		#endregion

		#region Properties
		public int AdminID {
			set { adminID = value; }
			get { return adminID; }
		}

		public string UID {
			set { uID = value; }
			get { return uID; }
		}

		public string Password {
			set { password = value; }
			get { return password; }
		}

		#endregion
	}
}
