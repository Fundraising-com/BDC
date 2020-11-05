using GA.BDC.Data.EzFund.Helpers;
using GA.BDC.Shared.Data;
using GA.BDC.Shared.Data.Repositories;
using GA.BDC.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace GA.BDC.WebApi.EzFund.Controllers
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
            using (var EzmainUnitOfWork = new UnitOfWork(Database.EZMain))
            {
                var blogtagsRepository = EzmainUnitOfWork.CreateRepository<IBlogTagRepository>();
                return Ok(blogtagsRepository.GetAll());
            }
        }

        [HttpGet]
        public IHttpActionResult Get(int id)
        {
            using (var EzmainUnitOfWork = new UnitOfWork(Database.EZMain))
            {
                var blogtagsRepository = EzmainUnitOfWork.CreateRepository<IBlogTagRepository>();
                var entity = blogtagsRepository.GetById(id);
                return entity == null ? BadRequest("Tag doesn't exist") as IHttpActionResult : Ok(entity);
            }

        }
        [HttpGet]
        public IHttpActionResult Get(string url)
        {
            using (var EzmainUnitOfWork = new UnitOfWork(Database.EZMain))
            {
                var blogtagsRepository = EzmainUnitOfWork.CreateRepository<IBlogTagRepository>();
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
            using (var EzmainUnitOfWork = new UnitOfWork(Database.EZMain))
            {
                var blogtagsRepository = EzmainUnitOfWork.CreateRepository<IBlogTagRepository>();
                id = blogtagsRepository.Save(model);
                EzmainUnitOfWork.Commit();
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
            using (var EzmainUnitOfWork = new UnitOfWork(Database.EZMain))
            {
                var blogtagsRepository = EzmainUnitOfWork.CreateRepository<IBlogTagRepository>();
                var model = blogtagsRepository.GetById(id);
                blogtagsRepository.Delete(model);
                EzmainUnitOfWork.Commit();
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
            using (var EzmainUnitOfWork = new UnitOfWork(Database.EZMain))
            {
                var blogtagsRepository = EzmainUnitOfWork.CreateRepository<IBlogTagRepository>();
                blogtagsRepository.Update(model);
                EzmainUnitOfWork.Commit();
                return Ok();
            }
        }

    }
}
