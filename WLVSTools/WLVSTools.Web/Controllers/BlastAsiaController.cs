using Microsoft.AspNetCore.Mvc;
using System.Net.Mail;
using System.Net;
using WLVSTools.Web.Models.BlastAsia;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using WLVSTools.Web.WebInfrastructure.Extensions;

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
                const int maxTimeInSecondsToFindElement = 60;
                IWebDriver driver = new ChromeDriver();
                driver.Navigate().GoToUrl("https://live.quickreach.co/");

                //LOGIN
                driver.FindElement(By.Id("inputEmail1"), maxTimeInSecondsToFindElement).SendKeys(model.Email);
                driver.FindElement(By.Id("inputPassword")).SendKeys(model.Password);
                driver.FindElement(By.CssSelector("button[data-cy='lg-submit-btn']")).Click();

                //CREATE REQUEST
                driver.FindElement(By.CssSelector("button[mattooltip='File New Request']"), maxTimeInSecondsToFindElement).Click();
                driver.FindElement(By.XPath("//*[@id=\"mat-dialog-0\"]/app-workflow-selection-wizard/section/div[2]/div/div[3]/section[2]/div/div/mat-card[4]"), maxTimeInSecondsToFindElement).Click();

                driver.Close();
                driver.Dispose();
            }

            return View(model);
        }
    }
}
