using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using WLVSTools.Web.Models.DeveloperTools;

namespace WLVSTools.Web.Models.AIFS.StudyAbroad
{
    public class Registration : BaseModel
    {
        [Required]
        public string? Country { get; set; } = "United States";

        [Required]
        public string? State { get; set; }

        [Required]
        public string? Url { get; set; }

        public List<SelectListItem> UrlList
        {
            get
            {
                return new List<SelectListItem>
                {
                    new SelectListItem("Please select", ""),
                    new SelectListItem("http://localhost/College/Account/Register", "http://localhost/College/Account/Register"),
                    new SelectListItem("https://securestg.aifsabroad.com/College/Account/Register", "https://securestg.aifsabroad.com/College/Account/Register")
                };
            }
        }

        public Personalnfo? Personalnfo { get; set; }
    }
}
