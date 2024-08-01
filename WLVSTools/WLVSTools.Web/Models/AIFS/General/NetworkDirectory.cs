using System.ComponentModel.DataAnnotations;

namespace WLVSTools.Web.Models.AIFS.General
{
    public class NetworkDirectory : BaseModel
    {
        public NetworkDirectory()
        {
                DirectoryFiles = new List<string>();
        }

        [Required]
        public string? Path { get; set; }
        public bool IsSubmitted { get; set; }

        public List<string> DirectoryFiles { get; set; }
    }
}
