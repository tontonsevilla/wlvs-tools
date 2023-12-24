using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WLVSTools.Web.Infrastructure.Authentication;
using WLVSTools.Web.ViewModels.Accounts;

namespace WLVSTools.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public AccountController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(ApplicationUser applicationUser)
        {
            return View();
        }

        public IActionResult Register()
        {
            return View(new RegisterViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(viewModel.Email);

                if (user == null)
                {
                    var resultCreateAsync = await _userManager.CreateAsync(new ApplicationUser
                    {
                        Email = viewModel.Email,
                        UserName = viewModel.Email,
                        FirstName = viewModel.FirstName,
                        LastName = viewModel.LastName
                    }, viewModel.Password);

                    if (resultCreateAsync.Succeeded)
                    {
                        return RedirectToAction("RegistrationSuccessful");
                    }

                    ModelState.AddModelError("", string.Join(", ", resultCreateAsync.Errors.Select(ie => ie.Description)));
                }

                ModelState.AddModelError("", "Already exists.");
            }

            return View(viewModel);
        }

        public IActionResult RegistrationSuccessful()
        {
            return View();
        }
    }
}
