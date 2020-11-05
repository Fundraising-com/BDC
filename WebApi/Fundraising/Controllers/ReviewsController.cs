using System.Linq;
using System.Web.Http;
using GA.BDC.Data.Fundraising.Helpers;
using GA.BDC.Shared.Data;
using GA.BDC.Shared.Data.Repositories;
using GA.BDC.Shared.Entities;

namespace GA.BDC.WebApi.Fundraising.Controllers
{
   public class ReviewsController : ApiController
   {
      [HttpOptions]
      public IHttpActionResult Options()
      {
         return Ok();
      }

      [HttpGet]
      public IHttpActionResult GetAllByProductId(int productId, bool showAll = false)
      {
         using (var efundStoreUnitOfWork = new UnitOfWork(Database.EFundStore))
         {
            var reviewsRepository = efundStoreUnitOfWork.CreateRepository<IReviewRepository>();
            var reviews = reviewsRepository.GetAllByProductId(productId).Where(p => showAll || p.IsApproved);
            return Ok(reviews);
         }
      }

      [HttpGet]
      public IHttpActionResult GetAll()
      {
         using (var efundStoreUnitOfWork = new UnitOfWork(Database.EFundStore))
         {
            var reviewsRepository = efundStoreUnitOfWork.CreateRepository<IReviewRepository>();
            var reviews = reviewsRepository.GetAll();
            return Ok(reviews);
         }
      }

      [HttpGet]
      public IHttpActionResult GetAllBySaleId(int saleId, bool showAll = false)
      {
         using (var efundStoreUnitOfWork = new UnitOfWork(Database.EFundStore))
         {
            var reviewsRepository = efundStoreUnitOfWork.CreateRepository<IReviewRepository>();
            var reviews = reviewsRepository.GetAllBySaleId(saleId).Where(p => showAll || p.IsApproved);
            return Ok(reviews);
         }
      }

      [HttpPost]
      public IHttpActionResult Post(Review review)
      {
         using (var efundStoreUnitOfWork = new UnitOfWork(Database.EFundStore))
         {
            var reviewsRepository = efundStoreUnitOfWork.CreateRepository<IReviewRepository>();
            var id = reviewsRepository.Save(review);
            efundStoreUnitOfWork.Commit();
            return Ok(id);
         }
      }

      [HttpDelete]
      public IHttpActionResult Delete(int id)
      {
         using (var efundStoreUnitOfWork = new UnitOfWork(Database.EFundStore))
         {
            var reviewsRepository = efundStoreUnitOfWork.CreateRepository<IReviewRepository>();
            var review = reviewsRepository.GetById(id);
            reviewsRepository.Delete(review);
            efundStoreUnitOfWork.Commit();
            return Ok();
         }
      }

      [HttpPut]
      public IHttpActionResult Put(Review review)
      {
         using (var efundStoreUnitOfWork = new UnitOfWork(Database.EFundStore))
         {
            var reviewsRepository = efundStoreUnitOfWork.CreateRepository<IReviewRepository>();
            reviewsRepository.Update(review);
            efundStoreUnitOfWork.Commit();
            return Ok();
         }
      }
   }
}
