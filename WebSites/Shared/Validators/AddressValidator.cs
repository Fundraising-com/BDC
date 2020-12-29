using FluentValidation;
using GA.BDC.Shared.Entities;
using GA.BDC.Shared.Validators.Rules;

namespace GA.BDC.Shared.Validators
{
    public class AddressValidator : AbstractValidator<Address>
    {
        public AddressValidator()
        {
            RuleFor(p => p.Address1).NotNull().Length(1,100).WithName("Address");
            RuleFor(p => p.City).NotNull().Length(1,50);
            RuleFor(p => p.PostCode).NotNull().Length(1, 10).SetValidator(new ZIPValidator<string>()).WithName("ZIP/Postal Code");
            RuleFor(p => p.Region).NotNull().WithName("State/Province");
            RuleFor(p => p.Region.Code).NotNull().Length(2).WithName("State/Province");
            RuleFor(p => p.Country).NotNull();
        }
    }
}
