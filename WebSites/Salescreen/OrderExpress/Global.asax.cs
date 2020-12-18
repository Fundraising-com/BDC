using System;
using System.Collections;
using System.ComponentModel;
using System.Web;
using System.Web.SessionState;
using System.Diagnostics;
using System.Data.SqlClient;
using System.Data;
using QSP.WebControl;
using System.Configuration;
using QSPForm.SystemFramework;
using System.Runtime.Remoting;
using System.IO;
using System.Web.Security;
using System.Xml;

namespace QSP.OrderExpress.Web {
    /// <summary>
    /// Summary description for Global.
    /// </summary>
    public class Global : System.Web.HttpApplication {
        private const string PROMO_LOGO_TEMP_FOLDER = "Physical_PromoLogoTempFolder";

        public Global() {
            InitializeComponent();
        }

        protected void Application_Start(Object sender, EventArgs e) {
            //We use the foloowing line to be able to set the connection string and
            //to consider all other config from the Web.config file
            ApplicationConfiguration.OnApplicationStart(Context.Server.MapPath(Context.Request.ApplicationPath));
            string configPath = Path.Combine(Context.Server.MapPath(Context.Request.ApplicationPath), "remotingclient.cfg");
            if (File.Exists(configPath))
                RemotingConfiguration.Configure(configPath);
            //We do it at Start cause the Server.MapPath is not avaliable at session_end event
            Application[PROMO_LOGO_TEMP_FOLDER] = Context.Server.MapPath(QSPForm.Common.QSPFormConfiguration.PromoLogoTempFolder);


        }

        protected void Session_Start(Object sender, EventArgs e) {
        }

        protected void Application_BeginRequest(Object sender, EventArgs e) {

        }

        protected void Application_EndRequest(Object sender, EventArgs e) {

        }

        protected void Application_AuthenticateRequest(Object sender, EventArgs e) {
            //francis stuff
            string spath = Context.Server.MapPath(QSPForm.Common.QSPFormConfiguration.PromoLogoTempFolder);


            // Create a new User object because we need to assign the roles.
            if (Request.IsAuthenticated) {
                // Retrieve user's identity from context user
                FormsIdentity ident = (FormsIdentity)HttpContext.Current.User.Identity;

                // Retrieve roles from the authentication ticket userdata field separated by comma
                string[] roles = ident.Ticket.UserData.Split(',');

                // If we didn't load the roles before, go to the DB
                if (roles[0].Length == 0) {
                    // Fetch roles from the database somehow.
                    //Employee emp = Employee.GetEmployee(User.Identity.Name);
                    //roles = emp.Roles.Split(",".ToCharArray());
                    QSPForm.Business.AuthSystem autSys = new QSPForm.Business.AuthSystem();
                    ArrayList roleName = new ArrayList();
                    foreach (DataRow r in autSys.SelectAllRoleByLoginName(User.Identity.Name)) {
                        roleName.Add(r[QSPForm.Common.DataDef.RoleTable.FLD_PKID].ToString());
                    }


                    //roles = (string[])roleName.ToArray(System.Type.GetType("string"));
                    roleName.CopyTo(roles);


                    // Store roles inside the Forms ticket because we can add the roles in
                    // the UserData property to avoid checking everytime we run into this method.
                    FormsAuthenticationTicket newTicket = new FormsAuthenticationTicket(
                        ident.Ticket.Version,
                        ident.Ticket.Name,
                        ident.Ticket.IssueDate,
                        ident.Ticket.Expiration,
                        ident.Ticket.IsPersistent,
                        String.Join(",", roles),
                        ident.Ticket.CookiePath);

                    // Create the cookie.    
                    HttpCookie authCookie = new HttpCookie(
                        FormsAuthentication.FormsCookieName,
                        FormsAuthentication.Encrypt(newTicket));
                    authCookie.Path = FormsAuthentication.FormsCookiePath + "; HttpOnly; noScriptAccess";
                    authCookie.Secure = FormsAuthentication.RequireSSL;

                    if (newTicket.IsPersistent)
                        authCookie.Expires = newTicket.Expiration;

                    Context.Response.Cookies.Add(authCookie);
                }

                // Create principal and attach to user
                Context.User = new System.Security.Principal.GenericPrincipal(ident, roles);
            }
        }

