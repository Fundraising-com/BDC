using GA.BDC.Shared.Entities;

namespace GA.BDC.Shared.Data.Repositories
{
   public interface IOAuthTokenRepository : IRepository<OAuthToken>
   {
      OAuthToken GetById(string id);
      new bool Save(OAuthToken model);
   }
}
