using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using efundraising.Configuration;
using System.Data.Sql;
using System.Data.SqlClient;

namespace AdminSection
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Login1_Authenticate(object sender, AuthenticateEventArgs e)
        {
           /* if (Request.Cookies[FormsAuthentication.FormsCookieName] != null)
            {
             
                if (Request.Cookies[FormsAuthentication.FormsCookieName] != null) {
                    FormsAuthenticationTicket fat = (FormsAuthenticationTicket) Request.Cookies[FormsAuthentication.FormsCookieName]; 
                }
            }*/




            string returnUrl = Request.QueryString["ReturnUrl"];

            bool Authenticated = false;
            Authenticated = SiteLevelCustomAuthenticationMethod(Login1.UserName, Login1.Password);
            e.Authenticated = Authenticated;
            if (Authenticated == true)
            {

                FormsAuthentication.Initialize();
                String strRole = "";// AssignRoles(txtUsername.Text);

                //The AddMinutes determines how long the user will be logged in after leaving
                //the site if he doesn't log off.
                FormsAuthenticationTicket fat = new FormsAuthenticationTicket(1,
                    Login1.UserName, DateTime.Now,
                    DateTime.Now.AddMinutes(30), false, strRole,
                    FormsAuthentication.FormsCookiePath);
                Response.Cookies.Add(new HttpCookie(FormsAuthentication.FormsCookieName,
                    FormsAuthentication.Encrypt(fat)));

                

                //FormsAuthentication.RedirectFromLoginPage()
                Response.Redirect(returnUrl);
            }
        }

        private bool SiteLevelCustomAuthenticationMethod(string userName, string password)
        {
            bool boolReturnValue = false;
            // Insert code that implements a site-specific custom 
            // authentication method here.
            // This example implementation always returns false.

            string strConnection = efundraising.Configuration.ApplicationSettings.GetConfig()["EFundraisingProd.SqlConnection.Release", "connectionString"];
            SqlConnection Connection = new SqlConnection(strConnection);
            String strSQL = "Select * From crm_users where user_name = '" + userName + "' and password = '" + password + "'";
            SqlCommand command = new SqlCommand(strSQL, Connection);
            SqlDataReader Dr;
            Connection.Open();
            Dr = command.ExecuteReader();
            if (Dr.HasRows)
            {
                return true;
            }
            else
            {
                return false;
            }

           Dr.Close();
  
  
        }



    }
}
