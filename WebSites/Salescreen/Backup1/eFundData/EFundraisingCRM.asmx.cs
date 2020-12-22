using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Web;
using System.Web.Security;
using System.Web.Services;

using efundraising.Diagnostics;
using efundraising.efundraisingCore;
using efundraising.Globalization;
using efundraising.Configuration;
using efundraising.EFundraisingCRM;

namespace efundraising.eFundData
{
	/// <summary>
	/// Summary description for EFundraisingCRM.
	/// </summary>
	[WebService(Namespace="http://webservices.efundraising.com/efundraisingcrm", Description="Web Service for eFundraising.com Web Site")]
	public class EFundraisingCRM : System.Web.Services.WebService
	{
		
		private const string LAST_ERROR_KEY = "LastError";
		private const string LEAD_KEY = "Lead";
		private const string PARTNER_KEY = "Partner";
		private const string PROMOTION_KEY = "Promotion";
		private const int LEAD_STATUS_ID = 1; // Free kit form lead type.
		private const string CLIENT_KEY = "ClientKey";
		private const string CLIENT_ACTIVITY_KEY = "ClientActivityKey";
		private const string CLIENT_SHIPPING_ADDRESS =  "ClientShippingAddress";
		private const string CLIENT_BILLING_ADDRESS =  "ClientBillingAddress";
		private const string CLIENT_CREDIT_INFO = "ClientCreditInfo";
		private const string SALES_KEY = "SalesKey";
		private const string SALES_PROPS_KEY = "SalesPropertiesKey";
		private const string CLIENT_ALREADY_CREATED = "ClientAlreadyCreated";
		private const string SALES_ID_LIST = "SalesIdList";
		
		private enum ProductProperties
		{
			PRODUCT_CODE,
			QUANTITY,
			PRICE,
			NAME,
            SHIPPING
		}
		
		struct CreditInfo
		{
			public string cardOwnerName;
			public short creditCardType;
			public string creditCardNumber;
			public string expDate;
			public string cvv2;
		}
		
		public EFundraisingCRM()
		{
			InitializeComponent();
		}

		#region Component Designer generated code
		
		//Required by the Web Services Designer 
		private IContainer components = null;
				
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if(disposing && components != null)
			{
				components.Dispose();
			}
			base.Dispose(disposing);		
		}
		
		#endregion

		#region Methods
		
		/// <summary>
		/// Add a new Lead
		/// </summary>
		/// <param name="pFirstName">First Name of new lead</param>
		/// <param name="pLastName">Last Name of new lead</param>
		/// <param name="pEmail">Email of new lead</param>
		/// <param name="pAddress">Street address</param>
		/// <param name="pCity">City of new lead</param>
		/// <param name="pState">State Code of new lead</param>
		/// <param name="pZip">Zip Code of new lead</param>
		/// <param name="pCountry">Country of new lead</param>
		/// <param name="pDayPhone">Day Phone Number to communicate with the new lead</param>
		/// <param name="pEveningPhone">Evening Phone Number to communicate with the new lead</param>
		/// <param name="pGroupSize">Number of possible Members for his campaign</param>
		[WebMethod(EnableSession=true, Description="Add New lead on www.efundraising.com Web Site")]
		public int AddNewLead(string pFirstName, string pLastName, string pEmail, string pAddress, string pCity,
			string pState, string pZip, string pCountry, string pDayPhone, string pEveningPhone, int pGroupSize,
			string pOrganizationName, int pPromotionID, string pTitle, string pEveningPhoneExt, string pDayPhoneExt,
			string pBestTimeToCall, int pOrganizationTypeID, int pGroupTypeID, string pFundraisingDate,
			bool pDecisionMaker, string pProductsInterestIn, bool pOnEmailList, string pComments, int pConsultantId, int pAddressZoneId) 
		{
			ClearError();
			bool isRepeatLead = false;
			
			string call = MethodCall("AddNewLead", pFirstName, pLastName, pEmail, pAddress, pCity,
				pState, pZip, pCountry, pDayPhone, pEveningPhone, pGroupSize,
				pOrganizationName, pPromotionID, pTitle, pEveningPhoneExt, pDayPhoneExt,
				pBestTimeToCall, pOrganizationTypeID, pGroupTypeID, pFundraisingDate,
				pDecisionMaker, pProductsInterestIn, pOnEmailList, pComments, pConsultantId, pAddressZoneId);
			Logger.LogInfo(call);

			try 
			{
				if(IsLoggedIn()) 
				{
					
					// Make sure OrganizationTypeId is set to something useful.
					// Check valid entries in table efundraisingprod.dbo.organization_type
					if (pOrganizationTypeID < 1 || pOrganizationTypeID > 33)
					{
						// Set to 'OTHER' (99)
						pOrganizationTypeID = 99;
					}

					// Make sure GroupTypeId is set to something useful.
					// Check valid entries in table efundraisingprod.dbo.group_type
					if (pGroupTypeID < 1 || pGroupTypeID > 53)
					{
						// Set to 'OTHER' (99)
						pGroupTypeID = 99;
					}

					// Get default promotion Id if not passed by client.
					if (pPromotionID <= 0)
					{
						pPromotionID = GetPromotionId();
					}
					

					// Set default group size to 15 if not specified per Nancy K's requirement.
					if (pGroupSize <= 0)
					{
						pGroupSize = 15;
					}

					try 
					{
						// Set country by default to US
						if (pCountry == null || pCountry == "")
							pCountry = "us";

						RegionInfo country = RegionInfo.GetRegion(pCountry);
						if (country != null)
							pCountry = country.Name.ToUpper();
						else
							throw new Exception("Invalid country.");
					}
					catch {}

					try 
					{
						StateInfo state = StateInfo.GetState(pState);
						if (state == null)
							pState = "N/A";
						else
							pState = state.Name.ToUpper();							
					}
					catch 
					{
						pState = "N/A";
					}

					try 
					{
						// Set default best time to call to daytime.
						if (pBestTimeToCall == null || pBestTimeToCall == "")
							pBestTimeToCall = "day";
					}
					catch {}

					EfundraisingLead newLead = new EfundraisingLead(pFirstName, pLastName, pEmail, pAddress, pCity, pState, pZip, pCountry, pOrganizationName,
						pFundraisingDate, pDayPhone, pEveningPhone, pDayPhoneExt, pEveningPhoneExt, pOrganizationTypeID, (byte) pGroupTypeID, 
						pGroupSize, 0, pPromotionID, pTitle, pBestTimeToCall, pDecisionMaker, pProductsInterestIn, 
						pOnEmailList, pComments, LEAD_STATUS_ID);

					// check if the lead already exist so we don't create more than one lead activity (see Lead.Integrate logic)
					efundraising.efundraisingCore.Lead repeatLead =  newLead.MatchLead();
					if (repeatLead != null)
						isRepeatLead = true;
						
					if (pConsultantId > 0)
						newLead.ConsultantID = pConsultantId;
					else
						newLead.ConsultantID = 0;
					
					newLead.AddressZoneId = pAddressZoneId;

					try 
					{
						newLead.PartnerID = GetPartnerId(); 
					
						// check if lead comes from store
						if (User.Identity.Name == "fastfundraising.com")
							newLead.Integrate(true);
						else
							newLead.Integrate();

						Session[LEAD_KEY] = newLead;
						
						if(newLead.LeadID != int.MinValue && newLead.LeadID != -1) {

							try {
								// after inserting a lead, insert the promotional kit
								// this promotional kit has to be inserted directly 
								// to the efundraising prod database through sql link server
								// the reason why there are no sql transaction is that
								// our link servers does not handle transactions

								// findout which kit type to send depending of the
								// lead arguments
								efundraising.EFundraisingCRM.KitType kitType =
									efundraising.EFundraisingCRM.KitType.GetProperKitTypeFromLeadInformation(
									newLead.ConsultantID, efundraising.EFundraisingCRM.LeadChannel.Internet.ChannelCode, 
									newLead.PromotionID, newLead.PartnerID,
									newLead.State, newLead.Country);
						
								// create a postal address object with lead information
								efundraising.EFundraisingCRM.PostalAddress postalAddress =
									new efundraising.EFundraisingCRM.PostalAddress(
									int.MinValue, newLead.StreetAddress, newLead.City, 
									newLead.ZipCode, newLead.Country, 
									newLead.Country + "-" + newLead.State, DateTime.Now);

								// insert the postal address, if it failed, log 
								// and continue the process (will insert an invalid
								// promotional kit with no postal address id)
								try {
									postalAddress.Insert();
								} catch(System.Exception ex) {
									efundraising.Diagnostics.Logger.LogWarn("Business Oportunity failed to insert postal address: Lead ID: " + newLead.LeadID, ex);
								}

								// create our promotional kit object 
								efundraising.EFundraisingCRM.PromotionalKit promotionalKit =
									new efundraising.EFundraisingCRM.PromotionalKit(
									int.MinValue, newLead.LeadID, newLead.LeadVisitID, kitType.KitTypeID,
									efundraising.EFundraisingCRM.Carrier.RegularMail.CarrierId, int.MinValue,
									postalAddress.PostalAddressId, (postalAddress.PostalAddressId == int.MinValue? 0: 1), DateTime.Now, DateTime.MinValue);

								// insert the promotional kit
								promotionalKit.Insert();
							} catch(System.Exception ex) {
								// let it go anyway, the promotional kit manager service will insert it
								efundraising.Diagnostics.Logger.LogWarn("Unable to insert promotional kit", ex);
							}
						}

						if (!isRepeatLead) 
						{
							// Insert first call activity for the lead otherwise it won't display in the CRM
							efundraising.efundraisingCore.LeadActivity leadActivity = new efundraising.efundraisingCore.LeadActivity(newLead.LeadID, (int)efundraising.efundraisingCore.LeadActivityType.FirstCall, pComments);
							leadActivity.InsertLeadActivity();
						}

						/*Components.Server.Tibo.TiboTalker tiboTalkerClient =
							new Components.Server.Tibo.TiboTalker("Lead Inserted", null, newLead, "New Lead Inserted", null, 1);*/

						return newLead.LeadID;
					}
					catch 
					{
						// On error, always insert into temporary 
						// table to avoid losing lead
						efundraisingCore.TempLead tempLead = new efundraisingCore.TempLead(newLead);
						tempLead.Insert();

						
						/*Components.Server.Tibo.TiboTalker tiboTalkerClient =
							new Components.Server.Tibo.TiboTalker("Temporary Lead Inserted", null, tempLead, "New Temporary Lead Inserted", null, 1);*/

					}
				}
				else 
				{
					throw new Exception("Authentication failed");
				}
			}
			catch (Exception ex)
			{
				PublishError("AddNewLead failed.", call, ex);
			}
			
			return -1;
		}


