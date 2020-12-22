using System;
using System.Xml;

namespace efundraising.EFundraisingCRM {

	public class CampaignReason: EFundraisingCRMDataObject {

		private short campaignReasonId;
		private short partyTypeId;
		private string campaignReasonDesc;


		public CampaignReason() : this(short.MinValue) { }
		public CampaignReason(short campaignReasonId) : this(campaignReasonId, short.MinValue) { }
		public CampaignReason(short campaignReasonId, short partyTypeId) : this(campaignReasonId, partyTypeId, null) { }
		public CampaignReason(short campaignReasonId, short partyTypeId, string campaignReasonDesc) {
			this.campaignReasonId = campaignReasonId;
			this.partyTypeId = partyTypeId;
			this.campaignReasonDesc = campaignReasonDesc;
		}


		#region XML Methods

		#region Save XML
		public override string GenerateXML() {
			return "<CampaignReason>\r\n" +
			"	<CampaignReasonId>" + campaignReasonId + "</CampaignReasonId>\r\n" +
			"	<PartyTypeId>" + partyTypeId + "</PartyTypeId>\r\n" +
			"	<CampaignReasonDesc>" + System.Web.HttpUtility.HtmlEncode(campaignReasonDesc) + "</CampaignReasonDesc>\r\n" +
			"</CampaignReason>\r\n";
		}
		#endregion

		#region Load Methods
		// loop through the xml and set the properties and child classes
		public override void Load(XmlNode childNodes) {
			foreach(XmlNode node in childNodes) {
				if(ToLowerCase(node.Name) == ToLowerCase("campaignReasonId")) {
					SetXmlValue(ref campaignReasonId, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("partyTypeId")) {
					SetXmlValue(ref partyTypeId, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("campaignReasonDesc")) {
					SetXmlValue(ref campaignReasonDesc, node.InnerText);
				}
			}
		}
		#endregion

		#endregion

		#region Data Source Methods
		public static CampaignReason[] GetCampaignReasons() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetCampaignReasons();
		}

		/*
		public static CampaignReason GetCampaignReasonByID(int id) {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetCampaignReasonByID(id);
		}

		public int Insert() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.InsertCampaignReason(this);
		}

		public int Update() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.UpdateCampaignReason(this);
		}*/
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

		public string CampaignReasonDesc {
			set { campaignReasonDesc = value; }
			get { return campaignReasonDesc; }
		}

		#endregion
	}
}
