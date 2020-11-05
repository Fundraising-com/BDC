using System.Net.Http;
using System.Web.Http;
using Microsoft.Owin.Security;


namespace GA.BDC.WebApi.Security.Controllers
{
   public class AccountController : ApiController
   {
      private IAuthenticationManager Authentication
      {
         get { return Request.GetOwinContext().Authentication; }
      }
   }
}
