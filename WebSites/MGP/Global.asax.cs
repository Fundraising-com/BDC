using System;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using Newtonsoft.Json;

namespace GA.BDC.Web.MGP
{
   public class MvcApplication : HttpApplication
   {

      protected void Application_Start()
      {
	      System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;
         AreaRegistration.RegisterAllAreas();
         WebApiConfig.Register(GlobalConfiguration.Configuration);

         FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
         RouteConfig.RegisterRoutes(RouteTable.Routes);
         BundleTable.EnableOptimizations = false;
         BundleConfig.RegisterBundles(BundleTable.Bundles);

         Database.SetInitializer<Data.MGP.EFRCommon.Models.DataProvider>(null);
         Database.SetInitializer<Data.MGP.esubs_global_v2.Models.DataProvider>(null);
         Database.SetInitializer<Data.MGP.EfundraisingProd.Models.DataProvider>(null);
      }

      protected void Application_BeginRequest(Object source, EventArgs e)
      {
         Initialize();

        }

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
   




    void Application_Error(object sender, EventArgs e)
      {
         var lastError = Server.GetLastError();
         var isAjaxRequest = (Request["X-Requested-With"] == "XMLHttpRequest") ||
                             (Request.Headers["X-Requested-With"] == "XMLHttpRequest");
         Response.Clear();
         if (lastError is HttpException && ((HttpException)lastError).GetHttpCode() == 404)
         {

            if (isAjaxRequest)
            {
               Response.Write(JsonConvert.SerializeObject(new { success = false }));
            }
            else
            {
               Response.Redirect(string.Format("http://{0}", Request.Url.Host), false);
            }
         }
         else
         {
            if (!IgnoreException(Request, lastError))
            {
               //SWCorporate.SystemEx.InstrumentationProvider.Current.SendExceptionNotification(lastError, null);

            }

            if (isAjaxRequest)
            {
               Response.Write(JsonConvert.SerializeObject(new
               {
                  success = false,
                  responseText = "An exception has ocurred in the server. We are working to fix it.",
                  ExceptionMessage = lastError.Message
               }));
            }
         }
         Server.ClearError();
      }

      /// <summary>
      /// Applies initialization logic on GET request
      /// </summary>
      /// <param name=""></param>
      /// <returns></returns>
      private void Initialize()
      {
         var request = HttpContext.Current.Request;
         var response = HttpContext.Current.Response;
         var authority = request.Url.Authority;
         var isTestEnvironment = request.Url.Host.Contains("test.") || request.Url.Host.Contains("localhost") || request.Url.Host.Contains("local.");
         if (!isTestEnvironment && authority.IndexOf("www.") != 0)
         {
            // Check for subdomains
            if (authority.Split(new Char[] { '.' }).Length == 2)
            {
               response.StatusCode = 301;
               response.Status = "301 Moved Permanently";
               response.AddHeader("Location", "http://www." + authority + request.Url.AbsolutePath + request.Url.Query);
               response.End();
            }
         }
         else if (!isTestEnvironment && authority.IndexOf("www.") == 0)
         {
            // Check for subdomains
            if (authority.Split(new Char[] { '.' }).Length == 4)
            {
               response.StatusCode = 301;
               response.Status = "301 Moved Permanently";
               response.AddHeader("Location", "http://" + authority.Replace("www.", "") + request.Url.AbsolutePath + request.Url.Query);
               response.End();
            }
         }
      }

      /// <summary>
      /// Ignores the exception if some of the rules apply
      /// </summary>
      /// <param name="request"></param>
      /// <param name="lastError"></param>
      /// <returns></returns>
      private static bool IgnoreException(HttpRequest request, Exception lastError)
      {
         if (request.UserHostAddress == null)
         {
            return false;
         }
         var botIpAddresses = ConfigurationManager.AppSettings["bots"].Replace(" ", "").Split(',');
         return (
            !string.IsNullOrEmpty(request.UserAgent) && request.UserAgent.Contains("bot"))
            || botIpAddresses.Any(botIpAddress => request.UserHostAddress.Contains(botIpAddress)
               || lastError.Message.Contains("Server cannot set status after HTTP headers have been sent")
               || lastError.Message.Contains("A potentially dangerous")
               || request.RawUrl.Contains("__browserLink"));
      }
   }
}