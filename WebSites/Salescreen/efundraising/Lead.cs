//
//	April 21, 2005	-	Louis Turmel	-	New Class Implementation
//	April 22, 2005	-	Louis Turmel	-	Code Added for Lead Operation
//
 
using System;
using System.Data;
using efundraising.Core;
using efundraising.Collections;
using efundraising.Configuration;
using efundraising.Email;
using efundraising.efundraisingCore.DataAccess;


namespace efundraising.efundraisingCore 
{
	/// <summary>
	/// Lead class.
	/// </summary>
	[Serializable()]
	public class Lead : BusinessBase
	{
		#region Constants
		private const int GoFundraisingPartnerId = 154;
		private const int FundraisingComPartnerId = 686;
		private const int WebServerConsultantId = 3426;
		#endregion

		#region Fields
		private string firstName;
		private string lastName;
		private string email;				
		private string streetAddress;
		private string city;
		private string state;
		private string zipCode;			
		private string country;			
		private string fundraisingDate;			
		private string dayPhone;
		private string eveningPhone;		
		private string dayPhoneExt;		
		private string eveningPhoneExt;	
		private string groupName;
		private string title;
		private string bestTimeToCall = "";		
		private string productsInterest = "";
		private string comments = "";
		private string partnerName = "";
		private int leadID = -1;
		private int leadVisitID = -1;
		private int leadStatusID = 1;
		private int organizationTypeID = 99;	
		private byte groupTypeID = 99;			
		private int participantCount = 0;
		private int consultantID = 0;
		private int promotionID = -1;
		private int partnerID = -1;
		private bool isConsultantActive;
		private bool onEmailList;
		private bool decisionMaker;
		private int isPostalAddressValidated = 0;
		private int tempLeadID = int.MinValue;
		private byte fundraisersPerYear = byte.MinValue;
		private int addressZoneId;
        private string bestTimeToCallAlt = "";
        private string groupwebsite;
              
      	#endregion

		#region Constructors
		public Lead ()
		{

		}

		public Lead(string firstName, string lastName, string email, string streetAddress, string city,
			string state, string zipCode, string country, string groupName, string fundraisingDate, 
			string dayPhone, string eveningPhone, string dayPhoneExt, string eveningPhoneExt,
			int organizationTypeID, byte groupTypeID, int participantCount, int consultantID, int promotionID,
			string title, string bestTimeToCall, bool decisionMaker, string productsInterest, bool onEmailList,
            string comments, int leadStatusID) 
		{

			this.FirstName = firstName;
			this.LastName = lastName;
			this.Email = email;
			this.StreetAddress = streetAddress;
			this.City = city;
			this.GroupName = groupName;
			this.State = state;
			this.ZipCode = zipCode;
			this.Country = country;
			this.FundraisingDate = fundraisingDate;
			this.DayPhone = dayPhone;
			this.EveningPhone = eveningPhone;
			this.DayPhoneExt = dayPhoneExt;
			this.EveningPhoneExt = eveningPhoneExt;
			this.OrganizationTypeID = organizationTypeID;
			this.GroupTypeID = groupTypeID;
			this.ParticipantCount = participantCount;
			this.ConsultantID = consultantID;
			this.PromotionID = promotionID;
			this.Title = title;
			this.BestTimeToCall = bestTimeToCall;
            this.DecisionMaker = decisionMaker;
			this.ProductsInterest = productsInterest;
			this.OnEmailList = onEmailList;
			this.Comments = comments;
			this.LeadStatusID = leadStatusID;
                        

		}

