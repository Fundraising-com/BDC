using System;
using System.Xml;

namespace efundraising.EFundraisingCRM {

	public class Bank: EFundraisingCRMDataObject {

		private int bankID;
		private string name;
		private string contact;
		private string streetAddress;
		private string stateCode;
		private string city;
		private string zipCode;
		private string countryCode;
		private string telephone;
		private string fax;
		

		public Bank() : this(int.MinValue) { }
		public Bank(int bankID) : this(bankID, null) { }
		public Bank(int bankID, string name) : this(bankID, name, null) { }
		public Bank(int bankID, string name, string contact) : this(bankID, name, contact, null) { }
		public Bank(int bankID, string name, string contact, string streetAddress) : this(bankID, name, contact, streetAddress, null) { }
		public Bank(int bankID, string name, string contact, string streetAddress, string stateCode) : this(bankID, name, contact, streetAddress, stateCode, null) { }
		public Bank(int bankID, string name, string contact, string streetAddress, string stateCode, string city) : this(bankID, name, contact, streetAddress, stateCode, city, null) { }
		public Bank(int bankID, string name, string contact, string streetAddress, string stateCode, string city, string zipCode) : this(bankID, name, contact, streetAddress, stateCode, city, zipCode, null) { }
		public Bank(int bankID, string name, string contact, string streetAddress, string stateCode, string city, string zipCode, string countryCode) : this(bankID, name, contact, streetAddress, stateCode, city, zipCode, countryCode, null) { }
		public Bank(int bankID, string name, string contact, string streetAddress, string stateCode, string city, string zipCode, string countryCode, string telephone) : this(bankID, name, contact, streetAddress, stateCode, city, zipCode, countryCode, telephone, null) { }
		public Bank(int bankID, string name, string contact, string streetAddress, string stateCode, string city, string zipCode, string countryCode, string telephone, string fax) {
			this.bankID = bankID;
			this.name = name;
			this.contact = contact;
			this.streetAddress = streetAddress;
			this.stateCode = stateCode;
			this.city = city;
			this.zipCode = zipCode;
			this.countryCode = countryCode;
			this.telephone = telephone;
			this.fax = fax;
		}


		#region XML Methods

		#region Save XML
		public override string GenerateXML() {
			return "<Bank>\r\n" +
			"	<BankID>" + bankID + "</BankID>\r\n" +
			"	<Name>" + System.Web.HttpUtility.HtmlEncode(name) + "</Name>\r\n" +
			"	<Contact>" + System.Web.HttpUtility.HtmlEncode(contact) + "</Contact>\r\n" +
			"	<StreetAddress>" + System.Web.HttpUtility.HtmlEncode(streetAddress) + "</StreetAddress>\r\n" +
			"	<StateCode>" + System.Web.HttpUtility.HtmlEncode(stateCode) + "</StateCode>\r\n" +
			"	<City>" + System.Web.HttpUtility.HtmlEncode(city) + "</City>\r\n" +
			"	<ZipCode>" + System.Web.HttpUtility.HtmlEncode(zipCode) + "</ZipCode>\r\n" +
			"	<CountryCode>" + System.Web.HttpUtility.HtmlEncode(countryCode) + "</CountryCode>\r\n" +
			"	<Telephone>" + System.Web.HttpUtility.HtmlEncode(telephone) + "</Telephone>\r\n" +
			"	<Fax>" + System.Web.HttpUtility.HtmlEncode(fax) + "</Fax>\r\n" +
			"</Bank>\r\n";
		}
		#endregion

		#region Load Methods
		// loop through the xml and set the properties and child classes
		public override void Load(XmlNode childNodes) {
			foreach(XmlNode node in childNodes) {
				if(ToLowerCase(node.Name) == ToLowerCase("bankId")) {
					SetXmlValue(ref bankID, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("name")) {
					SetXmlValue(ref name, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("contact")) {
					SetXmlValue(ref contact, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("streetAddress")) {
					SetXmlValue(ref streetAddress, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("stateCode")) {
					SetXmlValue(ref stateCode, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("city")) {
					SetXmlValue(ref city, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("zipCode")) {
					SetXmlValue(ref zipCode, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("countryCode")) {
					SetXmlValue(ref countryCode, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("telephone")) {
					SetXmlValue(ref telephone, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("fax")) {
					SetXmlValue(ref fax, node.InnerText);
				}
			}
		}
		#endregion

		#endregion

		#region Data Source Methods
		public static Bank[] GetBanks() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetBanks();
		}

		public static Bank GetBankByID(int id) {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetBankByID(id);
		}

		public int Insert() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.InsertBank(this);
		}

		public int Update() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.UpdateBank(this);
		}
		#endregion

		#region Properties
		public int BankID {
			set { bankID = value; }
			get { return bankID; }
		}

		public string Name {
			set { name = value; }
			get { return name; }
		}

		public string Contact {
			set { contact = value; }
			get { return contact; }
		}

		public string StreetAddress {
			set { streetAddress = value; }
			get { return streetAddress; }
		}

		public string StateCode {
			set { stateCode = value; }
			get { return stateCode; }
		}

		public string City {
			set { city = value; }
			get { return city; }
		}

		public string ZipCode {
			set { zipCode = value; }
			get { return zipCode; }
		}

		public string CountryCode {
			set { countryCode = value; }
			get { return countryCode; }
		}

		public string Telephone {
			set { telephone = value; }
			get { return telephone; }
		}

		public string Fax {
			set { fax = value; }
			get { return fax; }
		}

		#endregion
	}
}
