using System;
using System.Xml;

namespace efundraising.EFundraisingCRM {

	public class MailingName: EFundraisingCRMDataObject {

		private int mailingNameID;
		private string listName;
		private int listID;
		private string contactName;
		private string title;
		private string schoolName;
		private string schoolAddress;
		private string city;
		private string stateCode;
		private string zip;
		private string phoneNumber;
		private string faxNumber;
		private string email;
		private string schoolType;


		public MailingName() : this(int.MinValue) { }
		public MailingName(int mailingNameID) : this(mailingNameID, null) { }
		public MailingName(int mailingNameID, string listName) : this(mailingNameID, listName, int.MinValue) { }
		public MailingName(int mailingNameID, string listName, int listID) : this(mailingNameID, listName, listID, null) { }
		public MailingName(int mailingNameID, string listName, int listID, string contactName) : this(mailingNameID, listName, listID, contactName, null) { }
		public MailingName(int mailingNameID, string listName, int listID, string contactName, string title) : this(mailingNameID, listName, listID, contactName, title, null) { }
		public MailingName(int mailingNameID, string listName, int listID, string contactName, string title, string schoolName) : this(mailingNameID, listName, listID, contactName, title, schoolName, null) { }
		public MailingName(int mailingNameID, string listName, int listID, string contactName, string title, string schoolName, string schoolAddress) : this(mailingNameID, listName, listID, contactName, title, schoolName, schoolAddress, null) { }
		public MailingName(int mailingNameID, string listName, int listID, string contactName, string title, string schoolName, string schoolAddress, string city) : this(mailingNameID, listName, listID, contactName, title, schoolName, schoolAddress, city, null) { }
		public MailingName(int mailingNameID, string listName, int listID, string contactName, string title, string schoolName, string schoolAddress, string city, string stateCode) : this(mailingNameID, listName, listID, contactName, title, schoolName, schoolAddress, city, stateCode, null) { }
		public MailingName(int mailingNameID, string listName, int listID, string contactName, string title, string schoolName, string schoolAddress, string city, string stateCode, string zip) : this(mailingNameID, listName, listID, contactName, title, schoolName, schoolAddress, city, stateCode, zip, null) { }
		public MailingName(int mailingNameID, string listName, int listID, string contactName, string title, string schoolName, string schoolAddress, string city, string stateCode, string zip, string phoneNumber) : this(mailingNameID, listName, listID, contactName, title, schoolName, schoolAddress, city, stateCode, zip, phoneNumber, null) { }
		public MailingName(int mailingNameID, string listName, int listID, string contactName, string title, string schoolName, string schoolAddress, string city, string stateCode, string zip, string phoneNumber, string faxNumber) : this(mailingNameID, listName, listID, contactName, title, schoolName, schoolAddress, city, stateCode, zip, phoneNumber, faxNumber, null) { }
		public MailingName(int mailingNameID, string listName, int listID, string contactName, string title, string schoolName, string schoolAddress, string city, string stateCode, string zip, string phoneNumber, string faxNumber, string email) : this(mailingNameID, listName, listID, contactName, title, schoolName, schoolAddress, city, stateCode, zip, phoneNumber, faxNumber, email, null) { }
		public MailingName(int mailingNameID, string listName, int listID, string contactName, string title, string schoolName, string schoolAddress, string city, string stateCode, string zip, string phoneNumber, string faxNumber, string email, string schoolType) {
			this.mailingNameID = mailingNameID;
			this.listName = listName;
			this.listID = listID;
			this.contactName = contactName;
			this.title = title;
			this.schoolName = schoolName;
			this.schoolAddress = schoolAddress;
			this.city = city;
			this.stateCode = stateCode;
			this.zip = zip;
			this.phoneNumber = phoneNumber;
			this.faxNumber = faxNumber;
			this.email = email;
			this.schoolType = schoolType;
		}


		#region XML Methods

