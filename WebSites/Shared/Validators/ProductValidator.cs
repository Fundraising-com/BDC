using FluentValidation;
using GA.BDC.Shared.Entities;

namespace GA.BDC.Shared.Validators
{
    public class ProductValidator : AbstractValidator<Product>
    {
        public ProductValidator() { }
    }
}
