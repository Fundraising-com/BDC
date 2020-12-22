//
//	April	07, 2005	-	Louis Turmel	- Implementation and creation of class
//	April   11, 2005	-	Louis Turmel	- Implementation of xFramework.xUtilities.Notification
//	April   14, 2005	-	Louis Turmel	- Changing GetPartnerReport as GetLeadsReport
//  April	20, 2005	-	Louis Turmel	- IsLoggedIn Function Added as WebMethod
//											- Add new Code to getting the value about the new Fields returned
//	April	21, 2005	-	Louis Turmel	- Add of GetLastError Implementation
//												- With the addition of IError Interface
//												- And addition of Error members as Session variable
//												- Change the parameters validation functions
//	April	22, 2005	-	Louis Turmel	- Efundraising Lead Interagion added
//	April	27, 2005	-	Louis Turmel	- Add GetRptPartnerLeads Functions
//  April	29, 2005	-	Stephen Lim		- Commented out AddNewLeads
//	July	26, 2005	-	Stephen Lim		- Add AddNewLead method. 
//											- Clean up all code. 
//
using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Web;
using System.Web.Security;
using System.Web.Services;

using efundraising;
using efundraising.Collections;
using efundraising.Configuration;
using efundraising.Diagnostics;
using efundraising.Globalization;
using efundraising.Web;
using efundraising.Web.Services;
using efundraising.Web.Services.eFundSoap;
using efundraising.Web.Services.eFundSoap.Types;
using efundraising.Web.Services.eFundSoap.Errors;
using efundraising.Web.Services.eFundSoap.Tracing;
using efundraising.efundraisingCore;

namespace efundraising.eFundData {

	/// <summary>
	///	Web Services provides methods to get Information about efundraising.com web site
	/// </summary>
	[WebService(Namespace="http://webservices.efundraising.com", Description="Web Service for eFundraising.com Web Site")]
	public class eFundData : System.Web.Services.WebService
	{

		#region Constants

		private const string LAST_ERROR_KEY = "LastError";
		private const string PARTNER_KEY = "Partner";
		private const string PROMOTION_KEY = "Promotion";
		private const int LEAD_STATUS_ID = 1; // Free kit form lead type.

		#endregion

		#region Constructors

		/// <summary>
		/// Default class constructor
		/// </summary>
		public eFundData() {
			
			InitializeComponent();
		}

		#endregion

		#region Component Designer generated code
		
		private IContainer components = null;
		
		private void InitializeComponent() {

		}
		
