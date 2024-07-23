using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using WLVSTools.Web.Models.DeveloperTools;

namespace WLVSTools.Web.Models.AIFS.ProfessionalPathways
{
    public class ParticipatRegistration : BaseModel
    {
        [Required]
        public string? Country { get; set; }

        [Required]
        public string? Url { get; set; } = "http://localhost/Participant/Account/Register";

        public Personalnfo? Personalnfo { get; set; }

        public new List<SelectListItem> CountryList
        {
            get
            {
                return base.CountryList.Where(item => item.Text != "United States").ToList();
            }
        } 
    }
}
