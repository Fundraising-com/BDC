using System.Web.Http;
using GA.BDC.Data.Fundraising.Helpers;
using GA.BDC.Shared.Data;
using GA.BDC.Shared.Data.Repositories;

namespace GA.BDC.WebApi.Fundraising.Controllers
{
    public class ViewPortsController : ApiController
    {
      [HttpOptions]
      public IHttpActionResult Options()
      {
         return Ok();
      }

      public IHttpActionResult Get()
       {
          using (var efundStoreUnitOfWork = new UnitOfWork(Database.EFundStore))
          {
             var viewPortRepository = efundStoreUnitOfWork.CreateRepository<IViewPortRepository>();
             var viewPorts = viewPortRepository.GetAll();
             return Ok(viewPorts);
          }
       }
    }
}
