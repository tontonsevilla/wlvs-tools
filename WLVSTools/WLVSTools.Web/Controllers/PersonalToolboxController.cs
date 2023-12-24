using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WLVSTools.Web.Controllers
{
    [Authorize]
    public class PersonalToolboxController : Controller
    {
        public IActionResult AccountList()
        {
            return View();
        }

        public IActionResult ContactList()
        {
            return View();
        }
    }
}