		[WebMethod(EnableSession=true, Description="Create the client and client adresses.")]
		public void InsertClient(int leadId, string shippingAddress1, string shippingAddress2, string shippingCity, string shippingState, string shippingZip, string shippingCountry, string nameOnCreditCard, string creditCardType, string creditCardNumber, string monthExp, string yearExp, string cvv2, int billAddressZone, int shipAddressZone, string attentionOf)
		{
			ClearError();
			
			string call = MethodCall("InsertClient", leadId, shippingAddress1, 
				shippingAddress2, shippingCity, shippingState, 
				shippingZip, shippingCountry, nameOnCreditCard, 
				creditCardType, creditCardNumber, monthExp, 
				yearExp, cvv2);
			Logger.LogInfo(call);

			efundraising.EFundraisingCRM.Lead lead = efundraising.EFundraisingCRM.Lead.GetLeadByID(leadId);
			
			// Store the credit info in the session
			CreditInfo creditInfo = new CreditInfo();
			creditInfo.cardOwnerName = nameOnCreditCard;
			creditInfo.creditCardNumber = creditCardNumber;
			switch (creditCardType.ToUpper())
			{
				case "VISA"	: creditInfo.creditCardType = 2;
					break;
				case "MASTERCARD" : creditInfo.creditCardType = 3;
					break;
				default:
					creditInfo.creditCardType = 2;
					break;
			}
			creditInfo.cvv2 = cvv2;
			creditInfo.expDate = monthExp + "/" + yearExp;
			Session[CLIENT_CREDIT_INFO]	= creditInfo;
			
			// create the client
		
			Client client = (Client)lead;
			if (lead.PromotionId == 5953) // fastfundraising.com
				client.ClientSequenceCode = "OF";
			else
				client.ClientSequenceCode = "IF";
			if (client.OrganizationClassCode == null)
				client.OrganizationClassCode = "OTH";
			
			
			try 
			{
				// Set country by default to US
				if (shippingCountry == null || shippingCountry == "")
					shippingCountry = "us";

				RegionInfo country = RegionInfo.GetRegion(shippingCountry);
				if (country != null)
					shippingCountry = country.Name.ToUpper();
				else
					throw new Exception("Invalid country.");
			}
			catch {}

			try 
			{
				StateInfo state = StateInfo.GetState(shippingState);
				if (state == null)
					shippingState = "N/A";
				else
					shippingState = state.Name.ToUpper();							
			}
			catch 
			{
				shippingState = "N/A";
			}

			// create the addresses for the client
			ClientAddress billingAddress = new ClientAddress(int.MinValue,
				client.ClientSequenceCode,
				int.MinValue,
				ClientAddressType.BillingAddress.AddressType,
				lead.StreetAddress,
				lead.StateCode,
				lead.CountryCode,
				lead.City,
				lead.ZipCode);
			billingAddress.AddressZoneId = shipAddressZone;

			
			ClientAddress shippingAddress = new ClientAddress(int.MinValue,
				client.ClientSequenceCode,
				int.MinValue,
				ClientAddressType.ShippingAddress.AddressType,
				shippingAddress1 + ", " + shippingAddress2,
				shippingState,
				shippingCountry,
				shippingCity,
				shippingZip);
			shippingAddress.AddressZoneId = billAddressZone;
			if (attentionOf != null)
				shippingAddress.AttentionOf = (attentionOf.Length > 0) ? attentionOf : null;
			
			// Create a ClientActivity for the newly created client
			ClientActivity clientActivity = new ClientActivity();
			clientActivity.ClientSequenceCode = client.ClientSequenceCode;
			clientActivity.ClientActivityTypeId = ClientActivityType.ConfirmationCall.ClientActivityTypeId; // ClientActivityTypeID = 2
			clientActivity.ClientActivityDate = DateTime.Now;
			
						
			// store objects in the session
			Session[CLIENT_KEY] = client;
			Session[CLIENT_ACTIVITY_KEY] = clientActivity;
			Session[CLIENT_BILLING_ADDRESS] = billingAddress;
			Session[CLIENT_SHIPPING_ADDRESS] = shippingAddress;
		
		}
		

