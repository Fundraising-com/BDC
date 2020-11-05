using GA.BDC.Data.EzFund.Helpers;
using GA.BDC.Shared.Data;
using GA.BDC.Shared.Data.Repositories;
using GA.BDC.Shared.Entities;
using GA.BDC.Shared.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace GA.BDC.WebApi.EzFund.Controllers
{
    public class LeadsController : ApiController
    {

        [HttpOptions]
        public IHttpActionResult Options()
        {
            return Ok();
        }

        [HttpPost]
        public IHttpActionResult Post(Lead model)
        {
            model.Phone = PhoneHelper.Clean(model.Phone);

            using (var eZFundProdUnitOfWork = new UnitOfWork(Database.EZMain))
            {
                var leadRepository = eZFundProdUnitOfWork.CreateRepository<ILeadRepository>();
                var leadId =  leadRepository.Save(model);
                model.Id = leadId;
                eZFundProdUnitOfWork.Commit();
            }
            return Ok(model);
        }


        [HttpPut]
        public IHttpActionResult Put(Lead model)
        {
            try
            {
                using (var eZFundProdUnitOfWork = new UnitOfWork(Database.EZMain))
                {
                    var leadRepository = eZFundProdUnitOfWork.CreateRepository<ILeadRepository>();

                    leadRepository.Update(model);
                    model = leadRepository.GetById(model.Id);
                    eZFundProdUnitOfWork.Commit();

                    return Ok(model);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


    }
}
