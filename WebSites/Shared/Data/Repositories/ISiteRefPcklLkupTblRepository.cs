using System.Collections.Generic;
using GA.BDC.Shared.Entities;

namespace GA.BDC.Shared.Data.Repositories
{
    public interface ISiteRefPcklLkupTblRepository : IRepository<SiteRefPcklLkupTbl>
    {
        IList<SiteRefPcklLkupTbl> GetAllCategories();
    }
}
