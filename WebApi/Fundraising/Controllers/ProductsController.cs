using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using GA.BDC.Data.Fundraising.Helpers;
using GA.BDC.Shared.Data;
using GA.BDC.Shared.Data.Repositories;
using GA.BDC.Shared.Entities;
using System.Runtime.Caching;


namespace GA.BDC.WebApi.Fundraising.Controllers
{
   public class ProductsController : ApiController
   {
      [HttpOptions]
      public IHttpActionResult Options()
      {
         return Ok();
      }

      [HttpGet]
      public IEnumerable<Product> GetAllByCountry(int country)
      {
         using (var efundStoreUnitOfWork = new UnitOfWork(Database.EFundStore))
         {
            var productRepository = efundStoreUnitOfWork.CreateRepository<IProductRepository>();
            var categoriesRepository = efundStoreUnitOfWork.CreateRepository<ICategoriesRepository>();
            var products = productRepository.GetAllByCountry(country).ToList();
            foreach (var product in products)
            {
               product.Category = categoriesRepository.GetById(product.CategoryId);
               product.Category.Parent = categoriesRepository.GetById(product.Category.ParentId);
            }
            return products.OrderBy(p => p.DisplayOrder);
         }
      }

      /// <summary>
      /// Returns all Featured Products
      /// </summary>
      /// <param name="isFeatured"></param>
      /// <returns></returns>
      [HttpGet]
      public IEnumerable<Product> GetFeatured(bool isFeatured)
      {
         if (!isFeatured)
         {
            return new List<Product>();
         }
         using (var efundStoreUnitOfWork = new UnitOfWork(Database.EFundStore))
         {
            var productRepository = efundStoreUnitOfWork.CreateRepository<IProductRepository>();
            var categoriesRepository = efundStoreUnitOfWork.CreateRepository<ICategoriesRepository>();
            var featuredProducts = productRepository.GetAllFeatured().ToList();
            foreach (var product in featuredProducts)
            {
               product.Category = categoriesRepository.GetById(product.CategoryId);
               product.Category.Parent = categoriesRepository.GetById(product.Category.ParentId);
            }
            return featuredProducts.OrderBy(p => p.DisplayOrderFeatured);
         }
      }
      /// <summary>
      /// Returns all Products for Category selected
      /// </summary>
      /// <param name="categoryId"></param>
      /// <returns></returns>
      [HttpGet]
      public IEnumerable<Product> GetByCategory(int categoryId)
      {
         using (var efundStoreUnitOfWork = new UnitOfWork(Database.EFundStore))
         {
            var productRepository = efundStoreUnitOfWork.CreateRepository<IProductRepository>();
            var categoriesRepository = efundStoreUnitOfWork.CreateRepository<ICategoriesRepository>();
            var products = productRepository.GetByCategory(categoryId).ToList();
            foreach (var product in products)
            {
               product.Category = categoriesRepository.GetById(product.CategoryId);
               product.Category.Parent = categoriesRepository.GetById(product.Category.ParentId);
            }
            return products.OrderBy(p => p.DisplayOrder);
         }
      }
      /// <summary>
      /// Returns Product by id
      /// </summary>
      /// <param name="id"></param>
      /// <returns></returns>
      [HttpGet]
      public Product Get(int id)
      {
         using (var efundStoreUnitOfWork = new UnitOfWork(Database.EFundStore))
         {
            var productRepository = efundStoreUnitOfWork.CreateRepository<IProductRepository>();
            var categoriesRepository = efundStoreUnitOfWork.CreateRepository<ICategoriesRepository>();
            var product = productRepository.GetById(id);
            product.Category = categoriesRepository.GetById(product.CategoryId);
            product.Category.Parent = categoriesRepository.GetById(product.Category.ParentId);
            return product;
         }
      }

      /// <summary>
      /// Returns Product by URL
      /// </summary>
      /// <param name="country"></param>
      /// <param name="rootCategory"></param>
      /// <param name="category"></param>
      /// <param name="url"></param>
      /// <returns></returns>
      [HttpGet]
      public IHttpActionResult GetByUrl(int country, string rootCategory, string category, string url)
      {
         using (var efundStoreUnitOfWork = new UnitOfWork(Database.EFundStore))
         {
            var productRepository = efundStoreUnitOfWork.CreateRepository<IProductRepository>();
            var categoriesRepository = efundStoreUnitOfWork.CreateRepository<ICategoriesRepository>();
            try
            {
               var rootCategoryFound =
                  categoriesRepository.GetCategoryByUrl(country, rootCategory).First(p => p.Parent == null);
               var categoryFound =
                  categoriesRepository.GetByParent(rootCategoryFound.Id).First(p => p.Url == category);
               var products = productRepository.GetByCategory(categoryFound.Id);
               var product = products.First(p => p.Url == url);
               product.Category = categoryFound;
               product.Category.Parent = rootCategoryFound;
               return Ok(product);
            }
            catch
            {
               return BadRequest();
            }
         }
      }

