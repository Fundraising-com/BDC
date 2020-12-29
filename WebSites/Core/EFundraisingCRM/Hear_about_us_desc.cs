using System;
using System.Xml;

namespace GA.BDC.Core.EFundraisingCRM {

	public class HearAboutUsDesc: EFundraisingCRMDataObject {

		private short hearId;
		private short languageId;
		private string description;


		public HearAboutUsDesc() : this(short.MinValue) { }
		public HearAboutUsDesc(short hearId) : this(hearId, short.MinValue) { }
		public HearAboutUsDesc(short hearId, short languageId) : this(hearId, languageId, null) { }
		public HearAboutUsDesc(short hearId, short languageId, string description) {
			this.hearId = hearId;
			this.languageId = languageId;
			this.description = description;
		}


		#region XML Methods

		#region Save XML
		public override string GenerateXML() {
			return "<HearAboutUsDesc>\r\n" +
			"	<HearId>" + hearId + "</HearId>\r\n" +
			"	<LanguageId>" + languageId + "</LanguageId>\r\n" +
			"	<Description>" + System.Web.HttpUtility.HtmlEncode(description) + "</Description>\r\n" +
			"</HearAboutUsDesc>\r\n";
		}
		#endregion

		#region Load Methods
		// loop through the xml and set the properties and child classes
		public override void Load(XmlNode childNodes) {
			foreach(XmlNode node in childNodes) {
				if(ToLowerCase(node.Name) == ToLowerCase("hearId")) {
					SetXmlValue(ref hearId, node.InnerText);
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
		public static HearAboutUsDesc[] GetHearAboutUsDescs() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetHearAboutUsDescs();
		}

		/*
		public static HearAboutUsDesc GetHearAboutUsDescByID(int id) {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetHearAboutUsDescByID(id);
		}

		public int Insert() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.InsertHearAboutUsDesc(this);
		}

		public int Update() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.UpdateHearAboutUsDesc(this);
		}*/
		#endregion

		#region Properties
		public short HearId {
			set { hearId = value; }
			get { return hearId; }
		}

		public short LanguageId {
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
