using GA.BDC.Shared.Entities;

// ReSharper disable once CheckNamespace
namespace GA.BDC.Shared.Data.Repositories
{
   public interface IPromotionRepository : IRepository<Promotion>
    {
        /// <summary>
        /// Returns the promotion id
        /// </summary>
        /// <param name="partnerId"></param>
        /// <param name="abid"></param>
        /// <returns></returns>
        Promotion GetPromotionId(int partnerId, string abid);
    }
}
