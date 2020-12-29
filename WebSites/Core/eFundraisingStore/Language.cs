using System;
using System.Xml;

namespace GA.BDC.Core.eFundraisingStore {

	public class Language: eFundraisingStoreDataObject {

		private string languageCode;
		private string name;


		public Language() : this(null) { }
		public Language(string languageCode) : this(languageCode, null) { }
		public Language(string languageCode, string name) {
			this.languageCode = languageCode;
			this.name = name;
		}


		#region XML Methods

		#region Save XML
		public override string GenerateXML() {
			return "<Language>\r\n" +
			"	<LanguageCode>" + System.Web.HttpUtility.HtmlEncode(languageCode) + "</LanguageCode>\r\n" +
			"	<Name>" + System.Web.HttpUtility.HtmlEncode(name) + "</Name>\r\n" +
			"</Language>\r\n";
		}
		#endregion

		#region Load Methods
		// loop through the xml and set the properties and child classes
		public override void Load(XmlNode childNodes) {
			foreach(XmlNode node in childNodes) {
				if(node.Name.ToLower() == "languageCode") {
					SetXmlValue(ref languageCode, node.InnerText);
				}
				if(node.Name.ToLower() == "name") {
					SetXmlValue(ref name, node.InnerText);
				}
			}
		}
		#endregion

		#endregion

		#region Data Source Methods
		public static Language[] GetLanguages() {
			DataAccess.eFundraisingStoreDatabase dbo = new DataAccess.eFundraisingStoreDatabase();
			return dbo.GetLanguages();
		}

		/*
		public static Language GetLanguageByID(int id) {
			DataAccess.eFundraisingStoreDatabase dbo = new DataAccess.eFundraisingStoreDatabase();
			return dbo.GetLanguageByID(id);
		}

		public int Insert() {
			DataAccess.eFundraisingStoreDatabase dbo = new DataAccess.eFundraisingStoreDatabase();
			return dbo.InsertLanguage(this);
		}

		public int Update() {
			DataAccess.eFundraisingStoreDatabase dbo = new DataAccess.eFundraisingStoreDatabase();
			return dbo.UpdateLanguage(this);
		}*/
		#endregion

		#region Properties
		public string LanguageCode {
			set { languageCode = value; }
			get { return languageCode; }
		}

		public string Name {
			set { name = value; }
			get { return name; }
		}

		#endregion
	}
}
