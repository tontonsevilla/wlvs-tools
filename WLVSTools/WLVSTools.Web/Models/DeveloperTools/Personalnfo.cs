using WLVSTools.Web.Models.Generate;

namespace WLVSTools.Web.Models.DeveloperTools
{
    public class Personalnfo : ApiBaseModel
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Gender { get; set; }
        public string? Email { get; set; }
        public string Title { get; set; } = "Attorney";
        public string Phone { get; set; } = "(123) 456-7890";
        public string MobilePhone { get; set; } = "(098) 765-4321";

        public string Password { get; set; } = "P@ssw0rd";
        public Address? Address { get; set; }
        public UserInfo? UserInfo { get; set; }
        public Company? Company { get; set; }
    }
}
