using System;
using System.Web.Http;
using GA.BDC.Data.Fundraising.Helpers;
using GA.BDC.Shared.Data;
using GA.BDC.Shared.Data.Repositories;
using GA.BDC.Shared.Entities;
using System.Linq;
using System.Security.Claims;

namespace GA.BDC.WebApi.Fundraising.Controllers
{
   public class BlogController : ApiController
   {
      [HttpOptions]
      public IHttpActionResult Options()
      {
         return Ok();
      }
      [HttpGet]
      public IHttpActionResult GetAll(bool onlyPublishedReady = true)
      {
         using (var efundStoreUnitOfWork = new UnitOfWork(Database.EFundStore))
         {
            var blogPostRepository = efundStoreUnitOfWork.CreateRepository<IBlogPostRepository>();
            var result = blogPostRepository.GetAll();
            if (onlyPublishedReady)
            {
               result = result.Where(p => !p.IsDraft && p.Published < DateTime.Now).ToList();
            }
            foreach (var post in result)
            {
               LazyLoad(post);
            }
            return Ok(result);
         }
      }
      [HttpGet]
      public IHttpActionResult GetSortedPosts(bool sortByRecent, int limit = 0)
      {
         using (var efundStoreUnitOfWork = new UnitOfWork(Database.EFundStore))
         {
            var blogPostRepository = efundStoreUnitOfWork.CreateRepository<IBlogPostRepository>();
            var result = blogPostRepository.GetAll();
            if (sortByRecent)
            {
               result = result.Where(p => !p.IsDraft).OrderByDescending(p => p.Created).ToList();
            }
            if (limit > 0)
            {
               result = result.Take(limit).ToList();
            }
            foreach (var post in result)
            {
               LazyLoad(post);
            }
            return Ok(result);
         }
      }
      [HttpGet]
      public IHttpActionResult GetRandomPosts(bool random, int limit = 0)
      {
         using (var efundStoreUnitOfWork = new UnitOfWork(Database.EFundStore))
         {
            var blogPostRepository = efundStoreUnitOfWork.CreateRepository<IBlogPostRepository>();
            var result = blogPostRepository.GetAll();
            if (random)
            {
               var randomNumber = new Random(DateTime.Now.Millisecond);
               result = result.OrderBy(p => randomNumber.Next(0, 1000)).ToList();
            }
            if (limit > 0)
            {
               result = result.Take(limit).ToList();
            }
            foreach (var post in result)
            {
               LazyLoad(post);
            }
            return Ok(result);
         }
      }

      [HttpGet]
      public IHttpActionResult Get(int id)
      {
         using (var efundStoreUnitOfWork = new UnitOfWork(Database.EFundStore))
         {
            var blogPostRepository = efundStoreUnitOfWork.CreateRepository<IBlogPostRepository>();
            var result = blogPostRepository.GetById(id);
            LazyLoad(result);
            return Ok(result);
         }
      }
      [HttpGet]
      public IHttpActionResult Get(string url, bool isPreview = false)
      {
         using (var efundStoreUnitOfWork = new UnitOfWork(Database.EFundStore))
         {
            var blogPostRepository = efundStoreUnitOfWork.CreateRepository<IBlogPostRepository>();
            var result = blogPostRepository.GetByUrl(url, isPreview);
            if (result == null)
            {
               return BadRequest("Post doesn't exist");
            }
            LazyLoad(result);
            return Ok(result);
         }
      }
      [HttpGet]
      public IHttpActionResult GetAllByCategory(int categoryId)
      {
         using (var efundStoreUnitOfWork = new UnitOfWork(Database.EFundStore))
         {
            var blogPostRepository = efundStoreUnitOfWork.CreateRepository<IBlogPostRepository>();
            var result = blogPostRepository.GetAllByCategoryId(categoryId);
            foreach (var post in result)
            {
               LazyLoad(post);
            }
            return Ok(result);
         }
      }
      [HttpGet]
      public IHttpActionResult GetByTag(int tagId)
      {
         using (var efundStoreUnitOfWork = new UnitOfWork(Database.EFundStore))
         {
            var blogPostRepository = efundStoreUnitOfWork.CreateRepository<IBlogPostRepository>();
            var result = blogPostRepository.GetAllByTagId(tagId);
            foreach (var post in result)
            {
               LazyLoad(post);
            }
            return Ok(result);
         }
      }
      /// <summary>
      /// Loads the Tags collection and the Category
      /// </summary>
      /// <param name="post"></param>
      private void LazyLoad(BlogPost post)
      {
         using (var efundStoreUnitOfWork = new UnitOfWork(Database.EFundStore))
         {
            var blogTagRepository = efundStoreUnitOfWork.CreateRepository<IBlogTagRepository>();
            var blogCategoryRepository = efundStoreUnitOfWork.CreateRepository<IBlogCategoryRepository>();
            if (post != null)
            {
               var tags = blogTagRepository.GetAllByPostId(post.Id);
               if (tags != null)
               {
                  post.Tags.AddRange(tags);
               }
               var category = blogCategoryRepository.GetById(post.CategoryId);
               if (category != null)
               {
                  post.Category = category;
               }
            }
         }

      }
      /// <summary>
      /// Creates a Blog's Post on the DB with the object received
      /// </summary>
      /// <param name="model"></param>
      /// <returns></returns>
      [HttpPost]
      [Authorize]
      public IHttpActionResult Post(BlogPost model)
      {
         var user = (ClaimsIdentity) User.Identity;
         model.Author = user.Name;
         int id;
         using (var efundStoreUnitOfWork = new UnitOfWork(Database.EFundStore))
         {
            var blogPostRepository = efundStoreUnitOfWork.CreateRepository<IBlogPostRepository>();
            id = blogPostRepository.Save(model);
            
            efundStoreUnitOfWork.Commit();
         }
         return Get(id);
      }

      /// <summary>
      /// Updates a Blog's Post on the DB with the object received
      /// </summary>
      /// <param name="model"></param>
      /// <returns></returns>
      [HttpPut]
      [Authorize]
      public IHttpActionResult Put(BlogPost model)
      {
         using (var efundStoreUnitOfWork = new UnitOfWork(Database.EFundStore))
         {
            var blogPostRepository = efundStoreUnitOfWork.CreateRepository<IBlogPostRepository>();
            blogPostRepository.Update(model);
            efundStoreUnitOfWork.Commit();
            return Ok();
         }
      }

      /// <summary>
      /// Deletes the Post by the Id received
      /// </summary>
      /// <param name="id"></param>
      /// <returns></returns>
      [HttpDelete]
      [Authorize]
      public IHttpActionResult Delete(int id)
      {
         using (var efundStoreUnitOfWork = new UnitOfWork(Database.EFundStore))
         {
            var blogPostRepository = efundStoreUnitOfWork.CreateRepository<IBlogPostRepository>();
            var model = blogPostRepository.GetById(id);
            blogPostRepository.Delete(model);
            efundStoreUnitOfWork.Commit();
            return Ok();
         }
      }
   }
}