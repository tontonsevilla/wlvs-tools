using Microsoft.AspNetCore.Mvc;
using WLVSTools.Web.Models.DeveloperTools;

namespace WLVSTools.Web.Controllers
{
    public class DeveloperToolsController : Controller
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        public DeveloperToolsController(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult GenerateFakePersonalInfo(GenerateFakePersonalInfo viewModel)
        {
            if (!viewModel.HasData)
            {
                ModelState.Clear();
            }

            viewModel.WebHostEnvironment = _webHostEnvironment;

            return View(viewModel);
        }
    }
}
