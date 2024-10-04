using System.Text;
using System.Web;
using WLVSTools.Web.WebInfrastructure.Selenium.Automation;

namespace WLVSTools.Web.Services
{
    public class GeneratorService
    {
        public async Task<HttpResponseMessage> GeneratePersonalInfo(string countryCode)
        {
            var httpClient = new HttpClient();
            return await httpClient.GetAsync($"https://www.coolgenerator.com/{countryCode}fake-name-generator");
        }

        public string GeneratePersonalInfo(string countryCode, string state)
        {
            var seleniumManager = new WebInfrastructure.Managers.SeleniumManager();
            var personalInformationGenerationAutomation = new PersonalInformationGenerationAutomation(countryCode, state);

            personalInformationGenerationAutomation.Headless = false;
            personalInformationGenerationAutomation.EagerPageLoadStrategy = false;

            return seleniumManager.WebScrape(personalInformationGenerationAutomation);
        }

        public string GenerateCompanyName()
        {
            var seleniumManager = new WebInfrastructure.Managers.SeleniumManager();
            var companyNameGeneration = new CompanyNameGenerationAutomation();
            return seleniumManager.WebScrape(companyNameGeneration);
        }

        public string GenerateEIN()
        {
            var length = 9;
            var random = new Random();
            string s = string.Empty;
            for (int i = 0; i < length; i++)
                s = String.Concat(s, random.Next(10).ToString());
            return s;
        }
    }
}