        public Lead(string firstName, string lastName, string email, string streetAddress, string city,
            string state, string zipCode, string country, string groupName, string fundraisingDate,
            string dayPhone, string eveningPhone, string dayPhoneExt, string eveningPhoneExt,
            int organizationTypeID, byte groupTypeID, int participantCount, int consultantID, int promotionID,
            string title, string bestTimeToCall, bool decisionMaker, string productsInterest, bool onEmailList,
            string comments, int leadStatusID, string bestTimeToCallAlt, string groupwebsite)
        {

            this.FirstName = firstName;
            this.LastName = lastName;
            this.Email = email;
            this.StreetAddress = streetAddress;
            this.City = city;
            this.GroupName = groupName;
            this.State = state;
            this.ZipCode = zipCode;
            this.Country = country;
            this.FundraisingDate = fundraisingDate;
            this.DayPhone = dayPhone;
            this.EveningPhone = eveningPhone;
            this.DayPhoneExt = dayPhoneExt;
            this.EveningPhoneExt = eveningPhoneExt;
            this.OrganizationTypeID = organizationTypeID;
            this.GroupTypeID = groupTypeID;
            this.ParticipantCount = participantCount;
            this.ConsultantID = consultantID;
            this.PromotionID = promotionID;
            this.Title = title;
            this.BestTimeToCall = bestTimeToCall;
            this.DecisionMaker = decisionMaker;
            this.ProductsInterest = productsInterest;
            this.OnEmailList = onEmailList;
            this.Comments = comments;
            this.LeadStatusID = leadStatusID;
            this.BestTimeToCallAlt = bestTimeToCallAlt;
            this.GroupWebsite = groupwebsite;
            

        }






		#endregion

		#region Properties

        public string GroupWebsite
        {
            get { return this.groupwebsite; }
            set { this.groupwebsite = value; }
        }
            
        public string FirstName
		{
			get { return this.firstName; }
			set { this.firstName = value; }
		}

		public string LastName
		{
			get { return this.lastName; }
			set { this.lastName = value; }
		}

		public string Email
		{
			get { return this.email; }
			set { this.email = value; }
		}

		public string StreetAddress
		{
			get { return this.streetAddress; }
			set { this.streetAddress = value; }
		}

		public string City
		{
			get { return this.city; }
			set { this.city = value; }
		}

		public string State
		{
			get { return this.state; }
			set { this.state = value; }
		}

		public string ZipCode
		{
			get { return this.zipCode; }
			set { this.zipCode = value; }
		}

		public string Country
		{
			get { return this.country; }
			set { this.country = value; }
		}

		public string FundraisingDate
		{
			get { return this.fundraisingDate; }
			set { this.fundraisingDate = value; }
		}

		public string DayPhone
		{
			get { return this.dayPhone; }
			set { this.dayPhone = value; }
		}

		public string EveningPhone
		{
			get { return this.eveningPhone; }
			set
			{ 
				if (value == "--")
					this.eveningPhone = "";
				else
					this.eveningPhone = value;
			}
		}

		public string DayPhoneExt
		{
			get { return this.dayPhoneExt; }
			set { this.dayPhoneExt = value; }
		}

		public string EveningPhoneExt
		{
			get { return this.eveningPhoneExt; }
			set { this.eveningPhoneExt = value; }
		}

		public string GroupName
		{
			get { return this.groupName; }
			set { this.groupName = value; }
		}

		public string Title
		{
			get { return this.title; }
			set { this.title = value; }
		}

		public string BestTimeToCall
		{
			get { return this.bestTimeToCall; }
			set { this.bestTimeToCall = value; }
		}

		public string ProductsInterest
		{
			get { return this.productsInterest; }
			set { this.productsInterest = value; }
		}

		public string Comments
		{
			get { return this.comments; }
			set { this.comments = value; }
		}

		public string PartnerName
		{
			get { return this.partnerName; }
			set { this.partnerName = value; }
		}

		public int LeadID
		{
			get { return this.leadID; }
			set { this.leadID = value; }
		}

		public int LeadVisitID
		{
			get { return this.leadVisitID; }
			set { this.leadVisitID = value; }
		}

		public int LeadStatusID
		{
			get { return this.leadStatusID; }
			set { this.leadStatusID = value; }
		}

		public int OrganizationTypeID
		{
			get { return this.organizationTypeID; }
			set { this.organizationTypeID = value; }
		}

		public byte GroupTypeID
		{
			get { return this.groupTypeID; }
			set { this.groupTypeID = value; }
		}

		public int ParticipantCount
		{
			get { return this.participantCount; }
			set { this.participantCount = value; }
		}

		public int ConsultantID
		{
			get { return this.consultantID; }
			set { this.consultantID = value; }
		}

