using System;
using System.Collections.Generic;
using System.DirectoryServices.AccountManagement;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Security;
using GA.BDC.Data.Fundraising.Helpers;
using GA.BDC.Shared.Data;
using GA.BDC.Shared.Data.Repositories;
using GA.BDC.Shared.Entities;
using GA.BDC.Shared.Helpers;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;
using SWCorporate.SystemEx;
using SWCorporate.SystemEx.Web;
#pragma warning disable 1998

namespace GA.BDC.WebApi.Security.Providers
{
   public class ApplicationOAuthProvider : OAuthAuthorizationServerProvider
   {
      public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
      {
         try
         {
            string clientId;
            string clientSecret;

            if (!context.TryGetBasicCredentials(out clientId, out clientSecret))
            {
               context.TryGetFormCredentials(out clientId, out clientSecret);
            }

            if (context.ClientId == null)
            {
               context.Validated();
               return Task.FromResult<object>(null);
            }
            using (var eFundraisingProdUnitOfWork = new UnitOfWork(Database.EFRCommon))
            {
               var oAuthClientRepository = eFundraisingProdUnitOfWork.CreateRepository<IOAuthClientRepository>();
               var oAuthClient = oAuthClientRepository.GetById(context.ClientId);

               if (oAuthClient == null)
               {
                  context.SetError("invalid_clientId", $"Client '{context.ClientId}' is not registered in the system.");
                  return Task.FromResult<object>(null);
               }

               if (oAuthClient.ApplicationType == OAuthClientApplicationType.NativeConfidential)
               {
                  if (string.IsNullOrWhiteSpace(clientSecret))
                  {
                     context.SetError("invalid_clientId", "Client secret should be sent.");
                     return Task.FromResult<object>(null);
                  }
                  if (oAuthClient.Secret != HashHelper.GetHash(clientSecret))
                  {
                     context.SetError("invalid_clientId", "Client secret is invalid.");
                     return Task.FromResult<object>(null);
                  }
               }

               if (!oAuthClient.Active)
               {
                  context.SetError("invalid_clientId", "Client is inactive.");
                  return Task.FromResult<object>(null);
               }

               context.OwinContext.Set("as:clientAllowedOrigin", oAuthClient.AllowedOrigin);
               context.OwinContext.Set("as:clientRefreshTokenLifeTime", oAuthClient.RefreshTokenLifeTime.ToString());
               context.Options.AccessTokenExpireTimeSpan = TimeSpan.FromMinutes(oAuthClient.RefreshTokenLifeTime);
               context.Validated();
            }
         }
         catch (Exception exception)
         {
            var httpContext = context.Request.Context.Get<HttpContextBase>(typeof(HttpContextBase).FullName);
            var instrumentationContext = httpContext ?? new HttpContextWrapper(HttpContext.Current);
            instrumentationContext.SendExceptionNotification(InstrumentationProvider.Current, exception);
            context.SetError("server_error", "An error has ocurred while trying to login.");
         }
         return Task.FromResult<object>(null);
      }

      public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
      {
         try
         {
            ClaimsIdentity identity = null;
            var props = new Dictionary<string, string>();

            if (string.Equals(context.ClientId, "lisa", StringComparison.InvariantCultureIgnoreCase))
            {
               if (Membership.ValidateUser(context.UserName, context.Password))
               {
                  using (var principalContext = new PrincipalContext(ContextType.Domain, "SNA"))
                  {
                     var user = UserPrincipal.FindByIdentity(principalContext, context.UserName);
                     if (user != null)
                     {
                        identity = new ClaimsIdentity(context.Options.AuthenticationType);
                        identity.AddClaim(new Claim(ClaimTypes.Name, context.UserName));
                        identity.AddClaim(new Claim("displayName", user.DisplayName));
                        props.Add("as:client_id", context.ClientId);
                        var groups = user.GetAuthorizationGroups();
                        var roleClaims = GetUserClaims(groups);
                        identity.AddClaims(roleClaims);
                     }
                     else
                     {
                        context.SetError("invalid_grant", "Incorrect Username or Password");
                        return;
                     }
                  }
               }
               else
               {
                  context.SetError("invalid_grant", "Incorrect Username or Password");
                  return;
               }
            }
            if (string.Equals(context.ClientId, "efundraising", StringComparison.InvariantCultureIgnoreCase))
            {
               throw new NotImplementedException("Client Efundraising is not implemented");
            }

            var ticket = new AuthenticationTicket(identity, new AuthenticationProperties(props));
            context.Validated(ticket);
         }
         catch (Exception exception)
         {
            var httpContext = context.Request.Context.Get<HttpContextBase>(typeof(HttpContextBase).FullName);
            var instrumentationContext = httpContext ?? new HttpContextWrapper(HttpContext.Current);
            instrumentationContext.SendExceptionNotification(InstrumentationProvider.Current, exception);
            context.SetError("server_error", "An error has ocurred while trying to login.");
         }

      }


