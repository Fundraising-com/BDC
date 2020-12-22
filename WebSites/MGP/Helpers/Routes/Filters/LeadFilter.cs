using System.Linq;
using System.Web.Mvc;
using efundraisingProd = GA.BDC.Data.MGP.EfundraisingProd.Models;

namespace GA.BDC.Web.MGP.Helpers.Routes.Filters
{
    public class LeadFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.Controller.ValueProvider.GetValue(Constants.QUERY_PARAMETER_LEADID) == null)
            {
                return;
            }
	        int leadId;
	        if (!int.TryParse(filterContext.Controller.ValueProvider.GetValue(Constants.QUERY_PARAMETER_LEADID).AttemptedValue, out leadId)) return;
	        using (var efundraisingProdDataProvider = new efundraisingProd.DataProvider())
	        {
		        leadId = (from l in efundraisingProdDataProvider.leads
			        where l.lead_id == leadId
			        select l.lead_id).SingleOrDefault();
	        }
	        if (leadId > 0)
		        filterContext.HttpContext.Session[Constants.SESSION_KEY_LEAD_ID] = leadId;
        }
    }
}