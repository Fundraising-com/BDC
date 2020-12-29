using FluentValidation;
using GA.BDC.Shared.Entities;

namespace GA.BDC.Shared.Validators
{
    public class CategoryValidator :AbstractValidator<Category>
    {
        public CategoryValidator()
        {
            RuleFor(p => p.Id).NotNull();
            RuleFor(p => p.Name).NotNull().Length(1, 100);
            RuleFor(p => p.Order).NotNull();
            RuleFor(p => p.Url).NotNull().Length(1, 100);
            RuleFor(p => p.Description).Length(0, 1500);
            RuleFor(p => p.Description2).Length(0, 4000);
            RuleFor(p => p.LinkGroupKey).Length(0, 200);
            RuleFor(p => p.METADescription).Length(0, 200);
            RuleFor(p => p.METAKeywords).Length(0, 200);
        }
    }
}
