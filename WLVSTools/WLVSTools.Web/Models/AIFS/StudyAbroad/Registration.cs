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
        public string? Url { get; set; } = "http://localhost/College/Account/Register";

        public Personalnfo? Personalnfo { get; set; }
    }
}
