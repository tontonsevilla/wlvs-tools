using Microsoft.AspNetCore.Mvc;
using OpenQA.Selenium;
using WLVSTools.Web.Models.AIFS;
using WLVSTools.Web.WebInfrastructure.Selenium.Automation.AIFS.ProfessionalPathways;
using WLVSTools.Web.WebInfrastructure.Selenium.Interfaces;

namespace WLVSTools.Web.Controllers
{
    [Area("AIFS")]
    public class ProfessionalPathwaysController : Controller
    {
        public IActionResult HostRegistration(string type)
        {
            var viewModel = new ProfessionalPathwaysRegistration
            {
                Type = type
            };

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult HostRegistration(ProfessionalPathwaysRegistration viewModel)
        {
            if (ModelState.IsValid)
            {
                var manager = new WebInfrastructure.Managers.SeleniumManager();
                ISeleniumAutomation hostRegistrationAutomation = new HostRegistrationAutomation(viewModel);

                manager.Execute(hostRegistrationAutomation);
            }

            return View(viewModel);
        }
    }
}
