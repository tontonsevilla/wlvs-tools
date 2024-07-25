using Microsoft.AspNetCore.Mvc;
using WLVSTools.Web.ApplicationServices;
using WLVSTools.Web.Models.DeveloperTools;

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
            if (ModelState.IsValid)
            {
                var personData = _generateApplicationViewService.PersonalInfo(data);

                return Ok(personData);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        [HttpGet]
        public IActionResult CompanyName()
        {
            return Ok(_generateApplicationViewService.Company());
        }
    }
}
