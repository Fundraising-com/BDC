using System.Web.Http;
using GA.BDC.Data.Fundraising.Helpers;
using GA.BDC.Shared.Data;
using GA.BDC.Shared.Data.Repositories;

namespace GA.BDC.WebApi.Fundraising.Core.Controllers
{
   public class RegionController : ApiController
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
            var regionRepository = efundraisingProdUnitOfWork.CreateRepository<IRegionRepository>();
            var entity = regionRepository.GetByCode(code);
            return Ok(entity);
         }
      }

      [HttpGet]
      public IHttpActionResult GetAll()
      {
         using (var efundraisingProdUnitOfWork = new UnitOfWork(Database.EFundraisingProd))
         {
            var regionRepository = efundraisingProdUnitOfWork.CreateRepository<IRegionRepository>();
            var entities = regionRepository.GetAll();
            return Ok(entities);
         }
      }

      [HttpGet]
      public IHttpActionResult GetByCountryCode(string countryCode)
      {
         using (var efundraisingProdUnitOfWork = new UnitOfWork(Database.EFundraisingProd))
         {
            var regionRepository = efundraisingProdUnitOfWork.CreateRepository<IRegionRepository>();
            var entities = regionRepository.GetByCountryCode(countryCode);
            return Ok(entities);
         }
      }
   }
}