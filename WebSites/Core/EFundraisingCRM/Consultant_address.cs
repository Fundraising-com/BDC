using System;
using System.Xml;

namespace GA.BDC.Core.EFundraisingCRM {

	public class ConsultantAddress: EFundraisingCRMDataObject {

		private int consultantAddressId;
		private int consultantId;
		private string countryCode;
		private string stateCode;
		private string streetAddress;
		private string city;
		private string zipCode;
		private DateTime dateInserted;


		public ConsultantAddress() : this(int.MinValue) { }
		public ConsultantAddress(int consultantAddressId) : this(consultantAddressId, int.MinValue) { }
		public ConsultantAddress(int consultantAddressId, int consultantId) : this(consultantAddressId, consultantId, null) { }
		public ConsultantAddress(int consultantAddressId, int consultantId, string countryCode) : this(consultantAddressId, consultantId, countryCode, null) { }
		public ConsultantAddress(int consultantAddressId, int consultantId, string countryCode, string stateCode) : this(consultantAddressId, consultantId, countryCode, stateCode, null) { }
		public ConsultantAddress(int consultantAddressId, int consultantId, string countryCode, string stateCode, string streetAddress) : this(consultantAddressId, consultantId, countryCode, stateCode, streetAddress, null) { }
		public ConsultantAddress(int consultantAddressId, int consultantId, string countryCode, string stateCode, string streetAddress, string city) : this(consultantAddressId, consultantId, countryCode, stateCode, streetAddress, city, null) { }
		public ConsultantAddress(int consultantAddressId, int consultantId, string countryCode, string stateCode, string streetAddress, string city, string zipCode) : this(consultantAddressId, consultantId, countryCode, stateCode, streetAddress, city, zipCode, DateTime.MinValue) { }
		public ConsultantAddress(int consultantAddressId, int consultantId, string countryCode, string stateCode, string streetAddress, string city, string zipCode, DateTime dateInserted) {
			this.consultantAddressId = consultantAddressId;
			this.consultantId = consultantId;
			this.countryCode = countryCode;
			this.stateCode = stateCode;
			this.streetAddress = streetAddress;
			this.city = city;
			this.zipCode = zipCode;
			this.dateInserted = dateInserted;
		}


		#region XML Methods

		#region Save XML
		public override string GenerateXML() {
			return "<ConsultantAddress>\r\n" +
			"	<ConsultantAddressId>" + consultantAddressId + "</ConsultantAddressId>\r\n" +
			"	<ConsultantId>" + consultantId + "</ConsultantId>\r\n" +
			"	<CountryCode>" + System.Web.HttpUtility.HtmlEncode(countryCode) + "</CountryCode>\r\n" +
			"	<StateCode>" + System.Web.HttpUtility.HtmlEncode(stateCode) + "</StateCode>\r\n" +
			"	<StreetAddress>" + System.Web.HttpUtility.HtmlEncode(streetAddress) + "</StreetAddress>\r\n" +
			"	<City>" + System.Web.HttpUtility.HtmlEncode(city) + "</City>\r\n" +
			"	<ZipCode>" + System.Web.HttpUtility.HtmlEncode(zipCode) + "</ZipCode>\r\n" +
			"	<DateInserted>" + dateInserted + "</DateInserted>\r\n" +
			"</ConsultantAddress>\r\n";
		}
		#endregion

		#region Load Methods
		// loop through the xml and set the properties and child classes
		public override void Load(XmlNode childNodes) {
			foreach(XmlNode node in childNodes) {
				if(ToLowerCase(node.Name) == ToLowerCase("consultantAddressId")) {
					SetXmlValue(ref consultantAddressId, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("consultantId")) {
					SetXmlValue(ref consultantId, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("countryCode")) {
					SetXmlValue(ref countryCode, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("stateCode")) {
					SetXmlValue(ref stateCode, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("streetAddress")) {
					SetXmlValue(ref streetAddress, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("city")) {
					SetXmlValue(ref city, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("zipCode")) {
					SetXmlValue(ref zipCode, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("dateInserted")) {
					SetXmlValue(ref dateInserted, node.InnerText);
				}
			}
		}
		#endregion

		#endregion

		#region Data Source Methods
		public static ConsultantAddress[] GetConsultantAddresss() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetConsultantAddresss();
		}

		public static ConsultantAddress GetConsultantAddressByID(int id) {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetConsultantAddressByID(id);
		}

		public int Insert() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.InsertConsultantAddress(this);
		}

		public int Update() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.UpdateConsultantAddress(this);
		}
		#endregion

		#region Properties
		public int ConsultantAddressId {
			set { consultantAddressId = value; }
			get { return consultantAddressId; }
		}

		public int ConsultantId {
			set { consultantId = value; }
			get { return consultantId; }
		}

		public string CountryCode {
			set { countryCode = value; }
			get { return countryCode; }
		}

		public string StateCode {
			set { stateCode = value; }
			get { return stateCode; }
		}

		public string StreetAddress {
			set { streetAddress = value; }
			get { return streetAddress; }
		}

		public string City {
			set { city = value; }
			get { return city; }
		}

		public string ZipCode {
			set { zipCode = value; }
			get { return zipCode; }
		}

		public DateTime DateInserted {
			set { dateInserted = value; }
			get { return dateInserted; }
		}

		#endregion
	}
}
