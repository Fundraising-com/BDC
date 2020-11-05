using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using GA.BDC.Data.Fundraising.Helpers;
using GA.BDC.Shared.Data;
using GA.BDC.Shared.Data.Repositories;
using GA.BDC.Shared.Entities;

namespace GA.BDC.WebApi.Fundraising.Controllers
{
   public class CategoriesController : ApiController
   {
      [HttpOptions]
      public IHttpActionResult Options()
      {
         return Ok();
      }

      [HttpGet]
      public IEnumerable<Category> GetByPartner(int country, int partnerId)
      {
         using (var efundStoreUnitOfWork = new UnitOfWork(Database.EFundStore))
         {
            var categoriesRepository = efundStoreUnitOfWork.CreateRepository<ICategoriesRepository>();
            var categories = categoriesRepository.GetByPartner(country, partnerId);
            foreach (var category in categories)
            {
               category.Parent = categoriesRepository.GetById(category.ParentId);
            }
            return categories;
         }
      }
      [HttpGet]
      public IEnumerable<Category> GetByParent(int parentId)
      {
         using (var efundStoreUnitOfWork = new UnitOfWork(Database.EFundStore))
         {
            var categoriesRepository = efundStoreUnitOfWork.CreateRepository<ICategoriesRepository>();
            var categories = categoriesRepository.GetByParent(parentId);
            var parent = categoriesRepository.GetById(parentId);
            foreach (var category in categories)
            {
               category.Parent = parent;
            }
            return categories;
         }
      }

      /// <summary>
      /// Returns the first Category found by URL that is NOT a Root Package
      /// </summary>
      /// <param name="country"></param>
      /// <param name="url"></param>
      /// <param name="isRoot">True if the searched Category is a root</param>
      /// <returns></returns>
      [HttpGet]
      public IHttpActionResult GetByUrl(int country, string url, bool isRoot)
      {
         using (var efundStoreUnitOfWork = new UnitOfWork(Database.EFundStore))
         {
            var categoriesRepository = efundStoreUnitOfWork.CreateRepository<ICategoriesRepository>();
            var categories = categoriesRepository.GetCategoryByUrl(country, url).ToList();
            if (!categories.Any(p => (isRoot && p.Parent == null) || (!isRoot && p.Parent != null)))
            {
               return BadRequest();
            }
            var category = categories.First(p => (isRoot && p.Parent == null) || (!isRoot && p.Parent != null));
            category.Parent = categoriesRepository.GetById(category.ParentId);
            return Ok(category);
              
            }
      }
   }
}
