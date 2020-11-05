using System.Linq;
using System.Web.Http;
using GA.BDC.Data.Fundraising.Helpers;
using GA.BDC.Shared.Data;
using GA.BDC.Shared.Data.Repositories;

namespace GA.BDC.WebApi.Fundraising.Core.Controllers
{
   public class FundraiserCategoriesController : ApiController
   {
      [HttpOptions]
      public IHttpActionResult Options()
      {
         return Ok();
      }

      [HttpGet]
      public IHttpActionResult GetAll()
      {
         using (var fastFundraisingUnitOfWork = new UnitOfWork(Database.FastFundraising))
         {
            var fundraiserCategoryRepository =
               fastFundraisingUnitOfWork.CreateRepository<IFundraiserCategoryRepository>();
            var result = fundraiserCategoryRepository.GetAll();
            return Ok(result.OrderBy(p => p.Order).ToList());
         }
      }
   }
}
