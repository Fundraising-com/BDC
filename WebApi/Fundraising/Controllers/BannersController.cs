using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using GA.BDC.Data.Fundraising.Helpers;
using GA.BDC.Shared.Data;
using GA.BDC.Shared.Data.Repositories;
using GA.BDC.Shared.Entities;

namespace GA.BDC.WebApi.Fundraising.Controllers
{
    public class BannersController : ApiController
    {
      [HttpOptions]
      public IHttpActionResult Options()
      {
         return Ok();
      }
      [HttpGet]
        public IList<Banner> Get(int type, int partnerId, bool sort, int viewPortId = 0)
        {
           using (var efundStoreUnitOfWork = new UnitOfWork(Database.EFundStore))
           {
              var bannersRepository = efundStoreUnitOfWork.CreateRepository<IBannersRepository>();
              var banners = bannersRepository.GetByPartner(type, partnerId);
              if (sort)
              {
                 banners = banners.OrderBy(x => x.SortOrder).ToList();
              }
              else
              {
                 var random = new Random(DateTime.Now.Millisecond);
                 banners = banners.OrderBy(p => random.Next(0, 1000)).ToList();
              }
              if (viewPortId > 0)
              {
                 banners = banners.Where(p => p.ViewPorts.Any(a => a.Id == viewPortId)).ToList();
              }
              return banners;
           }
        }        
        
    }
}
