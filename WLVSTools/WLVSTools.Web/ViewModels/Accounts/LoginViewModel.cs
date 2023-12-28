using Microsoft.AspNetCore.Authentication;
using System.ComponentModel.DataAnnotations;

namespace WLVSTools.Web.ViewModels.Accounts
{
    public class LoginViewModel
    {
        [Required]
        public string? Email { get; set; }

        [Required]
        public string? Password { get; set; }

        [Required]
        public bool IsPersistent { get; set; }

        public string? Provider { get; set; }

        public string? ReturnUrl { get; set; }

        public IList<AuthenticationScheme>? ExternalLogins { get; set; }
    }
}
