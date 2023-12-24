using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WLVSTools.Web.Infrastructure.Authentication;

namespace WLVSTools.Web.Controllers
{
    public class BaseController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public BaseController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        protected async Task<string> GetUserId()
        {
            if (_userManager != null
                && User?.Identity?.Name != null)
            {
                ApplicationUser? user = await _userManager.FindByEmailAsync(User.Identity.Name);
                if (user != null)
                {
                    return user.Id;
                }
            }

            return "";
        }

        protected void GetInnerException(Exception ex)
        {
            if (ex.InnerException != null)
            {
                GetInnerException(ex.InnerException);
            }

            ModelState.AddModelError("", ex.Message);
        }
    }
}
