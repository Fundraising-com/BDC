using System;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Routing;
using GA.BDC.Data.MGP.esubs_global_v2.LINQ;
using GA.BDC.Web.MGP.Helpers.Extensions;

namespace GA.BDC.Web.MGP.Helpers.Routes.Constraints
{
    public class ParticipantConstraint : IRouteConstraint
    {
        public bool Match(HttpContextBase httpContext, Route route, string parameterName, RouteValueDictionary values,
            RouteDirection routeDirection)
        {
            if (values["event"] == null ||                
                values["participant"] == null ||
                values["event"].ToString().IsDefinedController() ||
                values.Keys.Count() > 4) 
                return false;
            using (var dataProvider = new DataProviderDataContext(ConfigurationManager.ConnectionStrings["esubs_global_v2"].ConnectionString))
            {
                var sponsorRedirect = values["event"].ToString();
                var participantRedirect = values["participant"].ToString();
                var participants = dataProvider.es_get_participant_constraint(sponsorRedirect, participantRedirect).ToList();                
                if (participants.Any())
                {
                    var participant = participants.First();
                    values.Remove("eventId");
                    values.Remove("participantId");
                    values.Add("eventId", participant.event_id);
                    values.Add("participantId", participant.event_participation_id);
                    return true;
                }
            }
            return false;
        }
    }
}