		public int PromotionID
		{
			get { return this.promotionID; }
			set { this.promotionID = value; }
		}

		public int PartnerID
		{
			get { return this.partnerID; }
			set { this.partnerID = value; }
		}

		public bool IsConsultantActive
		{
			get { return this.isConsultantActive; }
			set { this.isConsultantActive = value; }
		}

		public bool OnEmailList
		{
			get { return this.onEmailList; }
			set { this.onEmailList = value; }
		}

		public bool DecisionMaker
		{
			get { return this.decisionMaker; }
			set { this.decisionMaker = value; }
		}
		
		public int TempLeadID
		{
			get { return this.tempLeadID; }
			set { this.tempLeadID = value; }
		}

		public int IsPostalAddressValidated {
			get { return isPostalAddressValidated; }
			set { isPostalAddressValidated = value; }
		}

		public byte FundraisersPerYear {
			get { return fundraisersPerYear; }
			set { fundraisersPerYear = value; }
		}

		public int AddressZoneId 
		{
			get { return addressZoneId; }
			set { addressZoneId = value; }
		}

        public string BestTimeToCallAlt
        {
            get { return bestTimeToCallAlt; }
            set { bestTimeToCallAlt = value; }
        }

#endregion

		#region Methods
		/// <summary>
		/// Get the lead that matches the member's profile.
		/// </summary>
		/// <param name="street"></param>
		/// <param name="dayPhone"></param>
		/// <param name="eveningPhone"></param>
		/// <param name="email"></param>
		/// <returns></returns>
		public Lead MatchLead()
		{
			//EFundDatabase dbo = new EFundDatabase();
            //efundraising.EFundraisingCRM.DataAccess.EFundraisingCRMDatabase dbo = new efundraising.EFundraisingCRM.DataAccess.EFundraisingCRMDatabase();
			LeadCollection duplicateLeads = Lead.GetMatchingLeads(this.firstName, this.lastName, this.StreetAddress, this.zipCode, this.DayPhone, this.EveningPhone, this.Email);

			// Find best matching lead. Perform a second level of match in code
			// because stored procedure cannot locate the nearest match.
			Lead duplicateLead = null;
			if (duplicateLeads.Count > 1)
			{
				// Pick the lead that has the highest number of matches.
				int maxMatchingFields = 0;
				duplicateLead = duplicateLeads[0];
				for (int i = 0; i < duplicateLeads.Count; i++)
				{
					int matchingFieldsCounter = 0;

					// Match first name
					if (duplicateLeads[i].FirstName != null)
						if (duplicateLeads[i].FirstName.ToLower() == this.FirstName.ToLower())
							matchingFieldsCounter++;

					// Match last name
					if (duplicateLeads[i].LastName != null)
						if (duplicateLeads[i].LastName.ToLower() == this.LastName.ToLower())
							matchingFieldsCounter++;

					// Match day phones
					// first check if the dayphone is not null because some very old leads don't have it
					if (duplicateLeads[i].DayPhone != null && duplicateLeads[i].DayPhone.Length > 0 && this.DayPhone != null)
					{
						if (duplicateLeads[i].DayPhone.ToLower().IndexOf(this.DayPhone.ToLower()) >= 0 ||
							this.DayPhone.ToLower().IndexOf(duplicateLeads[i].DayPhone.ToLower()) >= 0)
							matchingFieldsCounter++;
					}
					
					// Match evening phones
					// first check if the evening is not null because it not a required field
					if (duplicateLeads[i].EveningPhone != null && duplicateLeads[i].EveningPhone.Length > 0 && this.EveningPhone != null)
					{
						if (duplicateLeads[i].EveningPhone.ToLower().IndexOf(this.EveningPhone.ToLower()) >= 0 ||
							this.EveningPhone.ToLower().IndexOf(duplicateLeads[i].EveningPhone.ToLower()) >= 0)
							matchingFieldsCounter++;
					
						// Match evening phone with day phone
						if (duplicateLeads[i].EveningPhone.ToLower().IndexOf(this.DayPhone.ToLower()) >= 0 ||
							this.DayPhone.ToLower().IndexOf(duplicateLeads[i].EveningPhone.ToLower()) >= 0)
							matchingFieldsCounter++;

						// Match day phone with evening phone
						if (duplicateLeads[i].DayPhone.ToLower().IndexOf(this.EveningPhone.ToLower()) >= 0 ||
							this.EveningPhone.ToLower().IndexOf(duplicateLeads[i].DayPhone.ToLower()) >= 0)
							matchingFieldsCounter++;
					}

					// Match zip code
					if (duplicateLeads[i].ZipCode != null && this.ZipCode != null)
						if (duplicateLeads[i].ZipCode.ToLower() == this.ZipCode.ToLower())
							matchingFieldsCounter++;

					// Match email
					if (duplicateLeads[i].Email != null && this.Email != null)
						if (duplicateLeads[i].Email.ToLower() == this.Email.ToLower())
							matchingFieldsCounter++;

					// Match street address
					if (duplicateLeads[i].StreetAddress != null && this.StreetAddress != null)
						if (duplicateLeads[i].StreetAddress.ToLower().IndexOf(this.StreetAddress.ToLower()) >= 0 ||
							this.StreetAddress.ToLower().IndexOf(duplicateLeads[i].StreetAddress.ToLower()) >= 0)
							matchingFieldsCounter++;


					// If number of matches is higher than last max, set this as our new lead.
					if (matchingFieldsCounter > maxMatchingFields)
					{
						duplicateLead = duplicateLeads[i];
						maxMatchingFields = matchingFieldsCounter;
					}
				}
			}
			else if (duplicateLeads.Count == 1)
			{
				duplicateLead = duplicateLeads[0];
			}


			if (this.PartnerID < 0)
			{
				Promotion promo = Promotion.GetPromotion(this.promotionID);
				if (promo != null)
					this.PartnerID = promo.PartnerId;
			}
						
			// Handle special case for GoFundraising, Fundraising.com and FastFundraising.com (same as Fundraising.com)
			// Pepsi/Coke approach implementation, whatever that means
			if (this.PartnerID == GoFundraisingPartnerId)
			{
				if (duplicateLead != null && duplicateLead.PartnerID == GoFundraisingPartnerId)
					return duplicateLead;
				else
					return null;
			}
			else if (this.PartnerID == FundraisingComPartnerId)
			{
				if (duplicateLead != null && duplicateLead.PartnerID == FundraisingComPartnerId)
					return duplicateLead;
				else
					return null;
			}
			else
			{
				if (duplicateLead != null && duplicateLead.PartnerID == FundraisingComPartnerId)
					return null;
				else
					return duplicateLead;
			}
		}

