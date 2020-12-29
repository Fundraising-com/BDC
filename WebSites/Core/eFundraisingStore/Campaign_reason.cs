using System;
using System.Xml;

namespace GA.BDC.Core.eFundraisingStore {

	public class CampaignReason: eFundraisingStoreDataObject {

		private short campaignReasonId;
		private short partyTypeId;
		private string description;


		public CampaignReason() : this(short.MinValue) { }
		public CampaignReason(short campaignReasonId) : this(campaignReasonId, short.MinValue) { }
		public CampaignReason(short campaignReasonId, short partyTypeId) : this(campaignReasonId, partyTypeId, null) { }
		public CampaignReason(short campaignReasonId, short partyTypeId, string description) {
			this.campaignReasonId = campaignReasonId;
			this.partyTypeId = partyTypeId;
			this.description = description;
		}


		#region XML Methods

		#region Save XML
		public override string GenerateXML() {
			return "<CampaignReason>\r\n" +
			"	<CampaignReasonId>" + campaignReasonId + "</CampaignReasonId>\r\n" +
			"	<PartyTypeId>" + partyTypeId + "</PartyTypeId>\r\n" +
			"	<Description>" + System.Web.HttpUtility.HtmlEncode(description) + "</Description>\r\n" +
			"</CampaignReason>\r\n";
		}
		#endregion

		#region Load Methods
		// loop through the xml and set the properties and child classes
		public override void Load(XmlNode childNodes) {
			foreach(XmlNode node in childNodes) {
				if(node.Name.ToLower() == "campaignReasonId") {
					SetXmlValue(ref campaignReasonId, node.InnerText);
				}
				if(node.Name.ToLower() == "partyTypeId") {
					SetXmlValue(ref partyTypeId, node.InnerText);
				}
				if(node.Name.ToLower() == "description") {
					SetXmlValue(ref description, node.InnerText);
				}
			}
		}
		#endregion

		#endregion

		#region Data Source Methods
		public static CampaignReason[] GetCampaignReasons() {
			DataAccess.eFundraisingStoreDatabase dbo = new DataAccess.eFundraisingStoreDatabase();
			return dbo.GetCampaignReasons();
		}

		public static CampaignReason GetCampaignReasonByID(int id) {
			DataAccess.eFundraisingStoreDatabase dbo = new DataAccess.eFundraisingStoreDatabase();
			return dbo.GetCampaignReasonByID(id);
		}

		public int Insert() {
			DataAccess.eFundraisingStoreDatabase dbo = new DataAccess.eFundraisingStoreDatabase();
			return dbo.InsertCampaignReason(this);
		}

		public int Update() {
			DataAccess.eFundraisingStoreDatabase dbo = new DataAccess.eFundraisingStoreDatabase();
			return dbo.UpdateCampaignReason(this);
		}
		#endregion

		#region Properties
		public short CampaignReasonId {
			set { campaignReasonId = value; }
			get { return campaignReasonId; }
		}

		public short PartyTypeId {
			set { partyTypeId = value; }
			get { return partyTypeId; }
		}

		public string Description {
			set { description = value; }
			get { return description; }
		}

		#endregion
	}
}
