using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;

namespace GA.BDC.Web.Fundraising.MVC
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_EndRequest(object sender, EventArgs e)
        {
            if (Response.Cookies.Count > 0)
            {
                foreach (string s in Response.Cookies.AllKeys)
                {
                    if (s == FormsAuthentication.FormsCookieName || "asp.net_sessionid".Equals(s, StringComparison.InvariantCultureIgnoreCase))
                    {
                        if (Response.Cookies[s] != null)
                        {
                            Response.Cookies[s].Secure = true;
                        }
                    }
                }
            }
        }

        protected void Application_Start(object sender, EventArgs e)
        {

        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }


        void Application_PostRequestHandlerExecute(object sender, EventArgs e)
        {
            Dictionary<String, HttpCookie> cookieCollection = new Dictionary<string, HttpCookie>();
            for (int i = HttpContext.Current.Response.Cookies.Count - 1; i >= 0; i--)
            {
                HttpCookie c = HttpContext.Current.Response.Cookies.Get(i);
                if (!cookieCollection.ContainsKey(c.Name))
                {
                    cookieCollection.Add(c.Name, c);
                }
            }
            HttpContext.Current.Items.Add("CookieCollection", cookieCollection);
            Response.Cookies.Clear();
        }


        void Application_PreSendRequestHeaders(object sender, EventArgs e)
        {
            if (HttpContext.Current.Items.Contains("CookieCollection"))
            {
                Dictionary<String, HttpCookie> cookieCollection = (Dictionary<String, HttpCookie>)HttpContext.Current.Items["CookieCollection"];
                foreach (HttpCookie c in cookieCollection.Values)
                {
                    HttpContext.Current.Response.Cookies.Add(c);

                }
            }
        }


    }
}