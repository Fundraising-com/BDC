using FluentValidation;
using GA.BDC.Shared.Entities;

namespace GA.BDC.Shared.Validators
{
    public class RepresentativeValidator : AbstractValidator<Representative>
    {
        public RepresentativeValidator()
        {
            RuleFor(p => p.Id).NotNull();
            RuleFor(p => p.Name).NotNull();
            RuleFor(p => p.ExternalId).NotNull();
            RuleFor(p => p.Email).EmailAddress();
            RuleFor(p => p.IsActive).NotNull();
            RuleFor(p => p.Redirect).NotNull();
            RuleFor(p => p.Image).NotNull();
        }
    }
}