		[WebMethod(EnableSession=true, Description="Create the sales.")]
		public void InsertSale(string productCode, int quantity, float price, string itemName)
		{
			ClearError();

			string call = MethodCall("InsertSale", productCode, quantity, price, itemName);
			Logger.LogInfo(call);
			
			// Product code sent by Fastfundraising begins with FFxxxxx.
			// Our database uses FRxxxxx. We need to convert to FRxxxxx.
			if(productCode.ToUpper().StartsWith("FF"))
				productCode = "FR" + productCode.Substring(2);
			
			ArrayList salesProperties = (Session[SALES_PROPS_KEY] != null ? (ArrayList)Session[SALES_PROPS_KEY] : new ArrayList());
			salesProperties.Add(new object[] { productCode, quantity, price, itemName});
			Session[SALES_PROPS_KEY] = salesProperties;
		}



        [WebMethod(EnableSession = true, Description = "Create the sales.")]
        public void InsertSaleTest(string productCode, int quantity, float price, string itemName, float shipping)
        {
            ClearError();

            string call = MethodCall("InsertSale", productCode, quantity, price, itemName, shipping);
            Logger.LogInfo(call);

            // Product code sent by Fastfundraising begins with FFxxxxx.
            // Our database uses FRxxxxx. We need to convert to FRxxxxx.
            if (productCode.ToUpper().StartsWith("FF"))
                productCode = "FR" + productCode.Substring(2);

            ArrayList salesProperties = (Session[SALES_PROPS_KEY] != null ? (ArrayList)Session[SALES_PROPS_KEY] : new ArrayList());
            salesProperties.Add(new object[] { productCode, quantity, price, itemName,shipping });
            Session[SALES_PROPS_KEY] = salesProperties;
        }


		[WebMethod(EnableSession=true,MessageName="InsertPayment", Description="Create the payment(s) after a sales has been paid (in full or in part).")]
		public void InsertPayments(string[] salesIdList, string authCode, string orderId, string nameOnCC) 
		{
			ClearError();
			
			string call = MethodCall("InsertPayments", salesIdList, authCode, orderId, nameOnCC);
			Logger.LogInfo(call);
			
			PaymentCollection payments = new PaymentCollection();
			SaleCollection sales = new SaleCollection();
			
			try
			{
				// create a PaymentCollection from a string list of saleIds, then insert the payments in the db
				foreach (string saleId in salesIdList)
				{
					try
					{
						Sale currentSale = Sale.GetSaleByID(int.Parse(saleId));
						Payment currentPayment = new Payment();
						currentPayment.SalesId = currentSale.SalesId;
						currentPayment.PaymentNo = currentPayment.GetNextPaymentNo();
						currentPayment.PaymentMethodId = (byte)currentSale.PaymentMethodId;
						currentPayment.CollectionStatusId = CollectionStatus.CheckInHouse.CollectionStatusID;
						currentPayment.PaymentEntryDate = DateTime.Now;
						currentPayment.CashableDate = DateTime.Now;
						currentPayment.NameOnCard = nameOnCC;
						currentPayment.CreditCardNo = currentSale.CreditCardNo;
						currentPayment.ExpiryDate = currentSale.ExpiryDate;
						currentPayment.AuthorizationNumber = authCode;
						currentPayment.PaymentAmount = currentSale.TotalAmount + (double)currentSale.ShippingFees; // UPDATE AUG13, 2013: Added shipping fees 
                        currentPayment.CommissionPaid = false;
						currentPayment.ForeignOrderId = int.Parse(orderId);
						
						payments.Add(currentPayment);
						
						// update the sale
						currentSale.SalesStatusId = SalesStatus.Confirmed.SalesStatusID;
						currentSale.ConfirmedDate = DateTime.Now;
						currentSale.ScheduledDeliveryDate = DateTime.Now.AddDays(9);
						currentSale.Comment = "PaymenTech OrderID: " + orderId;
						currentSale.Fuelsurcharge = 0;
						sales.Add(currentSale);
												
					}
					catch (Exception ex)
					{
						PublishError("InsertPayment failed.", call, ex);
					}
				}
				
				if (payments.Count > 0)
				{
					TransactionController trans = new TransactionController();
					trans.InsertPaymentsAndUpdateSales(payments, sales);
				}	
				
			}
			catch (Exception ex)
			{
				PublishError("InsertPayments failed.", call, ex);
			}			
			
		}

