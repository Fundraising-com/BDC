using System.Collections.Generic;
using GA.BDC.Shared.Entities;

namespace GA.BDC.Shared.Data.Repositories
{
   public interface IBannersRepository : IRepository<Banner>
   {
      IList<Banner> GetByPartner(int type, int partnerId);
   }
}