		#region Save XML
		public override string GenerateXML() {
			return "<MailingName>\r\n" +
			"	<MailingNameID>" + mailingNameID + "</MailingNameID>\r\n" +
			"	<ListName>" + System.Web.HttpUtility.HtmlEncode(listName) + "</ListName>\r\n" +
			"	<ListID>" + listID + "</ListID>\r\n" +
			"	<ContactName>" + System.Web.HttpUtility.HtmlEncode(contactName) + "</ContactName>\r\n" +
			"	<Title>" + System.Web.HttpUtility.HtmlEncode(title) + "</Title>\r\n" +
			"	<SchoolName>" + System.Web.HttpUtility.HtmlEncode(schoolName) + "</SchoolName>\r\n" +
			"	<SchoolAddress>" + System.Web.HttpUtility.HtmlEncode(schoolAddress) + "</SchoolAddress>\r\n" +
			"	<City>" + System.Web.HttpUtility.HtmlEncode(city) + "</City>\r\n" +
			"	<StateCode>" + System.Web.HttpUtility.HtmlEncode(stateCode) + "</StateCode>\r\n" +
			"	<Zip>" + System.Web.HttpUtility.HtmlEncode(zip) + "</Zip>\r\n" +
			"	<PhoneNumber>" + System.Web.HttpUtility.HtmlEncode(phoneNumber) + "</PhoneNumber>\r\n" +
			"	<FaxNumber>" + System.Web.HttpUtility.HtmlEncode(faxNumber) + "</FaxNumber>\r\n" +
			"	<Email>" + System.Web.HttpUtility.HtmlEncode(email) + "</Email>\r\n" +
			"	<SchoolType>" + System.Web.HttpUtility.HtmlEncode(schoolType) + "</SchoolType>\r\n" +
			"</MailingName>\r\n";
		}
		#endregion

		#region Load Methods
		// loop through the xml and set the properties and child classes
		public override void Load(XmlNode childNodes) {
			foreach(XmlNode node in childNodes) {
				if(ToLowerCase(node.Name) == ToLowerCase("mailingNameId")) {
					SetXmlValue(ref mailingNameID, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("listName")) {
					SetXmlValue(ref listName, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("listId")) {
					SetXmlValue(ref listID, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("contactName")) {
					SetXmlValue(ref contactName, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("title")) {
					SetXmlValue(ref title, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("schoolName")) {
					SetXmlValue(ref schoolName, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("schoolAddress")) {
					SetXmlValue(ref schoolAddress, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("city")) {
					SetXmlValue(ref city, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("stateCode")) {
					SetXmlValue(ref stateCode, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("zip")) {
					SetXmlValue(ref zip, node.InnerText);
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
				if(ToLowerCase(node.Name) == ToLowerCase("schoolType")) {
					SetXmlValue(ref schoolType, node.InnerText);
				}
			}
		}
		#endregion

		#endregion

		#region Data Source Methods
		public static MailingName[] GetMailingNames() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetMailingNames();
		}

		public static MailingName GetMailingNameByID(int id) {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetMailingNameByID(id);
		}

		public int Insert() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.InsertMailingName(this);
		}

		public int Update() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.UpdateMailingName(this);
		}
		#endregion

		#region Properties
		public int MailingNameID {
			set { mailingNameID = value; }
			get { return mailingNameID; }
		}

		public string ListName {
			set { listName = value; }
			get { return listName; }
		}

		public int ListID {
			set { listID = value; }
			get { return listID; }
		}

		public string ContactName {
			set { contactName = value; }
			get { return contactName; }
		}

		public string Title {
			set { title = value; }
			get { return title; }
		}

		public string SchoolName {
			set { schoolName = value; }
			get { return schoolName; }
		}

		public string SchoolAddress {
			set { schoolAddress = value; }
			get { return schoolAddress; }
		}

		public string City {
			set { city = value; }
			get { return city; }
		}

		public string StateCode {
			set { stateCode = value; }
			get { return stateCode; }
		}

		public string Zip {
			set { zip = value; }
			get { return zip; }
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

		public string SchoolType {
			set { schoolType = value; }
			get { return schoolType; }
		}

		#endregion
	}
}
