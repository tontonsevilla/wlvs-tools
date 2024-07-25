using Microsoft.AspNetCore.Mvc;

namespace WLVSTools.Web.Controllers
{
    public class ExternalController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
