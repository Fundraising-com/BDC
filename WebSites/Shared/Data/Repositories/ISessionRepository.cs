using GA.BDC.Shared.Entities;

namespace GA.BDC.Shared.Data.Repositories
{
   public interface ISessionRepository : IRepository<Session>
    {
        /// <summary>
        /// Returns the Session found related to the Guid given
        /// </summary>
        /// <param name="anonymousId">Anonymous Id</param>
        /// <returns></returns>
        Session GetByAnonymousId(string anonymousId);
    }
}
