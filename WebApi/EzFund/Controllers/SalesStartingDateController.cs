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
    public class SalesStartingDateController : ApiController
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
                var startingDateRepository = EzmainUnitOfWork.CreateRepository<IStartingDateRepository>();
                var referals = startingDateRepository.GetAll();
                return Ok(referals);
            }
        }
    }
}
