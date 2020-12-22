using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Routing;
using GA.BDC.Data.MGP.esubs_global_v2.LINQ;
using GA.BDC.Web.MGP.Helpers.Extensions;

namespace GA.BDC.Web.MGP.Helpers.Routes.Constraints
{
    public class GroupConstraint : IRouteConstraint
    {
        public bool Match(HttpContextBase httpContext, Route route, string parameterName, RouteValueDictionary values,
            RouteDirection routeDirection)
        {
            if (values["event"] == null ||
                values["event"].ToString().IsDefinedController() ||
                values["event"].ToString().ToLower() == "event" ||
                values["event"].ToString().IsAngularJSDirective())
                return false;
            using (var dataProvider = new DataProviderDataContext(ConfigurationManager.ConnectionStrings["esubs_global_v2"].ConnectionString))
            {
                var redirect = values["event"].ToString();
                var events = dataProvider.es_get_group_constraint(redirect).ToList();
                if (events.Any())
                {
                    var @event = events.First();
                    values.Remove("eventId");
                    values.Add("eventId", @event.event_id);
                    return true;
                }
            }
            return false;
        }
    }
}