using Microsoft.AspNetCore.Identity;

namespace WLVSTools.Web.Infrastructure.Authentication
{
    public class ApplicationUser : IdentityUser
    {
        //Adding New Properties FirstName, LastName, and BirthDate
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public DateTime? BirthDate { get; set; }
    }
}
