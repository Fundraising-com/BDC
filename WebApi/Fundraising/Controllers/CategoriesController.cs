using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using GA.BDC.Data.Fundraising.Helpers;
using GA.BDC.Shared.Data;
using GA.BDC.Shared.Data.Repositories;
using GA.BDC.Shared.Entities;
using System.Runtime.Caching;
using System;
using System.Web.SessionState;
using System.Web;

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
                //added to help reload speeds of fr all products page (US and CA)
                var categoriesRepository = efundStoreUnitOfWork.CreateRepository<ICategoriesRepository>();
                var expiration = DateTimeOffset.UtcNow.AddHours(6);
                var memoryCacheCA = MemoryCache.Default;
                var memoryCache = MemoryCache.Default;

                if (country == 1)
                {
                    if (!memoryCacheCA.Contains("categoriesCA"))
                    {

                        var getcategories = categoriesRepository.GetByPartner(country, partnerId);
                        memoryCacheCA.Add("categoriesCA", getcategories, expiration);
                    }
                    return (IEnumerable<Category>)memoryCacheCA.Get("categoriesCA", null);

                }
                else
                {
                    if (!memoryCache.Contains("categories"))
                    {

                        var getcategories = categoriesRepository.GetByPartner(country, partnerId);
                        memoryCache.Add("categories", getcategories, expiration);
                    }
                    return (IEnumerable<Category>)memoryCache.Get("categories", null);

                }

                //if (!memoryCache.Contains("categories"))
                //{
                //    var expiration = DateTimeOffset.UtcNow.AddMinutes(5);
                //    var getcategories = categoriesRepository.GetByPartner(country, partnerId);
                //    memoryCache.Add("categories", getcategories, expiration);
                //}


                //var categoriesRepository = efundStoreUnitOfWork.CreateRepository<ICategoriesRepository>();
                //var categories = categoriesRepository.GetByPartner(country, partnerId);
                //foreach (var category in categories)
                //{
                //    category.Parent = categoriesRepository.GetById(category.ParentId);
                //}
                //return (IEnumerable<Category>)memoryCache.Get("categories", null);
                //return categories;
            }
      }
      [HttpGet]
      public IEnumerable<Category> GetByParent(int parentId)
      {
         using (var efundStoreUnitOfWork = new UnitOfWork(Database.EFundStore))
         {
            
            var expiration = DateTimeOffset.UtcNow.AddHours(6);
            var memoryGetByParent = MemoryCache.Default;
            var memoryParentID = MemoryCache.Default;

            //var memorySCCache = MemoryCache.Default;
            
            //var scParentID = 787;

                var categoriesRepository = efundStoreUnitOfWork.CreateRepository<ICategoriesRepository>();
                
                
                //if (scParentID == parentId)
                //{
                //    if (!memorySCCache.Contains("SCcache"))
                //    {
                //        var getSCcategoriesbyparent = categoriesRepository.GetByParent(parentId);
                //        var getparent = categoriesRepository.GetById(parentId);
                //        foreach (var category in getSCcategoriesbyparent)
                //        {
                //            category.Parent = getparent;
                //        }
                //        memoryGetByParent.Add("SCcache", getSCcategoriesbyparent, expiration);
                //        //memoryParentID.Add("parentId", parentId, expiration);
                //        return (IEnumerable<Category>)memorySCCache.Get("SCcache", null);
                //    }
                //    else 
                //    {
                //        return (IEnumerable<Category>)memorySCCache.Get("SCcache", null);
                //    }
                    
                //}

            
                var result = memoryParentID.GetCacheItem("parentId");
                if (result == null)
                {
                    var getcategoriesbyparent = categoriesRepository.GetByParent(parentId);
                    var getparent = categoriesRepository.GetById(parentId);
                    foreach (var category in getcategoriesbyparent)
                    {
                        category.Parent = getparent;
                    }
                    memoryGetByParent.Add("GetByParent", getcategoriesbyparent, expiration);
                    memoryParentID.Add("parentId", parentId, expiration);
                    return (IEnumerable<Category>)memoryGetByParent.Get("GetByParent", null);
                }
                
                if (result.Value.ToString() == parentId.ToString() && memoryGetByParent.Contains("GetByParent"))
                {
                    return (IEnumerable<Category>)memoryGetByParent.Get("GetByParent", null);


                }
                else if (result.Value.ToString() != parentId.ToString() && memoryGetByParent.Contains("GetByParent"))
                {
                    memoryGetByParent.Remove("GetByParent", null);
                    memoryGetByParent.Remove("FilteredResults", null);
                    memoryParentID.Remove("parentId",null);
                    var getcategoriesbyparent = categoriesRepository.GetByParent(parentId);
                    var getparent = categoriesRepository.GetById(parentId);
                    foreach (var category in getcategoriesbyparent)
                    {
                        category.Parent = getparent;
                    }
                    memoryGetByParent.Add("GetByParent", getcategoriesbyparent, expiration);
                    memoryParentID.Add("parentId", parentId, expiration);
                    return (IEnumerable<Category>)memoryGetByParent.Get("GetByParent", null);
                }
                else if (!memoryGetByParent.Contains("GetByParent"))
                {
                    var getcategoriesbyparent = categoriesRepository.GetByParent(parentId);
                    var getparent = categoriesRepository.GetById(parentId);
                    foreach (var category in getcategoriesbyparent)
                    {
                        category.Parent = getparent;
                    }
                    memoryGetByParent.Add("GetByParent", getcategoriesbyparent, expiration);
                    memoryParentID.Add("parentId", parentId, expiration);
                    return (IEnumerable<Category>)memoryGetByParent.Get("GetByParent", null);
                }





                ////var categories = categoriesRepository.GetByParent(parentId);
                ////var parent = categoriesRepository.GetById(parentId);
                ////foreach (var category in categories)
                ////{
                ////   category.Parent = parent;
                ////}
                return (IEnumerable<Category>)memoryGetByParent.Get("GetByParent", null);
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
