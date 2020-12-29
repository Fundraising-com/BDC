using System;
using System.Xml;

namespace GA.BDC.Core.EFundraisingCRM {

	public class HearAboutUs: EFundraisingCRMDataObject {

		private short hearId;
		private short partyTypeId;
		private string name;
		private short orderOnWeb;
		private int isActive;


		public HearAboutUs() : this(short.MinValue) { }
		public HearAboutUs(short hearId) : this(hearId, short.MinValue) { }
		public HearAboutUs(short hearId, short partyTypeId) : this(hearId, partyTypeId, null) { }
		public HearAboutUs(short hearId, short partyTypeId, string name) : this(hearId, partyTypeId, name, short.MinValue) { }
		public HearAboutUs(short hearId, short partyTypeId, string name, short orderOnWeb) : this(hearId, partyTypeId, name, orderOnWeb, int.MinValue) { }
		public HearAboutUs(short hearId, short partyTypeId, string name, short orderOnWeb, int isActive) {
			this.hearId = hearId;
			this.partyTypeId = partyTypeId;
			this.name = name;
			this.orderOnWeb = orderOnWeb;
			this.isActive = isActive;
		}


		#region XML Methods

		#region Save XML
		public override string GenerateXML() {
			return "<HearAboutUs>\r\n" +
			"	<HearId>" + hearId + "</HearId>\r\n" +
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
				if(ToLowerCase(node.Name) == ToLowerCase("hearId")) {
					SetXmlValue(ref hearId, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("partyTypeId")) {
					SetXmlValue(ref partyTypeId, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("name")) {
					SetXmlValue(ref name, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("orderOnWeb")) {
					SetXmlValue(ref orderOnWeb, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("isActive")) {
					SetXmlValue(ref isActive, node.InnerText);
				}
			}
		}
		#endregion

		#endregion

		#region Data Source Methods
		public static HearAboutUs[] GetHearAboutUss() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetHearAboutUss();
		}

		/*
		public static HearAboutUs GetHearAboutUsByID(int id) {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetHearAboutUsByID(id);
		}

		public int Insert() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.InsertHearAboutUs(this);
		}

		public int Update() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.UpdateHearAboutUs(this);
		}*/
		#endregion

		#region Properties
		public short HearId {
			set { hearId = value; }
			get { return hearId; }
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

		public int IsActive {
			set { isActive = value; }
			get { return isActive; }
		}

		#endregion
	}
}
