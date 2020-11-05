using System.Collections.Generic;
using System.Web.Http;
using GA.BDC.Data.Fundraising.Helpers;
using GA.BDC.Shared.Data;
using GA.BDC.Shared.Data.Repositories;
using GA.BDC.Shared.Entities;

namespace GA.BDC.WebApi.Fundraising.Controllers
{
   public class BlogCategoriesController : ApiController
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
            var blogCategoryRepository = efundStoreUnitOfWork.CreateRepository<IBlogCategoryRepository>();
            return Ok(blogCategoryRepository.GetAll());
         }
      }



      [HttpGet]
      public IHttpActionResult Get(int id)
      {
         using (var efundStoreUnitOfWork = new UnitOfWork(Database.EFundStore))
         {
            var blogCategoryRepository = efundStoreUnitOfWork.CreateRepository<IBlogCategoryRepository>();
            return Ok(blogCategoryRepository.GetById(id));
         }

      }
      [HttpGet]
      public IHttpActionResult Get(string url)
      {
         using (var efundStoreUnitOfWork = new UnitOfWork(Database.EFundStore))
         {
            var blogCategoryRepository = efundStoreUnitOfWork.CreateRepository<IBlogCategoryRepository>();
            return Ok(blogCategoryRepository.GetByUrl(url));
         }
      }

      /// <summary>
      /// Creates a Blog's category on the DB with the object received
      /// </summary>
      /// <param name="model"></param>
      /// <returns></returns>
      [HttpPost]
      [Authorize]
      public IHttpActionResult Post(BlogCategory model)
      {
         int id;
         using (var efundStoreUnitOfWork = new UnitOfWork(Database.EFundStore))
         {
            var blogCategoryRepository = efundStoreUnitOfWork.CreateRepository<IBlogCategoryRepository>();
            id = blogCategoryRepository.Save(model);
            efundStoreUnitOfWork.Commit();
         }
         return Get(id);
      }

      /// <summary>
      /// Updates a Blog's category on the DB with the object received
      /// </summary>
      /// <param name="model"></param>
      /// <returns></returns>
      [HttpPut]
      [Authorize]
      public IHttpActionResult Put(BlogCategory model)
      {
         using (var efundStoreUnitOfWork = new UnitOfWork(Database.EFundStore))
         {
            var blogCategoryRepository = efundStoreUnitOfWork.CreateRepository<IBlogCategoryRepository>();
            blogCategoryRepository.Update(model);
            efundStoreUnitOfWork.Commit();
            return Ok();
         }
      }

      /// <summary>
      /// Deletes the category by the Id received
      /// </summary>
      /// <returns></returns>
      [HttpDelete]
      [Authorize]
      public IHttpActionResult Delete(int id)
      {
         using (var efundStoreUnitOfWork = new UnitOfWork(Database.EFundStore))
         {
            var blogCategoryRepository = efundStoreUnitOfWork.CreateRepository<IBlogCategoryRepository>();
            var model = blogCategoryRepository.GetById(id);
            blogCategoryRepository.Delete(model);
            efundStoreUnitOfWork.Commit();
            return Ok();
         }
      }
   }
}