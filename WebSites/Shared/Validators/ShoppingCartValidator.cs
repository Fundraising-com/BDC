using FluentValidation;
using GA.BDC.Shared.Entities;

namespace GA.BDC.Shared.Validators
{
    public class ShoppingCartValidator : AbstractValidator<ShoppingCart>
    {
        public ShoppingCartValidator()
        {
            RuleFor(p => p.Status).NotNull();
            RuleFor(p => p.Comments).Length(0, 400);
        }
    }
}
