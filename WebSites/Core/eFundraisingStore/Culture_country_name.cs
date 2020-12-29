using System;
using System.Xml;

namespace GA.BDC.Core.eFundraisingStore {

	public class CultureCountryName: eFundraisingStoreDataObject {

		private string cultureCode;
		private string countryCode;
		private string countryName;


		public CultureCountryName() : this(null) { }
		public CultureCountryName(string cultureCode) : this(cultureCode, null) { }
		public CultureCountryName(string cultureCode, string countryCode) : this(cultureCode, countryCode, null) { }
		public CultureCountryName(string cultureCode, string countryCode, string countryName) {
			this.cultureCode = cultureCode;
			this.countryCode = countryCode;
			this.countryName = countryName;
		}


		#region XML Methods

		#region Save XML
		public override string GenerateXML() {
			return "<CultureCountryName>\r\n" +
			"	<CultureCode>" + cultureCode + "</CultureCode>\r\n" +
			"	<CountryCode>" + countryCode + "</CountryCode>\r\n" +
			"	<CountryName>" + countryName + "</CountryName>\r\n" +
			"</CultureCountryName>\r\n";
		}
		#endregion

		#region Load Methods
		// loop through the xml and set the properties and child classes
		public override void Load(XmlNode childNodes) {
			foreach(XmlNode node in childNodes) {
				if(node.Name.ToLower() == "cultureCode") {
					SetXmlValue(ref cultureCode, node.InnerText);
				}
				if(node.Name.ToLower() == "countryCode") {
					SetXmlValue(ref countryCode, node.InnerText);
				}
				if(node.Name.ToLower() == "countryName") {
					SetXmlValue(ref countryName, node.InnerText);
				}
			}
		}
		#endregion

		#endregion

		#region Data Source Methods
		/*
		public static CultureCountryName[] GetCultureCountryNames() {
			DataAccess.eFundraisingStoreDatabase dbo = new DataAccess.eFundraisingStoreDatabase();
			return dbo.GetCultureCountryNames();
		}*/

		#endregion

		#region Properties
		public string CultureCode {
			set { cultureCode = value; }
			get { return cultureCode; }
		}

		public string CountryCode {
			set { countryCode = value; }
			get { return countryCode; }
		}

		public string CountryName {
			set { countryName = value; }
			get { return countryName; }
		}

		#endregion
	}
}
