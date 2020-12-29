using FluentValidation;
using GA.BDC.Shared.Entities;

namespace GA.BDC.Shared.Validators
{
    public class CreditCardValidator : AbstractValidator<CreditCard>
    {
        public CreditCardValidator()
        {
            RuleFor(p => p.Number).NotNull().CreditCard().WithMessage("Credit Card Number");
            RuleFor(p => p.ExpirationDate).NotNull().Length(4).WithMessage("Credit Card Expiration Date (MMYY)");
            RuleFor(p => p.CVV).Length(3, 4);
            RuleFor(p => p.Holder).NotNull();
            RuleFor(p => p.Address).NotNull().SetValidator(new AddressValidator()).WithMessage("Credit Card's Address");
            RuleFor(p => p.Amount).NotNull().WithMessage("Credit Card Total Amount");
            RuleFor(p => p.InternalPaymentMethod).NotNull();
        }
    }
}
