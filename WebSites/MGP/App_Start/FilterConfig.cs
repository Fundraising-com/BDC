using System.Web.Mvc;
using GA.BDC.Web.MGP.Helpers.Routes.Filters;

namespace GA.BDC.Web.MGP
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new CanadaFilter());
            //filters.Add(new ExceptionFilter());
            filters.Add(new UnsupportedBrowserFilter());
            filters.Add(new PartnerFilter());
            filters.Add(new LeadFilter());
            filters.Add(new EnvironmentFilter());
            filters.Add(new InitializeAuthenticationFilter());
            filters.Add(new HandleErrorAttribute());
            filters.Add(new CustomRequireHttpsAttribute());
        }
    }
}