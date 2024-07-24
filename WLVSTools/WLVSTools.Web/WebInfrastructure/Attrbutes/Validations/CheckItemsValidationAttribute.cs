using System.Collections;
using System.ComponentModel.DataAnnotations;

namespace WLVSTools.Web.WebInfrastructure.Attrbutes.Validations
{
    public class CheckItemsValidationAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
        {
            var list = value as IEnumerable;

            if (list != null) 
            {
                var result = new NestedValidationResult();
                List<ValidationResult> recursiveResultList = new List<ValidationResult>();

                foreach (var item in list)
                {
                    var nestedItemResult = new List<ValidationResult>();
                    var context = new ValidationContext(item, null, null);

                    var nestedParentResult = new NestedValidationResult();

                    Validator.TryValidateObject(item, context, nestedItemResult, true);
                    nestedParentResult.NestedResults = nestedItemResult;
                    recursiveResultList.Add(nestedParentResult);
                }

                result.NestedResults = recursiveResultList;

                if (recursiveResultList.Any(r => !string.IsNullOrWhiteSpace(r.ErrorMessage)))
                {
                    return result;
                }
            }
            else
            {
                throw new Exception("Can't validate non-enumerable.");
            }

            return ValidationResult.Success;
        }
    }
}
