using Microsoft.AspNetCore.Mvc;
using System.Net.Mail;
using System.Net;
using WLVSTools.Web.Models.BlastAsia;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using WLVSTools.Web.WebInfrastructure.Extensions;
using WLVSTools.Web.WebInfrastructure.Selenium.Automation.BlastAsia;

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

        public IActionResult EndOfDay()
        {
            return View(new EndOfDay());
        }

        [HttpPost]
        public IActionResult EndOfDay(EndOfDay model)
        {
            if (ModelState.IsValid)
            {
                var seleniumManager = new WebInfrastructure.Managers.SeleniumManager();
                var eodSubmissionAutomation = new EODSubmissionAutomation(model);
                seleniumManager.Execute(eodSubmissionAutomation);
            }

            return View(model);
        }
    }
}
