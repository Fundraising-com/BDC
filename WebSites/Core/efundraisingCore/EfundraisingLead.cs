using System;
using System.Web;
using System.Web.UI;
using System.Web.SessionState;
using GA.BDC.Core.Diagnostics;
using GA.BDC.Core.Email;

namespace GA.BDC.Core.efundraisingCore
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

        public EfundraisingLead(string firstName, string lastName, string email, string streetAddress, string city,
            string state, string zipCode, string country, string groupName, string fundraisingDate, 
            string dayPhone, string eveningPhone, string dayPhoneExt, string eveningPhoneExt, 
            int organizationTypeID, byte groupTypeID, int participantCount, int consultantID, int promotionID,
            string title, string bestTimeToCall, bool decisionMaker, string productsInterest, bool onEmailList, 
            string comments, int leadStatusID, int KitType)
            : base(firstName, lastName, email, streetAddress, city,
            state, zipCode, country, groupName, fundraisingDate,
            dayPhone, eveningPhone, dayPhoneExt, eveningPhoneExt,
            organizationTypeID, groupTypeID, participantCount, consultantID, promotionID,
            title, bestTimeToCall, decisionMaker, productsInterest, onEmailList,
            comments, leadStatusID, KitType)
        { }

		#endregion

		#region Methods
		
        
        public bool Integrate()
		{
            return Integrate(LeadActivityType.CallBack, false);
		}

        public bool Integrate(bool isFromStore)
		{
            return Integrate(LeadActivityType.CallBack, true);
		}
        /// <summary>
        /// 
        /// </summary>
        /// <param name="activityType"></param>
        /// <param name="isFromStore"></param>
        /// <returns>true if the Lead is duplicated</returns>
        public bool Integrate(LeadActivityType activityType, bool isFromStore)
		{
            var isLeadDuplicated = false;
			try 
			{
				Lead duplicateLead = this.MatchLead();

                //hold seesion variable for if dup is found
                if (HttpContext.Current.Session != null)
			    {
                    HttpContext.Current.Session["IsDuplicate"] = "false";    
			    }
                

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
                                {
                                    activityComments += "Group Name : " + this.GroupName + ", ";
                                    //Consultant fc = Consultant.GetConsultant(this.ConsultantID);
                                    //this.SendRepeatNotificationOrganization(fc.EmailAddress);
                                }
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
                        
						activityComments += "Comments : " + this.Comments;
													
						// Insert activity
                        LeadActivity leadActivity = new LeadActivity(this.LeadID, (int)LeadActivityType.FirstCall, activityComments);
						leadActivity.InsertLeadActivity();

                        //Update lead info in db if email and phone are the same
                        //holding seesion for lead dup issues (an extra layer for verifing is dup found)
                        if (HttpContext.Current.Session != null)
                        {
                            HttpContext.Current.Session["IsDuplicate"] = "false";
                        }
                        try
                        {
                            if (this.Email.ToLower() == duplicateLead.Email.ToLower())
                            {
                                if (this.DayPhone.ToLower() == duplicateLead.DayPhone.ToLower())
                                {
                                    this.duplicateLeadUpdate(duplicateLead.LeadID);
                                    this.SendRepeatNotificationLeadUpdated();
                                    if (HttpContext.Current.Session != null)
                                    {
                                        HttpContext.Current.Session["IsDuplicate"] = "True";
                                    }
                                    Logger.LogError("efundraisinglead.cs phone dup verification 1");
                                }
                                else if (this.EveningPhone.ToLower() == duplicateLead.EveningPhone.ToLower())
                                {
                                    this.duplicateLeadUpdate(duplicateLead.LeadID);
                                    this.SendRepeatNotificationLeadUpdated();
                                    if (HttpContext.Current.Session != null)
                                    {
                                        HttpContext.Current.Session["IsDuplicate"] = "True";
                                    }
                                    Logger.LogError("efundraisinglead.cs phone dup verification 2");
                                }
                                else if (this.DayPhone.ToLower() == duplicateLead.EveningPhone.ToLower())
                                {
                                    this.duplicateLeadUpdate(duplicateLead.LeadID);
                                    this.SendRepeatNotificationLeadUpdated();
                                    if (HttpContext.Current.Session != null)
                                    {
                                        HttpContext.Current.Session["IsDuplicate"] = "True";
                                    }
                                    Logger.LogError("efundraisinglead.cs phone dup verification 3");
                                }
                                
                                else if (this.EveningPhone.ToLower() == duplicateLead.DayPhone.ToLower())
                                {
                                    this.duplicateLeadUpdate(duplicateLead.LeadID);
                                    this.SendRepeatNotificationLeadUpdated();
                                    if (HttpContext.Current.Session != null)
                                    {
                                        HttpContext.Current.Session["IsDuplicate"] = "True";
                                    }
                                    Logger.LogError("efundraisinglead.cs phone dup verification 4");
                                }
                            }
                         }
                        catch { }

					}
					else
					{
						// Unassign the lead
						this.UnassignLead();	
					}					

					try 
					{
                        isLeadDuplicated = true;
						// Send email to notify repeat customer.
                        if (isFromStore && this.ConsultantID != 3450)
                        {
                            Consultant fc = Consultant.GetConsultant(this.ConsultantID);
                            if (fc != null)
                            {
                                if (fc.IsActive && fc.EmailAddress != null)
                                {
                                    this.SendRepeatNotificationStore(fc.EmailAddress);
                                    if (HttpContext.Current.Session != null)
                                    {
                                        HttpContext.Current.Session["IsDuplicate"] = "True";
                                    }
                                    Logger.LogError("efundraisinglead.cs send repeat email verification 1");
                                }
                                else
                                {
                                    this.SendRepeatNotificationStore(fc.EmailAddress);
                                    if (HttpContext.Current.Session != null)
                                    {
                                        HttpContext.Current.Session["IsDuplicate"] = "True";
                                    }
                                    Logger.LogError("efundraisinglead.cs send repeat email verification 2");
                                }

                            }
                            else
                            {
                                this.SendRepeatNotificationStore(fc.EmailAddress);
                                if (HttpContext.Current.Session != null)
                                {
                                    HttpContext.Current.Session["IsDuplicate"] = "True";
                                }
                                Logger.LogError("efundraisinglead.cs send repeat email verification 3");
                            }
                        }
                        else
                        {
                            Consultant fc = Consultant.GetConsultant(this.ConsultantID);
                            this.SendRepeatNotification(fc.EmailAddress);
                            if (HttpContext.Current.Session != null)
                            {
                                HttpContext.Current.Session["IsDuplicate"] = "True";
                            }
                            Logger.LogError("efundraisinglead.cs send repeat email verification 4");
                        }
					}
					catch (Exception ex){

                        Logger.LogError("efundraisinglead.cs send repeat email verification catch: ", ex);
                    }
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
            return isLeadDuplicated;
		}


        public void IntegrateWithFC( int FCID)
        {
            try
            {
                this.InsertNewLeadWihtFCID(FCID);
                this.InsertLeadVisit();
                   
            }
            catch (Exception ex)
            {
                Logger.LogError(ex);
            }
        }

		/// <summary>
		/// Send email to notify repeat customer.
		/// </summary>
        private void SendRepeatNotificationOrganization(string fcEmail)
        {
            string subject = "Freekit - Repeat Customer Organization has changed";
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
GroupName              : " + GroupName;

#if DEBUG
            for (int i = 0; i < GA.BDC.Core.Configuration.ApplicationSettings.GetConfig().GetCount("EFundraisingWeb.Leads.Report"); i++)
            {
                GA.BDC.Core.Email.SendMail.AsyncSend(GA.BDC.Core.Configuration.ApplicationSettings.GetConfig()["Common.Email.SmtpServer", "host"],
                    "services@fundraising.com", GA.BDC.Core.Configuration.ApplicationSettings.GetConfig()["EFundraisingWeb.Leads.Report", i, "email"],
                    null, "", "", "", subject, body, "");
            }
            
            
            //SendMail.AsyncSend(Config.SmtpServer,
            //    "service@efundraising.com", "jason.farrell@fundraising.com",
            //    "", "", "", "", subject, body, "");
#else		
            for (int i = 0; i < GA.BDC.Core.Configuration.ApplicationSettings.GetConfig().GetCount("EFundraisingWeb.Leads.Report"); i++)
            {
                GA.BDC.Core.Email.SendMail.AsyncSend(GA.BDC.Core.Configuration.ApplicationSettings.GetConfig()["Common.Email.SmtpServer", "host"],
                    "services@fundraising.com", GA.BDC.Core.Configuration.ApplicationSettings.GetConfig()["EFundraisingWeb.Leads.Report", i, "email"],
                    null, "", "", "", subject, body, "");
            }
#endif
        }

        /// <summary>
        /// Send email to notify repeat customer.
        /// </summary>
        private void SendRepeatNotificationLeadUpdated()
        {
            string subject = "Repeat Customer Lead Information has been update in database";
            //if (IsConsultantActive)
            //    subject += "Active";
            //else
            //    subject += "** Inactive **";

            //subject += " Consultant Notification";

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
DayPhone               : " + DayPhone + " " + DayPhoneExt + @"
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
            for (int i = 0; i < GA.BDC.Core.Configuration.ApplicationSettings.GetConfig().GetCount("EFundraisingWeb.Leads.Report"); i++)
            {
                GA.BDC.Core.Email.SendMail.AsyncSend(GA.BDC.Core.Configuration.ApplicationSettings.GetConfig()["Common.Email.SmtpServer", "host"],
                    "services@fundraising.com", GA.BDC.Core.Configuration.ApplicationSettings.GetConfig()["EFundraisingWeb.Leads.Report", i, "email"],
                    null, "", "", "", subject, body, "");
            }
#else		
            for (int i = 0; i < GA.BDC.Core.Configuration.ApplicationSettings.GetConfig().GetCount("EFundraisingWeb.Leads.Report"); i++)
            {
                GA.BDC.Core.Email.SendMail.AsyncSend(GA.BDC.Core.Configuration.ApplicationSettings.GetConfig()["Common.Email.SmtpServer", "host"],
                    "services@fundraising.com", GA.BDC.Core.Configuration.ApplicationSettings.GetConfig()["EFundraisingWeb.Leads.Report", i, "email"],
                    null, "", "", "", subject, body, "");
            }
#endif
        }    
        
        
        
        
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
            for (int i = 0; i < GA.BDC.Core.Configuration.ApplicationSettings.GetConfig().GetCount("EFundraisingWeb.Leads.Report"); i++)
            {
                GA.BDC.Core.Email.SendMail.AsyncSend(GA.BDC.Core.Configuration.ApplicationSettings.GetConfig()["Common.Email.SmtpServer", "host"],
                    "services@fundraising.com", GA.BDC.Core.Configuration.ApplicationSettings.GetConfig()["EFundraisingWeb.Leads.Report", i, "email"],
                    null, "", "", "", subject, body, "");
            }
