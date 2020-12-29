using System;
using System.Xml;

namespace GA.BDC.Core.EFundraisingCRM {
	/*
	 * Tile or role description by language of the client and/or lead.
	 * 
	 */

	public class TitleDesc: EFundraisingCRMDataObject {

		private int titleId;
		private int languageId;
		private string description;


		public TitleDesc() : this(int.MinValue) { }
		public TitleDesc(int titleId) : this(titleId, int.MinValue) { }
		public TitleDesc(int titleId, int languageId) : this(titleId, languageId, null) { }
		public TitleDesc(int titleId, int languageId, string description) {
			this.titleId = titleId;
			this.languageId = languageId;
			this.description = description;
		}


		#region XML Methods

		#region Save XML
		public override string GenerateXML() {
			return "<TitleDesc>\r\n" +
			"	<TitleId>" + titleId + "</TitleId>\r\n" +
			"	<LanguageId>" + languageId + "</LanguageId>\r\n" +
			"	<Description>" + System.Web.HttpUtility.HtmlEncode(description) + "</Description>\r\n" +
			"</TitleDesc>\r\n";
		}
		#endregion

		#region Load Methods
		// loop through the xml and set the properties and child classes
		public override void Load(XmlNode childNodes) {
			foreach(XmlNode node in childNodes) {
				if(ToLowerCase(node.Name) == ToLowerCase("titleId")) {
					SetXmlValue(ref titleId, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("languageId")) {
					SetXmlValue(ref languageId, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("description")) {
					SetXmlValue(ref description, node.InnerText);
				}
			}
		}
		#endregion

		#endregion

		#region Data Source Methods
		public static TitleDesc[] GetTitleDescs() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetTitleDescs();
		}

		/*
		public static TitleDesc GetTitleDescByID(int id) {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetTitleDescByID(id);
		}

		public int Insert() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.InsertTitleDesc(this);
		}

		public int Update() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.UpdateTitleDesc(this);
		}*/
		#endregion

		#region Properties
		public int TitleId {
			set { titleId = value; }
			get { return titleId; }
		}

		public int LanguageId {
			set { languageId = value; }
			get { return languageId; }
		}

		public string Description {
			set { description = value; }
			get { return description; }
		}

		#endregion
	}
}
