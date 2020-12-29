using System;
using System.Xml;

namespace GA.BDC.Core.EFundraisingCRM {

	public class CountryNames: EFundraisingCRMDataObject {

		private string countryCode;
		private short languageId;
		private string countryName;


		public CountryNames() : this(null) { }
		public CountryNames(string countryCode) : this(countryCode, short.MinValue) { }
		public CountryNames(string countryCode, short languageId) : this(countryCode, languageId, null) { }
		public CountryNames(string countryCode, short languageId, string countryName) {
			this.countryCode = countryCode;
			this.languageId = languageId;
			this.countryName = countryName;
		}


		#region XML Methods

		#region Save XML
		public override string GenerateXML() {
			return "<CountryNames>\r\n" +
			"	<CountryCode>" + System.Web.HttpUtility.HtmlEncode(countryCode) + "</CountryCode>\r\n" +
			"	<LanguageId>" + languageId + "</LanguageId>\r\n" +
			"	<CountryName>" + System.Web.HttpUtility.HtmlEncode(countryName) + "</CountryName>\r\n" +
			"</CountryNames>\r\n";
		}
		#endregion

		#region Load Methods
		// loop through the xml and set the properties and child classes
		public override void Load(XmlNode childNodes) {
			foreach(XmlNode node in childNodes) {
				if(ToLowerCase(node.Name) == ToLowerCase("countryCode")) {
					SetXmlValue(ref countryCode, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("languageId")) {
					SetXmlValue(ref languageId, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("countryName")) {
					SetXmlValue(ref countryName, node.InnerText);
				}
			}
		}
		#endregion

		#endregion

		#region Data Source Methods
		public static CountryNames[] GetCountryNamess() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetCountryNamess();
		}

		/*
		public static CountryNames GetCountryNamesByID(int id) {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetCountryNamesByID(id);
		}

		public int Insert() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.InsertCountryNames(this);
		}

		public int Update() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.UpdateCountryNames(this);
		}*/
		#endregion

		#region Properties
		public string CountryCode {
			set { countryCode = value; }
			get { return countryCode; }
		}

		public short LanguageId {
			set { languageId = value; }
			get { return languageId; }
		}

		public string CountryName {
			set { countryName = value; }
			get { return countryName; }
		}

		#endregion
	}
}
