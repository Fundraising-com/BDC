using System.Linq;
using System.Text;
using System.Web.Mvc;
using Ninject;
using Ninject.Extensions.Logging;

namespace GA.BDC.Web.Scratchcard.MVC.Helpers.Routes.Filters
{
    public class ExceptionHandlerAttribute : IExceptionFilter
    {
        [Inject]
        public ILogger Logger { get; set; }

        public void OnException(ExceptionContext filterContext)
        {
                
        }
        
    }
}