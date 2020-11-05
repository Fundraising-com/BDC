using System.Collections.Generic;
using System.Web.Http;
using GA.BDC.Data.Fundraising.Helpers;
using GA.BDC.Shared.Data;
using GA.BDC.Shared.Data.Repositories;
using GA.BDC.Shared.Entities;

namespace GA.BDC.WebApi.Fundraising.Controllers
{
    public class HomePageRotatorController : ApiController
    {
      [HttpOptions]
      public IHttpActionResult Options()
      {
         return Ok();
      }

      [HttpGet]
        public IList<HomePageRotator> GetAll()
        {
            using (var efundStoreUnitOfWork = new UnitOfWork(Database.EFundStore))
            {
                var homepagerotatorRepository = efundStoreUnitOfWork.CreateRepository<IHomePageRotatorRepository>();
                return  homepagerotatorRepository.GetAll();
            }
        }
    }
}
