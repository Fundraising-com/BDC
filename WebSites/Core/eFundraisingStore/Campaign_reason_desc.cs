using System;
using System.Xml;

namespace GA.BDC.Core.eFundraisingStore {

	public class CampaignReasonDesc: eFundraisingStoreDataObject {

		private short campaignReasonId;
		private string cultureCode;
		private string description;


		public CampaignReasonDesc() : this(short.MinValue) { }
		public CampaignReasonDesc(short campaignReasonId) : this(campaignReasonId, null) { }
		public CampaignReasonDesc(short campaignReasonId, string cultureCode) : this(campaignReasonId, cultureCode, null) { }
		public CampaignReasonDesc(short campaignReasonId, string cultureCode, string description) {
			this.campaignReasonId = campaignReasonId;
			this.cultureCode = cultureCode;
			this.description = description;
		}


		#region XML Methods

		#region Save XML
		public override string GenerateXML() {
			return "<CampaignReasonDesc>\r\n" +
			"	<CampaignReasonId>" + campaignReasonId + "</CampaignReasonId>\r\n" +
			"	<CultureCode>" + System.Web.HttpUtility.HtmlEncode(cultureCode) + "</CultureCode>\r\n" +
			"	<Description>" + System.Web.HttpUtility.HtmlEncode(description) + "</Description>\r\n" +
			"</CampaignReasonDesc>\r\n";
		}
		#endregion

		#region Load Methods
		// loop through the xml and set the properties and child classes
		public override void Load(XmlNode childNodes) {
			foreach(XmlNode node in childNodes) {
				if(node.Name.ToLower() == "campaignReasonId") {
					SetXmlValue(ref campaignReasonId, node.InnerText);
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
		public static CampaignReasonDesc[] GetCampaignReasonDescs() {
			DataAccess.eFundraisingStoreDatabase dbo = new DataAccess.eFundraisingStoreDatabase();
			return dbo.GetCampaignReasonDescs();
		}

		public static CampaignReasonDesc GetCampaignReasonDescByID(int id) {
			DataAccess.eFundraisingStoreDatabase dbo = new DataAccess.eFundraisingStoreDatabase();
			return dbo.GetCampaignReasonDescByID(id);
		}

		public int Insert() {
			DataAccess.eFundraisingStoreDatabase dbo = new DataAccess.eFundraisingStoreDatabase();
			return dbo.InsertCampaignReasonDesc(this);
		}

		public int Update() {
			DataAccess.eFundraisingStoreDatabase dbo = new DataAccess.eFundraisingStoreDatabase();
			return dbo.UpdateCampaignReasonDesc(this);
		}
		#endregion

		#region Properties
		public short CampaignReasonId {
			set { campaignReasonId = value; }
			get { return campaignReasonId; }
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
