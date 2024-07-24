using System.Collections;
using System.ComponentModel.DataAnnotations;

namespace WLVSTools.Web.WebInfrastructure.Attrbutes.Validations
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    public class AtLeastOneItemRequiredValidation : ValidationAttribute
    {
        protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
        {
            string defaultErrorMsg = string.Format("At least 1 item is required for {0}.", validationContext.DisplayName);
            var list = value as IEnumerable;
            var enumerator = list.GetEnumerator();

            if (list == null || !enumerator.MoveNext())
            {
                return new ValidationResult(ErrorMessage ?? defaultErrorMsg);
            }

            return ValidationResult.Success;
        }
    }
}
