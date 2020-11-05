using System;
using System.Web;
using System.Web.Http;
using GA.BDC.Data.Fundraising.Helpers;
using GA.BDC.Shared.Data;
using GA.BDC.Shared.Data.Repositories;
using SWCorporate.SystemEx;
using SWCorporate.SystemEx.Web;

namespace GA.BDC.WebApi.Fundraising.Controllers
{
   public class RoutesMappersController : ApiController
   {
      [HttpOptions]
      public IHttpActionResult Options()
      {
         return Ok();
      }

      [HttpGet]
      public string Get(string url)
      {
         using (var efundStoreUnitOfWork = new UnitOfWork(Database.EFundStore))
         {
            var routeMapperRepository = efundStoreUnitOfWork.CreateRepository<IRouteMapperRepository>();
            try
            {
               var redirect = routeMapperRepository.GetRedirect(url);
               return redirect;
            }
            catch (Exception exception)
            {
               new HttpContextWrapper(HttpContext.Current).SendExceptionNotification(InstrumentationProvider.Current, exception);
               return string.Empty;
            }
         }
      }
   }
}