        [WebMethod(EnableSession = true, Description = "Insert the Client and sales in the database.")]
        public string CommitTest()
        {
            ClearError();
            string salesIdList;
            string reportMessage = "";
            string email = "";
            SaleCollection sales = new SaleCollection();
            CommentsCollection comments = new CommentsCollection();

            // Get the previously created objects from the Session
            Client client = (Client)Session[CLIENT_KEY];
            ClientActivity clientActivity = (ClientActivity)Session[CLIENT_ACTIVITY_KEY];
            ArrayList salesProperties = (ArrayList)Session[SALES_PROPS_KEY];
            ClientAddress billingAddress = (ClientAddress)Session[CLIENT_BILLING_ADDRESS];
            ClientAddress shippingAddress = (ClientAddress)Session[CLIENT_SHIPPING_ADDRESS];

            // Consutant variables for setting email
            string emailDefault = string.Empty;
            Hashtable consutantIdsSentEmail = new Hashtable();
            Hashtable sentEmailToConsutantList = new Hashtable();
            string consultantEmailFormat = string.Empty;
            bool bSendReportMessage = false;
            try
            {
                consultantEmailFormat = ApplicationSettings.GetConfig()["Consultant.Email.Format", "format"];
            }
            catch (Exception)
            {
                consultantEmailFormat = @"Hi {0},\r\nYour client {1} under the lead id: {2} has bought from an online source.";
            }


            try
            {
                bSendReportMessage = bool.Parse(ApplicationSettings.GetConfig()["Consultant.Email.Format", "sendreportmessage"]);
            }
            catch (Exception)
            {
                bSendReportMessage = false;
            }


            try
            {
                reportMessage += "Consultant: Consultant Ids: ";
                string[] consultantIds = ApplicationSettings.GetConfig()["Consultant.Email.Format", "ConsultantIds"].Split(',');
                for (int i = 0; i < consultantIds.Length; i++)
                {
                    consutantIdsSentEmail[consultantIds[i]] = consultantIds[i];
                    reportMessage += "," + consultantIds[i];
                }
                reportMessage += "\r\n";
            }
            catch (Exception)
            {
                reportMessage += "Consultant: Error in consultantIds\r\n";
            }

            try
            {
                emailDefault = ApplicationSettings.GetConfig()["Consultant.Email.Format", "emaildefault"];
                reportMessage += "Consultant: emails Default: " + emailDefault + "\r\n";
            }
            catch (Exception)
            {
                emailDefault = string.Empty;
                reportMessage += "Consultant: Error in emailDefault\r\n";
            }

            try
            {
                // HACK: Some items still carry product_code = NONE.
                // We will manually process these sales items.
                foreach (object[] i in salesProperties)
                {
                    if ((string)i[(int)ProductProperties.PRODUCT_CODE] == "NONE")
                    {
                        try
                        {
                            ClientUtility clientUtility = new ClientUtility();
                            reportMessage = clientUtility.GenerateClientReport(client, billingAddress, shippingAddress, sales);
                            foreach (object[] j in salesProperties)
                            {
                                reportMessage += "Product Code: " + (string)j[(int)ProductProperties.PRODUCT_CODE] + "\r\n";
                                reportMessage += "    Item Name: " + (string)j[(int)ProductProperties.NAME] + "\r\n";
                                reportMessage += "    Price: " + ((float)j[(int)ProductProperties.PRICE]).ToString() + "\r\n";
                                reportMessage += "    Qty: " + ((int)j[(int)ProductProperties.QUANTITY]).ToString() + "\r\n\r\n";
                            }

                            // generate human readable email
                            email += client.ToHumanReadableString() + "\r\n\r\n";
                            email += billingAddress.ToHumanReadableString() + "\r\n\r\n";
                            email += shippingAddress.ToHumanReadableString() + "\r\n\r\n";
                            foreach (object[] j in salesProperties)
                            {
                                email += "Product Code: " + (string)j[(int)ProductProperties.PRODUCT_CODE] + "\r\n";
                                email += "    Item Name: " + (string)j[(int)ProductProperties.NAME] + "\r\n";
                                email += "    Price: " + ((float)j[(int)ProductProperties.PRICE]).ToString() + "\r\n";
                                email += "    Qty: " + ((int)j[(int)ProductProperties.QUANTITY]).ToString() + "\r\n\r\n";
                            }
                        }
                        catch { }

                        // Throw exception so a email report is generated.
                        throw new Exception("Product code NONE detected");
                    }
                }


                // Create the sales objects
                CreateSalesCollectionsTest(client, salesProperties, billingAddress, shippingAddress, sales, comments);
                TransactionController trans = new TransactionController();

                try
                {
                    ClientUtility clientUtility = new ClientUtility();
                    reportMessage = clientUtility.GenerateClientReport(client, billingAddress, shippingAddress, sales);

                    // generate human readable email
                    email += client.ToHumanReadableString() + "\r\n\r\n";
                    email += billingAddress.ToHumanReadableString() + "\r\n\r\n";
                    email += shippingAddress.ToHumanReadableString() + "\r\n\r\n";
                    foreach (Sale enteredSale in sales)
                    {
                        reportMessage += "Consultant: Consultant Id: " + enteredSale.ConsultantId.ToString();
                        if (
                            // Do not send email to those specific Consultants will be received emails
                            consutantIdsSentEmail[enteredSale.ConsultantId.ToString()] == null
                            // Send each consultant will receive only one email associated with the client (not with each Sale). 
                            && sentEmailToConsutantList[enteredSale.ConsultantId] == null)
                        {
                            sentEmailToConsutantList[enteredSale.ConsultantId] = enteredSale.ConsultantId;
                        }

                        email += enteredSale.ToHumanReadableString() + "\r\n\r\n";
                        foreach (SalesItem si in enteredSale.SalesItems)
                        {
                            email += si.ToHumanReadableString() + "\r\n";
                        }
                    }
                }
                catch (System.Exception ex) { reportMessage = "Unable to generate report client: " + ex.Message; }


                // retreive the email addresses to send confirmation
                string[] email_addresses = new string[ApplicationSettings.GetConfig().GetCount("Info.Report.Email")];
                for (int i = 0; i < ApplicationSettings.GetConfig().GetCount("Info.Report.Email"); i++)
                {
                    email_addresses[i] = ApplicationSettings.GetConfig()["Info.Report.Email", i, "Email"];
                }

                // we send the email directly by the web site
                string smptAddress = ApplicationSettings.GetConfig()["Common.Email.SmtpServer", "host"];
                efundraising.Email.SendMail.AsyncSend(
                    smptAddress, "efr-online@rd.com", email_addresses, null, null, "online@efundraising.com", null,
                    "FR Insert Sale", email, email.Replace("\r\n", "<br>"));


                // Do the actual database insertion transaction
                salesIdList = trans.InsertClientAndSalesNoTransaction(client, clientActivity, billingAddress, shippingAddress, ref sales, comments);
                Session[SALES_ID_LIST] = salesIdList;

                ////////////////////////////////////////////////////////////////////////////////////
                //INSERT IN QSP
                //set web config values
                /*   Hashtable hWebConfig = new Hashtable();
                   hWebConfig["ProgramType"] = efundraising.Configuration.ApplicationSettings.GetConfig()["OrderExpress", "programType"].ToString();
                   hWebConfig["Form"] = efundraising.Configuration.ApplicationSettings.GetConfig()["OrderExpress", "form"].ToString();
                   hWebConfig["accountID"] = efundraising.Configuration.ApplicationSettings.GetConfig()["OrderExpress", "accountID"].ToString();
                   hWebConfig["orderTypeID"] = efundraising.Configuration.ApplicationSettings.GetConfig()["OrderExpress", "orderTypeID"].ToString(); 

                   //prod
                   hWebConfig["isProd"] = "1";
                   hWebConfig["fmID"] = efundraising.Configuration.ApplicationSettings.GetConfig()["OrderExpress", "prodFmID"].ToString();
                   hWebConfig["custID"] = efundraising.Configuration.ApplicationSettings.GetConfig()["OrderExpress", "prodCustomerID"].ToString(); 
                
                   //dev
                   hWebConfig["isProd"] = "0";
                   hWebConfig["fmID"] = efundraising.Configuration.ApplicationSettings.GetConfig()["OrderExpress", "devFmID"].ToString();
                   hWebConfig["custID"] = efundraising.Configuration.ApplicationSettings.GetConfig()["OrderExpress", "prodCustomerID"].ToString(); 
                
                   OrderExpressClient.OEPlaceOrderEntryPoint oe = new OrderExpressClient.OEPlaceOrderEntryPoint();


                   foreach (Sale sale in sales)
                   {
                       string errorMsg = oe.PlaceOrder(sale, client.ClientId, client.ClientSequenceCode, hWebConfig, null);
                            
                       if (errorMsg != "")
                       {
                           efundraising.Email.SendMail.AsyncSend(
                           smptAddress, "error@efundraising.com", email_addresses, null, null, "online@efundraising.com", null,
                           "Error: salescreen (Online Store)", errorMsg, reportMessage.Replace("\r\n", "<br>"));

                       }
                   }
                   //////////////////////////////////////////////////////////////////////////////
                   /////////////////////////////////////////////////////////////////////////////
                   */
                string clientNameAndId = string.Format("{0}, {1} [client Id: {2}]", client.FirstName, client.LastName, client.ClientId);
                string clientLeadId = client.LeadId.ToString();


                if (bSendReportMessage)
                    efundraising.Email.SendMail.AsyncSend(
                        smptAddress, "efr-online@rd.com", email_addresses, null, null, "online@efundraising.com", null,
                        "Report Message Sale", reportMessage, reportMessage.Replace("\r\n", "<br>"));

                // log to tibo
                /*Components.Server.Tibo.TiboTalker tiboTalkerClient =
                    new Components.Server.Tibo.TiboTalker("Client Inserted", null, client, "New Client Inserted", null, 1);*/

                return salesIdList;

            }
            catch (Exception ex)
            {
                // generate report to send the users
                string smptAddress = ApplicationSettings.GetConfig()["Common.Email.SmtpServer", "host"];

                // we get all the emails 
                string[] email_addresses = new string[ApplicationSettings.GetConfig().GetCount("Error.Report.Email")];
                for (int i = 0; i < ApplicationSettings.GetConfig().GetCount("Error.Report.Email"); i++)
                {
                    email_addresses[i] = ApplicationSettings.GetConfig()["Error.Report.Email", i, "Email"];
                }

                // we send the email directly by the web site
                efundraising.Email.SendMail.AsyncSend(
                    smptAddress, "efr-online@rd.com", email_addresses, null, null, "online@efundraising.com", null,
                    "Failed Insert Sale", email, reportMessage.Replace("\r\n", "<br>"));

                /*Components.Server.Tibo.TiboTalker tiboTalkerSales =
                    new Components.Server.Tibo.TiboTalker("Error Inserting Client & Sales", null, new SalesItem(), "ERROR", null, 3);*/

                Logger.LogError(ex);
                SetError("Unable to Commit Transaction. " + ex.Message);

                return null;
            }

        }

				
		[WebMethod(EnableSession=true, Description="Insert the Client and sales in the database.")]
		public string Commit()
		{
			ClearError();
			string salesIdList;
			string reportMessage = "";
			string email ="";
			SaleCollection sales = new SaleCollection();
			CommentsCollection comments = new CommentsCollection();

			// Get the previously created objects from the Session
			Client client = (Client)Session[CLIENT_KEY];
			ClientActivity clientActivity = (ClientActivity)Session[CLIENT_ACTIVITY_KEY];
			ArrayList salesProperties = (ArrayList)Session[SALES_PROPS_KEY];
			ClientAddress billingAddress = (ClientAddress)Session[CLIENT_BILLING_ADDRESS];
			ClientAddress shippingAddress = (ClientAddress)Session[CLIENT_SHIPPING_ADDRESS];

			// Consutant variables for setting email
			string emailDefault = string.Empty;
			Hashtable consutantIdsSentEmail = new Hashtable();
			Hashtable sentEmailToConsutantList = new Hashtable();
			string consultantEmailFormat = string.Empty;
			bool bSendReportMessage = false;
			try
			{
				consultantEmailFormat = ApplicationSettings.GetConfig()["Consultant.Email.Format", "format"];
			}
			catch (Exception)
			{
				consultantEmailFormat = @"Hi {0},\r\nYour client {1} under the lead id: {2} has bought from an online source.";
			}

			
			try
			{
				bSendReportMessage = bool.Parse(ApplicationSettings.GetConfig()["Consultant.Email.Format", "sendreportmessage"]);
			}
			catch (Exception)
			{
				bSendReportMessage = false;
			}

			
			try
			{
				reportMessage += "Consultant: Consultant Ids: ";
				string[] consultantIds = ApplicationSettings.GetConfig()["Consultant.Email.Format", "ConsultantIds"].Split(',');
				for (int i= 0 ; i< consultantIds.Length; i++)
				{
					consutantIdsSentEmail[consultantIds[i]] = consultantIds[i];
					reportMessage += "," + consultantIds[i] ;
				}
				reportMessage += "\r\n";
			}
			catch (Exception)
			{
				reportMessage += "Consultant: Error in consultantIds\r\n";
			}

			try
			{
				emailDefault = ApplicationSettings.GetConfig()["Consultant.Email.Format", "emaildefault"];
				reportMessage += "Consultant: emails Default: " + emailDefault + "\r\n";
			}
			catch (Exception)
			{
				emailDefault = string.Empty;
				reportMessage += "Consultant: Error in emailDefault\r\n";
			}

			try
			{
				// HACK: Some items still carry product_code = NONE.
				// We will manually process these sales items.
				foreach (object[] i in salesProperties)
				{
					if ((string)i[(int)ProductProperties.PRODUCT_CODE] == "NONE")
					{
						try 
						{
							ClientUtility clientUtility = new ClientUtility();
							reportMessage = clientUtility.GenerateClientReport(client, billingAddress, shippingAddress, sales);
							foreach (object[] j in salesProperties)
							{
								reportMessage += "Product Code: " + (string)j[(int)ProductProperties.PRODUCT_CODE] + "\r\n";
								reportMessage += "    Item Name: " + (string)j[(int)ProductProperties.NAME] + "\r\n";
								reportMessage += "    Price: " + ((float) j[(int)ProductProperties.PRICE]).ToString() + "\r\n";
								reportMessage += "    Qty: " + ((int) j[(int)ProductProperties.QUANTITY]).ToString() + "\r\n\r\n";
							}

							// generate human readable email
							email += client.ToHumanReadableString() + "\r\n\r\n";
							email += billingAddress.ToHumanReadableString() + "\r\n\r\n";
							email += shippingAddress.ToHumanReadableString() + "\r\n\r\n";
							foreach (object[] j in salesProperties)
							{
								email += "Product Code: " + (string)j[(int)ProductProperties.PRODUCT_CODE] + "\r\n";
								email += "    Item Name: " + (string)j[(int)ProductProperties.NAME] + "\r\n";
								email += "    Price: " + ((float) j[(int)ProductProperties.PRICE]).ToString() + "\r\n";
								email += "    Qty: " + ((int) j[(int)ProductProperties.QUANTITY]).ToString() + "\r\n\r\n";
							}
						} 
						catch {}

						// Throw exception so a email report is generated.
						throw new Exception("Product code NONE detected");
					}
				}

				
				// Create the sales objects
				CreateSalesCollections(client, salesProperties, billingAddress, shippingAddress, sales, comments);
				TransactionController trans = new TransactionController();

				try 
				{
					ClientUtility clientUtility = new ClientUtility();
					reportMessage = clientUtility.GenerateClientReport(client, billingAddress, shippingAddress, sales);

					// generate human readable email
					email += client.ToHumanReadableString() + "\r\n\r\n";
					email += billingAddress.ToHumanReadableString() + "\r\n\r\n";
					email += shippingAddress.ToHumanReadableString() + "\r\n\r\n";
					foreach(Sale enteredSale in sales) 
					{
						reportMessage += "Consultant: Consultant Id: " + enteredSale.ConsultantId.ToString();
						if (
							// Do not send email to those specific Consultants will be received emails
							consutantIdsSentEmail[enteredSale.ConsultantId.ToString()] == null 
							// Send each consultant will receive only one email associated with the client (not with each Sale). 
							&& sentEmailToConsutantList[enteredSale.ConsultantId] == null)
						{
							sentEmailToConsutantList[enteredSale.ConsultantId] = enteredSale.ConsultantId;
						}

						email += enteredSale.ToHumanReadableString() + "\r\n\r\n";
						foreach(SalesItem si in enteredSale.SalesItems) 
						{
							email += si.ToHumanReadableString() + "\r\n";
						}
					}
				} 
				catch(System.Exception ex) { reportMessage = "Unable to generate report client: " + ex.Message; }
				

				// retreive the email addresses to send confirmation
				string[] email_addresses = new string[ApplicationSettings.GetConfig().GetCount("Info.Report.Email")]; 
				for (int i = 0; i < ApplicationSettings.GetConfig().GetCount("Info.Report.Email"); i++) 
				{
					email_addresses[i] = ApplicationSettings.GetConfig()["Info.Report.Email", i, "Email"];
				}

				// we send the email directly by the web site
				string smptAddress = ApplicationSettings.GetConfig()["Common.Email.SmtpServer", "host"];
				efundraising.Email.SendMail.AsyncSend(
					smptAddress, "efr-online@rd.com", email_addresses, null, null, "online@efundraising.com", null,
					"FR Insert Sale", email, email.Replace("\r\n", "<br>"));


				// Do the actual database insertion transaction
				salesIdList = trans.InsertClientAndSalesNoTransaction(client, clientActivity, billingAddress, shippingAddress, ref sales, comments);
				Session[SALES_ID_LIST] = salesIdList;

                try
                {
                    ////////////////////////////////////////////////////////////////////////////////////
                    //INSERT IN QSP
                    //set web config values
                    /*   Hashtable hWebConfig = new Hashtable();
                       hWebConfig["ProgramType"] = efundraising.Configuration.ApplicationSettings.GetConfig()["OrderExpress", "programType"].ToString();
                       hWebConfig["Form"] = efundraising.Configuration.ApplicationSettings.GetConfig()["OrderExpress", "form"].ToString();
                       hWebConfig["organizationID"] = efundraising.Configuration.ApplicationSettings.GetConfig()["OrderExpress", "organizationID"].ToString();
                       hWebConfig["createUserID"] = efundraising.Configuration.ApplicationSettings.GetConfig()["OrderExpress", "createUserID"].ToString();
                       hWebConfig["orderTypeID"] = efundraising.Configuration.ApplicationSettings.GetConfig()["OrderExpress", "orderTypeID"].ToString(); 

                       //prod
                       hWebConfig["isProd"] = "1";
                       hWebConfig["fmID"] = efundraising.Configuration.ApplicationSettings.GetConfig()["OrderExpress", "prodFmID"].ToString();
                       hWebConfig["custID"] = efundraising.Configuration.ApplicationSettings.GetConfig()["OrderExpress", "prodCustomerID"].ToString(); 
                
                       //dev
                       hWebConfig["isProd"] = "0";
                       hWebConfig["fmID"] = efundraising.Configuration.ApplicationSettings.GetConfig()["OrderExpress", "devFmID"].ToString();
                       hWebConfig["custID"] = efundraising.Configuration.ApplicationSettings.GetConfig()["OrderExpress", "prodCustomerID"].ToString(); 
                
                       OrderExpressClient.OEPlaceOrderEntryPoint oe = new OrderExpressClient.OEPlaceOrderEntryPoint();


                       foreach (Sale sale in sales)
                       {
                           string errorMsg = oe.PlaceOrder(sale, client.ClientId, client.ClientSequenceCode, hWebConfig, null);
                            
                           if (errorMsg != "")
                           {
                               efundraising.Email.SendMail.AsyncSend(
                               smptAddress, "error@efundraising.com", email_addresses, null, null, "online@efundraising.com", null,
                               "Error: salescreen (Online Store)", errorMsg, reportMessage.Replace("\r\n", "<br>"));

                           }
                       }
                       //////////////////////////////////////////////////////////////////////////////
                       /////////////////////////////////////////////////////////////////////////////
                       */
                }
                catch
                {
                    throw;
                }
                    
                string clientNameAndId = string.Format("{0}, {1} [client Id: {2}]", client.FirstName, client.LastName, client.ClientId);
				string clientLeadId = client.LeadId.ToString();
				

				if (bSendReportMessage)
					efundraising.Email.SendMail.AsyncSend(
						smptAddress, "efr-online@rd.com", email_addresses, null, null, "online@efundraising.com", null,
						"Report Message Sale", reportMessage, reportMessage.Replace("\r\n", "<br>"));

				// log to tibo
				/*Components.Server.Tibo.TiboTalker tiboTalkerClient =
					new Components.Server.Tibo.TiboTalker("Client Inserted", null, client, "New Client Inserted", null, 1);*/
		
				return salesIdList;
				
			}
			catch (Exception ex)
			{
				// generate report to send the users
				string smptAddress = ApplicationSettings.GetConfig()["Common.Email.SmtpServer", "host"];

				// we get all the emails 
				string[] email_addresses = new string[ApplicationSettings.GetConfig().GetCount("Error.Report.Email")]; 
				for (int i = 0; i < ApplicationSettings.GetConfig().GetCount("Error.Report.Email"); i++) 
				{
					email_addresses[i] = ApplicationSettings.GetConfig()["Error.Report.Email", i, "Email"];
				}

				// we send the email directly by the web site
				efundraising.Email.SendMail.AsyncSend(
					smptAddress, "efr-online@rd.com", email_addresses, null, null, "online@efundraising.com", null,
					"Failed Insert Sale", email, reportMessage.Replace("\r\n", "<br>"));

				/*Components.Server.Tibo.TiboTalker tiboTalkerSales =
					new Components.Server.Tibo.TiboTalker("Error Inserting Client & Sales", null, new SalesItem(), "ERROR", null, 3);*/

				Logger.LogError(ex);
				SetError("Unable to Commit Transaction. " + ex.Message);
				
				return null;
			}
			
		}


