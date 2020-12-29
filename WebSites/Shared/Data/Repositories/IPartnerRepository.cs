using GA.BDC.Shared.Entities;

// ReSharper disable once CheckNamespace
namespace GA.BDC.Shared.Data.Repositories
{
   public interface IPartnerRepository : IRepository<Partner>
    {
        /// <summary>
        /// Returns the partner with the a_aid given
        /// </summary>
        /// <param name="aaid">A Aid</param>
        /// <returns>Partner</returns>
        Partner GetByAAId(string aaid);
    }
}
