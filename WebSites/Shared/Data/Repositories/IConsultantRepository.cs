using System.Collections.Generic;
using GA.BDC.Shared.Entities;

namespace GA.BDC.Shared.Data.Repositories
{
   public interface IConsultantRepository : IRepository<Consultant>
   {
      IList<Consultant> GetAll(string name);
   }
}
