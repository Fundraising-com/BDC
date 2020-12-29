using FluentValidation;
using GA.BDC.Shared.Entities;
using GA.BDC.Shared.Validators.Rules;

namespace GA.BDC.Shared.Validators
{
   public class ClientValidator : AbstractValidator<Client>
   {
      public ClientValidator()
      {
         RuleFor(p => p.Id).NotNull();
         RuleFor(p => p.SequenceCode).NotNull().Length(2).WithMessage("Sequence Code");
         RuleFor(p => p.OrganizationClassCode).NotNull().Length(3).WithMessage("Organization Class Code");
         RuleFor(p => p.ChannelCode).NotNull().Length(2, 4).WithMessage("Channel Code");
         RuleFor(p => p.PromotionId).NotNull().GreaterThanOrEqualTo(0);
         RuleFor(p => p.DivisionId).NotNull().GreaterThanOrEqualTo(0);
         RuleFor(p => p.Salutation).Length(0, 10);
         RuleFor(p => p.FirstName).NotNull().Length(0, 50).WithMessage("First Name");
         RuleFor(p => p.LastName).NotNull().Length(0, 50).WithMessage("Last Name");
         RuleFor(p => p.Title).Length(0, 50);
         RuleFor(p => p.Organization).Length(0, 100);
         RuleFor(p => p.Phone).NotNull().Length(10).SetValidator(new PhoneValidator<string>());
         RuleFor(p => p.Email).NotNull().EmailAddress().Length(1, 50);
         //RuleFor(p => p.Email2).NotNull().EmailAddress().Length(1, 50).Equal(p => p.Email).When(p => p.Id == 0);
         When(p => p.Id == 0, () =>
         {
            RuleFor(p => p.Email2).NotNull();
            RuleFor(p => p.Email2).EmailAddress();
            RuleFor(p => p.Email2).Length(1, 50);
            RuleFor(p => p.Email2).Equal(p => p.Email);
         });
         RuleFor(p => p.Addresses).SetCollectionValidator(new ClientAddressValidator()).SetCollectionValidator(new AddressValidator());
      }
   }
}
