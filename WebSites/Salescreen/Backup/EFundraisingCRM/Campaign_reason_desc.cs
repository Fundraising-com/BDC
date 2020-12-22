using System;
using System.Xml;

namespace efundraising.EFundraisingCRM {

	public class CampaignReasonDesc: EFundraisingCRMDataObject {

		private short campaignReasonId;
		private short languageId;
		private string description;


		public CampaignReasonDesc() : this(short.MinValue) { }
		public CampaignReasonDesc(short campaignReasonId) : this(campaignReasonId, short.MinValue) { }
		public CampaignReasonDesc(short campaignReasonId, short languageId) : this(campaignReasonId, languageId, null) { }
		public CampaignReasonDesc(short campaignReasonId, short languageId, string description) {
			this.campaignReasonId = campaignReasonId;
			this.languageId = languageId;
			this.description = description;
		}


		#region XML Methods

		#region Save XML
		public override string GenerateXML() {
			return "<CampaignReasonDesc>\r\n" +
			"	<CampaignReasonId>" + campaignReasonId + "</CampaignReasonId>\r\n" +
			"	<LanguageId>" + languageId + "</LanguageId>\r\n" +
			"	<Description>" + System.Web.HttpUtility.HtmlEncode(description) + "</Description>\r\n" +
			"</CampaignReasonDesc>\r\n";
		}
		#endregion

		#region Load Methods
		// loop through the xml and set the properties and child classes
		public override void Load(XmlNode childNodes) {
			foreach(XmlNode node in childNodes) {
				if(ToLowerCase(node.Name) == ToLowerCase("campaignReasonId")) {
					SetXmlValue(ref campaignReasonId, node.InnerText);
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
		public static CampaignReasonDesc[] GetCampaignReasonDescs() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetCampaignReasonDescs();
		}

		/*
		public static CampaignReasonDesc GetCampaignReasonDescByID(int id) {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetCampaignReasonDescByID(id);
		}

		public int Insert() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.InsertCampaignReasonDesc(this);
		}

		public int Update() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.UpdateCampaignReasonDesc(this);
		}*/
		#endregion

		#region Properties
		public short CampaignReasonId {
			set { campaignReasonId = value; }
			get { return campaignReasonId; }
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
