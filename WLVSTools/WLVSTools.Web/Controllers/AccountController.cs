using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WLVSTools.Web.Infrastructure.Authentication;
using WLVSTools.Web.ViewModels.Accounts;

namespace WLVSTools.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AccountController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var resultPasswordSignInAsync = await _signInManager.PasswordSignInAsync(viewModel.Email,
                    viewModel.Password,
                    viewModel.IsPersistent, false);

                if (resultPasswordSignInAsync.Succeeded)
                {
                    return RedirectToAction("", "Home");
                }

                ModelState.AddModelError("", "Invalid account.");
            }

            return View(viewModel);
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

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();

            return RedirectToAction("Login");
        }
    }
}