		public static LeadCollection GetMatchingLeads(string firstName, string lastName, string street, string zipCode, string dayPhone, string eveningPhone, string email)
		{
            //EFundDatabase dbo = new EFundDatabase();
            //return dbo.GetMatchingLeads(firstName, lastName, street, zipCode, dayPhone, eveningPhone, email);
            efundraising.EFundraisingCRM.DataAccess.EFundraisingCRMDatabase dbo = new efundraising.EFundraisingCRM.DataAccess.EFundraisingCRMDatabase();
            EFundraisingCRM.LeadCollection temp = dbo.GetMatchingLeads(firstName, lastName, street, zipCode, dayPhone, eveningPhone, email);
            if (temp != null)
            {
                efundraisingCore.LeadCollection leadOutput = new LeadCollection();
                foreach (EFundraisingCRM.Lead l in temp)
                {
                    efundraisingCore.Lead output = (efundraisingCore.Lead)l;
                    leadOutput.Add(output);
                }

                return leadOutput;
            }
            else
            {
                return null;
            }
		}

        public static Lead GetLeadById(int leadID)
        {
            efundraising.EFundraisingCRM.DataAccess.EFundraisingCRMDatabase dbo = new efundraising.EFundraisingCRM.DataAccess.EFundraisingCRMDatabase();
            
            return (efundraisingCore.Lead)dbo.GetLeadByID(leadID);
      
        
        }


        // TODO Newsletter - GenerateMailingList



