using System.Web.Http;
using GA.BDC.Data.Fundraising.Helpers;
using GA.BDC.Shared.Data;
using GA.BDC.Shared.Data.Repositories;
using GA.BDC.Shared.Entities;

namespace GA.BDC.WebApi.Fundraising.Core.Controllers
{
    public class PaymentsController : ApiController
    {
      [HttpOptions]
      public IHttpActionResult Options()
      {
         return Ok();
      }

      [HttpPost]
        public IHttpActionResult Post(Payment payment)
        {
           using (var efundraisingProdUnitOfWOrk = new UnitOfWork(Database.EFundraisingProd))
           {
              var paymentsRepository = efundraisingProdUnitOfWOrk.CreateRepository<IPaymentsRepository>();
              var saleId = paymentsRepository.Save(payment);
              efundraisingProdUnitOfWOrk.Commit();
              return Ok(saleId);  
           }
        }
    }
}
