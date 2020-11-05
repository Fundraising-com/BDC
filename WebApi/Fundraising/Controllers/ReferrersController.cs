using System.Web.Http;

namespace GA.BDC.WebApi.Fundraising.Controllers
{
    public class ReferrersController : ApiController
    {
      [HttpOptions]
      public IHttpActionResult Options()
      {
         return Ok();
      }
   }
}
