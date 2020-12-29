using System.Collections.Generic;
using GA.BDC.Shared.Entities;

namespace GA.BDC.Shared.Data.Repositories
{
   public interface IRegionRepository : IRepository<Region>
   {
      Region GetByCode(string code);
      IList<Region> GetByCountryCode(string code);
   }
}
