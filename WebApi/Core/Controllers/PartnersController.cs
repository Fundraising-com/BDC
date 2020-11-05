using System.Web.Http;
using GA.BDC.Data.Fundraising.Helpers;
using GA.BDC.Shared.Data;
using GA.BDC.Shared.Data.Repositories;

namespace GA.BDC.WebApi.Fundraising.Core.Controllers
{
   public class PartnersController : ApiController
   {
      [HttpOptions]
      public IHttpActionResult Options()
      {
         return Ok();
      }

      [HttpGet]
      public IHttpActionResult Get(int id)
      {
         using (var efrCommonUnitOfWork = new UnitOfWork(Database.EFRCommon))
         {
            var partnerRepository = efrCommonUnitOfWork.CreateRepository<IPartnerRepository>();
            var partner = partnerRepository.GetById(id);
            return Ok(partner);
         }
      }
      [HttpGet]
      public IHttpActionResult Get(string aaid)
      {
         using (var efrCommonUnitOfWork = new UnitOfWork(Database.EFRCommon))
         {
            var partnerRepository = efrCommonUnitOfWork.CreateRepository<IPartnerRepository>();
            var partner = partnerRepository.GetByAAId(aaid) ?? partnerRepository.GetById(686);
            return Ok(partner);
         }
      }
      [HttpGet]
      public IHttpActionResult GetAll()
      {
         using (var efrCommonUnitOfWork = new UnitOfWork(Database.EFRCommon))
         {
            var partnerRepository = efrCommonUnitOfWork.CreateRepository<IPartnerRepository>();
            var result = partnerRepository.GetAll();
            return Ok(result);
         }
      }
   }
}
