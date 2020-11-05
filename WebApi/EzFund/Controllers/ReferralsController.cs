using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using GA.BDC.Data.EzFund.Helpers;
using GA.BDC.Shared.Data;
using GA.BDC.Shared.Data.Repositories;
using GA.BDC.Shared.Entities;

namespace GA.BDC.WebApi.EzFund.Controllers
{
    public class ReferralsController : ApiController
    {
        [HttpOptions]
        public IHttpActionResult Options()
        {
            return Ok();
        }

        [HttpGet]
        public IHttpActionResult GetReferral()
        {
            using (var EzmainUnitOfWork = new UnitOfWork(Database.EZMain))
            {
                var referalRepository = EzmainUnitOfWork.CreateRepository<IReferralRepository>();
                var referals = referalRepository.GetLeadActiveReferrals();
                return Ok(referals);
            }
        }
    }
}
