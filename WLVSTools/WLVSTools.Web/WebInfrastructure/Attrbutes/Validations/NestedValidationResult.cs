using System.ComponentModel.DataAnnotations;

namespace WLVSTools.Web.WebInfrastructure.Attrbutes.Validations
{
    public class NestedValidationResult : ValidationResult
    {
        public NestedValidationResult() 
            : base("")
        {

        }

        public IList<ValidationResult> NestedResults { get; set; }
    }
}
