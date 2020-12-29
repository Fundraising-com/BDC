using System;
using System.Xml;

namespace GA.BDC.Core.eFundraisingStore {

	public class HearAboutUsDesc: eFundraisingStoreDataObject {

		private short hearAboutUsId;
		private string cultureCode;
		private string description;


		public HearAboutUsDesc() : this(short.MinValue) { }
		public HearAboutUsDesc(short hearAboutUsId) : this(hearAboutUsId, null) { }
		public HearAboutUsDesc(short hearAboutUsId, string cultureCode) : this(hearAboutUsId, cultureCode, null) { }
		public HearAboutUsDesc(short hearAboutUsId, string cultureCode, string description) {
			this.hearAboutUsId = hearAboutUsId;
			this.cultureCode = cultureCode;
			this.description = description;
		}


		#region XML Methods

		#region Save XML
		public override string GenerateXML() {
			return "<HearAboutUsDesc>\r\n" +
			"	<HearAboutUsId>" + hearAboutUsId + "</HearAboutUsId>\r\n" +
			"	<CultureCode>" + System.Web.HttpUtility.HtmlEncode(cultureCode) + "</CultureCode>\r\n" +
			"	<Description>" + System.Web.HttpUtility.HtmlEncode(description) + "</Description>\r\n" +
			"</HearAboutUsDesc>\r\n";
		}
		#endregion

		#region Load Methods
		// loop through the xml and set the properties and child classes
		public override void Load(XmlNode childNodes) {
			foreach(XmlNode node in childNodes) {
				if(node.Name.ToLower() == "hearAboutUsId") {
					SetXmlValue(ref hearAboutUsId, node.InnerText);
				}
				if(node.Name.ToLower() == "cultureCode") {
					SetXmlValue(ref cultureCode, node.InnerText);
				}
				if(node.Name.ToLower() == "description") {
					SetXmlValue(ref description, node.InnerText);
				}
			}
		}
		#endregion

		#endregion

		#region Data Source Methods
		public static HearAboutUsDesc[] GetHearAboutUsDescs() {
			DataAccess.eFundraisingStoreDatabase dbo = new DataAccess.eFundraisingStoreDatabase();
			return dbo.GetHearAboutUsDescs();
		}

		public static HearAboutUsDesc GetHearAboutUsDescByID(int id) {
			DataAccess.eFundraisingStoreDatabase dbo = new DataAccess.eFundraisingStoreDatabase();
			return dbo.GetHearAboutUsDescByID(id);
		}

		public int Insert() {
			DataAccess.eFundraisingStoreDatabase dbo = new DataAccess.eFundraisingStoreDatabase();
			return dbo.InsertHearAboutUsDesc(this);
		}

		public int Update() {
			DataAccess.eFundraisingStoreDatabase dbo = new DataAccess.eFundraisingStoreDatabase();
			return dbo.UpdateHearAboutUsDesc(this);
		}
		#endregion

		#region Properties
		public short HearAboutUsId {
			set { hearAboutUsId = value; }
			get { return hearAboutUsId; }
		}

		public string CultureCode {
			set { cultureCode = value; }
			get { return cultureCode; }
		}

		public string Description {
			set { description = value; }
			get { return description; }
		}

		#endregion
	}
}
