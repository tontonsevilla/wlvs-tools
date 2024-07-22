using Microsoft.AspNetCore.Mvc;
using WLVSTools.Web.Models.DeveloperTools;

namespace WLVSTools.Web.Controllers
{
    public class DeveloperToolsController : Controller
    {
        public IActionResult GenerateFakePersonalInfo(GenerateFakePersonalInfo model)
        {
            if (!model.HasData)
            {
                ModelState.Clear();
            }

            return View(model);
        }
    }
}
