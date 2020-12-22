using System;
using System.Linq;
using System.Web.Mvc;
using GA.BDC.Web.MGP.Models.Branding;
using GA.BDC.Data.MGP.esubs_global_v2.Models;

namespace GA.BDC.Web.MGP.Helpers.Routes.Attributes
{
    public class RenderTouchBranding : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.HttpContext.Session == null) throw new Exception("Session is null");

            var touchId = filterContext.HttpContext.Session[Constants.SESSION_KEY_TOUCH_ID] != null
                            ? int.Parse(filterContext.HttpContext.Session[Constants.SESSION_KEY_TOUCH_ID].ToString())
                            : 0;
            if (touchId > 0)
            {
                using (var dataProvider = new DataProvider())
                {
                    var branding = (from m in dataProvider.members
                        from mh in dataProvider.member_hierarchy
                        from ep in dataProvider.event_participation
                        from t in dataProvider.touches
                        where mh.member_id == m.member_id
                              && mh.member_hierarchy_id == ep.member_hierarchy_id
                              && ep.event_participation_id == t.event_participation_id
                              && t.touch_id == touchId
                        select new TouchInvitation
                        {
                            Email = m.email_address,
                            EventParticipationId =
                                t.event_participation_id.HasValue ? t.event_participation_id.Value : 0,
                            FirstName = m.first_name,
                            LastName = m.last_name,
                            TouchId = t.touch_id,
                            TouchInfoId = t.touch_info_id
                        }).SingleOrDefault();
                    filterContext.Controller.ViewBag.TouchInvitation = branding ?? new TouchInvitation();
                }
            }
            else
            {
                filterContext.Controller.ViewBag.TouchInvitation = new TouchInvitation();
            }
        }
    }
}