using GA.BDC.Shared.Entities;

namespace GA.BDC.Shared.Data.Repositories
{
   public interface IOAuthClientRepository : IRepository<OAuthClient>
   {
      OAuthClient GetById(string id);
   }
}
