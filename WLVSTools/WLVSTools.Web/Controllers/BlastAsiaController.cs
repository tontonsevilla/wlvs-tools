using Microsoft.AspNetCore.Mvc;
using System.Net.Mail;
using System.Net;
using WLVSTools.Web.Models.BlastAsia;

namespace WLVSTools.Web.Controllers
{
    public class BlastAsiaController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult TimeIn() 
        {
            return View(new TimeKeeping(TimeKeepingType.In));
        }

        [HttpPost]
        public IActionResult TimeIn(TimeKeeping model)
        {
            return View(model);
        }

        public IActionResult TimeOut()
        {
            return View(new TimeKeeping(TimeKeepingType.Out));
        }

        [HttpPost]
        public IActionResult TimeOut(TimeKeeping model)
        {
            return View(model);
        }
    }
}
