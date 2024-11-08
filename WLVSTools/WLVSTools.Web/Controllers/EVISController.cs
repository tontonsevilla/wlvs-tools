using Microsoft.AspNetCore.Mvc;

namespace WLVSTools.Web.Controllers
{
    [Area("AIFS")]
    public class EVISController : Controller
    {
        public IActionResult BatchSent()
        {
            return View();
        }
    }
}
