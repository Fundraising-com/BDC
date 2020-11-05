using System.Web.Http;
using GA.BDC.Data.Fundraising.Helpers;
using GA.BDC.Shared.Data;
using GA.BDC.Shared.Data.Repositories;
using GA.BDC.Shared.Entities;

namespace GA.BDC.WebApi.Fundraising.Controllers
{
   public class BlogTagsController : ApiController
   {
      [HttpOptions]
      public IHttpActionResult Options()
      {
         return Ok();
      }
      [HttpGet]
      public IHttpActionResult GetAll()
      {
         using (var efundStoreUnitOfWork = new UnitOfWork(Database.EFundStore))
         {
            var blogtagsRepository = efundStoreUnitOfWork.CreateRepository<IBlogTagRepository>();
            return Ok(blogtagsRepository.GetAll());
         }
      }

      [HttpGet]
      public IHttpActionResult Get(int id)
      {
         using (var efundStoreUnitOfWork = new UnitOfWork(Database.EFundStore))
         {
            var blogtagsRepository = efundStoreUnitOfWork.CreateRepository<IBlogTagRepository>();
            var entity = blogtagsRepository.GetById(id);
            return entity == null ? BadRequest("Tag doesn't exist") as IHttpActionResult : Ok(entity);
         }

      }
      [HttpGet]
      public IHttpActionResult Get(string url)
      {
         using (var efundStoreUnitOfWork = new UnitOfWork(Database.EFundStore))
         {
            var blogtagsRepository = efundStoreUnitOfWork.CreateRepository<IBlogTagRepository>();
            var entity = blogtagsRepository.GetByUrl(url);
            return entity == null ? BadRequest("Tag doesn't exist") as IHttpActionResult : Ok(entity);
         }

      }

      /// <summary>
      /// Creates a Blog tag on the DB with the object received
      /// </summary>
      /// <param name="model"></param>
      /// <returns></returns>
      [HttpPost]
      [Authorize]
      public IHttpActionResult Post(BlogTag model)
      {
         int id;
         using (var efundStoreUnitOfWork = new UnitOfWork(Database.EFundStore))
         {
            var blogtagsRepository = efundStoreUnitOfWork.CreateRepository<IBlogTagRepository>();
            id = blogtagsRepository.Save(model);
            efundStoreUnitOfWork.Commit();
         }
         return Get(id);
      }

      /// <summary>
      /// Deletes the tag by the Id received
      /// </summary>
      /// <param name="id"></param>
      /// <returns></returns>
      [HttpDelete]
      [Authorize]
      public IHttpActionResult Delete(int id)
      {
         using (var efundStoreUnitOfWork = new UnitOfWork(Database.EFundStore))
         {
            var blogtagsRepository = efundStoreUnitOfWork.CreateRepository<IBlogTagRepository>();
            var model = blogtagsRepository.GetById(id);
            blogtagsRepository.Delete(model);
            efundStoreUnitOfWork.Commit();
            return Ok();
         }
      }

      /// <summary>
      /// Updates a Blog tag on the DB with the object received
      /// </summary>
      /// <param name="model"></param>
      /// <returns></returns>
      [HttpPut]
      [Authorize]
      public IHttpActionResult Put(BlogTag model)
      {
         using (var efundStoreUnitOfWork = new UnitOfWork(Database.EFundStore))
         {
            var blogtagsRepository = efundStoreUnitOfWork.CreateRepository<IBlogTagRepository>();
            blogtagsRepository.Update(model);
            efundStoreUnitOfWork.Commit();
            return Ok();
         }
      }

   }
}
