using System;
using System.Xml;

namespace GA.BDC.Core.EFundraisingCRM {

	public class EFOCampaign: EFundraisingCRMDataObject {

		private int campaignID;
		private int groupTypeID;
		private int qSPProgramID;
		private int campaignImageID;
		private int organizerID;
		private string groupName;
		private DateTime creationDate;
		private float financialGoal;
		private string fundRaisingReason;
		private string backgroundInfo;
		private string comments;
		private int isLaunched;
		private int isOver;
		private string accountNumber;


		public EFOCampaign() : this(int.MinValue) { }
		public EFOCampaign(int campaignID) : this(campaignID, int.MinValue) { }
		public EFOCampaign(int campaignID, int groupTypeID) : this(campaignID, groupTypeID, int.MinValue) { }
		public EFOCampaign(int campaignID, int groupTypeID, int qSPProgramID) : this(campaignID, groupTypeID, qSPProgramID, int.MinValue) { }
		public EFOCampaign(int campaignID, int groupTypeID, int qSPProgramID, int campaignImageID) : this(campaignID, groupTypeID, qSPProgramID, campaignImageID, int.MinValue) { }
		public EFOCampaign(int campaignID, int groupTypeID, int qSPProgramID, int campaignImageID, int organizerID) : this(campaignID, groupTypeID, qSPProgramID, campaignImageID, organizerID, null) { }
		public EFOCampaign(int campaignID, int groupTypeID, int qSPProgramID, int campaignImageID, int organizerID, string groupName) : this(campaignID, groupTypeID, qSPProgramID, campaignImageID, organizerID, groupName, DateTime.MinValue) { }
		public EFOCampaign(int campaignID, int groupTypeID, int qSPProgramID, int campaignImageID, int organizerID, string groupName, DateTime creationDate) : this(campaignID, groupTypeID, qSPProgramID, campaignImageID, organizerID, groupName, creationDate, float.MinValue) { }
		public EFOCampaign(int campaignID, int groupTypeID, int qSPProgramID, int campaignImageID, int organizerID, string groupName, DateTime creationDate, float financialGoal) : this(campaignID, groupTypeID, qSPProgramID, campaignImageID, organizerID, groupName, creationDate, financialGoal, null) { }
		public EFOCampaign(int campaignID, int groupTypeID, int qSPProgramID, int campaignImageID, int organizerID, string groupName, DateTime creationDate, float financialGoal, string fundRaisingReason) : this(campaignID, groupTypeID, qSPProgramID, campaignImageID, organizerID, groupName, creationDate, financialGoal, fundRaisingReason, null) { }
		public EFOCampaign(int campaignID, int groupTypeID, int qSPProgramID, int campaignImageID, int organizerID, string groupName, DateTime creationDate, float financialGoal, string fundRaisingReason, string backgroundInfo) : this(campaignID, groupTypeID, qSPProgramID, campaignImageID, organizerID, groupName, creationDate, financialGoal, fundRaisingReason, backgroundInfo, null) { }
		public EFOCampaign(int campaignID, int groupTypeID, int qSPProgramID, int campaignImageID, int organizerID, string groupName, DateTime creationDate, float financialGoal, string fundRaisingReason, string backgroundInfo, string comments) : this(campaignID, groupTypeID, qSPProgramID, campaignImageID, organizerID, groupName, creationDate, financialGoal, fundRaisingReason, backgroundInfo, comments, int.MinValue) { }
		public EFOCampaign(int campaignID, int groupTypeID, int qSPProgramID, int campaignImageID, int organizerID, string groupName, DateTime creationDate, float financialGoal, string fundRaisingReason, string backgroundInfo, string comments, int isLaunched) : this(campaignID, groupTypeID, qSPProgramID, campaignImageID, organizerID, groupName, creationDate, financialGoal, fundRaisingReason, backgroundInfo, comments, isLaunched, int.MinValue) { }
		public EFOCampaign(int campaignID, int groupTypeID, int qSPProgramID, int campaignImageID, int organizerID, string groupName, DateTime creationDate, float financialGoal, string fundRaisingReason, string backgroundInfo, string comments, int isLaunched, int isOver) : this(campaignID, groupTypeID, qSPProgramID, campaignImageID, organizerID, groupName, creationDate, financialGoal, fundRaisingReason, backgroundInfo, comments, isLaunched, isOver, null) { }
		public EFOCampaign(int campaignID, int groupTypeID, int qSPProgramID, int campaignImageID, int organizerID, string groupName, DateTime creationDate, float financialGoal, string fundRaisingReason, string backgroundInfo, string comments, int isLaunched, int isOver, string accountNumber) {
			this.campaignID = campaignID;
			this.groupTypeID = groupTypeID;
			this.qSPProgramID = qSPProgramID;
			this.campaignImageID = campaignImageID;
			this.organizerID = organizerID;
			this.groupName = groupName;
			this.creationDate = creationDate;
			this.financialGoal = financialGoal;
			this.fundRaisingReason = fundRaisingReason;
			this.backgroundInfo = backgroundInfo;
			this.comments = comments;
			this.isLaunched = isLaunched;
			this.isOver = isOver;
			this.accountNumber = accountNumber;
		}


