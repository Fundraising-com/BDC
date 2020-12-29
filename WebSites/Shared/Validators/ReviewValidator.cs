using FluentValidation;
using GA.BDC.Shared.Entities;

namespace GA.BDC.Shared.Validators
{
   public class ReviewValidator : AbstractValidator<Review>
   {
      public ReviewValidator()
      {
         RuleFor(p => p.Name).NotEmpty().Length(1, 30);
         RuleFor(p => p.Comments).NotEmpty().Length(1, 500);
         RuleFor(p => p.Rating).NotEmpty().InclusiveBetween(1, 5);
         RuleFor(p => p.ProductId).NotEmpty();
      }
   }
}
