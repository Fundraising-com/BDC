using FluentValidation;
using GA.BDC.Shared.Entities;

namespace GA.BDC.Shared.Validators
{
   public class OAuthTokenValidator : AbstractValidator<OAuthToken>
   {
      public OAuthTokenValidator()
      {
         RuleFor(p => p.Id).NotNull();
         RuleFor(p => p.ProtectedTicket).NotNull();
         RuleFor(p => p.Subject).NotNull().Length(1, 50);
         RuleFor(p => p.ClientId).NotNull().Length(1, 50);
      }
   }
}
