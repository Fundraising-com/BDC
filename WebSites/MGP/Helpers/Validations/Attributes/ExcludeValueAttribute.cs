using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace GA.BDC.Web.MGP.Helpers.Validations.Attributes
{
    public class ExcludeValueAttribute : ValidationAttribute
    {
        private readonly string _values;
        public ExcludeValueAttribute(string values)
            : base("{0} contains an invalid value.")
        {
            _values = values;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null)
            {
                var values = _values.Split(',');
                var valueToBeValidated = value.ToString();
                if (values.Any(excludedValue => valueToBeValidated.ToLower().Contains(excludedValue.ToLower())))
                {
                    return new ValidationResult(FormatErrorMessage(validationContext.DisplayName));
                }
            }

            return ValidationResult.Success;
        }
    }
}