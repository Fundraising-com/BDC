using System.Web.Http;
using GA.BDC.Data.Fundraising.Helpers;
using GA.BDC.Shared.Data;
using GA.BDC.Shared.Data.Repositories;
using GA.BDC.Shared.Entities;

namespace GA.BDC.WebApi.Fundraising.Controllers
{
    public class SessionsController : ApiController
    {
      [HttpOptions]
      public IHttpActionResult Options()
      {
         return Ok();
      }

      [HttpPost]
       public IHttpActionResult Post(Session session)
       {
          using (var eFundStoreUnitOfWork = new UnitOfWork(Database.EFundStore))
          {
             var sessionRepository = eFundStoreUnitOfWork.CreateRepository<ISessionRepository>();
             sessionRepository.Save(session);
             eFundStoreUnitOfWork.Commit();
             return Ok(session);
          }
       }

       [HttpPut]
       public IHttpActionResult Put(Session session)
       {
          using (var eFundStoreUnitOfWork = new UnitOfWork(Database.EFundStore))
          {
             var sessionRepository = eFundStoreUnitOfWork.CreateRepository<ISessionRepository>();
             sessionRepository.Update(session);
             eFundStoreUnitOfWork.Commit();
             return Ok(session);
          }
       }

       [HttpGet]
       public IHttpActionResult GetByAnonymousId(string anonymousId)
       {
          using (var eFundStoreUnitOfWork = new UnitOfWork(Database.EFundStore))
          {
             var sessionRepository = eFundStoreUnitOfWork.CreateRepository<ISessionRepository>();
             var session = sessionRepository.GetByAnonymousId(anonymousId);
             return Ok(session);
          }
       }
    }
}
