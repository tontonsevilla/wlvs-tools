using OpenQA.Selenium;
using WLVSTools.Web.WebInfrastructure.Extensions;
using WLVSTools.Web.WebInfrastructure.Selenium.Interfaces;

namespace WLVSTools.Web.WebInfrastructure.Selenium.Automation
{
    public class CompanyNameGenerationAutomation : ISeleniumAutomationWebScrape
    {
        public IWebDriver WebDriver { get; set; }

        public string WebScrape(int maxTimeInSecondsToFindElement = 60)
        {
            WebDriver.Navigate().GoToUrl("https://www.coolgenerator.com/company-name-generator");

            var quantityTextBox = WebDriver.FindElement(By.Name("quantity"), maxTimeInSecondsToFindElement);
            quantityTextBox.SendKeysCustom("1");

            var generateButton = WebDriver.FindElement(By.XPath("//button[.='Generate']"), maxTimeInSecondsToFindElement);
            generateButton.Click();

            return WebDriver.PageSource;
        }
    }
}