		[WebMethod(EnableSession=true, Description="Update the kit and create a comment")]
		public bool ValidateFRKitRequest(int leadId)
		{
			
			ClearError();
			string call = MethodCall("ValidateFRKitRequest", leadId);
			Logger.LogInfo(call);
			
			efundraising.EFundraisingCRM.Lead lead = efundraising.EFundraisingCRM.Lead.GetLeadByID(leadId);

			if (lead != null)
			{
				
				try
				{
					// update the lead
					lead.KitSent = true;
					lead.FkKitTypeId = 28; // FR USA
					
					lead.Update();
			
					// create and insert a comment for the lead
					Comments comment = new Comments(int.MinValue, 
						2,  // medium priority
						int.MinValue, 
						1523, 
						lead.LeadId, 
						9, // other 
						DateTime.Now, 
						"Kits and sample sent");
					comment.Insert();

					return true;
				}
				catch (Exception ex)
				{
					PublishError("Error while updating lead and inserting comment failed.", call, ex);
				}

				
			}

			return false;

		}
		
		public void CreateSalesCollections(Client client, ArrayList salesProperties, ClientAddress billingAddress, ClientAddress shippingAddress, SaleCollection sales, CommentsCollection comments)
		{
			ScratchBookCollection scratchBookCollection = new ScratchBookCollection();
			CreditInfo creditInfo = (CreditInfo)Session[CLIENT_CREDIT_INFO];
			efundraising.efundraisingCore.Lead lead = (efundraising.efundraisingCore.Lead)Session[LEAD_KEY];
			
			// determine the billing company
			int billingCompany = BillingCompany.eFundraising_USA.BillingCompanyID;  // 1 is the default - efundraising USA
			if (lead.PromotionID == 5953 || lead.PromotionID == 5961)
				billingCompany = BillingCompany.FR.BillingCompanyID;
			
			// exceptions for Fundraising.com FreeKit
			int saleStatus = int.MinValue;
			DateTime scheduledDeliveryDate = DateTime.MinValue;
			short paymentMethodId = short.MinValue;
			if (lead.PromotionID == 5961) // fundraising.com
			{
				saleStatus = SalesStatus.Confirmed.SalesStatusID;
				scheduledDeliveryDate = DateTime.Now.AddDays(9);
				paymentMethodId = PaymentMethod.Check.PaymentMethodId;
			}
			else
			{
				saleStatus = SalesStatus.OnHold.SalesStatusID;
				paymentMethodId = creditInfo.creditCardType;
			}
			

			// create all the scratchbook objects
			foreach (object[] i in salesProperties)
			{
				try 
				{
					ScratchBook scb = ScratchBook.GetScratchBookByProductCode((string)i[(int)ProductProperties.PRODUCT_CODE]);
					scratchBookCollection += scb;
				}
				catch (Exception ex)
				{
					throw new Exception("Product code " + i[(int)ProductProperties.PRODUCT_CODE] + " not found.", ex);
				}	
			}
						
			// group the scratchbood objects by ProductClass in collections
			ClientUtility clientUtility = new ClientUtility();
			SalesItemCollection[] collections = clientUtility.GetGroupedItems(scratchBookCollection);
			
			// loop through the collection to create the actual Sales and SalesItems
			foreach(SalesItemCollection col in collections) 
			{
				int productClass = col.ProductClass.ProductClassId;
				
				Sale sale = new Sale(int.MinValue,
					lead.ConsultantID,
					Carrier.FEDEX.CarrierId, 
					1, 
					PaymentTerm.Prepaid.PaymentTermId, 
					client.ClientSequenceCode,
					client.ClientId,
					saleStatus,
					paymentMethodId,
					short.Parse(PoStatus.Pending.PoStatusId.ToString()),
					ProductionStatus.Default.ProductionStatusID,
					int.MinValue, // Sponsor Consultant
					int.MinValue, // AR Consultant
					ARStatus.NotPaid.ARStatusID,
					client.LeadId,
					billingCompany,
					short.MinValue, // upfront payment 
					int.MinValue, // confirmer id
					CollectionStatus.Default.CollectionStatusID,
					ConfirmationMethod.CreditCard.ConfirmationMethodID, 
					CreditApprovalMethod.CreditApprovedByAR.CreditApprovalMethodID,  
					null, // PO number
					creditInfo.creditCardNumber, 
					creditInfo.expDate,
					DateTime.Now,	// sale date
					0, // shipping fees
					0, // shipping fees discounts
					DateTime.MinValue, // payment due date
					DateTime.MinValue, // confirmed date
					scheduledDeliveryDate, // scheduled delivery date
					DateTime.MinValue, // scheduled ship date
					DateTime.MinValue, // actual ship date
					null, // way bill no.
					null, // comment
					0, // is coupons sheet assigned
					0, // total amount
					DateTime.MinValue, // invoice date
					DateTime.MinValue, // cancellation date
					0, // is ordered
					DateTime.MinValue,  // PO received date
					0, // is delivered
					0, // local sponsor found
					DateTime.MinValue, // return date
					DateTime.MinValue, // reship date
					0, // upfront payment required
					DateTime.MinValue, // upfront payment due date
					0, // is sponsor required
					DateTime.MinValue,  // actual delivery date
					null,	// accounting comment
					null,	// social security number
					null,	// social security address
					null,	// social security city
					null,	// social security state
					null,	// social security country
					null,	// social security zip
					0,
					DateTime.MinValue,	// promise due date
					0,	// general flag (always 0)
					short.MinValue);	// fuel surcharge (always null));
					

				if (lead.Comments.Length > 0)
				{
					Comments comment = new Comments(int.MinValue,
									2, // Priority : medium
									sale.SalesId,
									lead.ConsultantID,
									lead.LeadID,
									9, // departement : other
									DateTime.Now,
									lead.Comments);
					comments.Add(comment);
				}
					
				int salesItemNo = 1;
				
				// create a SalesItem for each scratchbook of this product class
				foreach(ScratchBook sc in col) 
				{
					int productQuantity = 0;
					decimal productPrice = decimal.Zero;
					
					foreach (object[] i in salesProperties)
					{
						if (sc.ProductCode == (string)i[(int)ProductProperties.PRODUCT_CODE])
						{
							productQuantity = (int)i[(int)ProductProperties.QUANTITY];
							productPrice = Convert.ToDecimal(i[(int)ProductProperties.PRICE]);
							break;
						}
					}
					
					SalesItem salesItem = new SalesItem(int.MinValue, 
						salesItemNo, 
						sc.ScratchBookId, 
						(short)ServiceType.Bulk.ServiceTypeId,
						(short)productClass,
						"",
						productQuantity,
						productPrice,
						0,
						"",
						(productQuantity * productPrice), // total price,
						0,
						0,
						0,
						0,
						0,
						productQuantity,
						null);
					
					sale.SalesItems.Add(salesItem);
					salesItemNo++;
				}
				// calculate the total amount for the products included in this Sale
				sale.CalculateTotalArAmount();
				sales += sale;
			}
			
		}


