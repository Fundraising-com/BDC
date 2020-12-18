using System.Web.Mvc;
using GA.BDC.Web.Lisa.Filters;
using GA.BDC.Web.Lisa.Helpers.Attributes;

namespace GA.BDC.Web.Lisa
{
   public class FilterConfig
   {
      public static void RegisterGlobalFilters(GlobalFilterCollection filters)
      {
         filters.Add(new ExceptionHandlerAttribute());
         filters.Add(new TopMenuItemAttribute());
      }
   }
}
