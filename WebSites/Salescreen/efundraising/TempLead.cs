//
// 2006-07-05 : Maxime Normand - New Class.
//

using System;
using efundraising.Core;
using efundraising.efundraisingCore.DataAccess;

namespace efundraising.efundraisingCore
{
	/// <summary>
	/// Summary description for TempLead.
	/// </summary>
	public class TempLead : BusinessBase
	{
		#region Fields
		private int divisionID = 1;
		private int promotionID;
		private int tempLeadID;
		private string channelCode;
		private int leadStatusID = 1;
		private int consultantID;
		private DateTime leadEntryDate = DateTime.Now;
		private string salutation;
		private string firstName;
		private string lastName;
		private string organization;
		private string streetAddress;
		private string city;
		private string stateCode;
		private string countryCode;
		private string zipCode;
		private string dayPhone;
		private string dayTimeCall;
		private string eveningPhone;
		private string fax;
		private string email;
		private int groupTypeID;
		private int participantCount = 0;
		private int fundRaisingGoal = 0;
		private DateTime decisionDate;
		private bool decisionMaker = false;
		private DateTime fundRaiserStartDate;
		private bool onEmailList = false;
		private string comments;
		private int hearID = 0;
		private bool kitToSend = false;
		private bool kitSent = false;
		private DateTime kitSentDate;
		private string dayPhoneExt;
		private string eveningPhoneExt;
		private string rejectionReason;
		private string otherPhone;
		private string cookieContent;
		private string groupWebSite;
		private int organizationTypeID =  99;
		private int titleID;
		private int campaignReasonID = 99;
		private int webSiteID;
		private string otherPhoneExt;
		private bool isNew = true;
		private bool optInSweepstakes = false;
		private int groupID = int.MinValue;

		#endregion
		
		#region Constructors
		
		public TempLead()
		{}
		
		public TempLead(Lead lead)
		{
			promotionID = lead.PromotionID;
			tempLeadID = lead.TempLeadID;
			leadStatusID = lead.LeadStatusID;
			consultantID = lead.ConsultantID;
			firstName = lead.FirstName;
			lastName = lead.LastName;
			organization = lead.GroupName;
			streetAddress = lead.StreetAddress;
			city = lead.City;
			stateCode = lead.State;
			countryCode = lead.Country;
			zipCode = lead.ZipCode;
			dayPhone = lead.DayPhone;
			dayTimeCall = lead.BestTimeToCall;
			eveningPhone = lead.EveningPhone;
			email = lead.Email;
			groupTypeID = lead.GroupTypeID;
			participantCount = lead.ParticipantCount;
			decisionMaker = lead.DecisionMaker;
			onEmailList = lead.OnEmailList;
			comments = lead.Comments + " // Start Date : " + lead.FundraisingDate;
			dayPhoneExt = lead.DayPhoneExt;
			eveningPhoneExt = lead.EveningPhoneExt;
			organizationTypeID = lead.OrganizationTypeID;
			try {
				titleID = int.Parse(lead.Title);	
			} catch {
				titleID = 99;
			}			
			groupID =  int.MinValue;
		}
		
		
		#endregion
		
		#region Methods
		
		public static TempLead GetTempLead(int tempLeadID)
		{
			EFundDatabase dbo = new EFundDatabase();
			return dbo.GetTempLead(tempLeadID);
		}
		
		public static TempLeadCollection GetNewTempLeads()
		{
			EFundDatabase dbo = new EFundDatabase();
			return dbo.GetNewTempLeads();
		}
		
		public void Insert()
		{
			EFundDatabase dbo = new EFundDatabase();
			dbo.InsertTempLead(this);	
		}
		
		public void Update()
		{
			EFundDatabase dbo = new EFundDatabase();
			dbo.UpdateTempLead(this);
		}
		
		
		#endregion
		
		#region Properties
		public int DivisionID
		{
			get { return this.divisionID; }
			set { this.divisionID = value; }
		}
		
		public int WebSiteID
		{
			get { return this.webSiteID; }
			set { this.webSiteID = value; }
		}

		public int PromotionID
		{
			get { return this.promotionID; }
			set { this.promotionID = value; }
		}

		public string Organization
		{
			get { return this.organization; }
			set { this.organization = value; }
		}

		public int HearID
		{
			get { return this.hearID; }
			set { this.hearID = value; }
		}

		public int FundRaisingGoal
		{
			get { return this.fundRaisingGoal; }
			set { this.fundRaisingGoal = value; }
		}

		public string Email
		{
			get { return this.email; }
			set { this.email = value; }
		}