        public void CreateSalesCollectionsTest(Client client, ArrayList salesProperties, ClientAddress billingAddress, ClientAddress shippingAddress, SaleCollection sales, CommentsCollection comments)
        {
            ScratchBookCollection scratchBookCollection = new ScratchBookCollection();



          
            CreditInfo creditInfo = (CreditInfo)Session[CLIENT_CREDIT_INFO];
            efundraising.efundraisingCore.Lead lead = (efundraising.efundraisingCore.Lead)Session[LEAD_KEY];

            // determine the billing company
            int billingCompany = BillingCompany.eFundraising_USA.BillingCompanyID;  // 1 is the default - efundraising USA
            if (lead.PromotionID == 5953 || lead.PromotionID == 5961)
                billingCompany = BillingCompany.FR.BillingCompanyID;

            // exceptions for Fundraising.com FreeKit
            int saleStatus = int.MinValue;
            DateTime scheduledDeliveryDate = DateTime.MinValue;
            short paymentMethodId = short.MinValue;
            if (lead.PromotionID == 5961) // fundraising.com
            {
                saleStatus = SalesStatus.Confirmed.SalesStatusID;
                scheduledDeliveryDate = DateTime.Now.AddDays(9);
                paymentMethodId = PaymentMethod.Check.PaymentMethodId;
            }
            else
            {
                saleStatus = SalesStatus.OnHold.SalesStatusID;
                paymentMethodId = creditInfo.creditCardType;
            }


            decimal shipping = 0;
            // create all the scratchbook objects
            foreach (object[] i in salesProperties)
            {
                try
                {
                    shipping = Convert.ToDecimal(i[(int)ProductProperties.SHIPPING]);
                    ScratchBook scb = ScratchBook.GetScratchBookByProductCode((string)i[(int)ProductProperties.PRODUCT_CODE]);
                    scratchBookCollection += scb;
                }
                catch (Exception ex)
                {
                    throw new Exception("Product code " + i[(int)ProductProperties.PRODUCT_CODE] + " not found.", ex);
                }
            }

            // group the scratchbood objects by ProductClass in collections
            ClientUtility clientUtility = new ClientUtility();
            SalesItemCollection[] collections = clientUtility.GetGroupedItems(scratchBookCollection);

            // loop through the collection to create the actual Sales and SalesItems
            //JF- apply shipping to first sale only
            int nbSale = 0;
            foreach (SalesItemCollection col in collections)
            {
                nbSale++;
                if (nbSale > 1)
                {
                    shipping = 0; 
                }

                int productClass = col.ProductClass.ProductClassId;

                Sale sale = new Sale(int.MinValue,
                    lead.ConsultantID,
                    Carrier.FEDEX.CarrierId,
                    1,
                    PaymentTerm.Prepaid.PaymentTermId,
                    client.ClientSequenceCode,
                    client.ClientId,
                    saleStatus,
                    paymentMethodId,
                    short.Parse(PoStatus.Pending.PoStatusId.ToString()),
                    ProductionStatus.Default.ProductionStatusID,
                    int.MinValue, // Sponsor Consultant
                    int.MinValue, // AR Consultant
                    ARStatus.NotPaid.ARStatusID,
                    client.LeadId,
                    billingCompany,
                    short.MinValue, // upfront payment 
                    int.MinValue, // confirmer id
                    CollectionStatus.Default.CollectionStatusID,
                    ConfirmationMethod.CreditCard.ConfirmationMethodID,
                    CreditApprovalMethod.CreditApprovedByAR.CreditApprovalMethodID,
                    null, // PO number
                    creditInfo.creditCardNumber,
                    creditInfo.expDate,
                    DateTime.Now,	// sale date
                    shipping, // shipping fees
                    0, // shipping fees discounts
                    DateTime.MinValue, // payment due date
                    DateTime.MinValue, // confirmed date
                    scheduledDeliveryDate, // scheduled delivery date
                    DateTime.MinValue, // scheduled ship date
                    DateTime.MinValue, // actual ship date
                    null, // way bill no.
                    null, // comment
                    0, // is coupons sheet assigned
                    0, // total amount
                    DateTime.MinValue, // invoice date
                    DateTime.MinValue, // cancellation date
                    0, // is ordered
                    DateTime.MinValue,  // PO received date
                    0, // is delivered
                    0, // local sponsor found
                    DateTime.MinValue, // return date
                    DateTime.MinValue, // reship date
                    0, // upfront payment required
                    DateTime.MinValue, // upfront payment due date
                    0, // is sponsor required
                    DateTime.MinValue,  // actual delivery date
                    null,	// accounting comment
                    null,	// social security number
                    null,	// social security address
                    null,	// social security city
                    null,	// social security state
                    null,	// social security country
                    null,	// social security zip
                    0,
                    DateTime.MinValue,	// promise due date
                    0,	// general flag (always 0)
                    short.MinValue);	// fuel surcharge (always null));


                if (lead.Comments.Length > 0)
                {
                    Comments comment = new Comments(int.MinValue,
                                    2, // Priority : medium
                                    sale.SalesId,
                                    lead.ConsultantID,
                                    lead.LeadID,
                                    9, // departement : other
                                    DateTime.Now,
                                    lead.Comments);
                    comments.Add(comment);
                }

                int salesItemNo = 1;

                // create a SalesItem for each scratchbook of this product class
                foreach (ScratchBook sc in col)
                {
                    int productQuantity = 0;
                    decimal productPrice = decimal.Zero;

                    foreach (object[] i in salesProperties)
                    {
                        if (sc.ProductCode == (string)i[(int)ProductProperties.PRODUCT_CODE])
                        {
                            productQuantity = (int)i[(int)ProductProperties.QUANTITY];
                            productPrice = Convert.ToDecimal(i[(int)ProductProperties.PRICE]);
                            break;
                        }
                    }

                    SalesItem salesItem = new SalesItem(int.MinValue,
                        salesItemNo,
                        sc.ScratchBookId,
                        (short)ServiceType.Bulk.ServiceTypeId,
                        (short)productClass,
                        "",
                        productQuantity,
                        productPrice,
                        0,
                        "",
                        (productQuantity * productPrice), // total price,
                        0,
                        0,
                        0,
                        0,
                        0,
                        productQuantity,
                        null);

                    sale.SalesItems.Add(salesItem);
                    salesItemNo++;
                }
                // calculate the total amount for the products included in this Sale
                sale.CalculateTotalArAmount();
                sales += sale;
            }

        }
		
		
		/// <summary>
		/// Get the partner id.
		/// </summary>
		/// <returns>Partner Id.</returns>
		private int GetPartnerId()
		{
			if (Session[PARTNER_KEY] != null)
			{
				return (int) Session[PARTNER_KEY];
			}
			else 
			{
				// Lookup partner id.
				for (int i = 0; i < ApplicationSettings.GetConfig().GetCount("eFundData.Partner"); i++)
				{
					if (ApplicationSettings.GetConfig()["eFundData.Partner", i, "username"] == User.Identity.Name)
					{
						Session[PARTNER_KEY] = Convert.ToInt32(ApplicationSettings.GetConfig()["eFundData.Partner", i, "partnerId"]);
						return (int) Session[PARTNER_KEY];
					}
				}

				// Partner not found.
				return -1;
			}
		}
		
		
		private int GetPromotionId()
		{
			if (Session[PROMOTION_KEY] != null)
			{
				return (int) Session[PROMOTION_KEY];
			}
			else 
			{
				// Lookup partner id.
				for (int i = 0; i < ApplicationSettings.GetConfig().GetCount("eFundData.Partner"); i++)
				{
					if (ApplicationSettings.GetConfig()["eFundData.Partner", i, "username"] == User.Identity.Name)
					{
						Session[PROMOTION_KEY] = Convert.ToInt32(ApplicationSettings.GetConfig()["eFundData.Partner", i, "promotionId"]);
						return (int) Session[PROMOTION_KEY];
					}
				}

				// Partner not found.
				return -1;
			}
		}
		
