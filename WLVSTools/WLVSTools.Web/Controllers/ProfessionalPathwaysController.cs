using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using OpenQA.Selenium;
using WLVSTools.Web.ApplicationServices;
using WLVSTools.Web.Models.AIFS;
using WLVSTools.Web.Models.AIFS.ProfessionalPathways;
using WLVSTools.Web.WebInfrastructure.Selenium.Automation.AIFS.ProfessionalPathways;
using WLVSTools.Web.WebInfrastructure.Selenium.Interfaces;

namespace WLVSTools.Web.Controllers
{
    [Area("AIFS")]
    public class ProfessionalPathwaysController : Controller
    {
        private readonly GenerateApplicationViewService _generateApplicationViewService;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ProfessionalPathwaysController(
            GenerateApplicationViewService generateApplicationViewService,
            IWebHostEnvironment webHostEnvironment)
        {
            _generateApplicationViewService = generateApplicationViewService;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult HostRegistration(string type)
        {
            ModelState.Clear();

            var viewModel = new ProfessionalPathwaysRegistration
            {
                Type = type,
                WebHostEnvironment = _webHostEnvironment
            };

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult HostRegistration(ProfessionalPathwaysRegistration viewModel)
        {
            viewModel.WebHostEnvironment = _webHostEnvironment;

            if (ModelState.IsValid)
            {
                viewModel.Personalnfo = _generateApplicationViewService.PersonalInfo(new Models.DeveloperTools.GenerateFakePersonalInfo
                {
                    Country = viewModel.Country,
                    State = viewModel.State,
                });

                var manager = new WebInfrastructure.Managers.SeleniumManager();
                ISeleniumAutomation hostRegistrationAutomation = new HostRegistrationAutomation(viewModel);

                manager.Execute(hostRegistrationAutomation);
            }

            viewModel.RunEndDateTime = DateTime.Now;

            ViewBag.TotalRunTime = viewModel.TotalRunTimeInSeconds;

            return View(viewModel);
        }

        public IActionResult ParticipantRegistration()
        {
            ModelState.Clear();

            var viewModel = new ParticipatRegistration
            {
                WebHostEnvironment = _webHostEnvironment
            };

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult ParticipantRegistration(ParticipatRegistration viewModel)
        {
            viewModel.WebHostEnvironment = _webHostEnvironment;

            if (ModelState.IsValid)
            {
                viewModel.Personalnfo = _generateApplicationViewService.PersonalInfo(new Models.DeveloperTools.GenerateFakePersonalInfo
                {
                    Country = viewModel.Country
                });

                var manager = new WebInfrastructure.Managers.SeleniumManager();
                ISeleniumAutomation participantRegistrationAutomation = new ParticipantRegistrationAutomation(viewModel);

                manager.Execute(participantRegistrationAutomation);
            }

            viewModel.RunEndDateTime = DateTime.Now;

            ViewBag.TotalRunTime = viewModel.TotalRunTimeInSeconds;

            return View(viewModel);
        }
    }
}
