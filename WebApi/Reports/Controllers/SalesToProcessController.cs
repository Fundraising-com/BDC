using System.Web.Http;
using GA.BDC.Data.Fundraising.Helpers;
using GA.BDC.Shared.Data;
using GA.BDC.Shared.Data.Repositories;
using GA.BDC.Shared.Helpers;
using GA.BDC.WebApi.Reports.ViewModels;

namespace GA.BDC.WebApi.Reports.Controllers
{
   [Authorize]
   public class SalesToProcessController : ApiController
   {
      [HttpOptions]
      public IHttpActionResult Options()
      {
         return Ok();
      }
      [HttpPost]
      public IHttpActionResult Post(SalesToProcessViewModel viewModel)
      {
         using (var efundraisingProdUnitOfWork = new UnitOfWork(Database.EFundraisingProd))
         {
            var reportsFactory = efundraisingProdUnitOfWork.CreateRepository<IReportsRepository>();
            var result = reportsFactory.ExecuteReportSalesToProcess(DateHelper.PrepareStartDate(viewModel.SalesConfirmStart), 
                DateHelper.PrepareEndDate(viewModel.SalesConfirmEnd));
            return Ok(result);
         }
      }

      
   }   
}