		#endregion

		#region Authentication Methods

		/// <summary>
		/// Login method for the user of web service
		/// </summary>
		/// <param name="pUsername">Username to login in</param>
		/// <param name="pPassword">Password of the username</param>
		/// <returns></returns>
		[WebMethod(EnableSession=true,MessageName="Login", Description="Provide your Username and Password to Login on WebService.efundraising.com")]
		public bool Login(string pUsername, string pPassword) 
		{
			ClearError();
            pUsername = "fastfundraising.com";
            pPassword = "ergfrd45";

			string call = MethodCall("Login", pUsername, pPassword);
			Logger.LogInfo(call);

			try 
			{
				if (FormsAuthentication.Authenticate(pUsername, pPassword))
				{
					FormsAuthentication.SetAuthCookie(pUsername, false);
					return true;
				}
				else
				{
					PublishError("Login failed.", call);
					return false;
				}
			}
			catch (Exception ex)
			{
				PublishError("Login failed.", call, ex);
			}

			return false;
		}

		/// <summary>
		/// Function for Logout the user of web service
		/// </summary>
		/// <returns></returns>
		[WebMethod(EnableSession=true,Description="Provide the way to Logout of webservice.efundraising.com")]
		public bool Logout() 
		{
			ClearError();

			string call = MethodCall("Logout");
			Logger.LogInfo(call);

			if(User.Identity.IsAuthenticated) 
			{
				FormsAuthentication.SignOut();

				// Clear session
				Session.Clear();

				return true;
			} 
			else
				return false;
		}

