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
        public string? Url { get; set; }

        public Personalnfo? Personalnfo { get; set; }

        public new List<SelectListItem> CountryList
        {
            get
            {
                return base.CountryList.Where(item => item.Text != "United States").ToList();
            }
        }

        public List<SelectListItem> UrlList
        {
            get
            {
                return new List<SelectListItem>
                {
                    new SelectListItem("Please select", ""),
                    new SelectListItem("http://localhost/Participant/Account/Register", "http://localhost/Participant/Account/Register"),
                    new SelectListItem("https://participantstg.professionalpathways.com/Account/Register", "https://participantstg.professionalpathways.com/Account/Register")
                };
            }
        }
    }
}
