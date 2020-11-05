using System;
using System.Web.Http;
using GA.BDC.Data.Fundraising.Helpers;
using GA.BDC.Shared.Data;
using GA.BDC.Shared.Data.Repositories;
using GA.BDC.Shared.Entities;

namespace GA.BDC.WebApi.Fundraising.Controllers
{
   public class PromotionCodesController : ApiController
   {
      [HttpOptions]
      public IHttpActionResult Options()
      {
         return Ok();
      }
      [HttpGet]
      public IHttpActionResult Get(string code)
      {
         using (var efundStoreUnitOfWork = new UnitOfWork(Database.EFundraisingProd))
         {
            var promotionCodesRepository = efundStoreUnitOfWork.CreateRepository<IPromotionCodeRepository>();
            var promotionCode = promotionCodesRepository.GetByCode(code);
            if (promotionCode == null)
            {
               return NotFound();
            }
            return Ok(promotionCode);
         }
      }
      [Authorize]
      [HttpGet]
      public IHttpActionResult Get()
      {
         using (var efundStoreUnitOfWork = new UnitOfWork(Database.EFundraisingProd))
         {
            var promotionCodesRepository = efundStoreUnitOfWork.CreateRepository<IPromotionCodeRepository>();
            var result = promotionCodesRepository.GetAll();
            return Ok(result);
         }
      }
      [HttpGet]
      public IHttpActionResult Get(int id, bool isLimitValid)
      {
         using (var efundraisingProdUnitOfWork = new UnitOfWork(Database.EFundraisingProd))
         {
            var promotionCodesRepository = efundraisingProdUnitOfWork.CreateRepository<IPromotionCodeRepository>();
            var promotionCode = promotionCodesRepository.GetById(id);
            if (promotionCode == null)
            {
               return NotFound();
            }
            if (promotionCode.LimitType == PromotionCodeLimitType.Date && DateTime.Now > promotionCode.DateLimit)
            {
               return NotFound();
            }
            if (promotionCode.LimitType == PromotionCodeLimitType.Quantity)
            {
               var salesRepository = efundraisingProdUnitOfWork.CreateRepository<ISalesRepository>();
               var salesWithPromotionCode = salesRepository.GetByPromotionCodeUsed(promotionCode.Id);
               if (salesWithPromotionCode.Count >= promotionCode.QuantityLimit)
               {
                  return NotFound();
               }
            }
            return Ok(promotionCode);
         }
      }

      [Authorize]
      [HttpPut]
      public IHttpActionResult Update(PromotionCode model)
      {
         using (var efundStoreUnitOfWork = new UnitOfWork(Database.EFundraisingProd))
         {
            var promotionCodesRepository = efundStoreUnitOfWork.CreateRepository<IPromotionCodeRepository>();
            promotionCodesRepository.Update(model);
            efundStoreUnitOfWork.Commit();
            return Ok();
         }
      }

      [Authorize]
      [HttpDelete]
      public IHttpActionResult Delete(int id)
      {
         using (var efundStoreUnitOfWork = new UnitOfWork(Database.EFundraisingProd))
         {
            var promotionCodesRepository = efundStoreUnitOfWork.CreateRepository<IPromotionCodeRepository>();
            var model = promotionCodesRepository.GetById(id);
            promotionCodesRepository.Delete(model);
            efundStoreUnitOfWork.Commit();
            return Ok();
         }
      }

      [Authorize]
      [HttpPost]
      public IHttpActionResult Post(PromotionCode model)
      {
         int id;
         using (var efundStoreUnitOfWork = new UnitOfWork(Database.EFundraisingProd))
         {
            var promotionCodesRepository = efundStoreUnitOfWork.CreateRepository<IPromotionCodeRepository>();
            id = promotionCodesRepository.Save(model);
            efundStoreUnitOfWork.Commit();
         }
         return Get(id, true);
      }
   }
}
