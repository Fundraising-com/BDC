using System.Web.Http;
using GA.BDC.Data.Fundraising.Helpers;
using GA.BDC.Shared.Data;
using GA.BDC.Shared.Data.Repositories;
using System.Linq;

namespace GA.BDC.WebApi.Fundraising.Core.Controllers
{
   public class FundraiserProductsController : ApiController
   {
      [HttpOptions]
      public IHttpActionResult Options()
      {
         return Ok();
      }

      [HttpGet]
      public IHttpActionResult Get(int id)
      {
         using (var fastFundraisingUnitOfWork = new UnitOfWork(Database.FastFundraising))
         {
            var fundraiserProductRepository =
               fastFundraisingUnitOfWork.CreateRepository<IFundraiserProductRepository>();
            return Ok(fundraiserProductRepository.GetById(id));
         }

      }
      [HttpGet]
      public IHttpActionResult GetAllByCategory(int categoryId)
      {
         using (var fastFundraisingUnitOfWork = new UnitOfWork(Database.FastFundraising))
         {
            var fundraiserProductRepository =
               fastFundraisingUnitOfWork.CreateRepository<IFundraiserProductRepository>();
            return Ok(fundraiserProductRepository.GetAllByCategory(categoryId));
         }
      }

        [HttpGet]
        public IHttpActionResult GetAll()
        {
            using (var fastFundraisingUnitOfWork = new UnitOfWork(Database.FastFundraising))
            {
                var fundraiserProductRepository =
                   fastFundraisingUnitOfWork.CreateRepository<IFundraiserProductRepository>();
                var result = fundraiserProductRepository.GetAll();
                return Ok(result.OrderBy(p => p.Id).ToList());
            }

            



        }



    }
}
