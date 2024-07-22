using System.ComponentModel.DataAnnotations;
using WLVSTools.Web.WebInfrastructure.Attrbutes.Validations;

namespace WLVSTools.Web.Models.DeveloperTools
{
    public class GenerateFakePersonalInfo
    {
        [Required]
        public string Country { get; set; }

        [RequiredIf(PropertyToCheck = "Country", ValueToCheck = "United States")]
        public string State { get; set; }

        public bool HasData { get; set; }
    }
}
