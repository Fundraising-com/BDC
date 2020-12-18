using System;
using System.Xml;

namespace efundraising.EFundraisingCRM {

	public class AdministrativeEmail: EFundraisingCRMDataObject {

		private int administrativeID;
		private string email;
		private string firstName;
		private string lastName;


		public AdministrativeEmail() : this(int.MinValue) { }
		public AdministrativeEmail(int administrativeID) : this(administrativeID, null) { }
		public AdministrativeEmail(int administrativeID, string email) : this(administrativeID, email, null) { }
		public AdministrativeEmail(int administrativeID, string email, string firstName) : this(administrativeID, email, firstName, null) { }
		public AdministrativeEmail(int administrativeID, string email, string firstName, string lastName) {
			this.administrativeID = administrativeID;
			this.email = email;
			this.firstName = firstName;
			this.lastName = lastName;
		}


		#region XML Methods

		#region Save XML
		public override string GenerateXML() {
			return "<AdministrativeEmail>\r\n" +
			"	<AdministrativeID>" + administrativeID + "</AdministrativeID>\r\n" +
			"	<Email>" + System.Web.HttpUtility.HtmlEncode(email) + "</Email>\r\n" +
			"	<FirstName>" + System.Web.HttpUtility.HtmlEncode(firstName) + "</FirstName>\r\n" +
			"	<LastName>" + System.Web.HttpUtility.HtmlEncode(lastName) + "</LastName>\r\n" +
			"</AdministrativeEmail>\r\n";
		}
		#endregion

		#region Load Methods
		// loop through the xml and set the properties and child classes
		public override void Load(XmlNode childNodes) {
			foreach(XmlNode node in childNodes) {
				if(ToLowerCase(node.Name) == ToLowerCase("administrativeId")) {
					SetXmlValue(ref administrativeID, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("email")) {
					SetXmlValue(ref email, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("firstName")) {
					SetXmlValue(ref firstName, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("lastName")) {
					SetXmlValue(ref lastName, node.InnerText);
				}
			}
		}
		#endregion

		#endregion

		#region Data Source Methods
		public static AdministrativeEmail[] GetAdministrativeEmails() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetAdministrativeEmails();
		}

		public static AdministrativeEmail GetAdministrativeEmailByID(int id) {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetAdministrativeEmailByID(id);
		}

		public int Insert() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.InsertAdministrativeEmail(this);
		}

		public int Update() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.UpdateAdministrativeEmail(this);
		}
		#endregion

		#region Properties
		public int AdministrativeID {
			set { administrativeID = value; }
			get { return administrativeID; }
		}

		public string Email {
			set { email = value; }
			get { return email; }
		}

		public string FirstName {
			set { firstName = value; }
			get { return firstName; }
		}

		public string LastName {
			set { lastName = value; }
			get { return lastName; }
		}

		#endregion
	}
}
