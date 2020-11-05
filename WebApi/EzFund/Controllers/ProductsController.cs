using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using GA.BDC.Data.EzFund.Helpers;
using GA.BDC.Shared.Data;
using GA.BDC.Shared.Data.Repositories;
using GA.BDC.Shared.Entities;

namespace GA.BDC.WebApi.EzFund.Controllers
{
    public class ProductsController : ApiController
    {
        [HttpOptions]
        public IHttpActionResult Options()
        {
            return Ok();
        }
        /// <summary>
        /// Returns all the available products
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult GetProducts()
        {
            using (var EzmainUnitOfWork = new UnitOfWork(Database.EZMain))
            {
               
                var result = new List<Product>();
                var categoryRepository = EzmainUnitOfWork.CreateRepository<ICategoriesRepository>();
                var productRepository = EzmainUnitOfWork.CreateRepository<IProductRepository>();
                var categories = categoryRepository.GetAllCategories();
            
                foreach (var category in categories) {
                    var code = category.Id;
                    IList<Product> products = productRepository.GetRelatedProducts(code, 0, false);
                        foreach (var product in products) {
                            product.Category = category;
                        }
                        result.AddRange(products);
                    
                }
                return Ok(result);
            }
        }

        [HttpGet]
        public IHttpActionResult GetProducts(bool isSitemap)
        {
            using (var EzmainUnitOfWork = new UnitOfWork(Database.EZMain))
            {

                var result = new List<Product>();
                var categoryRepository = EzmainUnitOfWork.CreateRepository<ICategoriesRepository>();
                var productRepository = EzmainUnitOfWork.CreateRepository<IProductRepository>();
                var categories = categoryRepository.GetAllCategoriesSiteMap();

                foreach (var category in categories)
                {
                    var code = category.Id;
                    IList<Product> products = productRepository.GetRelatedProducts(code, 0, false);
                    foreach (var product in products)
                    {
                        product.Category = category;
                    }
                    result.AddRange(products);

                }
                return Ok(result);
            }
        }



        /// <summary>
        /// Returns Product by id
        /// </summary>
        /// <param productId="id"></param>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult Get(int id)
        {
            using (var EzmainUnitOfWork = new UnitOfWork(Database.EZMain))
            {
                var productRepository = EzmainUnitOfWork.CreateRepository<IProductRepository>();
                var product = productRepository.GetById(id);
                return Ok(product);
            }
        }
        /// <summary>
        /// Returns Program as a Product by code
        /// </summary>
        /// <param programCode="code"></param>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult Get(string code)
        {
            using (var EzmainUnitOfWork = new UnitOfWork(Database.EZMain))
            {
                var productRepository = EzmainUnitOfWork.CreateRepository<IProductRepository>();
                var product = productRepository.GetByCode(code);
                return Ok(product);
            }
        }
        /// <summary>
        /// Returns Related Products
        /// </summary>
        /// <param CategoryGroupCode="code"></param>
        /// <param MaximumResultsRequired="maxResults"></param>
        /// <param Randomize="isRandom"></param>
        /// <param Product or Program="canBePurchased"></param>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult GetCategoryRelatedProducts(string code, int maxResults, bool isRandom, bool canBePurchased)
        {
            using (var EzmainUnitOfWork = new UnitOfWork(Database.EZMain))
            {
                IList<Product> products;
                var repository = EzmainUnitOfWork.CreateRepository<IProductRepository>();
                products = repository.GetRelatedProducts(code, maxResults, isRandom, canBePurchased);
                return Ok(products);
            }
        }



        /// <summary>
        /// Returns Related Products
        /// </summary>
        /// <param CategoryId="id"></param>
        /// <param MaximumResultsRequired="maxResults"></param>
        /// <param Randomize="isRandom"></param>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult GetCategoryRelatedProducts(int id, int maxResults, bool isRandom)
        {
            using (var EzmainUnitOfWork = new UnitOfWork(Database.EZMain))
            {
                IList<Product> products;
                var repository = EzmainUnitOfWork.CreateRepository<IProductRepository>();
                var categoriesRepository = EzmainUnitOfWork.CreateRepository<ICategoriesRepository>();
                products = repository.GetRelatedProducts(id, maxResults, isRandom);
                foreach (var product in products) {
                    product.Category = categoriesRepository.GetByProductId(product.Id);
                    if(product.Category.ParentId > -1) { 
                        product.Category.Parent = categoriesRepository.GetParentById(product.Category.ParentId);
                    }
                    if (product.Category.Parent.ParentId > -1) { 
                        product.Category.Parent.Parent = categoriesRepository.GetParentById(product.Category.Parent.ParentId);
                    }
                }
                return Ok(products);
            }
        }

        /// <summary>
        /// Returns Product or Program as Product by id
        /// </summary>
        /// <param RootCategoryURL="rootCategory"></param>
        /// <param CategoryCleanURL="category"></param>
        /// <param ProductCleanURL="url"></param>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult GetProductByUrl(string rootCategoryUrl, string categoryUrl, string url)
        {
            using (var EzmainUnitOfWork = new UnitOfWork(Database.EZMain))
            {
                var productRepository = EzmainUnitOfWork.CreateRepository<IProductRepository>();
                var categoriesRepository = EzmainUnitOfWork.CreateRepository<ICategoriesRepository>();
                try
                {
                    var parentCategory = categoriesRepository.GetCategoryByUrl(rootCategoryUrl);
                    if (!parentCategory.Any())
                    {
                        return BadRequest();
                    }
                    var subCategory = categoriesRepository.GetCategoryByUrl(categoryUrl);
                    if (!subCategory.Any())
                    {
                        return BadRequest();
                    }
                    subCategory.First().Parent = parentCategory.First();

                    var product = productRepository.GetByUrl(url);
                    product.Category = subCategory.First();
                    return Ok(product);
                }
                catch
                {
                    return BadRequest();
                }
            }
        }
    }
}
