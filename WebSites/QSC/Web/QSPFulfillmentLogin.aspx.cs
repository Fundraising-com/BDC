using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using QSPFulfillment.CommonWeb;

namespace QSPFulfillment
{
	/// <summary>
	/// Summary description for QSPFulfillmentLogin.
	/// </summary>
	public partial class QSPFulfillmentLogin : QSPFulfillment.CommonWeb.QSPPage
	{
		protected login QLogin;
		private bool AsBeenClick = false;
		private static int Timeout = 0;

		private Business.UserProfile newUserProfile;
        
		override protected void OnInit(EventArgs e)
		{
			InitializeComponent();
			base.OnInit(e);
		}
		
		private void InitializeComponent()
		{    
			this.QLogin.Click +=new EventHandler(QLogin_Click);
			this.QLogin.PreRender +=new EventHandler(QLogin_PreRender);
		}
		
		 
		protected void Page_Load(object sender, System.EventArgs e)
		{
            // Try and login this user
			if(!IsPostBack)
			{
				Menu1.Visible = false;
			}
		}
		private void QLogin_PreRender(object sender, EventArgs e)
		{
			if(!AsBeenClick && !IsPostBack)
			{
				if(Request.QueryString["Logout"] != null)
				{
					lblStatus.Text = "You have been successfully logged out of the QSP Fulfillment system.";
					FormsAuthentication.SignOut();
					Session.Clear();
				}
				else if(Request.QueryString["Error"] != null)
				{
					lblStatus.Text = "The system was unable to detect your session.  You may have timed out.<br />"
						+ "Please log in to continue.";
				}
				else if(this.Page.User.Identity.IsAuthenticated && Request.QueryString["Logout"] == null)
				{
					lblStatus.Text = "You don't have the right to see this page, Please enter another user who have the right <br>or continue your navigation by using the menu.";
					Menu1.Visible = true;
				}
			}
		}
		

		private void QLogin_Click(object sender, EventArgs e)
		{
			newUserProfile = new Business.UserProfile();
			AsBeenClick = true;	
			if(newUserProfile.Login( QLogin.UserID, QLogin.Password ))
			{
					
				QSPPage.aUserProfile = newUserProfile;

				Session.Add("FMID", newUserProfile.FMID);
				Session.Add("Instance", newUserProfile.Instance);
				Session.Add("UserName", newUserProfile.UserName);
					
				FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(
					1, // Ticket version
					newUserProfile.UserName, // Username associated with ticket
					DateTime.Now, // Date/time issued
					DateTime.Now.AddMinutes(GetMinuteTimeOut()), // Date/time to expire
					false, // "true" for a persistent user cookie
					GetListRoles(), // User-data, in this case the roles
					FormsAuthentication.FormsCookiePath); // Path cookie valid for
 
				// Hash the cookie for transport
				string hash = FormsAuthentication.Encrypt(ticket);
				HttpCookie cookie = new HttpCookie(
					FormsAuthentication.FormsCookieName, // Name of auth cookie
					hash); // Hashed ticket
						
				// Add the cookie to the list for outgoing response
				Response.Cookies.Add(cookie);
				
				if(Request.QueryString["ReturnURL"] == null)
				{
					Response.Redirect("Home.aspx");
				}
				else
				{
					Response.Redirect(Request.QueryString["ReturnURL"]);
				}

			}
			else
			{
				lblStatus.Text = "Login failed.  Please try again or contact support if problems persist.";
			}
		}

		private int GetMinuteTimeOut()
		{
			if(Timeout == 0)
			{
				System.Xml.XmlDocument x = new System.Xml.XmlDocument();
				x.Load(Context.Server.MapPath(Context.Request.ApplicationPath ) + "/web.config");
				System.Xml.XmlNode node = x.SelectSingleNode("/configuration/system.web/authentication/forms");
				Timeout = int.Parse(node.Attributes["timeout"].Value,System.Globalization.CultureInfo.InvariantCulture.NumberFormat);
			}
			return Timeout;

			

		}
	}
}

