using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using WLVSTools.Web.WebInfrastructure.Extensions;
using WLVSTools.Web.WebInfrastructure.Selenium.Interfaces;

namespace WLVSTools.Web.WebInfrastructure.Selenium.Automation
{
    public class PersonalInformationGenerationAutomation : ISeleniumAutomationWebScrape
    {
        public PersonalInformationGenerationAutomation(string countryCode, string state)
        {
            CountryCode = countryCode;
            State = state;
        }

        public string CountryCode { get; private set; }
        public string State { get; private set; }

        public IWebDriver WebDriver { get; set; }

        public string WebScrape(int maxTimeInSecondsToFindElement = 60)
        {
            WebDriver.Navigate().GoToUrl($"https://www.coolgenerator.com/{CountryCode}fake-name-generator");

            if (string.IsNullOrWhiteSpace(CountryCode)
                && !string.IsNullOrWhiteSpace(State))
            {
                var ddlStateElement = WebDriver.FindElement(By.Name("state"), maxTimeInSecondsToFindElement);
                ddlStateElement.Click();
                ddlStateElement.FindElement(By.XPath($"option[@value='{State}']")).Click();

                IWebElement btnGenerate = WebDriver.FindElement(By.XPath("//button[.='Generate']"), maxTimeInSecondsToFindElement);
                btnGenerate.Click();
            }

            WebDriver.FindElement(By.XPath("//b[.='Basic information']"), maxTimeInSecondsToFindElement);

            return WebDriver.PageSource;
        }
    }
}
