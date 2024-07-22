using WLVSTools.Web.Models.Generate;

namespace WLVSTools.Web.Models.DeveloperTools
{
    public class Personalnfo
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public string Email { get; set; }
        public string Title { get; set; } = "Attorney";
        public string Phone { get; set; } = "1234567890";
        public string Password { get; set; } = "P@ssw0rd";
        public Address Address { get; set; }
        public UserInfo UserInfo { get; set; }
        public Company Company { get; set; }
    }
}
