using System;
using System.Collections;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Web;
using System.Web.Services.Protocols;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using QSP.OrderExpress.Reporting.ReportingService;


namespace QSP.OrderExpress.Reporting
{
    /// <summary>
    /// 
    /// </summary>
    public class RSClient : QSP.OrderExpress.Reporting.ReportingService.ReportingService
    {
        QSP.OrderExpress.Reporting.Properties.Settings settings = new QSP.OrderExpress.Reporting.Properties.Settings();

        /// <summary>
        /// 
        /// </summary>
        public bool m_needLogon = false;
        private string m_authCookieName;
        private Cookie m_authCookie;

        /// <summary>
        /// 
        /// </summary>
        public RSClient()
        {   
            // Set the server URL
            base.Url = settings.RSServer;

            // Set default credentials to integrated.
            Credentials = System.Net.CredentialCache.DefaultCredentials;
        }

        /// <summary>
        /// Gets the type of the item on the report server. Use the 
        /// new modifier to hide the base implementation.
        /// </summary>
        public new ItemTypeEnum GetItemType(string item)
        {
            ItemTypeEnum type = ItemTypeEnum.Unknown;
            try
            {
                type = base.GetItemType(item);
            }

            catch (SoapException)
            {
                return ItemTypeEnum.Unknown;
            }

            return type;
        }

        /// <summary>
        /// Get whether the given credentials can connect to the report server.
        /// Returns false if not authorized. Other errors throw an exception.
        /// </summary>
        public bool CheckAuthorized()
        {
            try
            {
                GetItemType("/");
            }
            catch (WebException e)
            {
                if (!(e.Response is HttpWebResponse) ||
                    ((HttpWebResponse)e.Response).StatusCode != HttpStatusCode.Unauthorized)
                {
                    throw;
                }
                return false;
            }
            catch (InvalidOperationException)
            {
                // This condition could be caused by a redirect to a forms logon page
                Console.WriteLine("InvalidOperationException");
                if (m_needLogon)
                {
                    NetworkCredential creds = Credentials as NetworkCredential;
                    if (creds != null && creds.UserName != null)
                    {
                        try
                        {
                            base.CookieContainer = new CookieContainer();
                            base.LogonUser(creds.UserName, creds.Password, null);
                            return true;
                        }
                        catch (Exception)
                        {
                            return false;
                        }
                    }
                }
                else
                {
                    throw;
                }
            }
            return true;
        }
        
        /// <summary>
        /// Should get this from the web.config.
        /// </summary>
        /// <returns></returns>
        public NetworkCredential GetCredentials()
        {
            return new NetworkCredential(settings.RSUID, settings.RSPwd);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="uri"></param>
        /// <returns></returns>
        protected override WebRequest GetWebRequest(Uri uri)
        {
            HttpWebRequest request;
            request = (HttpWebRequest)HttpWebRequest.Create(uri);
            request.Credentials = base.Credentials;
            request.CookieContainer = new CookieContainer();
            if (m_authCookie != null)
            {
                request.CookieContainer.Add(m_authCookie);
            }
            return request;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        protected override WebResponse GetWebResponse(WebRequest request)
        {
            WebResponse response = base.GetWebResponse(request);
            string cookieName = response.Headers["RSAuthenticationHeader"];
            if (cookieName != null)
            {
                m_authCookieName = cookieName;
                HttpWebResponse webResponse = (HttpWebResponse)response;
                Cookie authCookie = webResponse.Cookies[cookieName];
                // save it away 
                m_authCookie = authCookie;
            }
            // need to call logon
            if (response.Headers["RSNotAuthenticated"] != null)
            {
                m_needLogon = true;
            }
            return response;
        }
    }
}
