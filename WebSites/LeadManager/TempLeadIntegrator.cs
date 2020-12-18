//
// 2006-07-05 : Maxime Normand - New class.
//

using System;
using System.Threading;
using GA.BDC.Core.Collections;
using GA.BDC.Core.Configuration;
using GA.BDC.Core.Email;
using GA.BDC.Core.Diagnostics;
using GA.BDC.Core.ESubsGlobal;
using GA.BDC.Core.efundraisingCore;
//using GA.BDC.Core.EFundraisingCRM;




namespace efundraising.LeadManager
{
	/// <summary>
	/// Take any new Leads from the Temp_Lead table and integrate them in the master Lead table.
	/// </summary>
	public class TempLeadIntegrator
	{
		bool serviceIsActive = false;
		
		public TempLeadIntegrator()
		{
	    }
		
		#region Public Methods
		
		public void Start()
		{
			serviceIsActive = true;
			
			while (serviceIsActive)
			{
				try
				{
					TempLeadCollection tempLeadCollection = GA.BDC.Core.efundraisingCore.TempLead.GetNewTempLeads();
                    if (tempLeadCollection != null)
                    {
                        for (int i = 0; i < tempLeadCollection.Count; i++)
                        {
                            try
                            {
                                // Create a new eFundraisingLead from the current Temp Lead
                                EfundraisingLead lead = new EfundraisingLead();

                                lead.FirstName = tempLeadCollection[i].FirstName;
                                lead.LastName = tempLeadCollection[i].LastName;
                                lead.Email = tempLeadCollection[i].Email;
                                lead.StreetAddress = tempLeadCollection[i].StreetAddress;
                                lead.City = tempLeadCollection[i].City;

                                if (tempLeadCollection[i].StateCode != null)
                                {
                                    lead.State = tempLeadCollection[i].StateCode.ToUpper().Replace("US-", "");
                                }
                                else
                                {
                                    
                                    continue;
                                }
                                lead.ZipCode = tempLeadCollection[i].ZipCode;
                                lead.Country = tempLeadCollection[i].CountryCode;
                                lead.FundraisingDate = null;
                                lead.DayPhone = tempLeadCollection[i].DayPhone;
                                lead.EveningPhone = tempLeadCollection[i].EveningPhone;
                                lead.DayPhoneExt = tempLeadCollection[i].DayPhoneExt;
                                lead.EveningPhoneExt = tempLeadCollection[i].EveningPhoneExt;
                                lead.GroupName = tempLeadCollection[i].Organization;
                                lead.BestTimeToCall = tempLeadCollection[i].DayTimeCall;
                                lead.Comments = tempLeadCollection[i].Comments + " - Fundraising Start Date : " + tempLeadCollection[i].FundRaiserStartDate.ToString();
                                lead.LeadStatusID = tempLeadCollection[i].LeadStatusID;
                                lead.OrganizationTypeID = tempLeadCollection[i].OrganizationTypeID;
                                lead.GroupTypeID = (byte)tempLeadCollection[i].GroupTypeID;
                                lead.ParticipantCount = tempLeadCollection[i].ParticipantCount;
                                lead.ConsultantID = (tempLeadCollection[i].ConsultantID != int.MinValue && tempLeadCollection[1].ConsultantID != -1) ? tempLeadCollection[i].ConsultantID : 0;
                                lead.OnEmailList = tempLeadCollection[i].OnEmailList;
                                lead.DecisionMaker = tempLeadCollection[i].DecisionMaker;
                                lead.TempLeadID = tempLeadCollection[i].TempLeadID;
                                if (tempLeadCollection[i].PromotionID <= 0)
                                    lead.PromotionID = 10745;
                                else
                                    lead.PromotionID = tempLeadCollection[i].PromotionID;

                                lead.Integrate();

                                if (lead.LeadID != int.MinValue && lead.LeadID != -1)
                                {
                                    try
                                    {
                                        // after inserting a lead, insert the promotional kit
                                        // this promotional kit has to be inserted directly 
                                        // to the efundraising prod database through sql link server
                                        // the reason why there are no sql transaction is that
                                        // our link servers does not handle transactions

                                        // findout which kit type to send depending of the
                                        // lead arguments
                                       GA.BDC.Core.EFundraisingCRM.KitType kitType =
                                           GA.BDC.Core.EFundraisingCRM.KitType.GetProperKitTypeFromLeadInformation(
                                            lead.ConsultantID, GA.BDC.Core.EFundraisingCRM.LeadChannel.Internet.ChannelCode,
                                            lead.PromotionID, lead.PartnerID,
                                            lead.State, lead.Country);

                                        // create a postal address object with lead information
                                       GA.BDC.Core.EFundraisingCRM.PostalAddress postalAddress =
                                            new GA.BDC.Core.EFundraisingCRM.PostalAddress(
                                            int.MinValue, lead.StreetAddress, lead.City,
                                            lead.ZipCode, lead.Country,
                                            lead.Country + "-" + lead.State, DateTime.Now);

                                        // insert the postal address, if it failed, log 
                                        // and continue the process (will insert an invalid
                                        // promotional kit with no postal address id)
                                        try
                                        {
                                            postalAddress.Insert();
                                        }
                                        catch (System.Exception ex)
                                        {
                                            GA.BDC.Core.Diagnostics.Logger.LogWarn("Lead Manager failed to insert postal address: Lead ID: " + lead.LeadID, ex);
                                        }

                                        // create our promotional kit object 
                                       GA.BDC.Core.EFundraisingCRM.PromotionalKit promotionalKit =
                                            new GA.BDC.Core.EFundraisingCRM.PromotionalKit(
                                            int.MinValue, lead.LeadID, lead.LeadVisitID, kitType.KitTypeID,
                                            GA.BDC.Core.EFundraisingCRM.Carrier.RegularMail.CarrierId, int.MinValue,
                                            postalAddress.PostalAddressId, (postalAddress.PostalAddressId == int.MinValue ? 0 : 1), DateTime.Now, DateTime.MinValue);

                                        // insert the promotional kit
                                        promotionalKit.Insert();
                                    }
                                    catch (System.Exception ex)
                                    {
                                        // let it go anyway, the promotional kit manager service will insert it
                                        GA.BDC.Core.Diagnostics.Logger.LogWarn("Unable to insert promotional kit", ex);
                                    }

                                    // check for a eSubs group id, if present update the group info
                                    if (tempLeadCollection[i].GroupID > 0)
                                    {
                                        Group group = null;
                                        try
                                        {
                                            group = Group.LoadByGroupID(tempLeadCollection[i].GroupID);
                                            group.LeadID = lead.LeadID;
                                            group.UpdateInDatabase();
                                        }
                                        catch (Exception ex)
                                        {
                                            Logger.LogError("Error updating Group " + group.GroupID, ex);
                                        }
                                    }
                                    else
                                        SendConfirmationEmails(lead);

                                    // Flag the status of the Temp Lead as transferred to eFundraising Lead
                                    tempLeadCollection[i].IsNew = false;
                                    tempLeadCollection[i].Update();
                                }


                            }
                            catch (Exception ex)
                            {
                                Logger.LogError("Error in Temp Lead Integrator (Temp Lead) : " + tempLeadCollection[i].TempLeadID, ex);
                            }
                        }

                    }
                    else
                    {
                        throw new Exception("Failed to generate the temp lead collection");
                    }

					QSPLead[] qspLeads = QSPLead.GetQSPLeadsToTransfer();

					for (int i = 0; i < qspLeads.Length; i++)
					{		
						try
						{
							// Create a new eFundraisingLead from the current Temp Lead
							EfundraisingLead lead = new EfundraisingLead();
							lead.FirstName = qspLeads[i].FirstName;
							lead.LastName = qspLeads[i].LastName;
							lead.Email = qspLeads[i].Email;
							lead.StreetAddress = qspLeads[i].Address1 + " " + qspLeads[i].Address2;
							lead.City = qspLeads[i].City;
							lead.State = qspLeads[i].State;
							lead.ZipCode = qspLeads[i].Zip;
							lead.Country = "US";
							lead.FundraisingDate = null;
							if (qspLeads[i].DayPhone != null)
								if (qspLeads[i].DayPhone.Trim().Length == 10)
									lead.DayPhone = qspLeads[i].DayPhone.Trim().Substring(0, 3) + "-" + qspLeads[i].DayPhone.Trim().Substring(3, 3) + "-"  + qspLeads[i].DayPhone.Trim().Substring(6, 4);
								else
									lead.DayPhone = qspLeads[i].DayPhone;
							if (qspLeads[i].EveningPhone != null)
								if (qspLeads[i].EveningPhone.Trim().Length == 10)
									lead.EveningPhone = qspLeads[i].EveningPhone.Trim().Substring(0, 3) + "-" + qspLeads[i].EveningPhone.Trim().Substring(3, 3) + "-"  + qspLeads[i].EveningPhone.Trim().Substring(6, 4);
								else
									lead.EveningPhone = qspLeads[i].EveningPhone;
							lead.GroupName = qspLeads[i].Organization;
							lead.Comments = qspLeads[i].Comment + " - Fundraising Start Date : " + qspLeads[i].TimePeriod;
							lead.ConsultantID = 0;
							lead.OnEmailList = false;
							lead.DecisionMaker = false;
							lead.PromotionID = 681;
							lead.ParticipantCount = qspLeads[i].NoOfFundraisers;
							lead.Integrate();

							if(lead.LeadID != int.MinValue && lead.LeadID != -1) 
							{
								try 
								{
									// after inserting a lead, insert the promotional kit
									// this promotional kit has to be inserted directly 
									// to the efundraising prod database through sql link server
									// the reason why there are no sql transaction is that
									// our link servers does not handle transactions

									// findout which kit type to send depending of the
									// lead arguments
									GA.BDC.Core.EFundraisingCRM.KitType kitType =
                                       GA.BDC.Core.EFundraisingCRM.KitType.GetProperKitTypeFromLeadInformation(
                                        lead.ConsultantID, GA.BDC.Core.EFundraisingCRM.LeadChannel.Internet.ChannelCode, 
										lead.PromotionID, lead.PartnerID,
										lead.State, lead.Country);
						
									// create a postal address object with lead information
                                    GA.BDC.Core.EFundraisingCRM.PostalAddress postalAddress =
                                        new GA.BDC.Core.EFundraisingCRM.PostalAddress(
										int.MinValue, lead.StreetAddress, lead.City, 
										lead.ZipCode, lead.Country, 
										lead.Country + "-" + lead.State, DateTime.Now);

									// insert the postal address, if it failed, log 
									// and continue the process (will insert an invalid
									// promotional kit with no postal address id)
									try 
									{
										postalAddress.Insert();
									} 
									catch(System.Exception ex) 
									{
                                        GA.BDC.Core.Diagnostics.Logger.LogWarn("Lead Manager failed to insert postal address: Lead ID: " + lead.LeadID, ex);
									}

									// create our promotional kit object 
									GA.BDC.Core.EFundraisingCRM.PromotionalKit promotionalKit =
										new GA.BDC.Core.EFundraisingCRM.PromotionalKit(
										int.MinValue, lead.LeadID, lead.LeadVisitID, kitType.KitTypeID,
										GA.BDC.Core.EFundraisingCRM.Carrier.RegularMail.CarrierId, int.MinValue,
										postalAddress.PostalAddressId, (postalAddress.PostalAddressId == int.MinValue? 0: 1), DateTime.Now, DateTime.MinValue);

									// insert the promotional kit
									promotionalKit.Insert();
								} 
								catch(System.Exception ex) 
								{
									// let it go anyway, the promotional kit manager service will insert it
                                    GA.BDC.Core.Diagnostics.Logger.LogWarn("Unable to insert promotional kit", ex);
								}
								
							}
							
							QSPLead.InsertLeadGen_Lead(qspLeads[i].LeadGenId, lead.LeadID, DateTime.Now);

						}
						catch (Exception ex)
						{
							Logger.LogError("Error in Temp Lead Integrator (QSP) : " + qspLeads[i].LeadGenId, ex);
						}
					}
				
				}
				catch (Exception ex)
				{
					Logger.LogError(ex);
				}		
				
				// wait for 2 minutes
				Thread.Sleep(120000);
				
			}
		}
		
