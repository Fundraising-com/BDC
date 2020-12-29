using System;
using System.Xml;

namespace GA.BDC.Core.EFundraisingCRM {

	public class FSMAddress: EFundraisingCRMDataObject {

		private int fSMAddressID;
		private int fSMID;
		private string countryCode;
		private string stateCode;
		private string fSMAddressType;
		private string city;
		private string zip;
		private string streetAddress;


		public FSMAddress() : this(int.MinValue) { }
		public FSMAddress(int fSMAddressID) : this(fSMAddressID, int.MinValue) { }
		public FSMAddress(int fSMAddressID, int fSMID) : this(fSMAddressID, fSMID, null) { }
		public FSMAddress(int fSMAddressID, int fSMID, string countryCode) : this(fSMAddressID, fSMID, countryCode, null) { }
		public FSMAddress(int fSMAddressID, int fSMID, string countryCode, string stateCode) : this(fSMAddressID, fSMID, countryCode, stateCode, null) { }
		public FSMAddress(int fSMAddressID, int fSMID, string countryCode, string stateCode, string fSMAddressType) : this(fSMAddressID, fSMID, countryCode, stateCode, fSMAddressType, null) { }
		public FSMAddress(int fSMAddressID, int fSMID, string countryCode, string stateCode, string fSMAddressType, string city) : this(fSMAddressID, fSMID, countryCode, stateCode, fSMAddressType, city, null) { }
		public FSMAddress(int fSMAddressID, int fSMID, string countryCode, string stateCode, string fSMAddressType, string city, string zip) : this(fSMAddressID, fSMID, countryCode, stateCode, fSMAddressType, city, zip, null) { }
		public FSMAddress(int fSMAddressID, int fSMID, string countryCode, string stateCode, string fSMAddressType, string city, string zip, string streetAddress) {
			this.fSMAddressID = fSMAddressID;
			this.fSMID = fSMID;
			this.countryCode = countryCode;
			this.stateCode = stateCode;
			this.fSMAddressType = fSMAddressType;
			this.city = city;
			this.zip = zip;
			this.streetAddress = streetAddress;
		}


		#region XML Methods

		#region Save XML
		public override string GenerateXML() {
			return "<FSMAddress>\r\n" +
			"	<FSMAddressID>" + fSMAddressID + "</FSMAddressID>\r\n" +
			"	<FSMID>" + fSMID + "</FSMID>\r\n" +
			"	<CountryCode>" + System.Web.HttpUtility.HtmlEncode(countryCode) + "</CountryCode>\r\n" +
			"	<StateCode>" + System.Web.HttpUtility.HtmlEncode(stateCode) + "</StateCode>\r\n" +
			"	<FSMAddressType>" + System.Web.HttpUtility.HtmlEncode(fSMAddressType) + "</FSMAddressType>\r\n" +
			"	<City>" + System.Web.HttpUtility.HtmlEncode(city) + "</City>\r\n" +
			"	<Zip>" + System.Web.HttpUtility.HtmlEncode(zip) + "</Zip>\r\n" +
			"	<StreetAddress>" + System.Web.HttpUtility.HtmlEncode(streetAddress) + "</StreetAddress>\r\n" +
			"</FSMAddress>\r\n";
		}
		#endregion

		#region Load Methods
		// loop through the xml and set the properties and child classes
		public override void Load(XmlNode childNodes) {
			foreach(XmlNode node in childNodes) {
				if(ToLowerCase(node.Name) == ToLowerCase("fsmAddressId")) {
					SetXmlValue(ref fSMAddressID, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("fsmId")) {
					SetXmlValue(ref fSMID, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("countryCode")) {
					SetXmlValue(ref countryCode, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("stateCode")) {
					SetXmlValue(ref stateCode, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("fsmAddressType")) {
					SetXmlValue(ref fSMAddressType, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("city")) {
					SetXmlValue(ref city, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("zip")) {
					SetXmlValue(ref zip, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("streetAddress")) {
					SetXmlValue(ref streetAddress, node.InnerText);
				}
			}
		}
		#endregion

		#endregion

		#region Data Source Methods
		public static FSMAddress[] GetFSMAddresss() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetFSMAddresss();
		}

		public static FSMAddress GetFSMAddressByID(int id) {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetFSMAddressByID(id);
		}

		public int Insert() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.InsertFSMAddress(this);
		}

		public int Update() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.UpdateFSMAddress(this);
		}
		#endregion

		#region Properties
		public int FSMAddressID {
			set { fSMAddressID = value; }
			get { return fSMAddressID; }
		}

		public int FSMID {
			set { fSMID = value; }
			get { return fSMID; }
		}

		public string CountryCode {
			set { countryCode = value; }
			get { return countryCode; }
		}

		public string StateCode {
			set { stateCode = value; }
			get { return stateCode; }
		}

		public string FSMAddressType {
			set { fSMAddressType = value; }
			get { return fSMAddressType; }
		}

		public string City {
			set { city = value; }
			get { return city; }
		}

		public string Zip {
			set { zip = value; }
			get { return zip; }
		}

		public string StreetAddress {
			set { streetAddress = value; }
			get { return streetAddress; }
		}

		#endregion
	}
}
