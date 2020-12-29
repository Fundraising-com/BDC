using FluentValidation;
using GA.BDC.Shared.Entities;

namespace GA.BDC.Shared.Validators
{
    public class SaleItemValidator : AbstractValidator<SaleItem>
    {
        public SaleItemValidator()
        {
            RuleFor(p => p.SaleId).NotNull();
            RuleFor(p => p.Quantity).NotNull().GreaterThan(0);
            RuleFor(p => p.ScratchBookId).NotNull().GreaterThan(0);
            RuleFor(p => p.Product).NotNull().SetValidator(new ProductValidator());
        }
    }
}
