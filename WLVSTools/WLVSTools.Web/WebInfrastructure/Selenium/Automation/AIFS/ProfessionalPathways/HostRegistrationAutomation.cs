using OpenQA.Selenium;
using WLVSTools.Web.Models.AIFS;
using WLVSTools.Web.WebInfrastructure.Selenium.Interfaces;

namespace WLVSTools.Web.WebInfrastructure.Selenium.Automation.AIFS.ProfessionalPathways
{
    public class HostRegistrationAutomation : ISeleniumAutomation
    {
        public HostRegistrationAutomation(ProfessionalPathwaysRegistration data)
        {
            Data = data;
        }

        public ProfessionalPathwaysRegistration Data { get; private set; }

        public IWebDriver WebDriver { get; set; }
        public void Execute(int maxTimeInSecondsToFindElement = 60)
        {
            WebDriver.Navigate().GoToUrl(Data.Url);
        }
    }
}
