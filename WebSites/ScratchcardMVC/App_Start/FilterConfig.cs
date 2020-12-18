using System.Web;
using System.Web.Mvc;

namespace GA.BDC.Web.Scratchcard.MVC
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
