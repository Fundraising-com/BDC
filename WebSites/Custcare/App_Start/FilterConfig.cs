using System.Web;
using System.Web.Mvc;
using GA.BDC.Web.Custcare.Filters;

namespace GA.BDC.Web.Custcare
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new ExceptionFilter());
            filters.Add(new HandleErrorAttribute());
        }
    }
}