using FluentValidation;
using GA.BDC.Shared.Entities;

namespace GA.BDC.Shared.Validators
{
    public class ShoppingCartItemValidator : AbstractValidator<ShoppingCartItem>
    {
        public ShoppingCartItemValidator()
        {
            RuleFor(p => p.Quantity).NotNull();
            RuleFor(p => p.Comments).Length(0, 200);
            RuleFor(p => p.ProductId).NotNull().GreaterThan(0);
        }
    }
}
