using System.Web.Mvc;
using GA.BDC.Web.Fundraising.MVC.Helpers.Routes.Filters;

namespace GA.BDC.Web.SportsApparel.MVC
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new RequireWWWAttribute());
            filters.Add(new ExceptionHandlerAttribute());
            filters.Add(new CustomRequireHttpsAttribute());
        }
    }
}
