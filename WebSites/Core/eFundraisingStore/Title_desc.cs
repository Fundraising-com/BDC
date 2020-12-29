using System;
using System.Xml;

namespace GA.BDC.Core.eFundraisingStore {

	public class TitleDesc: eFundraisingStoreDataObject {

		private short titleId;
		private string cultureCode;
		private string description;


		public TitleDesc() : this(short.MinValue) { }
		public TitleDesc(short titleId) : this(titleId, null) { }
		public TitleDesc(short titleId, string cultureCode) : this(titleId, cultureCode, null) { }
		public TitleDesc(short titleId, string cultureCode, string description) {
			this.titleId = titleId;
			this.cultureCode = cultureCode;
			this.description = description;
		}


		#region XML Methods

		#region Save XML
		public override string GenerateXML() {
			return "<TitleDesc>\r\n" +
			"	<TitleId>" + titleId + "</TitleId>\r\n" +
			"	<CultureCode>" + System.Web.HttpUtility.HtmlEncode(cultureCode) + "</CultureCode>\r\n" +
			"	<Description>" + System.Web.HttpUtility.HtmlEncode(description) + "</Description>\r\n" +
			"</TitleDesc>\r\n";
		}
		#endregion

		#region Load Methods
		// loop through the xml and set the properties and child classes
		public override void Load(XmlNode childNodes) {
			foreach(XmlNode node in childNodes) {
				if(node.Name.ToLower() == "titleId") {
					SetXmlValue(ref titleId, node.InnerText);
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
		public static TitleDesc[] GetTitleDescs() {
			DataAccess.eFundraisingStoreDatabase dbo = new DataAccess.eFundraisingStoreDatabase();
			return dbo.GetTitleDescs();
		}

		public static TitleDesc GetTitleDescByID(int id) {
			DataAccess.eFundraisingStoreDatabase dbo = new DataAccess.eFundraisingStoreDatabase();
			return dbo.GetTitleDescByID(id);
		}

		public int Insert() {
			DataAccess.eFundraisingStoreDatabase dbo = new DataAccess.eFundraisingStoreDatabase();
			return dbo.InsertTitleDesc(this);
		}

		public int Update() {
			DataAccess.eFundraisingStoreDatabase dbo = new DataAccess.eFundraisingStoreDatabase();
			return dbo.UpdateTitleDesc(this);
		}
		#endregion

		#region Properties
		public short TitleId {
			set { titleId = value; }
			get { return titleId; }
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
