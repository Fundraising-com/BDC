using FluentValidation;
using GA.BDC.Shared.Entities;

namespace GA.BDC.Shared.Validators
{
   public class OAuthClientValidator : AbstractValidator<OAuthClient>
   {
      public OAuthClientValidator()
      {
         RuleFor(p => p.Id).NotNull();
         RuleFor(p => p.Secret).NotNull();
         RuleFor(p => p.Name).NotNull().Length(1, 100);
         RuleFor(p => p.Name).Length(0, 100);
      }
   }
}
