using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using efundraising.Utilities;
using efundraising.Utilities.CookieHandler;
using efundraising.Utilities.Compression;
using System.Security.Principal;
using System.Web.Security;

namespace EFundraisingCRMWeb
{
	/// <summary>
	/// Summary description for CrmLogin.
	/// </summary>
	public partial class CrmLogin : System.Web.UI.Page
	{
        string page = "";
        string lid = "";
        string sid = "";
        string clid = "";
        string seq = "";

	
        private void SetParams()
        {
            page = Request["page"];
            lid = Request["lid"];
            sid = Request["sid"];
            clid = Request["clid"];
            seq = Request["seq"];
        }


        private string BuildParamString(bool redirect)
        {
            string redirectString = "";
            if (page != null)
            {
                if (redirect)
                {
                    redirectString = page + ".aspx?";
                }
                else
                {
                    redirectString = "?page=" + page + "&";
                }               
                

                if (lid != null)
                {
                    redirectString = redirectString + "lid=" + lid;
                }
                if (sid != null)
                {
                    redirectString = redirectString + "sid=" + sid;
                }
                if (clid != null)
                {
                    redirectString = redirectString + "clid=" + clid + "&";
                }
                if (seq != null)
                {
                    redirectString = redirectString + "seq=" + seq;
                }
            }

            return redirectString;

        }

		protected void Page_Load(object sender, System.EventArgs e)
        {

            SetParams();
            string paramString = BuildParamString(false);        

                if (!IsPostBack && (Request.Cookies["__LOGINCOOKIE__"] == null || Request.Cookies["__LOGINCOOKIE__"].Value == ""))
                {
                    //At this point, we do not know if the session ID that we have is a new
                    //session ID or if the session ID was passed by the client. 
                    //Update the session ID.

                    Session.Abandon();
                    Response.Cookies.Add(new HttpCookie("ASP.NET_SessionId", ""));

                    //To make sure that the client clears the session ID cookie, respond to the client to tell 
                    //it that we have responded. To do this, set another cookie.
                    AddRedirCookie();
                    Response.Redirect(Request.Path + paramString);
                }

            

                //Make sure that someone is not trying to spoof.
                try
                {
                    FormsAuthenticationTicket ticket =
                    FormsAuthentication.Decrypt(Request.Cookies["__LOGINCOOKIE__"].Value);

                    if (ticket == null || ticket.Expired == true)
                        throw new Exception();

                  //RemoveRedirCookie();
                }
                catch
                {
                    //If someone is trying to spoof, do it again.
                    AddRedirCookie();
                    Response.Redirect(Request.Path + paramString);
                }
            

            //    Response.Write("Session.SessionID=" + Session.SessionID + "<br/>");
            //   Response.Write("Cookie ASP.NET_SessionId=" + Request.Cookies["ASP.NET_SessionId"].Value + "<br/>");
               //-------------------------------------------------


                if (!IsPostBack)
                {
                string name = GetLogInCookie();

                    /*
                if (CookieHandler.CookieExists(System.Web.HttpContext.Current.Request, Components.Server.ApplicationConstants.AuthenticationCookieName))
                {
                   name = CookieHandler.CookieValue(System.Web.HttpContext.Current.Request, Components.Server.ApplicationConstants.AuthenticationCookieName);
                }
                     * */
              
                UsernameTextBox.Text = name;
                System.Web.HttpContext.Current.Session[Global.SessionVariables.USER_NAME] = name;
            }

            System.Web.HttpContext.Current.Session["traceLOG"] = "Start";
            Trace.Write(Session["TraceLOG"].ToString());
            
 
            bool authentication =
            (bool)(Convert.ToBoolean(efundraising.EnterpriseComponents.Helper.GetWebConfigValue("IntegratedLogin")));
            if (authentication)
            {
                lblDisabled.Visible = false;
            }
            else
            {
                lblDisabled.Visible = true;
            }


			// Put user code to initialize the page here
		}

		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			InitializeComponent();
			base.OnInit(e);
		}
		
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{    

		}
		#endregion

		protected void LoginButton_Click(object sender, System.EventArgs e) {

            try
            {

                SetParams();
                string redirectString = BuildParamString(true);

                Components.Server.User.CrmUser crmUser =
                    Components.Server.User.CrmUser.FromIntegratedLogin(UsernameTextBox.Text, PassswordTextBox.Text);

                Trace.Write("CRM USER CREATED");
                if (crmUser != null)
                {

                    if (efundraising.Utilities.CookieHandler.CookieHandler.IsCookieEnable(Request))
                    {
                        string userName = crmUser.Name;
                        string userPassword = PassswordTextBox.Text;

                        //	efundraising.Utilities.Encryption.DES.TripleDES tripleDES =
                        //	new efundraising.Utilities.Encryption.DES.TripleDES();
                        //	userPassword = tripleDES.Encrypt(userPassword, Components.Server.ApplicationConstants.EncryptionKey);

                        //  Session["TraceLOG"] = Session["TraceLOG"].ToString() + "PWD:" + userPassword + "      ";

                        //efundraising.Utilities.CookieHandler.CookieHandler.SetCookie(Request, Response, Components.Server.ApplicationConstants.AuthenticationCookieName, userName);
                        SetLogInCookie(userName);


                        //efundraising.Utilities.CookieHandler.CookieHandler.SetCookie(Request, Response, Components.Server.ApplicationConstants.AuthenticationCookiePassword, userPassword);
                    }

                    crmUser.Save(Session);
                    Session["TraceLOG"] = Session["TraceLOG"].ToString() + "SAVED crmUser to session";

                    string redirection = "Sales/SalesScreen/" + redirectString;
                    Response.Redirect(redirection,false);


                    //object redirection = Components.Server.CurrentWorkingObject.Get(Session, "_REDIRECTION_FROM_SECURITY_");
                    /*   if (redirection == null)
                   {
                       redirection = "Sales/SalesScreen/Default.aspx";
                   
                   }
                   Session["TraceLOG"] = Session["TraceLOG"].ToString() + "REDIRECTION:" + redirection.ToString() + "     ";
                   if(redirection != null) {
                       Response.Redirect(redirection.ToString());
         
                       //Response.RedirectLocation = redirection.ToString();
                   } else {
                       Response.Redirect("IT/Home/Default.aspx");
                   }*/
                }
                else
                {
                    Response.Write("Unable to login");
                }

                Trace.Write(Session["TraceLOG"].ToString());
            }
            catch (Exception ex)
            {
                efundraising.Diagnostics.Logger.LogError("Sales Screen: Login Click", ex);
        

            }
        
        }

      
        private void RemoveRedirCookie()
        {
            Response.Cookies.Add(new HttpCookie("__LOGINCOOKIE__", ""));
        }

        private void AddRedirCookie()
        {

            FormsAuthenticationTicket ticket =
            new FormsAuthenticationTicket(1, "dfjrss", DateTime.Now, DateTime.Now.AddSeconds(20), false, "");
            string encryptedText = FormsAuthentication.Encrypt(ticket);
            Response.Cookies.Add(new HttpCookie("__LOGINCOOKIE__", encryptedText));
        }

        private void SetLogInCookie(string userName)
        {
            HttpCookie cookie = new HttpCookie("Login");
            cookie.Value = userName;
            cookie.Expires = DateTime.Now.AddDays(5);
            // Insert the cookie in the current HttpResponse.
            Response.Cookies.Add(cookie);
        }

        private string GetLogInCookie()
        {
            HttpCookie cookie = Request.Cookies.Get("Login");
            if (cookie != null)
            {
                return cookie.Value;
            }
            else
            {
                return "";
            }
        }

	}
    
}
