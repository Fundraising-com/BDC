using System;
using System.Xml;

namespace GA.BDC.Core.eFundraisingStore {

	public class Culture: eFundraisingStoreDataObject {

		private string cultureCode;
		private string countryCode;
		private string languageCode;
		private string cultureName;


		public Culture() : this(null) { }
		public Culture(string cultureCode) : this(cultureCode, null) { }
		public Culture(string cultureCode, string countryCode) : this(cultureCode, countryCode, null) { }
		public Culture(string cultureCode, string countryCode, string languageCode) : this(cultureCode, countryCode, languageCode, null) { }
		public Culture(string cultureCode, string countryCode, string languageCode, string cultureName) {
			this.cultureCode = cultureCode;
			this.countryCode = countryCode;
			this.languageCode = languageCode;
			this.cultureName = cultureName;
		}


		#region XML Methods

		#region Save XML
		public override string GenerateXML() {
			return "<Culture>\r\n" +
			"	<CultureCode>" + System.Web.HttpUtility.HtmlEncode(cultureCode) + "</CultureCode>\r\n" +
			"	<CountryCode>" + System.Web.HttpUtility.HtmlEncode(countryCode) + "</CountryCode>\r\n" +
			"	<LanguageCode>" + System.Web.HttpUtility.HtmlEncode(languageCode) + "</LanguageCode>\r\n" +
			"	<CultureName>" + System.Web.HttpUtility.HtmlEncode(cultureName) + "</CultureName>\r\n" +
			"</Culture>\r\n";
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
				if(node.Name.ToLower() == "languageCode") {
					SetXmlValue(ref languageCode, node.InnerText);
				}
				if(node.Name.ToLower() == "cultureName") {
					SetXmlValue(ref cultureName, node.InnerText);
				}
			}
		}
		#endregion

		#endregion

		#region Data Source Methods
		public static Culture[] GetCultures() {
			DataAccess.eFundraisingStoreDatabase dbo = new DataAccess.eFundraisingStoreDatabase();
			return dbo.GetCultures();
		}

		/*
		public static Culture GetCultureByID(int id) {
			DataAccess.eFundraisingStoreDatabase dbo = new DataAccess.eFundraisingStoreDatabase();
			return dbo.GetCultureByID(id);
		}

		public int Insert() {
			DataAccess.eFundraisingStoreDatabase dbo = new DataAccess.eFundraisingStoreDatabase();
			return dbo.InsertCulture(this);
		}

		public int Update() {
			DataAccess.eFundraisingStoreDatabase dbo = new DataAccess.eFundraisingStoreDatabase();
			return dbo.UpdateCulture(this);
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

		public string LanguageCode {
			set { languageCode = value; }
			get { return languageCode; }
		}

		public string CultureName {
			set { cultureName = value; }
			get { return cultureName; }
		}

		#endregion
	}
}
