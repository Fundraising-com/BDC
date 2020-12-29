using System;
using System.Xml;

namespace GA.BDC.Core.eFundraisingStore {

	public class HearAboutUs: eFundraisingStoreDataObject {

		private short hearAboutUsId;
		private short partyTypeId;
		private string name;
		private short orderOnWeb;
		private short isActive;


		public HearAboutUs() : this(short.MinValue) { }
		public HearAboutUs(short hearAboutUsId) : this(hearAboutUsId, short.MinValue) { }
		public HearAboutUs(short hearAboutUsId, short partyTypeId) : this(hearAboutUsId, partyTypeId, null) { }
		public HearAboutUs(short hearAboutUsId, short partyTypeId, string name) : this(hearAboutUsId, partyTypeId, name, short.MinValue) { }
		public HearAboutUs(short hearAboutUsId, short partyTypeId, string name, short orderOnWeb) : this(hearAboutUsId, partyTypeId, name, orderOnWeb, short.MinValue) { }
		public HearAboutUs(short hearAboutUsId, short partyTypeId, string name, short orderOnWeb, short isActive) {
			this.hearAboutUsId = hearAboutUsId;
			this.partyTypeId = partyTypeId;
			this.name = name;
			this.orderOnWeb = orderOnWeb;
			this.isActive = isActive;
		}


		#region XML Methods

		#region Save XML
		public override string GenerateXML() {
			return "<HearAboutUs>\r\n" +
			"	<HearAboutUsId>" + hearAboutUsId + "</HearAboutUsId>\r\n" +
			"	<PartyTypeId>" + partyTypeId + "</PartyTypeId>\r\n" +
			"	<Name>" + System.Web.HttpUtility.HtmlEncode(name) + "</Name>\r\n" +
			"	<OrderOnWeb>" + orderOnWeb + "</OrderOnWeb>\r\n" +
			"	<IsActive>" + isActive + "</IsActive>\r\n" +
			"</HearAboutUs>\r\n";
		}
		#endregion

		#region Load Methods
		// loop through the xml and set the properties and child classes
		public override void Load(XmlNode childNodes) {
			foreach(XmlNode node in childNodes) {
				if(node.Name.ToLower() == "hearAboutUsId") {
					SetXmlValue(ref hearAboutUsId, node.InnerText);
				}
				if(node.Name.ToLower() == "partyTypeId") {
					SetXmlValue(ref partyTypeId, node.InnerText);
				}
				if(node.Name.ToLower() == "name") {
					SetXmlValue(ref name, node.InnerText);
				}
				if(node.Name.ToLower() == "orderOnWeb") {
					SetXmlValue(ref orderOnWeb, node.InnerText);
				}
				if(node.Name.ToLower() == "isActive") {
					SetXmlValue(ref isActive, node.InnerText);
				}
			}
		}
		#endregion

		#endregion

		#region Data Source Methods
		public static HearAboutUs[] GetHearAboutUss() {
			DataAccess.eFundraisingStoreDatabase dbo = new DataAccess.eFundraisingStoreDatabase();
			return dbo.GetHearAboutUss();
		}

		public static HearAboutUs GetHearAboutUsByID(int id) {
			DataAccess.eFundraisingStoreDatabase dbo = new DataAccess.eFundraisingStoreDatabase();
			return dbo.GetHearAboutUsByID(id);
		}

		public int Insert() {
			DataAccess.eFundraisingStoreDatabase dbo = new DataAccess.eFundraisingStoreDatabase();
			return dbo.InsertHearAboutUs(this);
		}

		public int Update() {
			DataAccess.eFundraisingStoreDatabase dbo = new DataAccess.eFundraisingStoreDatabase();
			return dbo.UpdateHearAboutUs(this);
		}
		#endregion

		#region Properties
		public short HearAboutUsId {
			set { hearAboutUsId = value; }
			get { return hearAboutUsId; }
		}

		public short PartyTypeId {
			set { partyTypeId = value; }
			get { return partyTypeId; }
		}

		public string Name {
			set { name = value; }
			get { return name; }
		}

		public short OrderOnWeb {
			set { orderOnWeb = value; }
			get { return orderOnWeb; }
		}

		public short IsActive {
			set { isActive = value; }
			get { return isActive; }
		}

		#endregion
	}
}
