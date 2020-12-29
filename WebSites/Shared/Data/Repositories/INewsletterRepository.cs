using System.Collections.Generic;
using GA.BDC.Shared.Entities;

namespace GA.BDC.Shared.Data.Repositories
{
   public interface INewsletterRepository : IRepository<Newsletter>
   {
      IList<Newsletter> GetByPartner(int partnerId);
      Newsletter GetByUrl(string url);
   }
}
