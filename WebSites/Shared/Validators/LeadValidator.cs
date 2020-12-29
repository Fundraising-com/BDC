using FluentValidation;
using GA.BDC.Shared.Entities;
using GA.BDC.Shared.Validators.Rules;

namespace GA.BDC.Shared.Validators
{
    public class LeadValidator  : AbstractValidator<Lead>
    {
        public LeadValidator()
        {
            RuleFor(p => p.Id).NotNull();
            RuleFor(p => p.FirstName).NotNull().Length(1,50).WithName("First Name");
            RuleFor(p => p.LastName).NotNull().Length(1,50).WithName("Last Name");
            RuleFor(p => p.Group).Length(1,100);
            RuleFor(p => p.Email).NotNull().Length(1,50).EmailAddress();
            RuleFor(p => p.Address).NotNull().SetValidator(new AddressValidator()).When(p => p.KitType == 2 || p.KitType == 3);            
            RuleFor(p => p.Address.Region).NotNull().WithName("State/Province");
            RuleFor(p => p.Address.Region.Code).NotNull().Length(2).WithName("State/Province");
            RuleFor(p => p.Address.Country).NotNull();
            RuleFor(p => p.Phone).NotNull().Length(10).SetValidator(new PhoneValidator<string>());
            RuleFor(p => p.PromotionId).NotNull().GreaterThanOrEqualTo(0);
            RuleFor(p => p.PartnerId).NotNull().GreaterThanOrEqualTo(0);
            RuleFor(p => p.KitType).NotNull();
            RuleFor(p => p.Comments).Length(0, 1750);
            RuleFor(p => p.ChannelCode).Length(0, 3);
            RuleFor(p => p.RequestType).NotNull().InclusiveBetween(1, 3);
            RuleFor(p => p.TellMore).NotNull();
        }
    }
}
