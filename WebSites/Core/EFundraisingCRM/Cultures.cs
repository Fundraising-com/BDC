using System;
using System.Xml;

namespace GA.BDC.Core.EFundraisingCRM {

	public class Cultures: EFundraisingCRMDataObject {

		private short cultureId;
		private short languageId;
		private string countryCode;
		private string cultureName;
		private string displayName;
		private string cultureCode;
		private string isoCode;


		public Cultures() : this(short.MinValue) { }
		public Cultures(short cultureId) : this(cultureId, short.MinValue) { }
		public Cultures(short cultureId, short languageId) : this(cultureId, languageId, null) { }
		public Cultures(short cultureId, short languageId, string countryCode) : this(cultureId, languageId, countryCode, null) { }
		public Cultures(short cultureId, short languageId, string countryCode, string cultureName) : this(cultureId, languageId, countryCode, cultureName, null) { }
		public Cultures(short cultureId, short languageId, string countryCode, string cultureName, string displayName) : this(cultureId, languageId, countryCode, cultureName, displayName, null) { }
		public Cultures(short cultureId, short languageId, string countryCode, string cultureName, string displayName, string cultureCode) : this(cultureId, languageId, countryCode, cultureName, displayName, cultureCode, null) { }
		public Cultures(short cultureId, short languageId, string countryCode, string cultureName, string displayName, string cultureCode, string isoCode) {
			this.cultureId = cultureId;
			this.languageId = languageId;
			this.countryCode = countryCode;
			this.cultureName = cultureName;
			this.displayName = displayName;
			this.cultureCode = cultureCode;
			this.isoCode = isoCode;
		}


		#region XML Methods

		#region Save XML
		public override string GenerateXML() {
			return "<Cultures>\r\n" +
			"	<CultureId>" + cultureId + "</CultureId>\r\n" +
			"	<LanguageId>" + languageId + "</LanguageId>\r\n" +
			"	<CountryCode>" + System.Web.HttpUtility.HtmlEncode(countryCode) + "</CountryCode>\r\n" +
			"	<CultureName>" + System.Web.HttpUtility.HtmlEncode(cultureName) + "</CultureName>\r\n" +
			"	<DisplayName>" + System.Web.HttpUtility.HtmlEncode(displayName) + "</DisplayName>\r\n" +
			"	<CultureCode>" + System.Web.HttpUtility.HtmlEncode(cultureCode) + "</CultureCode>\r\n" +
			"	<IsoCode>" + System.Web.HttpUtility.HtmlEncode(isoCode) + "</IsoCode>\r\n" +
			"</Cultures>\r\n";
		}
		#endregion

		#region Load Methods
		// loop through the xml and set the properties and child classes
		public override void Load(XmlNode childNodes) {
			foreach(XmlNode node in childNodes) {
				if(ToLowerCase(node.Name) == ToLowerCase("cultureId")) {
					SetXmlValue(ref cultureId, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("languageId")) {
					SetXmlValue(ref languageId, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("countryCode")) {
					SetXmlValue(ref countryCode, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("cultureName")) {
					SetXmlValue(ref cultureName, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("displayName")) {
					SetXmlValue(ref displayName, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("cultureCode")) {
					SetXmlValue(ref cultureCode, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("isoCode")) {
					SetXmlValue(ref isoCode, node.InnerText);
				}
			}
		}
		#endregion

		#endregion

		#region Data Source Methods
		public static Cultures[] GetCulturess() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetCulturess();
		}

		/*
		public static Cultures GetCulturesByID(int id) {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetCulturesByID(id);
		}

		public int Insert() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.InsertCultures(this);
		}

		public int Update() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.UpdateCultures(this);
		}*/
		#endregion

		#region Properties
		public short CultureId {
			set { cultureId = value; }
			get { return cultureId; }
		}

		public short LanguageId {
			set { languageId = value; }
			get { return languageId; }
		}

		public string CountryCode {
			set { countryCode = value; }
			get { return countryCode; }
		}

		public string CultureName {
			set { cultureName = value; }
			get { return cultureName; }
		}

		public string DisplayName {
			set { displayName = value; }
			get { return displayName; }
		}

		public string CultureCode {
			set { cultureCode = value; }
			get { return cultureCode; }
		}

		public string IsoCode {
			set { isoCode = value; }
			get { return isoCode; }
		}

		#endregion
	}
}
