using System.Web.Http;
using GA.BDC.Data.Fundraising.Helpers;
using GA.BDC.Shared.Data;
using GA.BDC.Shared.Data.Repositories;
using GA.BDC.Shared.Helpers;
using GA.BDC.WebApi.Reports.ViewModels;

namespace GA.BDC.WebApi.Reports.Controllers
{
   [Authorize]
   public class SpiderController : ApiController
   {
      [HttpOptions]
      public IHttpActionResult Options()
      {
         return Ok();
      }
      [HttpPost]
      public IHttpActionResult Post(SpiderViewModel viewModel)
      {
         using (var efundraisingProdUnitOfWork = new UnitOfWork(Database.EFundraisingProd))
         {
            var reportsFactory = efundraisingProdUnitOfWork.CreateRepository<IReportsRepository>();
            var result = reportsFactory.ExecuteSpider(viewModel.SaleId, viewModel.LeadId, viewModel.PromotionId,
               viewModel.ScratchbookId, viewModel.PartnerId, viewModel.Region?.Code, viewModel.Consultant?.Id, viewModel.ZipCode, viewModel.Country?.Code,
               viewModel.DayPhone, viewModel.EveningPhone, viewModel.Email, viewModel.OrganizationType?.Id, viewModel.TotalAmount, viewModel.ProductClass?.Id,
               DateHelper.PrepareStartDate(viewModel.ShipDateStart), DateHelper.PrepareEndDate(viewModel.ShipDateEnd), DateHelper.PrepareStartDate(viewModel.FundraiserStart), DateHelper.PrepareEndDate(viewModel.FundraiserEnd),
               DateHelper.PrepareStartDate(viewModel.SalesConfirmStart), DateHelper.PrepareEndDate(viewModel.SalesConfirmEnd), DateHelper.PrepareStartDate(viewModel.LeadsEntryStart),
               DateHelper.PrepareEndDate(viewModel.LeadsEntryEnd), viewModel.GroupType?.Id);
            return Ok(result);
         }
      }

      
   }   
}