		public bool DecisionMaker
		{
			get { return this.decisionMaker; }
			set { this.decisionMaker = value; }
		}

		public string ChannelCode
		{
			get { return this.channelCode; }
			set { this.channelCode = value; }
		}

		public bool OnEmailList
		{
			get { return this.onEmailList; }
			set { this.onEmailList = value; }
		}

		public string City
		{
			get { return this.city; }
			set { this.city = value; }
		}

		public string CountryCode
		{
			get { return this.countryCode; }
			set { this.countryCode = value; }
		}

		public string DayPhoneExt
		{
			get { return this.dayPhoneExt; }
			set { this.dayPhoneExt = value; }
		}

		public bool KitToSend
		{
			get { return this.kitToSend; }
			set { this.kitToSend = value; }
		}

		public bool KitSent
		{
			get { return this.kitSent; }
			set { this.kitSent = value; }
		}

		public System.DateTime KitSentDate
		{
			get { return this.kitSentDate; }
			set { this.kitSentDate = value; }
		}

		public int TitleID
		{
			get { return this.titleID; }
			set { this.titleID = value; }
		}

		public string Fax
		{
			get { return this.fax; }
			set { this.fax = value; }
		}

		public System.DateTime FundRaiserStartDate
		{
			get { return this.fundRaiserStartDate; }
			set { this.fundRaiserStartDate = value; }
		}

		public string DayPhone
		{
			get { return this.dayPhone; }
			set { this.dayPhone = value; }
		}
		
		public string DayTimeCall
		{
			get { return this.dayTimeCall; }
			set { this.dayTimeCall = value; }
		}
		
		public string EveningPhone
		{
			get { return this.eveningPhone; }
			set { this.eveningPhone = value; }
		}

		public int LeadStatusID
		{
			get { return this.leadStatusID; }
			set { this.leadStatusID = value; }
		}

		public System.DateTime DecisionDate
		{
			get { return this.decisionDate; }
			set { this.decisionDate = value; }
		}

		public string EveningPhoneExt
		{
			get { return this.eveningPhoneExt; }
			set { this.eveningPhoneExt = value; }
		}

		public string StateCode
		{
			get { return this.stateCode; }
			set { this.stateCode = value; }
		}

		public string OtherPhoneExt
		{
			get { return this.otherPhoneExt; }
			set { this.otherPhoneExt = value; }
		}

		public string LastName
		{
			get { return this.lastName; }
			set { this.lastName = value; }
		}

		public bool IsNew
		{
			get { return this.isNew; }
			set { this.isNew = value; }
		}

		public string GroupWebSite
		{
			get { return this.groupWebSite; }
			set { this.groupWebSite = value; }
		}

		public string Comments
		{
			get { return this.comments; }
			set { this.comments = value; }
		}

		public int TempLeadID
		{
			get { return this.tempLeadID; }
			set { this.tempLeadID = value; }
		}

		public bool OptInSweepstakes
		{
			get { return this.optInSweepstakes; }
			set { this.optInSweepstakes = value; }
		}

		public string CookieContent
		{
			get { return this.cookieContent; }
			set { this.cookieContent = value; }
		}

		public System.DateTime LeadEntryDate
		{
			get { return this.leadEntryDate; }
			set { this.leadEntryDate = value; }
		}

		public int OrganizationTypeID
		{
			get { return this.organizationTypeID; }
			set { this.organizationTypeID = value; }
		}

		public string Salutation
		{
			get { return this.salutation; }
			set { this.salutation = value; }
		}

		public int CampaignReasonID
		{
			get { return this.campaignReasonID; }
			set { this.campaignReasonID = value; }
		}

		public string RejectionReason
		{
			get { return this.rejectionReason; }
			set { this.rejectionReason = value; }
		}

		public string OtherPhone
		{
			get { return this.otherPhone; }
			set { this.otherPhone = value; }
		}

		public int GroupTypeID
		{
			get { return this.groupTypeID; }
			set { this.groupTypeID = value; }
		}

		public int ParticipantCount
		{
			get { return this.participantCount; }
			set { this.participantCount = value; }
		}

		public string FirstName
		{
			get { return this.firstName; }
			set { this.firstName = value; }
		}

		public int ConsultantID
		{
			get { return this.consultantID; }
			set { this.consultantID = value; }
		}

		public string ZipCode
		{
			get { return this.zipCode; }
			set { this.zipCode = value; }
		}

		public string StreetAddress
		{
			get { return this.streetAddress; }
			set { this.streetAddress = value; }
		}
		
		public int GroupID
		{
			get { return this.groupID; }
			set { this.groupID = value; }
		}
		
		#endregion
	}
}
