using System;
using System.Linq;
using System.Web.Mvc;
using System.Web.Mvc.Filters;
using GA.BDC.Data.MGP.esubs_global_v2.Models;
using GA.BDC.Web.MGP.Models.Branding;
using WebMatrix.WebData;

namespace GA.BDC.Web.MGP.Helpers.Routes.Attributes
{
    // ReSharper disable once InconsistentNaming
    public class MGPAuthenticationAttribute : ActionFilterAttribute, IAuthenticationFilter
    {
        public void OnAuthentication(AuthenticationContext filterContext)
        {
            if (!WebSecurity.IsAuthenticated)
            {
                filterContext.Result = new HttpUnauthorizedResult("User doesn't have access to the page");
                return;
            }
            var email = WebSecurity.CurrentUserName.Split('|')[0];
            var password = filterContext.HttpContext.Session[Constants.SESSION_KEY_PASSWORD] != null 
                           ? filterContext.HttpContext.Session[Constants.SESSION_KEY_PASSWORD].ToString() 
                           : null;
            var partnerId = Convert.ToInt32(filterContext.HttpContext.Session[Constants.SESSION_KEY_PARTNER_ID]);
            using (var dataProvider = new DataProvider())
            {
                var user = (from u in dataProvider.users
                            where u.email_address == email && u.partner_id == partnerId && (password == null || u.password == password)
                            select new User { Id = u.user_id, FirstName = u.first_name, LastName = u.last_name, Email = u.email_address, Password = u.password, IsLoggedIn = true }).FirstOrDefault();
                if (user == null)
                {
                    user = (from u in dataProvider.users
                                where u.email_address == email
                                select new User { Id = u.user_id, FirstName = u.first_name, LastName = u.last_name, Email = u.email_address, Password = u.password, IsLoggedIn = true }).FirstOrDefault();
                    if (user == null)
                    {
                        filterContext.Result =
                            new HttpUnauthorizedResult(
                                string.Format("User not found. Email Address: {0}. Partner Id: {1}.", email, partnerId));
                    }
                    else
                    {
                        filterContext.Controller.ViewBag.User = user;
                    }
                }
                else
                {
                    filterContext.Controller.ViewBag.User = user;
                }
            }
        }

        public void OnAuthenticationChallenge(AuthenticationChallengeContext filterContext)
        {

        }
    }
}