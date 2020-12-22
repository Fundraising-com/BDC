using System;
using System.Xml;

namespace efundraising.EFundraisingCRM {


	
	public class Countries: EFundraisingCRMDataObject {

		private string countryCode;
		private string countryName;
		private string longCountryCode;
		private string numericCode;
		private string currencyCode;

		


		public Countries() : this(null) { }
		public Countries(string countryCode) : this(countryCode, null) { }
		public Countries(string countryCode, string countryName) : this(countryCode, countryName, null) { }
		public Countries(string countryCode, string countryName, string longCountryCode) : this(countryCode, countryName, longCountryCode, null) { }
		public Countries(string countryCode, string countryName, string longCountryCode, string numericCode) : this(countryCode, countryName, longCountryCode, numericCode, null) { }
		public Countries(string countryCode, string countryName, string longCountryCode, string numericCode, string currencyCode) {
			this.countryCode = countryCode;
			this.countryName = countryName;
			this.longCountryCode = longCountryCode;
			this.numericCode = numericCode;
			this.currencyCode = currencyCode;
		}


		#region XML Methods

		#region Save XML
		public override string GenerateXML() {
			return "<Countries>\r\n" +
			"	<CountryCode>" + System.Web.HttpUtility.HtmlEncode(countryCode) + "</CountryCode>\r\n" +
			"	<CountryName>" + System.Web.HttpUtility.HtmlEncode(countryName) + "</CountryName>\r\n" +
			"	<LongCountryCode>" + System.Web.HttpUtility.HtmlEncode(longCountryCode) + "</LongCountryCode>\r\n" +
			"	<NumericCode>" + System.Web.HttpUtility.HtmlEncode(numericCode) + "</NumericCode>\r\n" +
			"	<CurrencyCode>" + System.Web.HttpUtility.HtmlEncode(currencyCode) + "</CurrencyCode>\r\n" +
			"</Countries>\r\n";
		}
		#endregion

		#region Load Methods
		// loop through the xml and set the properties and child classes
		public override void Load(XmlNode childNodes) {
			foreach(XmlNode node in childNodes) {
				if(ToLowerCase(node.Name) == ToLowerCase("countryCode")) {
					SetXmlValue(ref countryCode, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("countryName")) {
					SetXmlValue(ref countryName, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("longCountryCode")) {
					SetXmlValue(ref longCountryCode, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("numericCode")) {
					SetXmlValue(ref numericCode, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("currencyCode")) {
					SetXmlValue(ref currencyCode, node.InnerText);
				}
			}
		}
		#endregion

		#endregion

		#region Data Source Methods
		public static Countries[] GetCountriess() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetCountriess();
		}

		/*
		public static Countries GetCountriesByID(int id) {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetCountriesByID(id);
		}

		public int Insert() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.InsertCountries(this);
		}

		public int Update() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.UpdateCountries(this);
		}*/
		#endregion

		#region Properties
		public string CountryCode {
			set { countryCode = value; }
			get { return countryCode; }
		}

		public string CountryName {
			set { countryName = value; }
			get { return countryName; }
		}

		public string LongCountryCode {
			set { longCountryCode = value; }
			get { return longCountryCode; }
		}

		public string NumericCode {
			set { numericCode = value; }
			get { return numericCode; }
		}

		public string CurrencyCode {
			set { currencyCode = value; }
			get { return currencyCode; }
		}

		#endregion
	}
}
