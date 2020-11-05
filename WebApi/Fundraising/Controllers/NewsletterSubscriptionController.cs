using System.Web.Http;
using GA.BDC.Data.Fundraising.Helpers;
using GA.BDC.Shared.Data;
using GA.BDC.Shared.Data.Repositories;
using GA.BDC.Shared.Entities;

namespace GA.BDC.WebApi.Fundraising.Controllers
{
   public class NewsletterSubscriptionController : ApiController
   {
      [HttpOptions]
      public IHttpActionResult Options()
      {
         return Ok();
      }

      /// <summary>
      /// Creates a newsletter subcription on the DB with the object received
      /// </summary>
      /// <param name="model"></param>
      /// <returns></returns>
      [HttpPost]
      public IHttpActionResult Post(NewsletterSubscription model)
      {
         using (var efundStoreUnitOfWork = new UnitOfWork(Database.EFundStore))
         {
            var newsLetterSupscriptionRepository = efundStoreUnitOfWork.CreateRepository<INewsletterSubscriptionRepository>();
            model.Id = newsLetterSupscriptionRepository.Save(model);
            efundStoreUnitOfWork.Commit();
            return Ok(model);
         }
      }

   }
}
