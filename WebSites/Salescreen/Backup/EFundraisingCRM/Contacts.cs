using System;
using System.Xml;

namespace efundraising.EFundraisingCRM {

	public class Contacts: EFundraisingCRMDataObject {

		private int contactID;
		private string firstName;
		private string lastName;
		private string phoneNumber;
		private string phoneExt;
		private string streetAddress;
		private string city;
		private string stateCode;
		private string countryCode;
		private string zipCode;
		private string comments;


		public Contacts() : this(int.MinValue) { }
		public Contacts(int contactID) : this(contactID, null) { }
		public Contacts(int contactID, string firstName) : this(contactID, firstName, null) { }
		public Contacts(int contactID, string firstName, string lastName) : this(contactID, firstName, lastName, null) { }
		public Contacts(int contactID, string firstName, string lastName, string phoneNumber) : this(contactID, firstName, lastName, phoneNumber, null) { }
		public Contacts(int contactID, string firstName, string lastName, string phoneNumber, string phoneExt) : this(contactID, firstName, lastName, phoneNumber, phoneExt, null) { }
		public Contacts(int contactID, string firstName, string lastName, string phoneNumber, string phoneExt, string streetAddress) : this(contactID, firstName, lastName, phoneNumber, phoneExt, streetAddress, null) { }
		public Contacts(int contactID, string firstName, string lastName, string phoneNumber, string phoneExt, string streetAddress, string city) : this(contactID, firstName, lastName, phoneNumber, phoneExt, streetAddress, city, null) { }
		public Contacts(int contactID, string firstName, string lastName, string phoneNumber, string phoneExt, string streetAddress, string city, string stateCode) : this(contactID, firstName, lastName, phoneNumber, phoneExt, streetAddress, city, stateCode, null) { }
		public Contacts(int contactID, string firstName, string lastName, string phoneNumber, string phoneExt, string streetAddress, string city, string stateCode, string countryCode) : this(contactID, firstName, lastName, phoneNumber, phoneExt, streetAddress, city, stateCode, countryCode, null) { }
		public Contacts(int contactID, string firstName, string lastName, string phoneNumber, string phoneExt, string streetAddress, string city, string stateCode, string countryCode, string zipCode) : this(contactID, firstName, lastName, phoneNumber, phoneExt, streetAddress, city, stateCode, countryCode, zipCode, null) { }
		public Contacts(int contactID, string firstName, string lastName, string phoneNumber, string phoneExt, string streetAddress, string city, string stateCode, string countryCode, string zipCode, string comments) {
			this.contactID = contactID;
			this.firstName = firstName;
			this.lastName = lastName;
			this.phoneNumber = phoneNumber;
			this.phoneExt = phoneExt;
			this.streetAddress = streetAddress;
			this.city = city;
			this.stateCode = stateCode;
			this.countryCode = countryCode;
			this.zipCode = zipCode;
			this.comments = comments;
		}


		#region XML Methods

		#region Save XML
		public override string GenerateXML() {
			return "<Contacts>\r\n" +
			"	<ContactID>" + contactID + "</ContactID>\r\n" +
			"	<FirstName>" + System.Web.HttpUtility.HtmlEncode(firstName) + "</FirstName>\r\n" +
			"	<LastName>" + System.Web.HttpUtility.HtmlEncode(lastName) + "</LastName>\r\n" +
			"	<PhoneNumber>" + System.Web.HttpUtility.HtmlEncode(phoneNumber) + "</PhoneNumber>\r\n" +
			"	<PhoneExt>" + System.Web.HttpUtility.HtmlEncode(phoneExt) + "</PhoneExt>\r\n" +
			"	<StreetAddress>" + System.Web.HttpUtility.HtmlEncode(streetAddress) + "</StreetAddress>\r\n" +
			"	<City>" + System.Web.HttpUtility.HtmlEncode(city) + "</City>\r\n" +
			"	<StateCode>" + System.Web.HttpUtility.HtmlEncode(stateCode) + "</StateCode>\r\n" +
			"	<CountryCode>" + System.Web.HttpUtility.HtmlEncode(countryCode) + "</CountryCode>\r\n" +
			"	<ZipCode>" + System.Web.HttpUtility.HtmlEncode(zipCode) + "</ZipCode>\r\n" +
			"	<Comments>" + System.Web.HttpUtility.HtmlEncode(comments) + "</Comments>\r\n" +
			"</Contacts>\r\n";
		}
		#endregion

		#region Load Methods
		// loop through the xml and set the properties and child classes
		public override void Load(XmlNode childNodes) {
			foreach(XmlNode node in childNodes) {
				if(ToLowerCase(node.Name) == ToLowerCase("contactId")) {
					SetXmlValue(ref contactID, node.InnerText);
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
				if(ToLowerCase(node.Name) == ToLowerCase("phoneExt")) {
					SetXmlValue(ref phoneExt, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("streetAddress")) {
					SetXmlValue(ref streetAddress, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("city")) {
					SetXmlValue(ref city, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("stateCode")) {
					SetXmlValue(ref stateCode, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("countryCode")) {
					SetXmlValue(ref countryCode, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("zipCode")) {
					SetXmlValue(ref zipCode, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("comments")) {
					SetXmlValue(ref comments, node.InnerText);
				}
			}
		}
		#endregion

		#endregion

		#region Data Source Methods
		public static Contacts[] GetContactss() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetContactss();
		}

		public static Contacts GetContactsByID(int id) {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetContactsByID(id);
		}

		public int Insert() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.InsertContacts(this);
		}

		public int Update() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.UpdateContacts(this);
		}
		#endregion

		#region Properties
		public int ContactID {
			set { contactID = value; }
			get { return contactID; }
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

		public string PhoneExt {
			set { phoneExt = value; }
			get { return phoneExt; }
		}

		public string StreetAddress {
			set { streetAddress = value; }
			get { return streetAddress; }
		}

		public string City {
			set { city = value; }
			get { return city; }
		}

		public string StateCode {
			set { stateCode = value; }
			get { return stateCode; }
		}

		public string CountryCode {
			set { countryCode = value; }
			get { return countryCode; }
		}

		public string ZipCode {
			set { zipCode = value; }
			get { return zipCode; }
		}

		public string Comments {
			set { comments = value; }
			get { return comments; }
		}

		#endregion
	}
}
