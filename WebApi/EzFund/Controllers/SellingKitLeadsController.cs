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
    public class SellingKitLeadsController : ApiController
    {

        [HttpOptions]
        public IHttpActionResult Options()
        {
            return Ok();
        }

        [HttpPost]
        public IHttpActionResult Post(SellingKitLead model)
        {
            model.Phone1 = PhoneHelper.Clean(model.Phone1);
            var primecode = GetPrimaryProgramCode(model.PrimaryProgramCode);
            model.PrimaryProgramCode = primecode.pgmcode;

            using (var eZFundProdUnitOfWork = new UnitOfWork(Database.EZMain))
            {
                var sellingkitleadsRepository = eZFundProdUnitOfWork.CreateRepository<ISellingKitLeadRepository>();
                var sellingkitId = sellingkitleadsRepository.Save(model);
                model.Id = sellingkitId;
                eZFundProdUnitOfWork.Commit();
            }
            return Ok(model);
        }

        SitePgmTbl GetPrimaryProgramCode(string ppdesc)
        {
           

            using (var eZFundProdUnitOfWork = new UnitOfWork(Database.EZMain))
            {
                var primaryprogramRepository = eZFundProdUnitOfWork.CreateRepository<IPrimaryProgramRepository>();
                var ppcode = primaryprogramRepository.GetPrimaryProgramCode(ppdesc);
                return ppcode;

            }
            
        }




    }
}
