using OpenQA.Selenium;
using WLVSTools.Web.Core.General;

namespace WLVSTools.Web.WebInfrastructure.Selenium.Interfaces
{
    public interface ISeleniumAutomation
    {
        bool Headless { get; set; }
        bool EagerPageLoadStrategy { get; set; }
        IWebDriver WebDriver { get; set; }
        ServiceResponse<ValueResponse<String>> Execute(int maxTimeInSecondsToFindElement = 60);
    }

    public interface ISeleniumAutomationWebScrape
    {
        IWebDriver WebDriver { get; set; }
        string WebScrape(int maxTimeInSecondsToFindElement = 60);
    }
}
