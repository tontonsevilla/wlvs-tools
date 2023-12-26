using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WLVSTools.Web.Core.Models;

namespace WLVSTools.Web.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
