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
    public class PrimaryProgramController : ApiController
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
                var primaryProgramRepository = EzmainUnitOfWork.CreateRepository<IPrimaryProgramRepository>();
                var programs = primaryProgramRepository.GetAll();
                return Ok(programs);
            }
        }
    }
}
