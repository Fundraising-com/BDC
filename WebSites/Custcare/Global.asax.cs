using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Newtonsoft.Json;
using log4net;

namespace GA.BDC.Web.Custcare
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        public ILog Log { get; set; }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            Database.SetInitializer<Data.MGP.EFRCommon.Models.DataProvider>(null);
            Database.SetInitializer<Data.MGP.esubs_global_v2.Models.DataProvider>(null);
            Database.SetInitializer<Data.MGP.EfundraisingProd.Models.DataProvider>(null);
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            if (Log == null)
                Log = LogManager.GetLogger(typeof(MvcApplication));
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
                Log.Fatal(string.Format("Url: {0}. Remote IP: {1}. User Agent: {2}.", Request.RawUrl, Request.UserHostAddress, Request.UserAgent), lastError);

                if (isAjaxRequest)
                {
                    Response.Write(JsonConvert.SerializeObject(new
                    {
                        success = false,
                        responseText = "An exception has ocurred in the server. We are working to fix it.",
                        ExceptionMessage = lastError.Message
                    }));
                }
                else
                {
                    Response.Redirect(string.Format("http://{0}{1}", Request.Url.Host, "/Error/500"), false);
                }
            }
            Server.ClearError();
        }
    }
}