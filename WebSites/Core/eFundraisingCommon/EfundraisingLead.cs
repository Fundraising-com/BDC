using System;
using GA.BDC.Core.Diagnostics;
using GA.BDC.Core.Email;
using GA.BDC.Core.efundraisingCore;
namespace GA.BDC.Core.eFundraisingCommon
{
  
	/// <summary>
	/// Summary description for EfundraisingLead.
	/// </summary>
	[Serializable()]
	public class EfundraisingLead : Lead
	{
		#region Constructors
		public EfundraisingLead() : base()
		{

		}

        public EfundraisingLead(string firstName, string lastName, string email, string streetAddress, string city,
            string state, string zipCode, string country, string groupName, string fundraisingDate,
            string dayPhone, string eveningPhone, string dayPhoneExt, string eveningPhoneExt,
            int organizationTypeID, byte groupTypeID, int participantCount, int consultantID, int promotionID,
            string title, string bestTimeToCall, bool decisionMaker, string productsInterest, bool onEmailList,
            string comments, int leadStatusID)
            : base(firstName, lastName, email, streetAddress, city,
            state, zipCode, country, groupName, fundraisingDate,
            dayPhone, eveningPhone, dayPhoneExt, eveningPhoneExt,
            organizationTypeID, groupTypeID, participantCount, consultantID, promotionID,
            title, bestTimeToCall, decisionMaker, productsInterest, onEmailList,
            comments, leadStatusID)
        { }

		#endregion

		#region Methods
		
        
        public void Integrate()
		{
			Integrate(LeadActivityType.CallBack, false);
		}

		public void Integrate(bool isFromStore)
		{
			Integrate(LeadActivityType.CallBack, true);
		}

