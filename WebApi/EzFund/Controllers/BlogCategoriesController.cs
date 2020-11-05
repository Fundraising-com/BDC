using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using GA.BDC.Shared.Data;
using GA.BDC.Shared.Data.Repositories;
using GA.BDC.Shared.Entities;
using GA.BDC.Data.EzFund.Helpers;

namespace GA.BDC.WebApi.EzFund.Controllers
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
            using (var EzmainUnitOfWork = new UnitOfWork(Database.EZMain))
            {
                var blogCategoryRepository = EzmainUnitOfWork.CreateRepository<IBlogCategoryRepository>();
                return Ok(blogCategoryRepository.GetAll());
            }
        }

        [HttpGet]
        public IHttpActionResult Get(int id)
        {
            using (var EzmainUnitOfWork = new UnitOfWork(Database.EZMain))
            {
                var blogCategoryRepository = EzmainUnitOfWork.CreateRepository<IBlogCategoryRepository>();
                return Ok(blogCategoryRepository.GetById(id));
            }
        }

        [HttpGet]
        public IHttpActionResult Get(string url)
        {
            using (var EzmainUnitOfWork = new UnitOfWork(Database.EZMain))
            {
                var blogCategoryRepository = EzmainUnitOfWork.CreateRepository<IBlogCategoryRepository>();
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
            using (var EzmainUnitOfWork = new UnitOfWork(Database.EZMain))
            {
                var blogCategoryRepository = EzmainUnitOfWork.CreateRepository<IBlogCategoryRepository>();
                id = blogCategoryRepository.Save(model);
                EzmainUnitOfWork.Commit();
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
            using (var EzmainUnitOfWork = new UnitOfWork(Database.EZMain))
            {
                var blogCategoryRepository = EzmainUnitOfWork.CreateRepository<IBlogCategoryRepository>();
                blogCategoryRepository.Update(model);
                EzmainUnitOfWork.Commit();
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
            using (var EzmainUnitOfWork = new UnitOfWork(Database.EZMain))
            {
                var blogCategoryRepository = EzmainUnitOfWork.CreateRepository<IBlogCategoryRepository>();
                var model = blogCategoryRepository.GetById(id);
                blogCategoryRepository.Delete(model);
                EzmainUnitOfWork.Commit();
                return Ok();
            }
        }
    }
}
