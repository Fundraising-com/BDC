using System.Web.Http;

namespace GA.BDC.WebApi.Fundraising.Controllers
{
   public class MergedShoppingCartsController : ApiController
   {

      [HttpOptions]
      public IHttpActionResult Options()
      {
         return Ok();
      }
   }
}
