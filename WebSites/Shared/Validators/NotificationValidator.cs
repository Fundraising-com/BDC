using FluentValidation;
using GA.BDC.Shared.Entities;

namespace GA.BDC.Shared.Validators
{
   public class NotificationValidator : AbstractValidator<Notification>
   {
      public NotificationValidator()
      {
         RuleFor(p => p.Id).NotNull().GreaterThan(0);
         RuleFor(p => p.Type).NotNull().NotEqual(NotificationType.Unknown);
         RuleFor(p => p.Email).NotNull().EmailAddress();
      }
   }
}
