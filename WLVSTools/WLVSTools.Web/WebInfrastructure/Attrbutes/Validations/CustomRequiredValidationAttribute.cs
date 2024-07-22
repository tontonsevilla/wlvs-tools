using System.ComponentModel.DataAnnotations;
using WLVSTools.Web.WebInfrastructure.Extensions;

namespace WLVSTools.Web.WebInfrastructure.Attrbutes.Validations
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true, Inherited = true)]
    public class RequiredIfAttribute : ValidationAttribute
    {
        public string PropertyToCheck { get; set; }
        public object ValueToCheck { get; set; }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            string defaultErrorMsg = string.Format("{0} is required.", validationContext.DisplayName);

            var property = validationContext.ObjectType.GetProperty(PropertyToCheck);
            var comparerValue = property.GetValue(validationContext.ObjectInstance, null);

            if (comparerValue.ToSafeString() != ""
                && comparerValue.ToSafeString() == ValueToCheck.ToSafeString() 
                && (value == null || value.ToSafeString() == ""))
            {
                var members = new List<string>
                {
                    validationContext.MemberName
                };
                return new ValidationResult(ErrorMessage ?? defaultErrorMsg, members);
            }

            return ValidationResult.Success;
        }
    }
}
