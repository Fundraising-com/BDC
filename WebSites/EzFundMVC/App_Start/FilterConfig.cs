using System.Web.Mvc;
using GA.BDC.Web.EzFundMVC.Helpers.Routes.Filters;

namespace GA.BDC.Web.EzFundMVC
{
   public class FilterConfig
   {
      public static void RegisterGlobalFilters(GlobalFilterCollection filters)
      {
         filters.Add(new HandleErrorAttribute());
         filters.Add(new RequireWWWAttribute());
         filters.Add(new CustomRequireHttpsAttribute());
        }
   }
}
