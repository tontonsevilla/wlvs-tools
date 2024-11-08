using Microsoft.AspNetCore.Mvc;
using WLVSTools.Web.ApplicationServices;
using WLVSTools.Web.Models.AIFS.StudyAbroad;
using WLVSTools.Web.WebInfrastructure.Selenium.Automation.AIFS.StudyAbroad;
using WLVSTools.Web.WebInfrastructure.Selenium.Interfaces;

namespace WLVSTools.Web.Controllers
{
    [Area("AIFS")]
    public class StudyAbroadController : Controller
    {
        private readonly GenerateApplicationViewService _generateApplicationViewService;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public StudyAbroadController(
            GenerateApplicationViewService generateApplicationViewService,
            IWebHostEnvironment webHostEnvironment)
        {
            _generateApplicationViewService = generateApplicationViewService;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Registration()
        {
            var viewModel = new Registration
            {
                WebHostEnvironment = _webHostEnvironment
            };

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Registration(Registration viewModel)
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
                ISeleniumAutomation hostRegistrationAutomation = new CollegeRegistrationAutomation(viewModel);

                manager.Execute(hostRegistrationAutomation);
            }

            viewModel.RunEndDateTime = DateTime.Now;

            ViewBag.TotalRunTime = viewModel.TotalRunTimeInSeconds;

            return View(viewModel);
        }

        public IActionResult CFLRegistration()
        {
            var viewModel = new Registration
            {
                WebHostEnvironment = _webHostEnvironment
            };

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult CFLRegistration(Registration viewModel)
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
                ISeleniumAutomation hostRegistrationAutomation = new CollegeRegistrationAutomation(viewModel);

                manager.Execute(hostRegistrationAutomation);
            }

            viewModel.RunEndDateTime = DateTime.Now;

            ViewBag.TotalRunTime = viewModel.TotalRunTimeInSeconds;

            return View(viewModel);
        }
    }
}
