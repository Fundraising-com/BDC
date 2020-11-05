using System.Web.Http;
using GA.BDC.Data.Fundraising.Helpers;
using GA.BDC.Shared.Data;
using GA.BDC.Shared.Data.Repositories;

namespace GA.BDC.WebApi.Fundraising.Core.Controllers
{
   public class RepresentativesController : ApiController
   {
      [HttpOptions]
      public IHttpActionResult Options()
      {
         return Ok();
      }

      [HttpGet]
      public IHttpActionResult GetByRedirect(string redirect)
      {
         using (var efrCommonUnitOfWork = new UnitOfWork(Database.FastFundraising))
         {
            var representativeRepository = efrCommonUnitOfWork.CreateRepository<IRepresentativeRepository>();
            var representative = representativeRepository.GetByRedirect(redirect);
            return Ok(representative);
         }
      }

      [HttpGet]
      public IHttpActionResult Get(int id)
      {
         using (var efrCommonUnitOfWork = new UnitOfWork(Database.FastFundraising))
         {
            var representativeRepository = efrCommonUnitOfWork.CreateRepository<IRepresentativeRepository>();
            var representative = representativeRepository.GetById(id);
            return Ok(representative);
         }
      }
      [HttpGet]
      public IHttpActionResult Get()
      {
         using (var efrCommonUnitOfWork = new UnitOfWork(Database.FastFundraising))
         {
            var representativeRepository = efrCommonUnitOfWork.CreateRepository<IRepresentativeRepository>();
            var representatives = representativeRepository.GetAll();
            return Ok(representatives);
         }
      }

      [HttpGet]
      public IHttpActionResult GetByLead(int leadId)
      {
         using (var efrCommonUnitOfWork = new UnitOfWork(Database.FastFundraising))
         {
            var representativeRepository = efrCommonUnitOfWork.CreateRepository<IRepresentativeRepository>();
            var representative = representativeRepository.GetByLead(leadId);
            return representative != null ? (IHttpActionResult)Ok(representative) : BadRequest();
         }
      }
   }
}
