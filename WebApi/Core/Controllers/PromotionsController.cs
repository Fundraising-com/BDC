using System.Web.Http;
using GA.BDC.Data.Fundraising.Helpers;
using GA.BDC.Shared.Data;
using GA.BDC.Shared.Data.Repositories;
using GA.BDC.Shared.Entities;

namespace GA.BDC.WebApi.Fundraising.Core.Controllers
{
    public class PromotionsController : ApiController
    {
      [HttpOptions]
      public IHttpActionResult Options()
      {
         return Ok();
      }

      [HttpGet]
       public IHttpActionResult GetByPartner(int partnerId)
       {
          using (var efrCommonUnitOfWork = new UnitOfWork(Database.EFRCommon))
          {
             var promotionsRepository = efrCommonUnitOfWork.CreateRepository<IPromotionRepository>();
             var promotion = promotionsRepository.GetPromotionId(partnerId, string.Empty);
             return Ok(promotion);
          }
       }

       [HttpGet]
       public IHttpActionResult GetByPartnerAndAbid(int partnerId, string abid)
       {
          using (var efrCommonUnitOfWork = new UnitOfWork(Database.EFRCommon))
          {
             var promotionsRepository = efrCommonUnitOfWork.CreateRepository<IPromotionRepository>();
             var promotion = promotionsRepository.GetPromotionId(partnerId, abid);
             return Ok(promotion);
          }
       }

       [HttpPost]
       public IHttpActionResult Post(Promotion promotion)
       {
         using (var efrCommonUnitOfWork = new UnitOfWork(Database.EFRCommon))
         {
            var promotionsRepository = efrCommonUnitOfWork.CreateRepository<IPromotionRepository>();
            promotion.Id = promotionsRepository.Save(promotion);
            efrCommonUnitOfWork.Commit();
            return Ok(promotion);
         }
      }
    }
}
