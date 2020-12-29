using GA.BDC.Shared.Entities;
using System.Collections.Generic;

namespace GA.BDC.Shared.Data.Repositories
{
    public interface IHomePageRotatorRepository : IRepository<HomePageRotator>
    {
       IList<HomePageRotator> GetAll(bool active);
    }
}