		public void Stop()
		{
			serviceIsActive = false;
		}
		
		#endregion
		
		#region Private Methods

        private void SendConfirmationEmails(GA.BDC.Core.efundraisingCore.Lead lead)
		{
			
			// Email the Client
			GA.BDC.Core.efundraisingCore.Promotion promo = GA.BDC.Core.efundraisingCore.Promotion.GetPromotion(lead.PromotionID);
			GA.BDC.Core.efundraisingCore.Partner partner = GA.BDC.Core.efundraisingCore.Partner.GetPartnerInfoByID(promo.PartnerId);
			string clientSubject = "Fundraising Info Request Received - Start Earning Profits Now!";
			
			string clientBody = @"
<!DOCTYPE HTML PUBLIC '-//W3C//DTD HTML 4.0 Transitional//EN'>
<HTML>
	<HEAD>
		<META http-equiv='content-type' content='text/html; charset=ISO-8859-1'>
	</HEAD>
	<BODY>
		<P><font face='Verdana' size='2'>Dear " + lead.FirstName + " " + lead.LastName + @", <BR>
				<BR>
				Thank you for your interest in " + partner.PartnerName + @"
				<BR>
				<BR>
				We received your request for a fundraising information kit and you should be 
				receiving it in the mail shortly.
				<BR>
				<BR>
				In the meantime, start making money NOW - at no cost to you - with our <A href='http://www.magfundraising.com/cmwizard.aspx?lid=" + lead.LeadID + @"&amp;ccid=27&amp;pId'>
					online fundraising program</A>!
				<BR>
				<BR>
				Friends and family across the United States can purchase or renew their 
				magazine subscriptions on your group's personalized online magazine store at 
				savings of up to 85% off newsstand prices. Your group keeps 40% profit from all 
				purchase amounts!
				<BR>
				<BR>
				SPECIAL OFFER: Get free shipping on your chocolate, frozen food or Scratchcard 
				order when you start an online fundraiser that generates at least one magazine 
				subscription.
				<br>
				<br>
				<B><A href='http://www.magfundraising.com/cmwizard.aspx?lid=" + lead.LeadID + @"&amp;ccid=27&amp;pId'>Start 
						your online fundraiser</A> and start earning money today!</B>
				<BR>
				Thank You!
				<BR>
				<BR>" + partner.PartnerName + @"
				<BR>
				<a href='http://" + partner.Url + @"' target='_blank'>" + partner.Url + @"</a>
				<BR>
				<BR>
				P.S. If you are ready to place your order or if you have any questions, please 
				call " + partner.PhoneNumber + @" and your fundraising consultant will be more than 
				happy to assist you.</font></P>
	</BODY>
</HTML>";

			SendMail.AsyncSend(ApplicationSettings.GetConfig()["Common.Email.SmtpServer", "host"], 
				"services@efundraising.com", lead.Email, 
				"", "", "", "", clientSubject, "", clientBody);

			
			// Email the the list of persons who receive the lead confirmations at eFr
			string subject = "A new lead has been added";
			
			string body = @"
PartnerID              : " + partner.PartnerName + @" 
PromotionID            : " + promo.Description + @" 
LeadID                 : " + lead.LeadID + @"
FirstName              : " + lead.FirstName + @"
LastName               : " + lead.LastName + @"
Title                  : " + lead.Title + @"
Email                  : " + lead.Email + @"
Addresss               : " + lead.StreetAddress + @"
City                   : " + lead.City + @"
State                  : " + lead.State + @"
Country                : " + lead.Country + @"
Zip                    : " + lead.ZipCode + @"
DayPhone               : " + lead.DayPhone + " " + lead.DayPhoneExt +@"
EveningPhone           : " + lead.EveningPhone + " " + lead.EveningPhoneExt + @"
Best Time To Call      : " + lead.BestTimeToCall + @"
Organization Type      : " + lead.OrganizationTypeID + @" 
GroupName              : " + lead.GroupName + @"
Number of Group Members: " + lead.ParticipantCount + @"
Fundraising Date       : " + lead.FundraisingDate + @"
Is Decision Maker      : " + lead.DecisionMaker.ToString() + @"
Newsletter Opt-in      : " + lead.OnEmailList.ToString() + @"
Comments               : " + lead.Comments;

#if DEBUG
			SendMail.AsyncSend(ApplicationSettings.GetConfig()["Common.Email.SmtpServer", "host"], 
				"services@efundraising.com", "mnormand@rd.com", 
				"", "", "", "", subject, body, "");
#else
			// Send email to all people interested to receive reports
			for (int i = 0; i < ApplicationSettings.GetConfig().GetCount("LeadManager.Leads.Report"); i++)
			{
				SendMail.AsyncSend(ApplicationSettings.GetConfig()["Common.Email.SmtpServer", "host"],
					"services@efundraising.com", ApplicationSettings.GetConfig()["LeadManager.Leads.Report", i, "email"],
					null, null, null, "",  subject, body, "");	
			}
#endif

		}

		
		#endregion
	}
}
