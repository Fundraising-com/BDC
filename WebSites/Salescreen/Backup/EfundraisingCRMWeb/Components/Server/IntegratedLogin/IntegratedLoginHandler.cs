using System;
using efundraising.Intranet;

namespace EFundraisingCRMWeb.Components.Server.IntegratedLogin
{
	/// <summary>
	/// Summary description for IntegratedLoginHandler.
	/// </summary>
	public class IntegratedLoginHandler
	{
		private LdapAuthentication la;

		public IntegratedLoginHandler()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		private bool AuthenticateUser(string domainController, string domain, string userName, string password) {
			bool b = false;
			la = new LdapAuthentication("LDAP://" + domainController);
			b = la.IsAuthenticated(domain,userName, password);
			return b;
		}

		private bool DoAuthenticateUser(string username, string password) {
			bool result = false;
			int iCount= efundraising.Configuration.ApplicationSettings.GetConfig().GetCount("AUTHENTICATEUSER");
			for (int i=0; i< iCount; i++) {
				string domaincontroller  = efundraising.Configuration.ApplicationSettings.GetConfig()["AUTHENTICATEUSER", i, "domaincontroller"];
				string domain  = efundraising.Configuration.ApplicationSettings.GetConfig()["AUTHENTICATEUSER", i, "domain"];
                System.Web.HttpContext.Current.Session["traceLOG"] = ".";
                System.Web.HttpContext.Current.Session["traceLOG"] = 
                     System.Web.HttpContext.Current.Session["traceLOG"].ToString() + "Authenticate: " + username + " " + password + " DC:" + domaincontroller + " domain:" + domain;

				result = AuthenticateUser(domaincontroller, domain, username, password);
				if (result){
                   	return result;
                }else{
                     System.Web.HttpContext.Current.Session["traceLOG"] = 
                        System.Web.HttpContext.Current.Session["traceLOG"].ToString() + "LDAP Authentication FAILED";
 
				
                }
			}

			return result;
		}

		public string[] AuthenticateUser(string username, string password) {
			bool authenticated = DoAuthenticateUser(username, password);
			if (authenticated) {
				// Retrieve user's roles
				string roles = la.GetGroups();
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
