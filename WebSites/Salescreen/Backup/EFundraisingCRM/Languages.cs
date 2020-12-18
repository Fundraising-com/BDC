using System;
using System.Xml;

namespace efundraising.EFundraisingCRM {

	public class Languages: EFundraisingCRMDataObject {

		private short languageId;
		private string languageName;
		private string longLanguageCode;
		private string shortLanguageCode;


		public Languages() : this(short.MinValue) { }
		public Languages(short languageId) : this(languageId, null) { }
		public Languages(short languageId, string languageName) : this(languageId, languageName, null) { }
		public Languages(short languageId, string languageName, string longLanguageCode) : this(languageId, languageName, longLanguageCode, null) { }
		public Languages(short languageId, string languageName, string longLanguageCode, string shortLanguageCode) {
			this.languageId = languageId;
			this.languageName = languageName;
			this.longLanguageCode = longLanguageCode;
			this.shortLanguageCode = shortLanguageCode;
		}


		#region XML Methods

		#region Save XML
		public override string GenerateXML() {
			return "<Languages>\r\n" +
			"	<LanguageId>" + languageId + "</LanguageId>\r\n" +
			"	<LanguageName>" + System.Web.HttpUtility.HtmlEncode(languageName) + "</LanguageName>\r\n" +
			"	<LongLanguageCode>" + System.Web.HttpUtility.HtmlEncode(longLanguageCode) + "</LongLanguageCode>\r\n" +
			"	<ShortLanguageCode>" + System.Web.HttpUtility.HtmlEncode(shortLanguageCode) + "</ShortLanguageCode>\r\n" +
			"</Languages>\r\n";
		}
		#endregion

		#region Load Methods
		// loop through the xml and set the properties and child classes
		public override void Load(XmlNode childNodes) {
			foreach(XmlNode node in childNodes) {
				if(ToLowerCase(node.Name) == ToLowerCase("languageId")) {
					SetXmlValue(ref languageId, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("languageName")) {
					SetXmlValue(ref languageName, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("longLanguageCode")) {
					SetXmlValue(ref longLanguageCode, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("shortLanguageCode")) {
					SetXmlValue(ref shortLanguageCode, node.InnerText);
				}
			}
		}
		#endregion

		#endregion

		#region Data Source Methods
		public static Languages[] GetLanguagess() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetLanguagess();
		}

		/*
		public static Languages GetLanguagesByID(int id) {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetLanguagesByID(id);
		}

		public int Insert() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.InsertLanguages(this);
		}

		public int Update() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.UpdateLanguages(this);
		}*/
		#endregion

		#region Properties
		public short LanguageId {
			set { languageId = value; }
			get { return languageId; }
		}

		public string LanguageName {
			set { languageName = value; }
			get { return languageName; }
		}

		public string LongLanguageCode {
			set { longLanguageCode = value; }
			get { return longLanguageCode; }
		}

		public string ShortLanguageCode {
			set { shortLanguageCode = value; }
			get { return shortLanguageCode; }
		}

		#endregion
	}
}