		public void Integrate(LeadActivityType activityType, bool isFromStore)
		{
			try 
			{
				Lead duplicateLead = this.MatchLead();

				if (duplicateLead != null)
				{
					// Sync our lead object
					this.LeadID = duplicateLead.LeadID;
					this.ConsultantID = duplicateLead.ConsultantID;
					this.IsConsultantActive = duplicateLead.IsConsultantActive;

					// Consultant is active
					if (this.IsConsultantActive)
					{
						// check if the lead's infos have changed since last submission
						string activityComments = "The following infos changed since last visit : ";
						if (this.PartnerID >= 0)
							if (this.PartnerID != duplicateLead.PartnerID)
							{
								Partner partner = Partner.GetPartnerInfoByID(this.PartnerID);
								activityComments += "Partner : " + partner.PartnerName + ", ";
							}
						if (this.PromotionID > 0)
							if (this.PromotionID != duplicateLead.PromotionID)
							{
								Promotion promo = Promotion.GetPromotion(this.PromotionID);
								activityComments += "Promotion : " + promo.Description + ", ";
							}
						try
						{
							if (duplicateLead.FirstName != null)
							{
								if (this.FirstName.ToLower() != duplicateLead.FirstName.ToLower())
									activityComments += "First Name : " + this.FirstName + ", ";
							}
							else
								activityComments += "First Name : " + this.FirstName + ", ";
						}
						catch {}
						try
						{
							if (duplicateLead.LastName != null)
							{
								if (this.LastName.ToLower() != duplicateLead.LastName.ToLower())
									activityComments += "Last Name : " + this.LastName + ", ";	
							}
							else
								activityComments += "Last Name : " + this.LastName + ", ";
						}
						catch {}
						try
						{
							
							if (duplicateLead.Email != null)
							{
								if (this.Email.ToLower() != duplicateLead.Email.ToLower())
									activityComments += "Email : " + this.Email + ", ";
							}
							else
								activityComments += "Email : " + this.Email + ", ";
						}
						catch {}
						try
						{
							if (duplicateLead.StreetAddress != null)
							{
								if (this.StreetAddress.ToLower() != duplicateLead.StreetAddress.ToLower())
									activityComments += "Address : " + this.StreetAddress + ", ";
							}
							else
								activityComments += "Address : " + this.StreetAddress + ", ";								
						}
						catch {}
						try
						{
							if (duplicateLead.City != null)
							{
								if (this.City.ToLower() != duplicateLead.City.ToLower())
									activityComments += "City : " + this.City + ", ";
							}
							else
								activityComments += "City : " + this.City + ", ";
						}
						catch {}
						try
						{
							if (duplicateLead.State != null)
							{
								if (this.State.ToLower() != duplicateLead.State.ToLower())
									activityComments += "State : " + this.State + ", ";
							}
							else
								activityComments += "State : " + this.State + ", ";	
						}
						catch {}
						try
						{
							if (duplicateLead.Country != null)
							{
								if (this.Country.ToLower() != duplicateLead.Country.ToLower())
									activityComments += "Country : " + this.Country + ", ";
							}
							else
								activityComments += "Country : " + this.Country + ", ";								
						}
						catch {}
						try
						{
							if (duplicateLead.DayPhone != null)
							{
								if (this.DayPhone.ToLower() != duplicateLead.DayPhone.ToLower())
									activityComments += "Day Phone : " + this.DayPhone + ", ";
							}
							else
								activityComments += "Day Phone : " + this.DayPhone + ", ";
						}
						catch {}
						try
						{
							if (duplicateLead.DayPhoneExt != null)
							{
								if (this.DayPhoneExt.ToLower() != duplicateLead.DayPhoneExt.ToLower())
									activityComments += "Day Phone Ext : " + this.DayPhoneExt + ", ";
							}
							else
								activityComments += "Day Phone Ext : " + this.DayPhoneExt + ", ";
						}
						catch {}
						try
						{
							if (duplicateLead.EveningPhone != null)
							{
								if (this.EveningPhone.ToLower() != duplicateLead.EveningPhone.ToLower())
									activityComments += "Evening Phone : " + this.EveningPhone + ", ";
							}
							else
								activityComments += "Evening Phone : " + this.EveningPhone + ", ";
						}
						catch {}
						try
						{
							if (duplicateLead.EveningPhoneExt != null)
							{
								if (this.EveningPhoneExt.ToLower() != duplicateLead.EveningPhoneExt.ToLower())
									activityComments += "Evening Phone Ext : " + this.EveningPhoneExt + ", ";
							}
							else
								activityComments += "Evening Phone Ext : " + this.EveningPhoneExt + ", ";
						}
						catch {}
						try
						{
							if (this.BestTimeToCall != duplicateLead.BestTimeToCall)
								activityComments += "Best Time to Call : " + this.BestTimeToCall + ", ";
						}
						catch {}
						try
						{
							if (duplicateLead.GroupName != null)
							{
								if (this.GroupName.ToLower() != duplicateLead.GroupName.ToLower())
									activityComments += "Group Name : " + this.GroupName + ", ";
							}
							else
								activityComments += "Group Name : " + this.GroupName + ", ";
						}
						catch {}
						try
						{
							if (this.ParticipantCount != duplicateLead.ParticipantCount)
								activityComments += "Participant Count : " + this.ParticipantCount.ToString() + ", ";
						}
						catch {}
						try
						{
							if (this.DecisionMaker != duplicateLead.DecisionMaker)
								activityComments += "Decision Maker : " + this.DecisionMaker.ToString() + ", ";
						}
						catch {}
                        // Looks like we do not need this part due to new simplified kit page, so we omit the following part
                        //try
                        //{
                        //    if (duplicateLead.FundraisingDate != null)
                        //    {
                        //        if (this.FundraisingDate.ToLower() != duplicateLead.FundraisingDate.ToLower())
                        //            activityComments += "Fundraising Date : " + this.FundraisingDate + ", ";
                        //    }
                        //    else
                        //        activityComments += "Fundraising Date : " + this.FundraisingDate + ", ";
                        //}
                        //catch  {}
						activityComments += "Comments : " + this.Comments;
													
						// Insert activity
                        LeadActivity leadActivity = new LeadActivity(this.LeadID, (int)LeadActivityType.FirstCall, activityComments);
						leadActivity.InsertLeadActivity();
					}
					else
					{
						// Unassign the lead
						this.UnassignLead();	
					}					

					try 
					{
						// Send email to notify repeat customer.
                        if (isFromStore && this.ConsultantID != 3450)
                        {
                            Consultant fc = Consultant.GetConsultant(this.ConsultantID);
                            if (fc != null)
                                if (fc.IsActive && fc.EmailAddress != null)
                                    this.SendRepeatNotificationStore(fc.EmailAddress);
                                else
                                    this.SendRepeatNotificationStore(fc.EmailAddress);
                            else
                                this.SendRepeatNotificationStore(fc.EmailAddress);
                        }
                        else
                        {
                            Consultant fc = Consultant.GetConsultant(this.ConsultantID);
                            this.SendRepeatNotification(fc.EmailAddress);
                        }
					}
					catch {}
				}
				else
				{
					
					// Create a new lead
					this.InsertNewLead();
				}	

				// Log visit
				this.InsertLeadVisit();
			} 
			catch (Exception ex)
			{
                Logger.LogError(ex);
                throw ex;
				
			}
		}


