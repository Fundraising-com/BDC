using FluentValidation;
using GA.BDC.Shared.Entities;

namespace GA.BDC.Shared.Validators
{
    public class PaymentValidator : AbstractValidator<Payment>
    {
        public PaymentValidator()
        {
            RuleFor(p => p.SaleId).NotNull();
            RuleFor(p => p.Number).NotNull().GreaterThan(0);
            RuleFor(p => p.InternalPaymentMethod).NotNull();
            RuleFor(p => p.CreditCard)
                .NotNull()
                .SetValidator(new CreditCardValidator())
                .When(
                    p =>
                        (p.InternalPaymentMethod == InternalPaymentMethod.AMEX ||
                         p.InternalPaymentMethod == InternalPaymentMethod.DISCOVER ||
                         p.InternalPaymentMethod == InternalPaymentMethod.MASTERCARD ||
                         p.InternalPaymentMethod == InternalPaymentMethod.VISA));
            RuleFor(p => p.AuthorizationNumber).Length(6,10);
            RuleFor(p => p.Amount).NotNull().WithMessage("Payment Amount");
            RuleFor(p => p.IsComissionPaid).NotNull();
        }
    }
}
