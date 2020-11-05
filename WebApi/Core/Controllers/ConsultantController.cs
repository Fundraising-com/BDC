using System.Web.Http;
using GA.BDC.Data.Fundraising.Helpers;
using GA.BDC.Shared.Data;
using GA.BDC.Shared.Data.Repositories;

namespace GA.BDC.WebApi.Fundraising.Core.Controllers
{
    public class ConsultantController : ApiController
    {
       [HttpOptions]
       public IHttpActionResult Options()
       {
          return Ok();
       }

      [HttpGet]
      public IHttpActionResult GetAll()
      {
         using (var efundraisingProdUnitOfWork = new UnitOfWork(Database.EFundraisingProd))
         {
            var consultantRepository = efundraisingProdUnitOfWork.CreateRepository<IConsultantRepository>();
            var entities = consultantRepository.GetAll();
            return Ok(entities);
         }
      }

      [HttpGet]
      public IHttpActionResult Get(int id)
      {
         using (var efundraisingProdUnitOfWork = new UnitOfWork(Database.EFundraisingProd))
         {
            var consultantRepository = efundraisingProdUnitOfWork.CreateRepository<IConsultantRepository>();
            var entity = consultantRepository.GetById(id);
            return Ok(entity);
         }
      }

      [HttpGet]
      public IHttpActionResult GetAll(string name)
      {
         using (var efundraisingProdUnitOfWork = new UnitOfWork(Database.EFundraisingProd))
         {
            var consultantRepository = efundraisingProdUnitOfWork.CreateRepository<IConsultantRepository>();
            var entities = consultantRepository.GetAll(name);
            return Ok(entities);
         }
      }
   }
}
