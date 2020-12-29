using FluentValidation;
using GA.BDC.Shared.Entities;

namespace GA.BDC.Shared.Validators
{
    public class ClientAddressValidator : AbstractValidator<ClientAddress>
    {
        public ClientAddressValidator()
        {
            RuleFor(p => p.Id).NotNull();
            RuleFor(p => p.ClientId).NotNull();
            RuleFor(p => p.ClientSequenceCode).NotNull().Length(2).WithMessage("Client Sequence Code");
            RuleFor(p => p.Type).NotNull().Length(2);
            RuleFor(p => p.AttentionOf).Length(0, 100);
            RuleFor(p => p.AddressZoneId).NotNull();
        }
    }
}
