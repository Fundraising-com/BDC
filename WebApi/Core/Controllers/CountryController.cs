using System.Web.Http;
using GA.BDC.Data.Fundraising.Helpers;
using GA.BDC.Shared.Data;
using GA.BDC.Shared.Data.Repositories;

namespace GA.BDC.WebApi.Fundraising.Core.Controllers
{
    public class CountryController : ApiController
    {
      [HttpOptions]
      public IHttpActionResult Options()
      {
         return Ok();
      }
      [HttpGet]
       public IHttpActionResult GetByCode(string code)
       {
          using (var efundraisingProdUnitOfWork = new UnitOfWork(Database.EFundraisingProd))
          {
             var countryRepository = efundraisingProdUnitOfWork.CreateRepository<ICountryRepository>();
             var entity = countryRepository.GetByCode(code);
             return Ok(entity);
          }
       }

      [HttpGet]
      public IHttpActionResult GetAll()
      {
         using (var efundraisingProdUnitOfWork = new UnitOfWork(Database.EFundraisingProd))
         {
            var countryRepository = efundraisingProdUnitOfWork.CreateRepository<ICountryRepository>();
            var entities = countryRepository.GetAll();
            return Ok(entities);
         }
      }
   }
}