        protected void Application_Error(Object sender, EventArgs e) {
            /*Suppress email error reporting incase the exception is triggered from a known set of IP address ranges 
              of Time Security Scan system*/
            try
            {
                string clientIPAddress = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                if (clientIPAddress.Equals(string.Empty) || clientIPAddress.ToLower().Equals("unknown"))
                    clientIPAddress = Request.ServerVariables["REMOTE_ADDR"];

                if (IsTimeSecurityScanIP(clientIPAddress))
                {
                    QSPForm.SystemFramework.ApplicationError.LogErrorAndSuppressMail(System.Web.HttpContext.Current.Server.GetLastError().GetBaseException());
                }
                else
                {
                    QSPForm.SystemFramework.ApplicationError.ManageError(System.Web.HttpContext.Current.Server.GetLastError().GetBaseException());
                }
            }
            catch
            {
            }
        }

        private bool IsTimeSecurityScanIP(string clientIPAddress)
        {
          bool isClientIPWithinRange = false;
          string xmlFilePath = Server.MapPath(Request.ApplicationPath + "XML/SecurityScanIPRanges.xml");
          DataSet ipRangesDataSet = new DataSet();
          ipRangesDataSet.ReadXml(xmlFilePath);
          DataTable ipRangeDataTable = ipRangesDataSet.Tables["IP_RANGE"];

          string[] clientIPOctets = clientIPAddress.Split(new char[] { '.' });

          foreach (DataRow ipRange in ipRangeDataTable.Rows)
          {
            string lowerRange = ipRange["LOWER_LIMIT"].ToString();
            string upperRange = ipRange["UPPER_LIMIT"].ToString();

            string[] lowerRangeOctets = lowerRange.Split(new char[]{'.'});
            string[] upperRangeOctets = upperRange.Split(new char[] { '.' });

            if (int.Parse(clientIPOctets[0]) >= int.Parse(lowerRangeOctets[0]) && int.Parse(clientIPOctets[1]) >= int.Parse(lowerRangeOctets[1]) &&
                int.Parse(clientIPOctets[2]) >= int.Parse(lowerRangeOctets[2]) && int.Parse(clientIPOctets[3]) >= int.Parse(lowerRangeOctets[3]) &&
                int.Parse(clientIPOctets[0]) <= int.Parse(upperRangeOctets[0]) && int.Parse(clientIPOctets[1]) <= int.Parse(upperRangeOctets[1]) &&
                int.Parse(clientIPOctets[2]) <= int.Parse(upperRangeOctets[2]) && int.Parse(clientIPOctets[3]) <= int.Parse(upperRangeOctets[3]))
            {
              isClientIPWithinRange = true;
              break;
            }
          }

          return isClientIPWithinRange;
        }

        protected void Session_End(Object sender, EventArgs e) {
            //try {
            //    if (Application[PROMO_LOGO_TEMP_FOLDER] != null) {
            //        string spath = Application[PROMO_LOGO_TEMP_FOLDER].ToString();
            //        DeletePromoLogoImageTemp(Session.SessionID, spath);
            //    }
            //}
            //catch (Exception ex) {
            //    string message = ex.Message;
            //}
        }

        protected void Application_End(Object sender, EventArgs e) {

        }

        #region Web Form Designer generated code
        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {

        }
        #endregion

        //private void DeletePromoLogoImageTemp(string sID, string path) {
        //    DirectoryInfo directory = new DirectoryInfo(path);
        //    foreach (FileInfo f in directory.GetFiles(sID + "-*.*")) {
        //        f.Delete();
        //    }
        //}
    }
}