      private IEnumerable<Claim> GetUserClaims(PrincipalSearchResult<Principal> groups)
      {
         var result = new List<Claim>();
         var iterGroup = groups.GetEnumerator();
         using (iterGroup)
         {
            while (iterGroup.MoveNext())
            {
               try
               {
                  Principal p = iterGroup.Current;
                  result.Add(new Claim(ClaimTypes.Role, p.Name));
               }
               catch (NoMatchingPrincipalException)
               {
               }
            }
         }
         return result;
      }

      public override Task GrantRefreshToken(OAuthGrantRefreshTokenContext context)
      {
         try
         {
            var originalClient = context.Ticket.Properties.Dictionary["as:client_id"];
            var currentClient = context.ClientId;
            ClaimsIdentity identity = null;
            var props = new Dictionary<string, string>();

            if (originalClient != currentClient)
            {
                context.SetError("invalid_clientId", "Refresh token is issued to a different clientId.");
                return Task.FromResult<object>(null);
            }

            if (string.Equals(context.ClientId, "lisa", StringComparison.InvariantCultureIgnoreCase))
            {
                using (var principalContext = new PrincipalContext(ContextType.Domain, "SNA"))
                {
                    var user = UserPrincipal.FindByIdentity(principalContext, context.Ticket.Identity.Name);
                    if (user != null)
                    {
                        identity = new ClaimsIdentity(context.Options.AuthenticationType);
                        identity.AddClaim(new Claim(ClaimTypes.Name, context.Ticket.Identity.Name));
                        identity.AddClaim(new Claim("displayName", user.DisplayName));
                        props.Add("as:client_id", context.ClientId);
                        var groups = user.GetAuthorizationGroups();
                        var roleClaims = GetUserClaims(groups);
                        identity.AddClaims(roleClaims);
                    }
                    else
                    {
                        context.SetError("invalid_grant", "Incorrect Username or Password");
                        return Task.FromResult<object>(null);
                    }
                }
            }
            var newTicket = new AuthenticationTicket(identity, new AuthenticationProperties(props));
            context.Validated(newTicket);
         }
         catch (Exception exception)
         {
            var httpContext = context.Request.Context.Get<HttpContextBase>(typeof(HttpContextBase).FullName);
            var instrumentationContext = httpContext ?? new HttpContextWrapper(HttpContext.Current);
            instrumentationContext.SendExceptionNotification(InstrumentationProvider.Current, exception);
            context.SetError("server_error", "An error has ocurred while refreshing the token.");
         }
         return Task.FromResult<object>(null);
        }

        public override Task TokenEndpoint(OAuthTokenEndpointContext context)
        {
            foreach (var property in context.Properties.Dictionary)
            {
                context.AdditionalResponseParameters.Add(property.Key, property.Value);
            }
            return Task.FromResult<object>(null);
        }

        public override Task MatchEndpoint(OAuthMatchEndpointContext context)
        {
            if (context.IsTokenEndpoint && context.Request.Method == "OPTIONS")
            {
                //context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { "*" });
                context.OwinContext.Response.Headers.Add("Access-Control-Allow-Headers", new[] { "authorization" });
                context.RequestCompleted();
                return Task.FromResult(0);
            }
            return base.MatchEndpoint(context);
        }
    }
}