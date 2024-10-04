using OpenQA.Selenium;
using WLVSTools.Web.WebInfrastructure.Extensions.Selenium;
using WLVSTools.Web.WebInfrastructure.Selenium.Interfaces;

namespace WLVSTools.Web.WebInfrastructure.Selenium.Automation
{
    public class CompanyNameGenerationAutomation : ISeleniumAutomationWebScrape
    {
        public IWebDriver WebDriver { get; set; }
        public int MaxTimeInSecondsToFindElement { get; set; } = 60;
        public bool Headless { get; set; } = true;
        public bool EagerPageLoadStrategy { get; set; } = true;

        public string WebScrape()
        {
            WebDriver.Navigate().GoToUrl("https://www.coolgenerator.com/company-name-generator");

            var quantityTextBox = WebDriver.FindElement(By.Name("quantity"), MaxTimeInSecondsToFindElement);
            quantityTextBox.SendKeysCustom("1");

            var generateButton = WebDriver.FindElement(By.XPath("//button[.='Generate']"), MaxTimeInSecondsToFindElement);
            generateButton.Click();

            return WebDriver.PageSource;
        }
    }
}
