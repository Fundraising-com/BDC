using System;
using System.Xml;

namespace efundraising.EFundraisingCRM {

	public class AliasCountryCode: EFundraisingCRMDataObject {

		private string inputCountryCode;
		private string countryCode;


		public AliasCountryCode() : this(null) { }
		public AliasCountryCode(string inputCountryCode) : this(inputCountryCode, null) { }
		public AliasCountryCode(string inputCountryCode, string countryCode) {
			this.inputCountryCode = inputCountryCode;
			this.countryCode = countryCode;
		}


		#region XML Methods

		#region Save XML
		public override string GenerateXML() {
			return "<AliasCountryCode>\r\n" +
			"	<InputCountryCode>" + System.Web.HttpUtility.HtmlEncode(inputCountryCode) + "</InputCountryCode>\r\n" +
			"	<CountryCode>" + System.Web.HttpUtility.HtmlEncode(countryCode) + "</CountryCode>\r\n" +
			"</AliasCountryCode>\r\n";
		}
		#endregion

		#region Load Methods
		// loop through the xml and set the properties and child classes
		public override void Load(XmlNode childNodes) {
			foreach(XmlNode node in childNodes) {
				if(ToLowerCase(node.Name) == ToLowerCase("inputCountryCode")) {
					SetXmlValue(ref inputCountryCode, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("countryCode")) {
					SetXmlValue(ref countryCode, node.InnerText);
				}
			}
		}
		#endregion

		#endregion

		#region Data Source Methods
		public static AliasCountryCode[] GetAliasCountryCodes() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetAliasCountryCodes();
		}

		/*
		public static AliasCountryCode GetAliasCountryCodeByID(int id) {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetAliasCountryCodeByID(id);
		}

		public int Insert() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.InsertAliasCountryCode(this);
		}

		public int Update() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.UpdateAliasCountryCode(this);
		}*/
		#endregion

		#region Properties
		public string InputCountryCode {
			set { inputCountryCode = value; }
			get { return inputCountryCode; }
		}

		public string CountryCode {
			set { countryCode = value; }
			get { return countryCode; }
		}

		#endregion
	}
}
