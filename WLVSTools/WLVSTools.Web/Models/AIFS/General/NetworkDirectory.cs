using System.ComponentModel.DataAnnotations;

namespace WLVSTools.Web.Models.AIFS.General
{
    public class NetworkDirectory : BaseModel
    {
        public NetworkDirectory()
        {
            DirectoryFiles = new List<string>();
        }

        [Display(Name = "User Name"), Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }

        [Display(Name = "Domain Name"), Required]
        public string DomainName { get; set; }

        [Required]
        public string? Path { get; set; }

        public string? Search { get; set; }
        
        public bool IsSubmitted { get; set; }

        public List<string> DirectoryFiles { get; set; }
    }
}