        // TODO UpdateNewLead() for 2-step sample kit
        public void UpdateNewLead()
        {
            efundraising.EFundraisingCRM.DataAccess.EFundraisingCRMDatabase dbo = new efundraising.EFundraisingCRM.DataAccess.EFundraisingCRMDatabase();
            dbo.UpdateLead(this.LeadID, this.LeadStatusID, int.MinValue, int.MinValue, this.TempLeadID, 1, 
            this.PromotionID, null, this.ConsultantID, this.GroupTypeID, this.OrganizationTypeID, 6, int.MinValue,
            int.MinValue, int.MinValue, int.MinValue, 99, 99, int.MinValue, 0, 1,
            int.MinValue, null, this.FirstName, this.LastName, null, this.StreetAddress, this.City, this.State, this.Country,
            this.ZipCode, this.DayPhone, this.bestTimeToCall, this.EveningPhone, this.BestTimeToCallAlt, null, this.Email,
            DateTime.MinValue, int.MinValue, this.participantCount, int.MinValue, DateTime.MinValue, false, false,
            DateTime.MinValue,DateTime.MinValue ,this.onEmailList, false, false, this.Comments, false, false,
            DateTime.MinValue, DateTime.MinValue, this.productsInterest,false, this.DayPhoneExt, this.EveningPhoneExt, null, 
            null, int.MinValue, DateTime.MinValue, null, DateTime.MinValue, false, false, int.MinValue, false, null, DateTime.MinValue,
            null, int.MinValue, int.MinValue, int.MinValue, this.AddressZoneId, this.FundraisersPerYear, this.BestTimeToCallAlt);
     
        
        }

        //TODO EFundraisingCRM.DataAccess insert new lead
        public void InsertNewLead()
		{
            //EFundDatabase dbo = new EFundDatabase();
            //dbo.InsertNewLead(this);
            efundraising.EFundraisingCRM.DataAccess.EFundraisingCRMDatabase dbo = new efundraising.EFundraisingCRM.DataAccess.EFundraisingCRMDatabase();
            this.LeadID = dbo.InsertNewLead(this.FirstName, this.LastName, this.Email, this.StreetAddress, this.City, this.State, this.ZipCode, this.Country,
                this.FundraisingDate, this.DayPhone, this.EveningPhone, this.DayPhoneExt, this.EveningPhoneExt, this.GroupName, this.Title,
                this.BestTimeToCall, this.ProductsInterest, this.Comments, this.PartnerName, this.LeadVisitID, this.LeadStatusID,
                this.OrganizationTypeID, this.GroupTypeID, this.ParticipantCount, this.ConsultantID, this.PromotionID, this.PartnerID,
                this.IsConsultantActive, this.OnEmailList, this.DecisionMaker, this.isPostalAddressValidated, this.TempLeadID,
                this.FundraisersPerYear, this.AddressZoneId, this.BestTimeToCallAlt, this.GroupWebsite);
		}

		public void InsertTempLead()
		{
			EFundDatabase dbo = new EFundDatabase();
			dbo.InsertTempLead(this);
		}

		public void InsertLeadVisit()
		{
            //EFundDatabase dbo = new EFundDatabase();
            //dbo.InsertLeadVisit(this);
            efundraising.EFundraisingCRM.DataAccess.EFundraisingCRMDatabase dbo = new efundraising.EFundraisingCRM.DataAccess.EFundraisingCRMDatabase();
            this.leadVisitID = dbo.InsertLeadVisit(this.LeadID, this.PromotionID, this.TempLeadID);


		}

        public void InsertLeadActivity(LeadActivityType activityType)
		{
            //EFundDatabase dbo = new EFundDatabase();
            //dbo.InsertLeadActivity(this, activityType);
            efundraising.EFundraisingCRM.DataAccess.EFundraisingCRMDatabase dbo = new efundraising.EFundraisingCRM.DataAccess.EFundraisingCRMDatabase();
            dbo.InsertLeadActivity(this.LeadID, (int)activityType, this.comments);
		}
		
