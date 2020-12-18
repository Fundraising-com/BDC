using System;
using System.Xml;

namespace efundraising.EFundraisingCRM {

	public class AdvertisingSupportContact: EFundraisingCRMDataObject {

		private int advertisingSupportContactID;
		private int advertisingSupportID;
		private string firstName;
		private string lastName;
		private string phoneNumber;
		private string faxNumber;
		private string email;


		public AdvertisingSupportContact() : this(int.MinValue) { }
		public AdvertisingSupportContact(int advertisingSupportContactID) : this(advertisingSupportContactID, int.MinValue) { }
		public AdvertisingSupportContact(int advertisingSupportContactID, int advertisingSupportID) : this(advertisingSupportContactID, advertisingSupportID, null) { }
		public AdvertisingSupportContact(int advertisingSupportContactID, int advertisingSupportID, string firstName) : this(advertisingSupportContactID, advertisingSupportID, firstName, null) { }
		public AdvertisingSupportContact(int advertisingSupportContactID, int advertisingSupportID, string firstName, string lastName) : this(advertisingSupportContactID, advertisingSupportID, firstName, lastName, null) { }
		public AdvertisingSupportContact(int advertisingSupportContactID, int advertisingSupportID, string firstName, string lastName, string phoneNumber) : this(advertisingSupportContactID, advertisingSupportID, firstName, lastName, phoneNumber, null) { }
		public AdvertisingSupportContact(int advertisingSupportContactID, int advertisingSupportID, string firstName, string lastName, string phoneNumber, string faxNumber) : this(advertisingSupportContactID, advertisingSupportID, firstName, lastName, phoneNumber, faxNumber, null) { }
		public AdvertisingSupportContact(int advertisingSupportContactID, int advertisingSupportID, string firstName, string lastName, string phoneNumber, string faxNumber, string email) {
			this.advertisingSupportContactID = advertisingSupportContactID;
			this.advertisingSupportID = advertisingSupportID;
			this.firstName = firstName;
			this.lastName = lastName;
			this.phoneNumber = phoneNumber;
			this.faxNumber = faxNumber;
			this.email = email;
		}


		#region XML Methods

		#region Save XML
		public override string GenerateXML() {
			return "<AdvertisingSupportContact>\r\n" +
			"	<AdvertisingSupportContactID>" + advertisingSupportContactID + "</AdvertisingSupportContactID>\r\n" +
			"	<AdvertisingSupportID>" + advertisingSupportID + "</AdvertisingSupportID>\r\n" +
			"	<FirstName>" + System.Web.HttpUtility.HtmlEncode(firstName) + "</FirstName>\r\n" +
			"	<LastName>" + System.Web.HttpUtility.HtmlEncode(lastName) + "</LastName>\r\n" +
			"	<PhoneNumber>" + System.Web.HttpUtility.HtmlEncode(phoneNumber) + "</PhoneNumber>\r\n" +
			"	<FaxNumber>" + System.Web.HttpUtility.HtmlEncode(faxNumber) + "</FaxNumber>\r\n" +
			"	<Email>" + System.Web.HttpUtility.HtmlEncode(email) + "</Email>\r\n" +
			"</AdvertisingSupportContact>\r\n";
		}
		#endregion

		#region Load Methods
		// loop through the xml and set the properties and child classes
		public override void Load(XmlNode childNodes) {
			foreach(XmlNode node in childNodes) {
				if(ToLowerCase(node.Name) == ToLowerCase("advertisingSupportContactId")) {
					SetXmlValue(ref advertisingSupportContactID, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("advertisingSupportId")) {
					SetXmlValue(ref advertisingSupportID, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("firstName")) {
					SetXmlValue(ref firstName, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("lastName")) {
					SetXmlValue(ref lastName, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("phoneNumber")) {
					SetXmlValue(ref phoneNumber, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("faxNumber")) {
					SetXmlValue(ref faxNumber, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("email")) {
					SetXmlValue(ref email, node.InnerText);
				}
			}
		}
		#endregion

		#endregion

		#region Data Source Methods
		public static AdvertisingSupportContact[] GetAdvertisingSupportContacts() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetAdvertisingSupportContacts();
		}

		public static AdvertisingSupportContact GetAdvertisingSupportContactByID(int id) {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetAdvertisingSupportContactByID(id);
		}

		public int Insert() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.InsertAdvertisingSupportContact(this);
		}

		public int Update() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.UpdateAdvertisingSupportContact(this);
		}
		#endregion

		#region Properties
		public int AdvertisingSupportContactID {
			set { advertisingSupportContactID = value; }
			get { return advertisingSupportContactID; }
		}

		public int AdvertisingSupportID {
			set { advertisingSupportID = value; }
			get { return advertisingSupportID; }
		}

		public string FirstName {
			set { firstName = value; }
			get { return firstName; }
		}

		public string LastName {
			set { lastName = value; }
			get { return lastName; }
		}

		public string PhoneNumber {
			set { phoneNumber = value; }
			get { return phoneNumber; }
		}

		public string FaxNumber {
			set { faxNumber = value; }
			get { return faxNumber; }
		}

		public string Email {
			set { email = value; }
			get { return email; }
		}

		#endregion
	}
}