		/// <summary>
		/// Function returning the value about if the current Web Service user is properly Authenticated
		/// </summary>
		/// <returns></returns>
		[WebMethod(EnableSession=true, Description="Get if the current user is authenticate")]
		public bool IsLoggedIn() 
		{
			ClearError();

			string call = MethodCall("IsLoggedIn");
			Logger.LogInfo(call);

			return User.Identity.IsAuthenticated;
		}
		
		#endregion
		
		#region Error Methods

		/// <summary>
		/// Clear the Current Error object
		/// </summary>
		public void ClearError() 
		{
			Session[LAST_ERROR_KEY] = "";
		}

		/// <summary>
		/// Function returning the last Error encounted by the current Web Service User
		/// </summary>
		/// <returns></returns>
		/// <remarks>Each Web Service Users have their own Error variable</remarks>
		[WebMethod(EnableSession=true, Description="Get the Lastest errors encounted on your Web Service session")]
		public string GetLastError() 
		{
			if (Session[LAST_ERROR_KEY] == null)
				return "";
			else
			{
				return (string) Session[LAST_ERROR_KEY];
			}
		}

		/// <summary>
		/// Set last error.
		/// </summary>
		/// <param name="error"></param>
		public void SetError(string error) 
		{
			Session[LAST_ERROR_KEY] = error;
		}

		#endregion
		
		#region Helper Methods
		/// <summary>
		/// Dump string representation of method call.
		/// </summary>
		/// <param name="methodName"></param>
		/// <param name="parameters"></param>
		/// <returns></returns>
		private string MethodCall(string methodName, params object[] parameters)
		{
			string call = methodName + "(";
			
			// Write out parameters
			if (parameters != null)
			{
				for (int i = 0; i < parameters.Length; i++)
				{
					if (i > 0)
						call += ", ";

					try 
					{
						if (parameters[i] == null)
							call += "null";
						else if (parameters[i].GetType().FullName == "System.String")
							call += "\"" + parameters[i] + "\"";
						else
							call += parameters[i].ToString();
					}
					catch 
					{
						call += "?";
					}
				}
			}

			call += ")";

			return call;
		}
		
		private void PublishError(string message)
		{
			PublishError(message, "");
		}

		private void PublishError(string message, string methodCall)
		{
			PublishError(message, methodCall, null);
		}

		private void PublishError(string message, string methodCall, Exception ex)
		{
			SetError(message);
			Logger.LogError(message + " " + methodCall, ex);
		}
		#endregion
		
	}
	
}
