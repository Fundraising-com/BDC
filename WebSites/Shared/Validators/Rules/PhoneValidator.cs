using System.Text.RegularExpressions;
using FluentValidation.Validators;

namespace GA.BDC.Shared.Validators.Rules
{
    public class PhoneValidator<T> : PropertyValidator
    {
        public PhoneValidator() : base("Property {PropertyName} must be a valid phone number.") { }

        protected override bool IsValid(PropertyValidatorContext context)
        {
            var regex = new Regex(@"^\d{10}$");
            return context.PropertyValue != null && regex.IsMatch(context.PropertyValue.ToString());
        }
    }
}
