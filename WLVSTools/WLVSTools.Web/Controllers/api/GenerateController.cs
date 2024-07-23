using HtmlAgilityPack;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.Metrics;
using System.Globalization;
using System.Web;
using WLVSTools.Web.ApplicationServices;
using WLVSTools.Web.Models.DeveloperTools;
using WLVSTools.Web.Models.Generate;
using WLVSTools.Web.Services;

namespace WLVSTools.Web.Controllers.api
{
    public class GenerateController : ControllerBase
    {
        private readonly GenerateApplicationViewService _generateApplicationViewService;

        public GenerateController(GenerateApplicationViewService generateApplicationViewService)
        {
            _generateApplicationViewService = generateApplicationViewService;
        }

        [HttpGet]
        public IActionResult PersonalInfo(GenerateFakePersonalInfo data) 
        {
            var personData = _generateApplicationViewService.PersonalInfo(data);

            personData.RunEndDateTime = DateTime.Now;

            return Ok(personData);
        }

        [HttpGet]
        public IActionResult CompanyName()
        {
            return Ok(_generateApplicationViewService.Company());
        }
    }
}