      /// <summary>
      /// Returns products to be suggested in the Right section of the product page, excluding the current product
      /// </summary>
      /// <param name="currentProductId">Product being shown in the Website, this will be excluded</param>
      /// <param name="country"></param>
      /// <param name="maxSuggestions">Max number of suggestins</param>
      /// <returns></returns>
      [HttpGet]
      public IEnumerable<Product> GetSuggested(int currentProductId, int country, int maxSuggestions = 2)
      {
         using (var efundStoreUnitOfWork = new UnitOfWork(Database.EFundStore))
         {
            var productRepository = efundStoreUnitOfWork.CreateRepository<IProductRepository>();
            var result = new List<Product>();
            var products = productRepository.GetAllByCountry(country).ToList();
            products.RemoveAll(p => p.Id == currentProductId); // remove the current product
            var random = new Random(DateTime.Now.Millisecond);
            products = products.OrderBy(p => random.Next(0, 1000)).Take(maxSuggestions).ToList();
            foreach (var product in products)
            {
               var p = Get(product.Id);
               result.Add(p);
            }
            return result;
         }
      }
      /// <summary>
      /// Returns all the Products by the filter
      /// </summary>
      /// <param name="price">Up to Price, 0 = ALL</param>
      /// <param name="profit">Up to Profit, 0 = ALL</param>
      /// <param name="productTypes">Product Types searched, NULL = ALL</param>
      /// <returns></returns>
      [HttpGet]
      public IEnumerable<Product> GetFiltered(int price, int profit, string productTypes)
      {
         using (var efundStoreUnitOfWork = new UnitOfWork(Database.EFundStore))
         {
            
            var expiration = DateTimeOffset.UtcNow.AddHours(6);
            var memoryFilteredProducts = MemoryCache.Default; 
            //var memorySCFilteredProducts = MemoryCache.Default;   
            var productRepository = efundStoreUnitOfWork.CreateRepository<IProductRepository>();
            var categoriesRepository = efundStoreUnitOfWork.CreateRepository<ICategoriesRepository>();
            var result = new List<Product>();
            var types = new List<int>();
            foreach (var t in productTypes.Split(','))
            {
               if (!string.IsNullOrEmpty(t))
               {
                  types.Add(int.Parse(t));
               }
            }
                //var typeCatID = types[0];
                //var scproductTypesID = 787;
                ////var match = types.Contains(scproductTypesID);


                //if (scproductTypesID == typeCatID)
                //{

                //    if (!memorySCFilteredProducts.Contains("SCFilteredResults"))
                //    {
                //        var products = productRepository.GetFiltered(price, profit, types.ToArray()).ToList();
                //        foreach (var product in products)
                //        {
                //            product.Category = categoriesRepository.GetById(product.CategoryId);
                //            product.Category.Parent = categoriesRepository.GetById(product.Category.ParentId);
                //            result.AddRange(products);
                //            result.OrderBy(p => p.DisplayOrder);
                //            memorySCFilteredProducts.Add("SCFilteredResults", result, expiration);
                //            return (IEnumerable<Product>)memorySCFilteredProducts.Get("SCFilteredResults", null);
                //        }
                //    }
                //    else
                //    {
                //        return (IEnumerable<Product>)memorySCFilteredProducts.Get("SCFilteredResults", null);
                //    }


                //}


                if (!memoryFilteredProducts.Contains("FilteredResults"))
                {
                    var products = productRepository.GetFiltered(price, profit, types.ToArray()).ToList();
                    foreach (var product in products)
                    {
                        product.Category = categoriesRepository.GetById(product.CategoryId);
                        product.Category.Parent = categoriesRepository.GetById(product.Category.ParentId);
                    }
                    result.AddRange(products);
                    result.OrderBy(p => p.DisplayOrder);
                    memoryFilteredProducts.Add("FilteredResults", result, expiration);
                }
                //    var products = productRepository.GetFiltered(price, profit, types.ToArray()).ToList();
                //    foreach (var product in products)
                //    {
                //        product.Category = categoriesRepository.GetById(product.CategoryId);
                //        product.Category.Parent = categoriesRepository.GetById(product.Category.ParentId);
                //    }
                //result.AddRange(products);


                //return result.OrderBy(p => p.DisplayOrder);
                return (IEnumerable<Product>)memoryFilteredProducts.Get("FilteredResults", null);
         }
      }

      [HttpGet]
      public IHttpActionResult GetByRootCategory(int rootCategoryId)
      {
         using (var efundStoreUnitOfWork = new UnitOfWork(Database.EFundStore))
         {


            var productRepository = efundStoreUnitOfWork.CreateRepository<IProductRepository>();
            var categoriesRepository = efundStoreUnitOfWork.CreateRepository<ICategoriesRepository>();
            var categories = categoriesRepository.GetByParent(rootCategoryId);

            var expiration = DateTimeOffset.UtcNow.AddHours(6);
            var memoryCacheCA = MemoryCache.Default;
            var memoryCache = MemoryCache.Default;                
            IList<Product> result = new List<Product>();
            
            foreach (var category in categories)
            {
               var subCategories = categoriesRepository.GetByParent(category.Id);
               foreach (var subCategory in subCategories)
               {
                  subCategory.Parent = category;
                  var products = productRepository.GetByCategory(subCategory.Id);
                  foreach (var product in products)
                  {
                     product.Category = subCategory;
                     result.Add(product);
                  }
               }
            }
            return Ok(result);
         }
      }

      [HttpPut]
      public IHttpActionResult Update(Product product)
      {
         using (var efundStoreUnitOfWork = new UnitOfWork(Database.EFundStore))
         {
            var productRepository = efundStoreUnitOfWork.CreateRepository<IProductRepository>();
            productRepository.Update(product);
            efundStoreUnitOfWork.Commit();
            return Ok();
         }
      }
   }
}
