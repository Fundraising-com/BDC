using System;
using GA.BDC.Core.Email;
using GA.BDC.Core.ESubsGlobal.Common;
using GA.BDC.Core.ESubsGlobal.DataAccess;
using GA.BDC.Core.ESubsGlobal.Payment;
using GA.BDC.Core.ESubsGlobal.Users;
using GA.BDC.Core.efundraisingCore;

namespace GA.BDC.Core.ESubsGlobal
{
	/// <summary>
	/// Summary description for ESubsLead.
	/// </summary>
	public class ESubsLead : Lead
	{
		#region Fields
		private Group rootGroup = null;
		private Sponsor sponsor = null;
		private Partner partner = null;
		private PaymentInfo paymentInfo = null;
		#endregion

		#region Constructors
        public ESubsLead(Group group, Partner partner, Promotion promo) : base()
		{
			// Get the root group because we are only interested in monitoring the root group.
			this.rootGroup = Group.LoadRootGroupByGroupID(group.GroupID);
			this.partner = partner;
			this.sponsor = Sponsor.LoadByHierarchyID(rootGroup.SponsorID);
			this.paymentInfo = PaymentInfo.LoadPaymentInfoBySponsorID(sponsor.HierarchyID);

			if (promo != null)
				this.PromotionID = promo.PromotionID;

			this.GroupName = rootGroup.Name;
			this.FirstName = sponsor.FirstName;
			this.LastName = sponsor.LastName;
				
			if (paymentInfo != null)
			{
				// Get street
				if (paymentInfo.PostalAddress != null)
				{
					this.StreetAddress = paymentInfo.PostalAddress.Address1;
					this.City = paymentInfo.PostalAddress.City;
					this.State = paymentInfo.PostalAddress.SubDivisionCode;
					this.ZipCode = paymentInfo.PostalAddress.ZipCode;
					this.Country = (string) paymentInfo.PostalAddress.CountryCode;
				}				

				// Get phone numbers
				if (paymentInfo.PhoneNumber != null)
				{
					if (paymentInfo.PhoneNumber.PhoneNumberTypeID == PhoneNumberType.DAY_PHONE)
						this.DayPhone = paymentInfo.PhoneNumber.FormattedPhoneNumber;
					else if (paymentInfo.PhoneNumber.PhoneNumberTypeID == PhoneNumberType.EVENING_PHONE)
						this.EveningPhone = paymentInfo.PhoneNumber.FormattedPhoneNumber;
				}
			}

			// Get email
			if (sponsor.EmailAddress != null)
				this.Email = sponsor.EmailAddress.Email;
		}

		#endregion

		#region Methods

        public static new efundraisingCore.Lead GetLeadById(int lead_id)
        {
            return Lead.GetLeadById(lead_id);
        }

        public void Integrate()
        {
            Integrate(true);
        }

		public void Integrate(bool insertNewLeads)
		{
			if (sponsor != null)
			{			
				// Find duplicate lead that best matches the profile.
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
						// Insert activity
                        this.InsertLeadActivity(LeadActivityType.CallBack);

                        Consultant consultant = Consultant.GetConsultant(this.ConsultantID);
                        NotifyConsultant(consultant);
					}
					else
					{
						// Unassign the lead
						this.UnassignLead();

						// Insert activity
						this.InsertLeadActivity(LeadActivityType.MagfundraisingStep1Completed);	
					}				
				}	
				else
				{
                    if (insertNewLeads)
                    {
                        // Insert new lead
                        this.InsertNewLead();
                    }
				}

                if (this.LeadID > 0)
                {
                    // Log visit
                    this.InsertLeadVisit();

                    // Update lead information in group
                    rootGroup.LeadID = this.LeadID;
                    rootGroup.UpdateInDatabase();

                    // Update lead information in member also
                    sponsor.LeadID = this.LeadID;
                    sponsor.UpdateInDatabase();
                    sponsor.UpdateUserInDatabase();
                }
			
			}
		}

        /// <summary>
		/// Send email to notify repeat customer.
		/// </summary>
		private void NotifyConsultant(Consultant consultant)
		{
			if(consultant.ConsultantId == 0) {
				consultant.EmailAddress = "efr-qa@rd.com";
			}
			// Subject
			string subject = "*** [partner_name] SELF-REGISTER LEADS #[lead_id] *** ";
			subject = subject.Replace("[partner_name]", partner.Name);
			subject = subject.Replace("[lead_id]", this.LeadID.ToString());

			// Message body
			string body = @"*** [partner_name] SELF-REGISTER LEADS #[lead_id]  ***
-------------------------------------------------------------------------
Hi [Consultant_Name],
This is an automated message.  This is to notify you that an existing lead has created a campaign through the eSubs program.


Name            : [Lead_Name]
Group Name      : [Organization]
Address         : [Address]
City            : [City]
Day Phone       : [Phone]
Evening Phone   : [Phone]
Email           : [EmailAddress]

# of Participant: [Participant_Count]

";			
			body = body.Replace("[lead_id]", this.LeadID != int.MinValue ? this.LeadID.ToString() : "");
			body = body.Replace("[partner_name]", this.partner.Name);
			body = body.Replace("[Consultant_Name]", consultant.Name);
			body = body.Replace("[Lead_Name]", this.sponsor.CompleteName);
			body = body.Replace("[Organization]", this.rootGroup.Name);
			body = body.Replace("[Address]", this.StreetAddress);
			body = body.Replace("[City]", this.City);
			body = body.Replace("[Phone]", this.DayPhone);
			body = body.Replace("[Phone2]", this.EveningPhone);
			body = body.Replace("[Participant_Count]", this.ParticipantCount != int.MinValue ? this.ParticipantCount.ToString() : "");
			body = body.Replace("[EmailAddress]", this.Email);

#if DEBUG
			// Send email
			SendMail.AsyncSend(Config.SmtpServer, "LeadIntegrator@efundraising.com", consultant.EmailAddress, "", "", "", "", subject, body, "");

			// Send email to responsible persons
			SendMail.AsyncSend(AppConfig.SmtpServer, "LeadIntegrator@efundraising.com",  "jiro.hidaka@fundraising.com", "", "", "", "", subject, body, "");
#else
			
			// Send email
			SendMail.AsyncSend(Config.SmtpServer, "LeadIntegrator@efundraising.com", consultant.EmailAddress, "", "", "", "", subject, body, "");

			// Send email to responsible persons
			foreach (string email in AppConfig.ResponsibleLeadsEmails)
			{

				SendMail.AsyncSend(AppConfig.SmtpServer, "LeadIntegrator@efundraising.com", email, "", "", "", "", subject, body, "");
			}
#endif
		}
		#endregion
	}
}
