using FluentValidation.Validators;

namespace GA.BDC.Shared.Validators.Rules
{
    public class IntegerValidator<T> : PropertyValidator
    {
        public IntegerValidator() : base("Property {PropertyName} must be integer.") { }

        protected override bool IsValid(PropertyValidatorContext context)
        {
            if (context.PropertyValue == null) return false;
            int value;
            return int.TryParse(context.PropertyValue.ToString(), out value);
        }
    }
}
