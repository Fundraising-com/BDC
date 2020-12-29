using FluentValidation;
using GA.BDC.Shared.Entities;

namespace GA.BDC.Shared.Validators
{
   public class AppliedTaxValidator : AbstractValidator<AppliedTax>
   {
      public AppliedTaxValidator()
      {
         RuleFor(p => p.TaxCode).NotNull();
         RuleFor(p => p.Amount).GreaterThan(0);
      }
   }
}
