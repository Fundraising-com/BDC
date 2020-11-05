using System;
using System.Web;
using System.Web.Http;
using System.Configuration;
using GA.BDC.Data.EzFund.Helpers;
using GA.BDC.Shared.Data;
using GA.BDC.Shared.Data.Repositories;
using Sentry;
using Sentry.EntityFramework;

namespace GA.BDC.WebApi.EzFund.Controllers
{
    public class RoutesMappersController : ApiController
    {
        private IDisposable _sentry;
        [HttpOptions]
		public IHttpActionResult Options()
		{
			return Ok();
		}

		[HttpGet]
		public string Get(string url)
		{


            //sentry error handling
            SentryDatabaseLogging.UseBreadcrumbs();
            _sentry = SentrySdk.Init(o =>
            {
                // We store the DSN inside Web.config; make sure to use your own DSN!
                o.Dsn = new Dsn(ConfigurationManager.AppSettings["SentryDSN"]);

                // Get Entity Framework integration
                o.AddEntityFramework();
                o.SendDefaultPii = true;

            });

            using (var EzmainUnitOfWork = new UnitOfWork(Database.EZMain))
			{
				var routeMapperRepository = EzmainUnitOfWork.CreateRepository<IRouteMapperRepository>();
				try
				{
					var redirect = routeMapperRepository.GetRedirect(url);
					return redirect;
				}
				catch (Exception exception)
				{
                    SentrySdk.CaptureException(exception);
                    //new HttpContextWrapper(HttpContext.Current).SendExceptionNotification(InstrumentationProvider.Current, exception);
                    return string.Empty;
				}
			}
		}
	}
}
