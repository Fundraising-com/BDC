using System;
using System.Xml;

namespace GA.BDC.Core.eFundraisingStore {

	public class CultureCountry: eFundraisingStoreDataObject {

		private string cultureCode;
		private string countryCode;
		private string name;


		public CultureCountry() : this(null) { }
		public CultureCountry(string cultureCode) : this(cultureCode, null) { }
		public CultureCountry(string cultureCode, string countryCode) : this(cultureCode, countryCode, null) { }
		public CultureCountry(string cultureCode, string countryCode, string name) {
			this.cultureCode = cultureCode;
			this.countryCode = countryCode;
			this.name = name;
		}


		#region XML Methods

		#region Save XML
		public override string GenerateXML() {
			return "<CultureCountry>\r\n" +
			"	<CultureCode>" + System.Web.HttpUtility.HtmlEncode(cultureCode) + "</CultureCode>\r\n" +
			"	<CountryCode>" + System.Web.HttpUtility.HtmlEncode(countryCode) + "</CountryCode>\r\n" +
			"	<Name>" + System.Web.HttpUtility.HtmlEncode(name) + "</Name>\r\n" +
			"</CultureCountry>\r\n";
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
				if(node.Name.ToLower() == "name") {
					SetXmlValue(ref name, node.InnerText);
				}
			}
		}
		#endregion

		#endregion

		#region Data Source Methods
		public static CultureCountry[] GetCultureCountrys() {
			DataAccess.eFundraisingStoreDatabase dbo = new DataAccess.eFundraisingStoreDatabase();
			return dbo.GetCultureCountrys();
		}

		/*
		public static CultureCountry GetCultureCountryByID(int id) {
			DataAccess.eFundraisingStoreDatabase dbo = new DataAccess.eFundraisingStoreDatabase();
			return dbo.GetCultureCountryByID(id);
		}

		public int Insert() {
			DataAccess.eFundraisingStoreDatabase dbo = new DataAccess.eFundraisingStoreDatabase();
			return dbo.InsertCultureCountry(this);
		}

		public int Update() {
			DataAccess.eFundraisingStoreDatabase dbo = new DataAccess.eFundraisingStoreDatabase();
			return dbo.UpdateCultureCountry(this);
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

		public string Name {
			set { name = value; }
			get { return name; }
		}

		#endregion
	}
}
