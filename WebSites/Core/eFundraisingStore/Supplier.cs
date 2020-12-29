using System;
using System.Xml;

namespace GA.BDC.Core.eFundraisingStore {

	public class Supplier: eFundraisingStoreDataObject {

		private int supplierId;
		private string name;
		private string streetAdress;
		private string city;
		private string zip;
		private string contactName;
		private string phone;
		private string fax;
		private string accountNo;
		private Decimal creditMargin;
		private string stateCode;
		private string countryCode;
		private string comment;


		public Supplier() : this(int.MinValue) { }
		public Supplier(int supplierId) : this(supplierId, null) { }
		public Supplier(int supplierId, string name) : this(supplierId, name, null) { }
		public Supplier(int supplierId, string name, string streetAdress) : this(supplierId, name, streetAdress, null) { }
		public Supplier(int supplierId, string name, string streetAdress, string city) : this(supplierId, name, streetAdress, city, null) { }
		public Supplier(int supplierId, string name, string streetAdress, string city, string zip) : this(supplierId, name, streetAdress, city, zip, null) { }
		public Supplier(int supplierId, string name, string streetAdress, string city, string zip, string contactName) : this(supplierId, name, streetAdress, city, zip, contactName, null) { }
		public Supplier(int supplierId, string name, string streetAdress, string city, string zip, string contactName, string phone) : this(supplierId, name, streetAdress, city, zip, contactName, phone, null) { }
		public Supplier(int supplierId, string name, string streetAdress, string city, string zip, string contactName, string phone, string fax) : this(supplierId, name, streetAdress, city, zip, contactName, phone, fax, null) { }
		public Supplier(int supplierId, string name, string streetAdress, string city, string zip, string contactName, string phone, string fax, string accountNo) : this(supplierId, name, streetAdress, city, zip, contactName, phone, fax, accountNo, Decimal.MinValue) { }
		public Supplier(int supplierId, string name, string streetAdress, string city, string zip, string contactName, string phone, string fax, string accountNo, Decimal creditMargin) : this(supplierId, name, streetAdress, city, zip, contactName, phone, fax, accountNo, creditMargin, null) { }
		public Supplier(int supplierId, string name, string streetAdress, string city, string zip, string contactName, string phone, string fax, string accountNo, Decimal creditMargin, string stateCode) : this(supplierId, name, streetAdress, city, zip, contactName, phone, fax, accountNo, creditMargin, stateCode, null) { }
		public Supplier(int supplierId, string name, string streetAdress, string city, string zip, string contactName, string phone, string fax, string accountNo, Decimal creditMargin, string stateCode, string countryCode) : this(supplierId, name, streetAdress, city, zip, contactName, phone, fax, accountNo, creditMargin, stateCode, countryCode, null) { }
		public Supplier(int supplierId, string name, string streetAdress, string city, string zip, string contactName, string phone, string fax, string accountNo, Decimal creditMargin, string stateCode, string countryCode, string comment) {
			this.supplierId = supplierId;
			this.name = name;
			this.streetAdress = streetAdress;
			this.city = city;
			this.zip = zip;
			this.contactName = contactName;
			this.phone = phone;
			this.fax = fax;
			this.accountNo = accountNo;
			this.creditMargin = creditMargin;
			this.stateCode = stateCode;
			this.countryCode = countryCode;
			this.comment = comment;
		}


		#region XML Methods

		#region Save XML
		public override string GenerateXML() {
			return "<Supplier>\r\n" +
			"	<SupplierId>" + supplierId + "</SupplierId>\r\n" +
			"	<Name>" + System.Web.HttpUtility.HtmlEncode(name) + "</Name>\r\n" +
			"	<StreetAdress>" + System.Web.HttpUtility.HtmlEncode(streetAdress) + "</StreetAdress>\r\n" +
			"	<City>" + System.Web.HttpUtility.HtmlEncode(city) + "</City>\r\n" +
			"	<Zip>" + System.Web.HttpUtility.HtmlEncode(zip) + "</Zip>\r\n" +
			"	<ContactName>" + System.Web.HttpUtility.HtmlEncode(contactName) + "</ContactName>\r\n" +
			"	<Phone>" + System.Web.HttpUtility.HtmlEncode(phone) + "</Phone>\r\n" +
			"	<Fax>" + System.Web.HttpUtility.HtmlEncode(fax) + "</Fax>\r\n" +
			"	<AccountNo>" + System.Web.HttpUtility.HtmlEncode(accountNo) + "</AccountNo>\r\n" +
			"	<CreditMargin>" + creditMargin + "</CreditMargin>\r\n" +
			"	<StateCode>" + System.Web.HttpUtility.HtmlEncode(stateCode) + "</StateCode>\r\n" +
			"	<CountryCode>" + System.Web.HttpUtility.HtmlEncode(countryCode) + "</CountryCode>\r\n" +
			"	<Comment>" + System.Web.HttpUtility.HtmlEncode(comment) + "</Comment>\r\n" +
			"</Supplier>\r\n";
		}
		#endregion

