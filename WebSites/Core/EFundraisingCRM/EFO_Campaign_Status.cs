using System;
using System.Xml;

namespace GA.BDC.Core.EFundraisingCRM {

	public class EFOCampaignStatus: EFundraisingCRMDataObject {

		private int campaignID;
		private DateTime dateToChange;
		private int statusID;
		private int emailTypeID;


		public EFOCampaignStatus() : this(int.MinValue) { }
		public EFOCampaignStatus(int campaignID) : this(campaignID, DateTime.MinValue) { }
		public EFOCampaignStatus(int campaignID, DateTime dateToChange) : this(campaignID, dateToChange, int.MinValue) { }
		public EFOCampaignStatus(int campaignID, DateTime dateToChange, int statusID) : this(campaignID, dateToChange, statusID, int.MinValue) { }
		public EFOCampaignStatus(int campaignID, DateTime dateToChange, int statusID, int emailTypeID) {
			this.campaignID = campaignID;
			this.dateToChange = dateToChange;
			this.statusID = statusID;
			this.emailTypeID = emailTypeID;
		}


		#region XML Methods

		#region Save XML
		public override string GenerateXML() {
			return "<EFOCampaignStatus>\r\n" +
			"	<CampaignID>" + campaignID + "</CampaignID>\r\n" +
			"	<DateToChange>" + dateToChange + "</DateToChange>\r\n" +
			"	<StatusID>" + statusID + "</StatusID>\r\n" +
			"	<EmailTypeID>" + emailTypeID + "</EmailTypeID>\r\n" +
			"</EFOCampaignStatus>\r\n";
		}
		#endregion

		#region Load Methods
		// loop through the xml and set the properties and child classes
		public override void Load(XmlNode childNodes) {
			foreach(XmlNode node in childNodes) {
				if(ToLowerCase(node.Name) == ToLowerCase("campaignId")) {
					SetXmlValue(ref campaignID, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("dateToChange")) {
					SetXmlValue(ref dateToChange, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("statusId")) {
					SetXmlValue(ref statusID, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("emailTypeId")) {
					SetXmlValue(ref emailTypeID, node.InnerText);
				}
			}
		}
		#endregion

		#endregion

		#region Data Source Methods
		public static EFOCampaignStatus[] GetEFOCampaignStatuss() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetEFOCampaignStatuss();
		}

		public static EFOCampaignStatus GetEFOCampaignStatusByID(int id) {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetEFOCampaignStatusByID(id);
		}

		public int Insert() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.InsertEFOCampaignStatus(this);
		}

		public int Update() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.UpdateEFOCampaignStatus(this);
		}
		#endregion

		#region Properties
		public int CampaignID {
			set { campaignID = value; }
			get { return campaignID; }
		}

		public DateTime DateToChange {
			set { dateToChange = value; }
			get { return dateToChange; }
		}

		public int StatusID {
			set { statusID = value; }
			get { return statusID; }
		}

		public int EmailTypeID {
			set { emailTypeID = value; }
			get { return emailTypeID; }
		}

		#endregion
	}
}
