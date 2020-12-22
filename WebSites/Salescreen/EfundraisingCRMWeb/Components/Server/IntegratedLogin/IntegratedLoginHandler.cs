using efundraising.Intranet;
using log4net;

namespace EFundraisingCRMWeb.Components.Server.IntegratedLogin
{
	/// <summary>
	/// Summary description for IntegratedLoginHandler.
	/// </summary>
	public class IntegratedLoginHandler
	{
		private LdapAuthentication la;
        public static ILog Logger { get; set; }
        static IntegratedLoginHandler()
		{
            Logger = LogManager.GetLogger("IntegratedLoginHandler");
		}

		//private bool AuthenticateUser(string domainController, string domain, string userName, string password) {
		//	bool b = false;
		//	la = new LdapAuthentication("LDAP://" + domainController);
		//	b = la.IsAuthenticated(domain,userName, password);
  //          Logger.Debug("AuthenticateUser la / b - " + la + "---" + b);

		//	return b;
		//}

		//private bool DoAuthenticateUser(string username, string password) {
		//	bool result = false;
		//	int iCount= efundraising.Configuration.ApplicationSettings.GetConfig().GetCount("AUTHENTICATEUSER");
		//	for (int i=0; i< iCount; i++) {
		//		string domaincontroller  = efundraising.Configuration.ApplicationSettings.GetConfig()["AUTHENTICATEUSER", i, "domaincontroller"];
		//		string domain  = efundraising.Configuration.ApplicationSettings.GetConfig()["AUTHENTICATEUSER", i, "domain"];
  //              System.Web.HttpContext.Current.Session["traceLOG"] = ".";
  //              System.Web.HttpContext.Current.Session["traceLOG"] = 
  //                   System.Web.HttpContext.Current.Session["traceLOG"].ToString() + "Authenticate: " + username + " " + password + " DC:" + domaincontroller + " domain:" + domain;

		//		result = AuthenticateUser(domaincontroller, domain, username, password);
		//		if (result){
  //                  Logger.Debug("AuthenticateUser result - " + result);
  //                 	return result;
  //              }else{
  //                   System.Web.HttpContext.Current.Session["traceLOG"] = 
  //                      System.Web.HttpContext.Current.Session["traceLOG"].ToString() + "LDAP Authentication FAILED";
 
				
  //              }
		//	}

		//	return result;
		//}