#else		
            for (int i = 0; i < GA.BDC.Core.Configuration.ApplicationSettings.GetConfig().GetCount("EFundraisingWeb.Leads.Report"); i++)
            {
                GA.BDC.Core.Email.SendMail.AsyncSend(GA.BDC.Core.Configuration.ApplicationSettings.GetConfig()["Common.Email.SmtpServer", "host"],
                    "services@fundraising.com", GA.BDC.Core.Configuration.ApplicationSettings.GetConfig()["EFundraisingWeb.Leads.Report", i, "email"],
                    null, "", "", "", subject, body, "");
            }
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
            for (int i = 0; i < GA.BDC.Core.Configuration.ApplicationSettings.GetConfig().GetCount("EFundraisingWeb.Leads.Report"); i++)
            {
                GA.BDC.Core.Email.SendMail.AsyncSend(GA.BDC.Core.Configuration.ApplicationSettings.GetConfig()["Common.Email.SmtpServer", "host"],
                    "services@fundraising.com", GA.BDC.Core.Configuration.ApplicationSettings.GetConfig()["EFundraisingWeb.Leads.Report", i, "email"],
                    null, "", "", "", subject, body, "");
            }
#else		
			for (int i = 0; i < GA.BDC.Core.Configuration.ApplicationSettings.GetConfig().GetCount("EFundraisingWeb.Leads.Report"); i++)
            {
                GA.BDC.Core.Email.SendMail.AsyncSend(GA.BDC.Core.Configuration.ApplicationSettings.GetConfig()["Common.Email.SmtpServer", "host"],
                    "services@fundraising.com", GA.BDC.Core.Configuration.ApplicationSettings.GetConfig()["EFundraisingWeb.Leads.Report", i, "email"],
                    null, "", "", "", subject, body, "");
            }
#endif
        }

		#endregion

	}	


}