		public void UnassignLead(int userId)
		{
            //EFundDatabase dbo = new EFundDatabase();
            //dbo.UnassignLead(this, userId);
            efundraising.EFundraisingCRM.DataAccess.EFundraisingCRMDatabase dbo = new efundraising.EFundraisingCRM.DataAccess.EFundraisingCRMDatabase();
            dbo.UnassignLead(this.LeadID, this.ConsultantID, userId);
		}

		public void UnassignLead()
		{
            //EFundDatabase dbo = new EFundDatabase();
            //dbo.UnassignLead(this, WebServerConsultantId);
            efundraising.EFundraisingCRM.DataAccess.EFundraisingCRMDatabase dbo = new efundraising.EFundraisingCRM.DataAccess.EFundraisingCRMDatabase();
            dbo.UnassignLead(this.LeadID, this.ConsultantID, WebServerConsultantId);
		}

        //TODO Insert admin url into lead comment field
        public void InsertLeadAdvertisingComment(int id, string comments)
        {
            efundraising.EFundraisingCRM.DataAccess.EFundraisingCRMDatabase dbo = new efundraising.EFundraisingCRM.DataAccess.EFundraisingCRMDatabase();
            dbo.InsertLeadAdvertisingComment(id, comments);
        }

        #endregion

        #region Explicit Conversion
        
        // By PT: Since presently we have 3 diffrent Lead classes (EFundraisingCRM.Lead, efundraisingCore.Lead, efundraising.Lead) classes,
        // the explicit conversin operators were added.
        // TODO 3 diffrent Lead classes (EFundraisingCRM.Lead, efundraisingCore.Lead, efundraising.Lead)
        public static explicit operator Lead(EFundraisingCRM.Lead crmLead)
        {
            Lead coreLead = new Lead();
            coreLead.LeadID = crmLead.LeadId;
            coreLead.FirstName = crmLead.FirstName;
            coreLead.LastName = crmLead.LastName;
            coreLead.Email = crmLead.Email;
            coreLead.StreetAddress = crmLead.StreetAddress;
            coreLead.City = crmLead.City;
            coreLead.State = crmLead.StateCode;
            coreLead.ZipCode = crmLead.ZipCode;
            coreLead.Country = crmLead.CountryCode;
            coreLead.FundraisingDate = crmLead.FundRaiserStartDate.ToShortDateString();
            coreLead.DayPhone = crmLead.DayPhone;
            coreLead.EveningPhone = crmLead.EveningPhone;
            coreLead.DayPhoneExt = crmLead.DayPhoneExt;
            coreLead.EveningPhoneExt = crmLead.EveningPhoneExt;
            coreLead.GroupName = crmLead.Organization;
            coreLead.Title = crmLead.Salutation;
            coreLead.BestTimeToCall = crmLead.DayTimeCall;
            coreLead.ProductsInterest = crmLead.Interests;
            coreLead.Comments = crmLead.Comments;
            coreLead.LeadStatusID = crmLead.LeadStatusId;
            coreLead.OrganizationTypeID = crmLead.OrganizationTypeId;
            coreLead.GroupTypeID = (byte)crmLead.GroupTypeId;
            coreLead.ParticipantCount = crmLead.ParticipantCount;
            coreLead.ConsultantID = crmLead.ConsultantId;
            coreLead.PromotionID = crmLead.PromotionId;
            coreLead.OnEmailList = crmLead.Onemaillist;
            coreLead.DecisionMaker = crmLead.DecisionMaker;
            coreLead.TempLeadID = crmLead.TempLeadId;
            coreLead.FundraisersPerYear = crmLead.FundraisersPerYear;
            coreLead.AddressZoneId = crmLead.AddressZoneId;
            EFundraisingCRM.Consultant temp = EFundraisingCRM.Consultant.GetConsultantByID(crmLead.ConsultantId);
            if (temp != null)
            {
                if (temp.IsActive == 1)
                {
                    coreLead.isConsultantActive = true;
                }
                else
                {
                    coreLead.isConsultantActive = false;
                }
            }
            else
            {
                coreLead.isConsultantActive = true;
            }
            coreLead.PartnerID = crmLead.PartnerID; // 6/2/10: Added by JH 
            return coreLead;
        }
        
        #endregion

    }
}
    