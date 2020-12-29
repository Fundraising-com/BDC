using System.Text.RegularExpressions;
using FluentValidation.Validators;

namespace GA.BDC.Shared.Validators.Rules
{
    // ReSharper disable once InconsistentNaming
    public class ZIPValidator<T> : PropertyValidator
    {
        public ZIPValidator() : base("Property {PropertyName} must be a valid ZIP/Postal Code.") { }

        protected override bool IsValid(PropertyValidatorContext context)
        {
            const string usaRegEx = @"^\d{5}(?:[-\s]\d{4})?$";
            const string canadaRegEx = @"^([ABCEGHJKLMNPRSTVXY]\d[ABCEGHJKLMNPRSTVWXYZ])\ {0,1}(\d[ABCEGHJKLMNPRSTVWXYZ]\d)$";
            return context.PropertyValue != null && (Regex.Match(context.PropertyValue.ToString(), usaRegEx, RegexOptions.IgnoreCase).Success || Regex.Match(context.PropertyValue.ToString(), canadaRegEx, RegexOptions.IgnoreCase).Success);
        }
    }
}
