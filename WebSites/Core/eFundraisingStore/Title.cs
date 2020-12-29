using System;
using System.Xml;

namespace GA.BDC.Core.eFundraisingStore {

	public class Title: eFundraisingStoreDataObject {

		private short titleId;
		private short partyTypeId;
		private string titleDesc;


		public Title() : this(short.MinValue) { }
		public Title(short titleId) : this(titleId, short.MinValue) { }
		public Title(short titleId, short partyTypeId) : this(titleId, partyTypeId, null) { }
		public Title(short titleId, short partyTypeId, string titleDesc) {
			this.titleId = titleId;
			this.partyTypeId = partyTypeId;
			this.titleDesc = titleDesc;
		}


		#region XML Methods

		#region Save XML
		public override string GenerateXML() {
			return "<Title>\r\n" +
			"	<TitleId>" + titleId + "</TitleId>\r\n" +
			"	<PartyTypeId>" + partyTypeId + "</PartyTypeId>\r\n" +
			"	<TitleDesc>" + System.Web.HttpUtility.HtmlEncode(titleDesc) + "</TitleDesc>\r\n" +
			"</Title>\r\n";
		}
		#endregion

		#region Load Methods
		// loop through the xml and set the properties and child classes
		public override void Load(XmlNode childNodes) {
			foreach(XmlNode node in childNodes) {
				if(node.Name.ToLower() == "titleId") {
					SetXmlValue(ref titleId, node.InnerText);
				}
				if(node.Name.ToLower() == "partyTypeId") {
					SetXmlValue(ref partyTypeId, node.InnerText);
				}
				if(node.Name.ToLower() == "titleDesc") {
					SetXmlValue(ref titleDesc, node.InnerText);
				}
			}
		}
		#endregion

		#endregion

		#region Data Source Methods
		public static Title[] GetTitles() {
			DataAccess.eFundraisingStoreDatabase dbo = new DataAccess.eFundraisingStoreDatabase();
			return dbo.GetTitles();
		}

		public static Title GetTitleByID(int id) {
			DataAccess.eFundraisingStoreDatabase dbo = new DataAccess.eFundraisingStoreDatabase();
			return dbo.GetTitleByID(id);
		}

		public int Insert() {
			DataAccess.eFundraisingStoreDatabase dbo = new DataAccess.eFundraisingStoreDatabase();
			return dbo.InsertTitle(this);
		}

		public int Update() {
			DataAccess.eFundraisingStoreDatabase dbo = new DataAccess.eFundraisingStoreDatabase();
			return dbo.UpdateTitle(this);
		}
		#endregion

		#region Properties
		public short TitleId {
			set { titleId = value; }
			get { return titleId; }
		}

		public short PartyTypeId {
			set { partyTypeId = value; }
			get { return partyTypeId; }
		}

		public string TitleDesc {
			set { titleDesc = value; }
			get { return titleDesc; }
		}

		#endregion
	}
}
