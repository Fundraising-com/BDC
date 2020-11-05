using System;
using System.Threading.Tasks;
using GA.BDC.Data.Fundraising.Helpers;
using GA.BDC.Shared.Data;
using GA.BDC.Shared.Data.Repositories;
using GA.BDC.Shared.Entities;
using GA.BDC.Shared.Helpers;
using Microsoft.Owin.Security.Infrastructure;
#pragma warning disable 1998

namespace GA.BDC.WebApi.Security.Providers
{
   public class ApplicationRefreshTokenProvider : IAuthenticationTokenProvider
   {

      public async Task CreateAsync(AuthenticationTokenCreateContext context)
      {
         Create(context);
      }

      public async Task ReceiveAsync(AuthenticationTokenReceiveContext context)
      {
         Receive(context);
      }

      public void Create(AuthenticationTokenCreateContext context)
      {
         var clientid = context.Ticket.Properties.Dictionary["as:client_id"];

         if (string.IsNullOrEmpty(clientid))
         {
            return;
         }

         var refreshTokenId = Guid.NewGuid().ToString("n");

         using (var eFundraisingProdUnitOfWork = new UnitOfWork(Database.EFRCommon))
         {
            var tokenRepository = eFundraisingProdUnitOfWork.CreateRepository<IOAuthTokenRepository>();
            var refreshTokenLifeTime = context.OwinContext.Get<string>("as:clientRefreshTokenLifeTime");

                var token = new OAuthToken()
                {
                    Id = HashHelper.GetHash(refreshTokenId),
                    ClientId = clientid,
                    Subject = context.Ticket.Identity.Name,
                    IssuedUtc = DateTime.UtcNow,
                    ExpiresUtc = DateTime.UtcNow.AddMinutes(Convert.ToDouble(refreshTokenLifeTime))
            };

            context.Ticket.Properties.IssuedUtc = token.IssuedUtc;
            context.Ticket.Properties.ExpiresUtc = token.ExpiresUtc;

            token.ProtectedTicket = context.SerializeTicket();

            var result = tokenRepository.Save(token);
            eFundraisingProdUnitOfWork.Commit();
            if (result)
            {
               context.SetToken(refreshTokenId);
            }
         }
      }

      public void Receive(AuthenticationTokenReceiveContext context)
      {
         //var allowedOrigin = context.OwinContext.Get<string>("as:clientAllowedOrigin");
         //context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { allowedOrigin });

         var hashedTokenId = HashHelper.GetHash(context.Token);

         using (var eFundraisingProdUnitOfWork = new UnitOfWork(Database.EFRCommon))
         {
            var tokenRepository = eFundraisingProdUnitOfWork.CreateRepository<IOAuthTokenRepository>();
            var refreshToken = tokenRepository.GetById(hashedTokenId);

            if (refreshToken != null)
            {
               //Get protectedTicket from refreshToken class
               context.DeserializeTicket(refreshToken.ProtectedTicket);
               tokenRepository.Delete(refreshToken);
               eFundraisingProdUnitOfWork.Commit();
            }
         }
      }
   }
}