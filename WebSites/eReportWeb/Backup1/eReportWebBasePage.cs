using System;

using efundraising.eReport;
using efundraising.eReport.DataAccess;
using efundraising.Utilities.CookieHandler;
using efundraising.eReportWeb.Components.Server;

namespace efundraising.eReportWeb
{
	/// <summary>
	/// Summary description for eReportWebBasePage.
	/// </summary>
	
	using efundraising.Intranet.BusinessEntities.Security.Principal;
	using efundraising.Intranet.BusinessEntities.Security.Identity;
	using System.Security.Principal;


	public class eReportWebBasePage : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.Label LabelLogin = new System.Web.UI.WebControls.Label();
		protected efundraising.eReport.User userLogin = null;
		protected efundraising.efundraisingCore.Partner thePartner
		{
			get
			{
				return efundraising.efundraisingCore.Partner.CreateOnly(Session);
			}
			set
			{
				efundraising.efundraisingCore.Partner p =(efundraising.efundraisingCore.Partner)value;
				if (p != null)
					p.Save(Session);
			}
		}

		private string hostSessionId = "hostSessionId";

		public eReportWebBasePage() {}

		protected override void OnInit(EventArgs e)
		{
			userLogin = efundraising.eReport.User.CreateOnly(Session) ;//efundraising.efundraisingCore.Partner.CreateOnly(Session);

			if(!IsPostBack)
			{
				// Check if we are in External Mode in Web.config
				if(Config.IsExternal)
				{
					// Show Login link on all pages except login page
					if(!(this is Login))
					{
						LabelLogin.Text = "<a href=\"Login.aspx\">Login</a> >> ";
						LabelLogin.CssClass = "Passive";
						LabelLogin.Visible = true;
					}

					// If partner is not authenticated, try cookies
					if(userLogin == null)
					{
						// Check if the partner is authenticated in a cookie
						if(!AuthenticateWithCookie())
						{
							// No redirection if page is Login
							if(!(this is Login))
							{
								// Redirect to Login.aspx with in the request the page that user was trying to view
								// because no authentication was found
								System.Text.StringBuilder path = new System.Text.StringBuilder(Request.FilePath);
								path.Replace(Request.ApplicationPath + "/", "");
								if(Request["rid"] != null)
									path.Append("?rid=" + Request["rid"]);
								Response.Redirect("Login.aspx?RedirectUrl=" + path);
							}
						}
					}
				}
				// If Internal login page is not needed
				else
				{
					CreateInternalUserLogin();
					if(this is Login)
						Response.Redirect("ReportSelection.aspx");
				}

				base.OnInit(e);
			}
		}
		
		private void CreateInternalUserLogin()
		{
			CustomIdentity custIdentity = CustomIdentity.GetCustomIdentityFromCookie(this.Page.Request);
			if (userLogin != null)
			{
				if (custIdentity == null) 
					return;  
				else 
				{
					string theName = (custIdentity as IIdentity).Name.Trim();
					if (theName == string.Empty || userLogin.UserName == theName)
						return;
				}
			}
			if (custIdentity != null)
			{
				userLogin = new efundraising.eReport.User((custIdentity as IIdentity).Name);
				userLogin.Roles = custIdentity.UserRoles.Split('|');
				userLogin.IsActiveDirectoryUser = true;
				userLogin.Save(Session);
			}
			
		}

		#region Private Methods
		/// <summary>
		/// 2 cookies should exist if the partner is authenticated with a cookie. 
		/// First we try Username, then the encrypted password cookie and it returns
		/// true only if the partner is saved in the session
		/// </summary>
		/// <returns>Boolean on if the partner was successfully authenticated with cookies</returns>
		private bool AuthenticateWithCookie()
		{
			try
			{
				if(CookieHandler.CookieExists(Request, "USERNAME"))
				{
					if(CookieHandler.CookieValue(Request, "USERNAME") != "")
					{
						userLogin = efundraising.eReport.User.GetUserByUserName(CookieHandler.CookieValue(Request, "USERNAME"));
					}

					if(userLogin != null)
					{
						// Verify password
						if(CookieHandler.CookieExists(Request, "PASSWORD"))
						{
							Utilities.Encryption.DES.TripleDES tripleDES = new Utilities.Encryption.DES.TripleDES();
							if(userLogin.Password == tripleDES.Decrypt(CookieHandler.CookieValue(Request, "PASSWORD"),"H4LV9TB"))
							{
								// Authenticate the partner in the session
								userLogin.Save(Session);
								return true;
							}
						}
					}
				}
			}
			catch
			{
			}
			return false;
		}
		#endregion
	}
}