using System;
using System.Xml;

namespace GA.BDC.Core.EFundraisingCRM {

	public class Supplier: EFundraisingCRMDataObject {

		private int supplierId;
		private string supplierName;
		private string streetAdress;
		private string city;
		private string zip;
		private string contactName;
		private string phone;
		private string fax;
		private string accountNo;
		private double creditMargin;
		private string stateCode;
		private string countryCode;
		private string comments;


		public Supplier() : this(int.MinValue) { }
		public Supplier(int supplierId) : this(supplierId, null) { }
		public Supplier(int supplierId, string supplierName) : this(supplierId, supplierName, null) { }
		public Supplier(int supplierId, string supplierName, string streetAdress) : this(supplierId, supplierName, streetAdress, null) { }
		public Supplier(int supplierId, string supplierName, string streetAdress, string city) : this(supplierId, supplierName, streetAdress, city, null) { }
		public Supplier(int supplierId, string supplierName, string streetAdress, string city, string zip) : this(supplierId, supplierName, streetAdress, city, zip, null) { }
		public Supplier(int supplierId, string supplierName, string streetAdress, string city, string zip, string contactName) : this(supplierId, supplierName, streetAdress, city, zip, contactName, null) { }
		public Supplier(int supplierId, string supplierName, string streetAdress, string city, string zip, string contactName, string phone) : this(supplierId, supplierName, streetAdress, city, zip, contactName, phone, null) { }
		public Supplier(int supplierId, string supplierName, string streetAdress, string city, string zip, string contactName, string phone, string fax) : this(supplierId, supplierName, streetAdress, city, zip, contactName, phone, fax, null) { }
		public Supplier(int supplierId, string supplierName, string streetAdress, string city, string zip, string contactName, string phone, string fax, string accountNo) : this(supplierId, supplierName, streetAdress, city, zip, contactName, phone, fax, accountNo, float.MinValue) { }
		public Supplier(int supplierId, string supplierName, string streetAdress, string city, string zip, string contactName, string phone, string fax, string accountNo, float creditMargin) : this(supplierId, supplierName, streetAdress, city, zip, contactName, phone, fax, accountNo, creditMargin, null) { }
		public Supplier(int supplierId, string supplierName, string streetAdress, string city, string zip, string contactName, string phone, string fax, string accountNo, float creditMargin, string stateCode) : this(supplierId, supplierName, streetAdress, city, zip, contactName, phone, fax, accountNo, creditMargin, stateCode, null) { }
		public Supplier(int supplierId, string supplierName, string streetAdress, string city, string zip, string contactName, string phone, string fax, string accountNo, float creditMargin, string stateCode, string countryCode) : this(supplierId, supplierName, streetAdress, city, zip, contactName, phone, fax, accountNo, creditMargin, stateCode, countryCode, null) { }
		public Supplier(int supplierId, string supplierName, string streetAdress, string city, string zip, string contactName, string phone, string fax, string accountNo, float creditMargin, string stateCode, string countryCode, string comments) {
			this.supplierId = supplierId;
			this.supplierName = supplierName;
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
			this.comments = comments;
		}


		#region XML Methods

		#region Save XML
		public override string GenerateXML() {
			return "<Supplier>\r\n" +
			"	<SupplierId>" + supplierId + "</SupplierId>\r\n" +
			"	<SupplierName>" + System.Web.HttpUtility.HtmlEncode(supplierName) + "</SupplierName>\r\n" +
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
			"	<Comments>" + System.Web.HttpUtility.HtmlEncode(comments) + "</Comments>\r\n" +
			"</Supplier>\r\n";
		}
		#endregion

		#region Load Methods
		// loop through the xml and set the properties and child classes
		public override void Load(XmlNode childNodes) {
			foreach(XmlNode node in childNodes) {
				if(ToLowerCase(node.Name) == ToLowerCase("supplierId")) {
					SetXmlValue(ref supplierId, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("supplierName")) {
					SetXmlValue(ref supplierName, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("streetAdress")) {
					SetXmlValue(ref streetAdress, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("city")) {
					SetXmlValue(ref city, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("zip")) {
					SetXmlValue(ref zip, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("contactName")) {
					SetXmlValue(ref contactName, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("phone")) {
					SetXmlValue(ref phone, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("fax")) {
					SetXmlValue(ref fax, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("accountNo")) {
					SetXmlValue(ref accountNo, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("creditMargin")) {
					SetXmlValue(ref creditMargin, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("stateCode")) {
					SetXmlValue(ref stateCode, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("countryCode")) {
					SetXmlValue(ref countryCode, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("comments")) {
					SetXmlValue(ref comments, node.InnerText);
				}
			}
		}
		#endregion

		#endregion

		#region Data Source Methods
		public static Supplier[] GetSuppliers() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetSuppliers();
		}

		/*
		public static Supplier GetSupplierByID(int id) {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetSupplierByID(id);
		}

		public int Insert() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.InsertSupplier(this);
		}

		public int Update() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.UpdateSupplier(this);
		}*/
		#endregion

		#region Properties
		public int SupplierId {
			set { supplierId = value; }
			get { return supplierId; }
		}

		public string SupplierName {
			set { supplierName = value; }
			get { return supplierName; }
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

		public double CreditMargin {
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

		public string Comments {
			set { comments = value; }
			get { return comments; }
		}

		#endregion
	}
}
