using System;
using System.Xml;

namespace GA.BDC.Core.EFundraisingCRM {

	public class LanguageDesc: EFundraisingCRMDataObject {

		private short languageId;
		private short displayLanguageId;
		private string languageName;


		public LanguageDesc() : this(short.MinValue) { }
		public LanguageDesc(short languageId) : this(languageId, short.MinValue) { }
		public LanguageDesc(short languageId, short displayLanguageId) : this(languageId, displayLanguageId, null) { }
		public LanguageDesc(short languageId, short displayLanguageId, string languageName) {
			this.languageId = languageId;
			this.displayLanguageId = displayLanguageId;
			this.languageName = languageName;
		}


		#region XML Methods

		#region Save XML
		public override string GenerateXML() {
			return "<LanguageDesc>\r\n" +
			"	<LanguageId>" + languageId + "</LanguageId>\r\n" +
			"	<DisplayLanguageId>" + displayLanguageId + "</DisplayLanguageId>\r\n" +
			"	<LanguageName>" + System.Web.HttpUtility.HtmlEncode(languageName) + "</LanguageName>\r\n" +
			"</LanguageDesc>\r\n";
		}
		#endregion

		#region Load Methods
		// loop through the xml and set the properties and child classes
		public override void Load(XmlNode childNodes) {
			foreach(XmlNode node in childNodes) {
				if(ToLowerCase(node.Name) == ToLowerCase("languageId")) {
					SetXmlValue(ref languageId, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("displayLanguageId")) {
					SetXmlValue(ref displayLanguageId, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("languageName")) {
					SetXmlValue(ref languageName, node.InnerText);
				}
			}
		}
		#endregion

		#endregion

		#region Data Source Methods
		public static LanguageDesc[] GetLanguageDescs() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetLanguageDescs();
		}

		/*
		public static LanguageDesc GetLanguageDescByID(int id) {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetLanguageDescByID(id);
		}

		public int Insert() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.InsertLanguageDesc(this);
		}

		public int Update() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.UpdateLanguageDesc(this);
		}*/
		#endregion

		#region Properties
		public short LanguageId {
			set { languageId = value; }
			get { return languageId; }
		}

		public short DisplayLanguageId {
			set { displayLanguageId = value; }
			get { return displayLanguageId; }
		}

		public string LanguageName {
			set { languageName = value; }
			get { return languageName; }
		}

		#endregion
	}
}
