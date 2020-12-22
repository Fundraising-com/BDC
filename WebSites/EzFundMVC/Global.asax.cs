using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Routing;
using Sentry;
using Sentry.EntityFramework;

namespace GA.BDC.Web.EzFundMVC
{
    public class WebApiApplication : System.Web.HttpApplication
    {

        private IDisposable _sentry;
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
        }



        protected void Application_BeginRequest(object sender, EventArgs e)
        {

            SentryDatabaseLogging.UseBreadcrumbs();
            _sentry = SentrySdk.Init(o =>
            {
                // We store the DSN inside Web.config; make sure to use your own DSN!
                o.Dsn = new Dsn(ConfigurationManager.AppSettings["SentryDSN"]);

                // Get Entity Framework integration
                o.AddEntityFramework();
                o.SendDefaultPii = true;

            });

            //int total = 3;
            //int numberOf = 0;

            //var tot = total / numberOf; // DivideByZeroException thrown 


        }

        protected void Application_Error(object sender, EventArgs e)
        {
            var exception = Server.GetLastError();

            // Capture unhandled exceptions
            SentrySdk.CaptureException(exception);
        }
        protected void Application_End(object sender, EventArgs e)
        {
            // Close the Sentry SDK (flushes queued events to Sentry)
            _sentry?.Dispose();
        }

        public override void Dispose()
        {
            _sentry.Dispose();
            base.Dispose();
        }



    }
}