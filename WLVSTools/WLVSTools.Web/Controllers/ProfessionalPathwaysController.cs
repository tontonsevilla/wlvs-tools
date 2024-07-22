using Microsoft.AspNetCore.Mvc;
using WLVSTools.Web.Models.AIFS;

namespace WLVSTools.Web.Controllers
{
    [Area("AIFS")]
    public class ProfessionalPathwaysController : Controller
    {
        public IActionResult HostRegistration(string type)
        {
            var viewModel = new ProfessionalPathwaysRegistration
            {
                Type = type
            };

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult HostRegistration(ProfessionalPathwaysRegistration viewModel)
        {
            return View(viewModel);
        }
    }
}
