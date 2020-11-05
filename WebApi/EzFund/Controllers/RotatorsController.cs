using GA.BDC.Data.EzFund.Helpers;
using GA.BDC.Shared.Data;
using GA.BDC.Shared.Data.Repositories;
using GA.BDC.Shared.Entities;
using System.Collections.Generic;
using System.Web.Http;

namespace GA.BDC.WebApi.EzFund.Controllers
{
    public class RotatorsController : ApiController
    {
        [HttpGet]
        public IHttpActionResult GetAllActive(bool isActive)
        {

            using (var EzmainUnitOfWork = new UnitOfWork(Database.EZMain))
            {
                var homepagerotatorRepository = EzmainUnitOfWork.CreateRepository<IHomePageRotatorRepository>();
                var results = homepagerotatorRepository.GetAll(isActive);
                return Ok(results);
            }
        }
    }
}
