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
    public class CategoriesController : ApiController
    {
        [HttpOptions]
        public IHttpActionResult Options()
        {
            return Ok();
        }
        /// <summary>
        /// Returns all Products SubCategories
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult GetAllCategories()
        {
            using (var EzmainUnitOfWork = new UnitOfWork(Database.EZMain))
            {
                var categoriesRepository = EzmainUnitOfWork.CreateRepository<ICategoriesRepository>();
                try
                {
                    var categoriesFound = categoriesRepository.GetAllCategories();
                    return Ok(categoriesFound);
                }
                catch
                {
                    return BadRequest();
                }
            }
        }
        /// <summary>
        /// Returns all Products SubCategories
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult GetCategoryByUrl(string url)
        {
            using (var EzmainUnitOfWork = new UnitOfWork(Database.EZMain))
            {
                var categoriesRepository = EzmainUnitOfWork.CreateRepository<ICategoriesRepository>();
                try
                {
                    var parentCategory = categoriesRepository.GetCategoryByUrl(url);
                    if (!parentCategory.Any()) {
                        return BadRequest();
                    }
                    
                    var subCategories= categoriesRepository.GetByParent(parentCategory.First().Id);
                    foreach (var subCategory in subCategories)
                    {
                        subCategory.Parent = parentCategory.First();
                    }
                    return Ok(subCategories);
                }
                catch
                {
                    return BadRequest();
                }
            }
        }
        /// <summary>
        /// Returns a Category from a CleanUrl
        /// </summary>
        /// <param MainCategory="rootCategory"></param>
        /// <param CategoryCleanURL="category"></param>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult GetSubCategoryByUrl(string rootCategory, string url)
        {
            using (var EzmainUnitOfWork = new UnitOfWork(Database.EZMain))
            {
                var categoriesRepository = EzmainUnitOfWork.CreateRepository<ICategoriesRepository>();
                try
                {
                    var parentCategory = categoriesRepository.GetCategoryByUrl(rootCategory);
                    if (!parentCategory.Any())
                    {
                        return BadRequest();
                    }

                    var subCategory = categoriesRepository.GetCategoryByUrl(url);
                    if (!subCategory.Any())
                    {
                        return BadRequest();
                    }

                    subCategory.First().Parent = parentCategory.First();
                    return Ok(subCategory.First());
                }
                catch
                {
                    return BadRequest();
                }
            }
        }
    }
}
