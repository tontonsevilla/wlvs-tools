using HtmlAgilityPack;
using Microsoft.AspNetCore.Mvc;
using WLVSTools.Web.Models.Generate;
using WLVSTools.Web.Services;

namespace WLVSTools.Web.Controllers.api
{
    public class GenerateController : ControllerBase
    {
        private readonly GeneratorService _generatorService;

        public GenerateController()
        {
            _generatorService = new GeneratorService();
        }

        [HttpGet]
        public IActionResult PersonalInfo() 
        { 
            return Ok(new { Prop1 = "Sample Data" });
        }

        [HttpGet]
        public IActionResult CompanyName(string keyword = "", string quantity = "") 
        {
            var htmlString = _generatorService.GenerateCompanyName(keyword, quantity);
            var htmlDoc = new HtmlDocument();

            htmlDoc.LoadHtml(htmlString);

            var nodes = htmlDoc.DocumentNode.Descendants(0).Where(n => n.HasClass("content-list"));

            var list = nodes.ElementAt(0).Descendants("li").Select(n => new Company
            {
                Name = $"{n.ChildNodes[1].InnerText} Test Data",
                Url = $"www.{n.ChildNodes[1].InnerText.Replace(" ", "")}.test"
            }).ToList();

            var einList = new List<string>();
            list.ForEach((company) =>
            {
                string ein = "";

                while (string.IsNullOrEmpty(ein) || einList.Contains(ein))
                {
                    ein = _generatorService.GenerateEIN();
                }

                company.EIN = ein;

                einList.Add(ein);
            });

            return Ok(new
            {
                Company = list
            });
        }
    }
}
