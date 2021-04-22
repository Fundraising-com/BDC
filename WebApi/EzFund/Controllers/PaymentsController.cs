using System.Web.Http;
//using GA.BDC.Data.Fundraising.Helpers;
using GA.BDC.Shared.Data;
using GA.BDC.Shared.Data.Repositories;
using GA.BDC.Shared.Entities;
using GA.BDC.Data.EzFund.Helpers;

namespace GA.BDC.WebApi.EzFund.Controllers
{
    public class PaymentsController : ApiController
    {
      [HttpOptions]
      public IHttpActionResult Options()
      {
         return Ok();
      }

      [HttpPost]
        public IHttpActionResult Post(ArTrnsTbl payment)
        {
           using (var EZMainUnitOfWork = new UnitOfWork(Database.EZMain))
           {
              var paymentsRepository = EZMainUnitOfWork.CreateRepository<IArTrnsTblRepository>();
              paymentsRepository.InsertPayment(payment);
                EZMainUnitOfWork.Commit();
                return Ok();
            }
        }
    }
}
