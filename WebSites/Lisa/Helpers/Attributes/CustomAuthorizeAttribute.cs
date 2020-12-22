using GA.BDC.Shared.Helpers;
using System;
using System.Configuration;
using System.Security.Claims;
using System.Web;
using System.Web.ClientServices;
using System.Web.Mvc;

namespace GA.BDC.Web.Lisa.Helpers.Attributes
{
    public class CustomAuthorizeAttribute : AuthorizeAttribute
    {
        const string ACCESS_TOKEN_KEY = "access_token";
        const string REFRESH_TOKEN_KEY = "refresh_token";
        const string EXPIRES_IN_KEY = "expires_in";

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            var isAuthorized = false;
            var cookieToken = httpContext.Request.Cookies["lisa.auth"];
            if (cookieToken == null)
            {
                return false;
            }
            var tokenHelper = new TokenHelper(ConfigurationManager.AppSettings["security.webapi.host"], "lisa", "/token");
            var tokenValues = tokenHelper.ParseToken(cookieToken.Value);
            var accessToken = tokenValues[ACCESS_TOKEN_KEY];
            var unprotectedToken = Startup.OAuthBearerOptions.AccessTokenFormat.Unprotect(accessToken);
            if (unprotectedToken.Properties.ExpiresUtc < DateTime.Now.ToUniversalTime())
            {
                return false;
            }
            var identity = unprotectedToken.Identity;

            var principal = new ClientRolePrincipal(identity);
            if (!string.IsNullOrEmpty(Roles))
            {
                var roles = Roles.Split(',');
                foreach (var role in roles)
                {
                    isAuthorized = isAuthorized || (principal.Identity as ClaimsIdentity).HasClaim(ClaimTypes.Role, role);
                }
            }
            else
            {
                isAuthorized = true;
            }

            if (isAuthorized)
            {
                httpContext.User = principal;
            }
            return isAuthorized;
        }

        protected override void HandleUnauthorizedRequest(System.Web.Mvc.AuthorizationContext filterContext)
        {
            filterContext.Result = new RedirectResult($"/login?redirectUrl={filterContext.HttpContext.Request.Path}");
        }
    }
}