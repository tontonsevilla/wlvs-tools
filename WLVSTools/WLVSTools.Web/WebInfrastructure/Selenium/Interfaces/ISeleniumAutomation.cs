using OpenQA.Selenium;

namespace WLVSTools.Web.WebInfrastructure.Selenium.Interfaces
{
    public interface ISeleniumAutomation
    {
        IWebDriver WebDriver { get; set; }
        void Execute(int maxTimeInSecondsToFindElement = 60);
    }

    public interface ISeleniumAutomationWebScrape
    {
        IWebDriver WebDriver { get; set; }
        string WebScrape(int maxTimeInSecondsToFindElement = 60);
    }
}
