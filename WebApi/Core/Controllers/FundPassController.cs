using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using GA.BDC.Data.Fundraising.Helpers;
using GA.BDC.Shared.Data;
using GA.BDC.Shared.Data.Repositories;
using GA.BDC.Shared.Entities;
using System.Configuration;

namespace GA.BDC.WebApi.Fundraising.Core.Controllers
{
    public class FundPassController : ApiController
    {

        /// <summary>
        /// Get Fund Pass emails to send
        /// </summary>
        ///
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult GetFundPassLeads()
        {
            
            using (var efundraisingProdUnitOfWork = new UnitOfWork(Database.EFundraisingProd))
            {
                var fundpassRepository = efundraisingProdUnitOfWork.CreateRepository<IFundPassCouponRepositoryRepository>();
                var leadsToEmails = fundpassRepository.GetCodesToProcessAll();
                return Ok(leadsToEmails);
            }

        }

    }
}