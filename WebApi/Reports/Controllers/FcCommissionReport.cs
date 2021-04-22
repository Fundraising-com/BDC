using System.Web.Http;
using GA.BDC.Data.Fundraising.Helpers;
using GA.BDC.Shared.Data;
using GA.BDC.Shared.Data.Repositories;
using GA.BDC.Shared.Helpers;
using GA.BDC.WebApi.Reports.ViewModels;

namespace GA.BDC.WebApi.Reports.Controllers
{
   [Authorize]
   public class FcCommissionReportController : ApiController
   {
      [HttpOptions]
      public IHttpActionResult Options()
      {
         return Ok();
      }
      [HttpPost]
      public IHttpActionResult Post(FcCommissionViewModel viewModel)
      {
         using (var efundraisingProdUnitOfWork = new UnitOfWork(Database.EFundraisingProd))
         {
            var reportsFactory = efundraisingProdUnitOfWork.CreateRepository<IReportsRepository>();
            var result = reportsFactory.ExecuteReportGetFcCommissionReport(DateHelper.PrepareStartDate(viewModel.start_date), 
                DateHelper.PrepareEndDate(viewModel.end_date), viewModel.Consultant?.Id);
            return Ok(result);
         }
      }

      
   }   
}
