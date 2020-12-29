using FluentValidation;
using GA.BDC.Shared.Entities;

namespace GA.BDC.Shared.Validators
{
    public class SaleValidator : AbstractValidator<Sale>
    {
        public SaleValidator()
        {
            RuleFor(p => p.Id).NotNull();
            RuleFor(p => p.PaymentMethod).NotNull();
            RuleFor(p => p.InternalPaymentMethod).NotNull();
            RuleFor(p => p.Client).SetValidator(new ClientValidator());
            RuleFor(p => p.CreditCard)
                .SetValidator(new FluentValidation.Validators.CreditCardValidator())
                .When(p => p.PaymentMethod == PaymentMethod.CreditCard);
            RuleFor(p => p.Items).SetCollectionValidator(new SaleItemValidator());
            RuleFor(p => p.Status).NotNull();
            RuleFor(p => p.ARStatus).NotNull();
        }
    }
}