		#region XML Methods

		#region Save XML
		public override string GenerateXML() {
			return "<EFOCampaign>\r\n" +
			"	<CampaignID>" + campaignID + "</CampaignID>\r\n" +
			"	<GroupTypeID>" + groupTypeID + "</GroupTypeID>\r\n" +
			"	<QSPProgramID>" + qSPProgramID + "</QSPProgramID>\r\n" +
			"	<CampaignImageID>" + campaignImageID + "</CampaignImageID>\r\n" +
			"	<OrganizerID>" + organizerID + "</OrganizerID>\r\n" +
			"	<GroupName>" + System.Web.HttpUtility.HtmlEncode(groupName) + "</GroupName>\r\n" +
			"	<CreationDate>" + creationDate + "</CreationDate>\r\n" +
			"	<FinancialGoal>" + financialGoal + "</FinancialGoal>\r\n" +
			"	<FundRaisingReason>" + System.Web.HttpUtility.HtmlEncode(fundRaisingReason) + "</FundRaisingReason>\r\n" +
			"	<BackgroundInfo>" + System.Web.HttpUtility.HtmlEncode(backgroundInfo) + "</BackgroundInfo>\r\n" +
			"	<Comments>" + System.Web.HttpUtility.HtmlEncode(comments) + "</Comments>\r\n" +
			"	<IsLaunched>" + isLaunched + "</IsLaunched>\r\n" +
			"	<IsOver>" + isOver + "</IsOver>\r\n" +
			"	<AccountNumber>" + System.Web.HttpUtility.HtmlEncode(accountNumber) + "</AccountNumber>\r\n" +
			"</EFOCampaign>\r\n";
		}
		#endregion

		#region Load Methods
		// loop through the xml and set the properties and child classes
		public override void Load(XmlNode childNodes) {
			foreach(XmlNode node in childNodes) {
				if(ToLowerCase(node.Name) == ToLowerCase("campaignId")) {
					SetXmlValue(ref campaignID, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("groupTypeId")) {
					SetXmlValue(ref groupTypeID, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("qspProgramId")) {
					SetXmlValue(ref qSPProgramID, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("campaignImageId")) {
					SetXmlValue(ref campaignImageID, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("organizerId")) {
					SetXmlValue(ref organizerID, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("groupName")) {
					SetXmlValue(ref groupName, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("creationDate")) {
					SetXmlValue(ref creationDate, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("financialGoal")) {
					SetXmlValue(ref financialGoal, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("fundRaisingReason")) {
					SetXmlValue(ref fundRaisingReason, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("backgroundInfo")) {
					SetXmlValue(ref backgroundInfo, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("comments")) {
					SetXmlValue(ref comments, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("isLaunched")) {
					SetXmlValue(ref isLaunched, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("isOver")) {
					SetXmlValue(ref isOver, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("accountNumber")) {
					SetXmlValue(ref accountNumber, node.InnerText);
				}
			}
		}
		#endregion

		#endregion

		#region Data Source Methods
		public static EFOCampaign[] GetEFOCampaigns() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetEFOCampaigns();
		}

		public static EFOCampaign GetEFOCampaignByID(int id) {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetEFOCampaignByID(id);
		}

		public int Insert() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.InsertEFOCampaign(this);
		}

		public int Update() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.UpdateEFOCampaign(this);
		}
		#endregion

		#region Properties
		public int CampaignID {
			set { campaignID = value; }
			get { return campaignID; }
		}

		public int GroupTypeID {
			set { groupTypeID = value; }
			get { return groupTypeID; }
		}

		public int QSPProgramID {
			set { qSPProgramID = value; }
			get { return qSPProgramID; }
		}

		public int CampaignImageID {
			set { campaignImageID = value; }
			get { return campaignImageID; }
		}

		public int OrganizerID {
			set { organizerID = value; }
			get { return organizerID; }
		}

		public string GroupName {
			set { groupName = value; }
			get { return groupName; }
		}

		public DateTime CreationDate {
			set { creationDate = value; }
			get { return creationDate; }
		}

		public float FinancialGoal {
			set { financialGoal = value; }
			get { return financialGoal; }
		}

		public string FundRaisingReason {
			set { fundRaisingReason = value; }
			get { return fundRaisingReason; }
		}

		public string BackgroundInfo {
			set { backgroundInfo = value; }
			get { return backgroundInfo; }
		}

		public string Comments {
			set { comments = value; }
			get { return comments; }
		}

		public int IsLaunched {
			set { isLaunched = value; }
			get { return isLaunched; }
		}

		public int IsOver {
			set { isOver = value; }
			get { return isOver; }
		}

		public string AccountNumber {
			set { accountNumber = value; }
			get { return accountNumber; }
		}

		#endregion
	}
}