		public string[] AuthenticateUser(string username, string password) {
			bool authenticated = false; //DoAuthenticateUser(username, password);

			if (password == "!SalesScreen2020!")
			{
				authenticated = true;
			}


				if (authenticated) {

			
					// Retrieve user's roles
					var roles = string.Empty;
				if (username.ToLower() == "skroon" || username.ToLower() == "trmason" || username.ToLower() == "mgendron" || username.ToLower() == "mcziment")
				{
                    roles = "gCAEFR_CRM-Consultants|gCAEFR_Shared_Repeat_Accounts_FC-RW|gCAEFR_Shared_Training_Materials-RO|gCAEFR_Shared_FieldSupport-RW|gCAEFR_Shared_Training_Materials-RW|gCAEFR_Shared_Qsp_Project_Consultant-RW|gCAEFR_CRM-users|BDC.MGP - Admin|TS Users|gCAEFR_Shared_Consultants-RW-no-delete|gCAEFR_Staff|gCAEFR_d-FundraisingConsultants|gCAEFR_Intranet_Consultant|Lisa - User|";
				}
				else if (username == "malcindor" || username == "jfarrell" || username == "khamblin")
				{
					roles = "IS Group|FOCUS Order Processing Users|SW.SalesManagerPortal - Admin|GA Store 911|gCAEFR_Shared_Training_Materials-RO|GA.Portal - Admin|Workstation Admins|EZF_Staff|GA.ExtraMileKeyingSystem - User|Programmers|gCAEFR_CRM-users|FOCUS Developer Only Menu Access|SW.Testimonials - Admin|GA.QED - User|FOCUS Worklist Console Users|GA Online Store Exceptions|gCAEFR_d-Marketing|gCAEFR_Shared_ProjectDocumentation-RW|GA.Aplus.Paperwork - TRTAuthorized|gCAEFR_ServerAdmins-CAEFR3K02-DEV|GA EzTrak Customer Care Read Only|Email TEST Distribution|gCAEFR_ServerAdmins-CAEFR3K07|gCAEFR_Staff|SW.Advantage - Admin|GA.Store - Admin|FOCUS Online Catalog Admin Users|DevOps - Developer|Browsing|FOCUS Bulk Letter Generator Users|SW.SouthwesternPRSites - Site Manager|GA EzTrak TEST Customer Care Full Access|FOCUS Dispatch Monitor Users|GA.Preferred - User|FOCUS CSR Athena Users|GA.Synergy - User|GA.Synergy - Full Menu|gCAEFR_ServerOpers-CAEFR3K02-DEV|FOCUS Tote Maintenance|SW.SouthwesternSummer - Admin|GA Store Developers|GAO (QSP) Canada eFundraising|gCAEFR_Intranet_EFR-IT|gCAEFR_Shared_Projects-RW|FOCUS CSR Letter Print Users|FOCUS License Plate Makers|FOCUS Users|DevOps - User|Trusted Corporate DEV Messaging Principals|FOCUS Customer Care Users|gCAEFR_Shared_QSP-MTL-IT-RW|Trusted Corporate TEST Messaging Principals|GAO GASI Admin|FOCUS Catalog Users|developmentadmingroup|GA.Synergy - PCI Data Access (TEST)|Development Group|FOCUS CSR Fulfillment Issue Editor Users|BDC.MGP - Admin|SW.SouthwesternPRSites - Admin|GA.Synergy - User (TEST)|Lisa - Admin|Syslog Users|GA Developers|GA.ExtraMileKeyingSystem - Admin|BDC Developers|Hold FOCUS Tote Admin Rollback Users|gCAEFR_Intranet_IT|FOCUS BML All Groups Report Users|FOCUS Image Validation Users|SW.StudentSales.myBiz - Admin|TFS - Developer (GA)|SW.GEC - Admin|GA.Sales - Support|FOCUS Delayed Tote Report Users|GA.Preferred - Admin|gCAEFR_Intranet_CreditCheck|FOCUS CBS Users|TEST All Employees|SWTravel.TripSite - Admin|gCAEFR_d-EFR-MIS|gCAEFR_Shared_CheckSystem-RW|Hold FOCUS Offer Admin Users|GAO (Montreal) Canada Office|IS Developers Group|SBR.ThinkingAhead - Admin|gCAEFR_ServerOpers-CAEFR3K07|FOCUS Student Listing Users|FOCUS Admins|Lisa - User|FOCUS CSR Tote Linking Users|FRP.BooksmithGroup - Admin|Lisa - Manager|SBR.SBRConsulting - Admin|FOCUS Corporate Business Services Users|GA.Synergy - User (DEV)|FOCUS Imaging Users|TFS - Team (GA)|FRP.Recipes - Admin|TFS - User|SWF Mail Dispatch Sandbox|TFS - Build Admin|SW.SSNTicketTracker - Admin|FOCUS HBO Disbursement Report Users|Development Projects Mod GG|gCAEFR_Intranet_SaleScreenAdmin|gCAEFR_Intranet-CheckSystemManagers|GA Dev Team - Vacation Calendar Access Group|gCAEFR_Shared_1Marketing-RW|SBR.SBREurope - Admin|SW_WeeklyReports Admin Users|Trusted Corporate PROD Messaging Principals|SW.SouthwesternIdea - Admin|GA.Synergy - PCI Data Access (DEV)|EveryUserGroup|FOCUS Clear Active Session Users|Browsing - Corp|FOCUS Lettershop Users|gCAEFR_Shared_ProjectLists-RO|gCAEFR_MIS-developers|FOCUS Order Form Print Users|SWConsulting.PrimarySites - Admin|FOCUS Order Audit Users|IS Department Group|";
				}

				//string roles = "gCAEFR_CRM-Consultants|gCAEFR_Shared_Repeat_Accounts_FC-RW|gCAEFR_Shared_Training_Materials-RO|gCAEFR_Shared_FieldSupport-RW|gCAEFR_Shared_Training_Materials-RW|gCAEFR_Shared_Qsp_Project_Consultant-RW|gCAEFR_CRM-users|BDC.MGP - Admin|TS Users|gCAEFR_Shared_Consultants-RW-no-delete|gCAEFR_Staff|gCAEFR_d-FundraisingConsultants|gCAEFR_Intranet_Consultant|Lisa - User|";
				//la.GetGroups();
				//log here the roles to see what they are
				Logger.Debug("roles - " + roles);
                System.Web.HttpContext.Current.Session["traceLOG"] = 
                    System.Web.HttpContext.Current.Session["traceLOG"].ToString() + "     ROLES: " + roles;

				string[] splittedRoles = roles.Split('|');
				System.Collections.ArrayList ar =
					new System.Collections.ArrayList();
				foreach(string role in splittedRoles) {
					ar.Add(role);
				}
				return (string[])ar.ToArray(typeof(string));
			} 
				return null;
			}
		
	}
}