		protected override void Dispose( bool disposing ) {
			if(disposing && components != null) {
				components.Dispose();
			}
			base.Dispose(disposing);		
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
		[WebMethod(EnableSession=true, Description="Check if the current user is authenticated.")]
		public bool IsLoggedIn() 
		{
			ClearError();

			string call = MethodCall("IsLoggedIn");
			Logger.LogInfo(call);

			return User.Identity.IsAuthenticated;
		}

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
		public void AddNewLead(string pFirstName, string pLastName, string pEmail, string pAddress, string pCity,
			string pState, string pZip, string pCountry, string pDayPhone, string pEveningPhone, int pGroupSize,
			string pOrganizationName, int pPromotionID, string pTitle, string pEveningPhoneExt, string pDayPhoneExt,
			string pBestTimeToCall, int pOrganizationTypeID, int pGroupTypeID, string pFundraisingDate,
			bool pDecisionMaker, string pProductsInterestIn, bool pOnEmailList, string pComments) {
			

			ClearError();

			string call = MethodCall("AddNewLead", pFirstName, pLastName, pEmail, pAddress, pCity,
							pState, pZip, pCountry, pDayPhone, pEveningPhone, pGroupSize,
							pOrganizationName, pPromotionID, pTitle, pEveningPhoneExt, pDayPhoneExt,
							pBestTimeToCall, pOrganizationTypeID, pGroupTypeID, pFundraisingDate,
							pDecisionMaker, pProductsInterestIn, pOnEmailList, pComments);
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

					try 
					{
						newLead.PartnerID = GetPartnerId(); 
						newLead.Integrate();
					}
					catch 
					{
						// On error, always insert into temporary 
						// table to avoid losing lead
						TempLead tempLead = new TempLead(newLead);
						tempLead.Insert();
					}

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
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="pStartDate"></param>
		/// <param name="pEndDate"></param>
		/// <returns></returns>
		[WebMethod(EnableSession=true, Description="")]
		//public Fundraiser[] GetLeadsReport(int partnerID, string username, string password, string pStartDate, string pEndDate) {
		public Fundraiser[] GetLeadsReport(string pStartDate, string pEndDate) {
			ClearError();

			string call = MethodCall("GetLeadsReport", pStartDate, pEndDate);
			Logger.LogInfo(call);

			Fundraiser[] oFund = new Fundraiser[0];

			try 
			{
				// Parse dates
				DateTime startDate = DateTime.Parse(pStartDate);
				DateTime endDate = DateTime.Parse(pEndDate);

				if(IsLoggedIn()) 
				{
					int partnerID = GetPartnerId();
					if(partnerID == -1) {
						throw new Exception("Partner Id cannot be -1");
					}
					Database.efundraising.DatabaseObject oEFund = new Database.efundraising.DatabaseObject();
					
					DataTable oTbl = oEFund.GetRptPartnerLeads(partnerID, startDate, endDate);
					if (oTbl != null) 
					{
						oFund = new Fundraiser[oTbl.Rows.Count];
						for(int i=0;i<oTbl.Rows.Count;i++) 
						{
							int groupSize = 0;

							try 
							{
								groupSize = int.Parse(oTbl.Rows[i]["participant_count"].ToString());
							}
							catch {}
							
							DateTime date = DateTime.MinValue;
							if(oTbl.Rows[i]["date"] != null) {
								//date = (DateTime)oTbl.Rows[i]["date"];
							}

							oFund[i] = new Fundraiser(oTbl.Rows[i]["first_name"].ToString(),
								oTbl.Rows[i]["last_name"].ToString(), oTbl.Rows[i]["email"].ToString(),
								oTbl.Rows[i]["address"].ToString(), oTbl.Rows[i]["city"].ToString(),
								oTbl.Rows[i]["state_code"].ToString(),oTbl.Rows[i]["zip_code"].ToString(),oTbl.Rows[i]["country_code"].ToString(),
								oTbl.Rows[i]["day_phone"].ToString(), oTbl.Rows[i]["evening_phone"].ToString(),
								oTbl.Rows[i]["lead_entry_date"].ToString(),oTbl.Rows[i]["organization_type"].ToString(), oTbl.Rows[i]["group_type"].ToString(),
								groupSize, oTbl.Rows[i]["organization"].ToString(), oTbl.Rows[i]["interests"].ToString(),
								oTbl.Rows[i]["promotion_description"].ToString());
						}
					}
				}
				else 
				{
					throw new Exception("Authentication failed");
				}
			}
			catch (Exception ex)
			{
				PublishError("GetLeadsReport failed.", call, ex);
			}

			return oFund;
		}

		/// <summary>
		/// Function returning an array of Fundraiser about the sales of Leads between two dates
		/// </summary>
		/// <param name="pStartDate">Start date of Report</param>
		/// <param name="pEndDate">End Date of the Report</param>
		/// <returns>return an efundraising.eFundData.eFundType.Customers.Fundraiser array</returns>
		[WebMethod(EnableSession=true, CacheDuration=60, Description="Get a Listing of Users ...")]
		public Fundraiser[] GetSalesReport(string pStartDate, string pEndDate) {

			ClearError();

			string call = MethodCall("GetSalesReport", pStartDate, pEndDate);
			Logger.LogInfo(call);


			Fundraiser[] oFund = new Fundraiser[0];

			try
			{
				// Parse dates
				DateTime startDate = DateTime.Parse(pStartDate);
				DateTime endDate = DateTime.Parse(pEndDate);

				if(IsLoggedIn()) 
				{
					Database.efundraising.DatabaseObject oEFund = new Database.efundraising.DatabaseObject();
					if(GetPartnerId() != -1) 
					{
						DataTable oTbl = oEFund.GetRptPartnerSales(GetPartnerId(), startDate, endDate);
						if(oTbl != null) 
						{
							oFund = new Fundraiser[oTbl.Rows.Count];
							for(int i=0;i<oTbl.Rows.Count;i++) 
							{
								int groupSize = 0;
								try 
								{
									groupSize = int.Parse(oTbl.Rows[i]["participant_count"].ToString());
								}
								catch {}

								int totalProduct = 0;
								try 
								{
									totalProduct = int.Parse(oTbl.Rows[i]["total_product"].ToString());
								}
								catch {}

								int salesId = 0;
								try 
								{
									salesId = int.Parse(oTbl.Rows[i]["sales_id"].ToString());
								}
								catch {}

								double totalAmount = 0.0;
								try 
								{
									totalAmount = double.Parse(oTbl.Rows[i]["total_amount"].ToString());
								}
								catch {}


								// We get the fundraiser informations
								oFund[i] = new Fundraiser(oTbl.Rows[i]["First_Name"].ToString(), 
									oTbl.Rows[i]["Last_Name"].ToString(), oTbl.Rows[i]["Email"].ToString(),
									oTbl.Rows[i]["Address"].ToString(), oTbl.Rows[i]["City"].ToString(),
									oTbl.Rows[i]["State_Code"].ToString(), oTbl.Rows[i]["Zip_Code"].ToString(),
									oTbl.Rows[i]["Country_Code"].ToString(), oTbl.Rows[i]["Day_Phone"].ToString(),
									oTbl.Rows[i]["Evening_Phone"].ToString(), oTbl.Rows[i]["ship_date"].ToString(),
									oTbl.Rows[i]["Organization_Type"].ToString(), oTbl.Rows[i]["Group_Type"].ToString(),
									groupSize, oTbl.Rows[i]["organization"].ToString(), oTbl.Rows[i]["interests"].ToString(), "");
								// We get the sales
								oFund[i].Sale = new Sales(oTbl.Rows[i]["sales_date"].ToString(), oTbl.Rows[i]["confirmed_date"].ToString(),
									oTbl.Rows[i]["ship_date"].ToString(), oTbl.Rows[i]["product_class_desc"].ToString(),
									totalProduct, salesId,
									totalAmount);
							}
						}
					}
				}
				else 
				{
					throw new Exception("Authentication failed");
				}
			}
			catch (Exception ex)
			{
				PublishError("GetSalesReport failed.", call, ex);
			}

			return oFund;			
		}


		#endregion

		#region Partner Methods
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

		private int GetPartnerId(string username, string password) {
			if (Session[PARTNER_KEY] != null) {
				return (int) Session[PARTNER_KEY];
			}
			else {
				// Lookup partner id.
				for (int i = 0; i < ApplicationSettings.GetConfig().GetCount("eFundData.Partner"); i++) {
					if (ApplicationSettings.GetConfig()["eFundData.Partner", i, "username"] == username &&
						ApplicationSettings.GetConfig()["eFundData.Partner", i, "password"] == password) {
						Session[PARTNER_KEY] = Convert.ToInt32(ApplicationSettings.GetConfig()["eFundData.Partner", i, "partnerId"]);
						return (int) Session[PARTNER_KEY];
					}
				}

				// Partner not found.
				return -1;
			}
		}

		#endregion

		#region Promotion Methods
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