		#region Load Methods
		// loop through the xml and set the properties and child classes
		public override void Load(XmlNode childNodes) {
			foreach(XmlNode node in childNodes) {
				if(node.Name.ToLower() == "supplierId") {
					SetXmlValue(ref supplierId, node.InnerText);
				}
				if(node.Name.ToLower() == "name") {
					SetXmlValue(ref name, node.InnerText);
				}
				if(node.Name.ToLower() == "streetAdress") {
					SetXmlValue(ref streetAdress, node.InnerText);
				}
				if(node.Name.ToLower() == "city") {
					SetXmlValue(ref city, node.InnerText);
				}
				if(node.Name.ToLower() == "zip") {
					SetXmlValue(ref zip, node.InnerText);
				}
				if(node.Name.ToLower() == "contactName") {
					SetXmlValue(ref contactName, node.InnerText);
				}
				if(node.Name.ToLower() == "phone") {
					SetXmlValue(ref phone, node.InnerText);
				}
				if(node.Name.ToLower() == "fax") {
					SetXmlValue(ref fax, node.InnerText);
				}
				if(node.Name.ToLower() == "accountNo") {
					SetXmlValue(ref accountNo, node.InnerText);
				}
				if(node.Name.ToLower() == "creditMargin") {
					SetXmlValue(ref creditMargin, node.InnerText);
				}
				if(node.Name.ToLower() == "stateCode") {
					SetXmlValue(ref stateCode, node.InnerText);
				}
				if(node.Name.ToLower() == "countryCode") {
					SetXmlValue(ref countryCode, node.InnerText);
				}
				if(node.Name.ToLower() == "comment") {
					SetXmlValue(ref comment, node.InnerText);
				}
			}
		}
		#endregion

		#endregion

		#region Data Source Methods
		public static Supplier[] GetSuppliers() {
			DataAccess.eFundraisingStoreDatabase dbo = new DataAccess.eFundraisingStoreDatabase();
			return dbo.GetSuppliers();
		}

		public static Supplier GetSupplierByID(int id) {
			DataAccess.eFundraisingStoreDatabase dbo = new DataAccess.eFundraisingStoreDatabase();
			return dbo.GetSupplierByID(id);
		}

		public int Insert() {
			DataAccess.eFundraisingStoreDatabase dbo = new DataAccess.eFundraisingStoreDatabase();
			return dbo.InsertSupplier(this);
		}

		public int Update() {
			DataAccess.eFundraisingStoreDatabase dbo = new DataAccess.eFundraisingStoreDatabase();
			return dbo.UpdateSupplier(this);
		}
		#endregion

		#region Properties
		public int SupplierId {
			set { supplierId = value; }
			get { return supplierId; }
		}

		public string Name {
			set { name = value; }
			get { return name; }
		}

		public string StreetAdress {
			set { streetAdress = value; }
			get { return streetAdress; }
		}

		public string City {
			set { city = value; }
			get { return city; }
		}

		public string Zip {
			set { zip = value; }
			get { return zip; }
		}

		public string ContactName {
			set { contactName = value; }
			get { return contactName; }
		}

		public string Phone {
			set { phone = value; }
			get { return phone; }
		}

		public string Fax {
			set { fax = value; }
			get { return fax; }
		}

		public string AccountNo {
			set { accountNo = value; }
			get { return accountNo; }
		}

		public Decimal CreditMargin {
			set { creditMargin = value; }
			get { return creditMargin; }
		}

		public string StateCode {
			set { stateCode = value; }
			get { return stateCode; }
		}

		public string CountryCode {
			set { countryCode = value; }
			get { return countryCode; }
		}

		public string Comment {
			set { comment = value; }
			get { return comment; }
		}

		#endregion
	}
}
