using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using WLVSTools.Web.Models.DeveloperTools;
using WLVSTools.Web.WebInfrastructure.Attrbutes.Validations;

namespace WLVSTools.Web.Models.AIFS
{
    public class ProfessionalPathwaysRegistration : BaseModel
    {
        [Required]
        public string? Type { get; set; }

        [Required]
        public string? Country { get; set; }

        [RequiredIf(PropertyToCheck = "Country", ValueToCheck = "United States")]
        public string? State { get; set; }

        [Required]
        public string? Url { get; set; }

        public List<SelectListItem> TypeList
        {
            get
            {
                return new List<SelectListItem>
                {
                    new SelectListItem("Please select", ""),
                    new SelectListItem("Host Contact", "Contact"),
                    new SelectListItem("Third Party Contact", "ThirdParty")
                };
            }
        }

        public List<SelectListItem> UrlList
        {
            get
            {
                return new List<SelectListItem>
                {
                    new SelectListItem("Please select", ""),
                    new SelectListItem("http://localhost/Host/Account/Register", "http://localhost/Host/Account/Register"),
                    new SelectListItem("https://hoststg.professionalpathways.com/Account/Register", "https://hoststg.professionalpathways.com/Account/Register")
                };
            }
        }

        public Personalnfo? Personalnfo { get; set; }
    }
}
