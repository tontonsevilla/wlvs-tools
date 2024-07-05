using OpenQA.Selenium;

namespace WLVSTools.Web.WebInfrastructure.Selenium.Interfaces
{
    public interface ISeleniumAutomation
    {
        IWebDriver WebDriver { get; set; }
        void Execute();
    }

    public interface ISeleniumAutomationWebScrape
    {
        IWebDriver WebDriver { get; set; }
        string WebScrape(int maxTimeInSecondsToFindElement = 60);
    }
}
