using GA.BDC.Shared.Entities;

namespace GA.BDC.Shared.Data.Repositories
{
   public interface ICountryRepository : IRepository<Country>
   {
      Country GetByCode(string code);
   }
}
