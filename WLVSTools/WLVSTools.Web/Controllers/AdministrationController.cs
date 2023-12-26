using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WLVSTools.Web.Controllers
{
    [Authorize]
    public class AdministrationController : Controller
    {
        public IActionResult Databases()
        {
            return View(Directory.EnumerateFiles("Database", "*.*", SearchOption.TopDirectoryOnly)
                .Where(s => s.EndsWith(".db")).ToList());
        }
    }
}