		/// <summary>
		/// Send email to notify repeat customer.
		/// </summary>
        private void SendRepeatNotification(string fcEmail)
		{
			string subject = "Freekit - Repeat Customer ";
			if (IsConsultantActive)
				subject += "Active";
			else
				subject += "** Inactive **";

			subject += " Consultant Notification";

			string body = @"
PartnerID              : " + PartnerID + @"
PromotionID            : " + PromotionID + @"
LeadID                 : " + LeadID + @"
FirstName              : " + FirstName + @"
LastName               : " + LastName + @"
Title                  : " + Title + @"
Email                  : " + Email + @"
Addresss               : " + StreetAddress + @"
City                   : " + City + @"
State                  : " + State + @"
Country                : " + Country + @"
Zip                    : " + ZipCode + @"
DayPhone               : " + DayPhone + " " + DayPhoneExt +@"
EveningPhone           : " + EveningPhone + " " + EveningPhoneExt + @"
Best Time To Call      : " + BestTimeToCall + @"
Organization Type      : " + OrganizationTypeID + @" 
GroupName              : " + GroupName + @"
Number of Group Members: " + ParticipantCount + @"
Fundraising Date       : " + FundraisingDate + @"
Is Decision Maker      : " + DecisionMaker.ToString() + @"
Newsletter Opt-in      : " + OnEmailList.ToString() + @"
Comments               : " + Comments;

#if DEBUG
			SendMail.AsyncSend(Config.SmtpServer,
                "service@efundraising.com", "jason.farrell@fundraising.com", 
				"", "", "", "", subject, body, "");
#else		
            SendMail.AsyncSend(Config.SmtpServer, "service@efundraising.com", fcEmail, "xavier_desaunettes@fundraising.com", "jason_farrell@fundraising.com", "", "", subject, body, "");
#endif
        }


		/// <summary>
		/// Send email to FC notify repeat customer.
		/// </summary>
		private void SendRepeatNotificationStore(string fcEmail)
		{
			string subject = "Freekit - Repeat Customer ";
			
			string body = @"The following lead is assigned to you and just made a purchase on the online store :

PartnerID              : " + PartnerID + @"
PromotionID            : " + PromotionID + @"
LeadID                 : " + LeadID + @"
FirstName              : " + FirstName + @"
LastName               : " + LastName + @"
Title                  : " + Title + @"
Email                  : " + Email + @"
Addresss               : " + StreetAddress + @"
City                   : " + City + @"
State                  : " + State + @"
Country                : " + Country + @"
Zip                    : " + ZipCode + @"
DayPhone               : " + DayPhone + " " + DayPhoneExt +@"
EveningPhone           : " + EveningPhone + " " + EveningPhoneExt + @"
Best Time To Call      : " + BestTimeToCall + @"
Organization Type      : " + OrganizationTypeID + @" 
GroupName              : " + GroupName + @"
Number of Group Members: " + ParticipantCount + @"
Fundraising Date       : " + FundraisingDate + @"
Is Decision Maker      : " + DecisionMaker.ToString() + @"
Newsletter Opt-in      : " + OnEmailList.ToString() + @"
Comments               : " + Comments;

#if DEBUG
			SendMail.AsyncSend(Config.SmtpServer,
                "service@efundraising.com", "jason.farrell@fundraising.com", 
				"", "", "", "", subject, body, "");
#else		
			SendMail.AsyncSend(Config.SmtpServer, 
				"service@efundraising.com", fcEmail, 
				"efr-sale-support@fundraising.com", "jason_farrell@fundraising.com", "", "", subject, body, "");
#endif
        }

		#endregion

	}	


}
