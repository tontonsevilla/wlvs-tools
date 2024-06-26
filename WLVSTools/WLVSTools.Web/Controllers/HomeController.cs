using Microsoft.AspNetCore.Mvc;

namespace WLVSTools.Web.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
