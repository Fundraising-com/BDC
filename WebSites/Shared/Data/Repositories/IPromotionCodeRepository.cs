using GA.BDC.Shared.Entities;

namespace GA.BDC.Shared.Data.Repositories
{
   public interface IPromotionCodeRepository : IRepository<PromotionCode>
   {
      PromotionCode GetByCode(string code);
   }
}
