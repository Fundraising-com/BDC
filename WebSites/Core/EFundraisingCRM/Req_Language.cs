using System;
using System.Xml;

namespace GA.BDC.Core.EFundraisingCRM {

	public class ReqLanguage: EFundraisingCRMDataObject {

		private int languageId;
		private string language;


		public ReqLanguage() : this(int.MinValue) { }
		public ReqLanguage(int languageId) : this(languageId, null) { }
		public ReqLanguage(int languageId, string language) {
			this.languageId = languageId;
			this.language = language;
		}


		#region XML Methods

		#region Save XML
		public override string GenerateXML() {
			return "<ReqLanguage>\r\n" +
			"	<LanguageId>" + languageId + "</LanguageId>\r\n" +
			"	<Language>" + System.Web.HttpUtility.HtmlEncode(language) + "</Language>\r\n" +
			"</ReqLanguage>\r\n";
		}
		#endregion

		#region Load Methods
		// loop through the xml and set the properties and child classes
		public override void Load(XmlNode childNodes) {
			foreach(XmlNode node in childNodes) {
				if(ToLowerCase(node.Name) == ToLowerCase("languageId")) {
					SetXmlValue(ref languageId, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("language")) {
					SetXmlValue(ref language, node.InnerText);
				}
			}
		}
		#endregion

		#endregion

		#region Data Source Methods
		public static ReqLanguage[] GetReqLanguages() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetReqLanguages();
		}

		public static ReqLanguage GetReqLanguageByID(int id) {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetReqLanguageByID(id);
		}

		public int Insert() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.InsertReqLanguage(this);
		}

		public int Update() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.UpdateReqLanguage(this);
		}
		#endregion

		#region Properties
		public int LanguageId {
			set { languageId = value; }
			get { return languageId; }
		}

		public string Language {
			set { language = value; }
			get { return language; }
		}

		#endregion
	}
}
