using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.Extensions;
using System.Collections.Generic;
using WLVSTools.Web.Core.General;
using WLVSTools.Web.WebInfrastructure.Selenium.Interfaces;

namespace WLVSTools.Web.WebInfrastructure.Managers
{
    public class SeleniumManager
    {
        const int maxmaxTimeInSecondsToFindElement = 60;
        ChromeOptions options = new ChromeOptions();
        IWebDriver webDriver;

        public SeleniumManager()
        {
            webDriver = new ChromeDriver(@"C:\WebDrivers", options);
        }

        private void setOptions(ISeleniumAutomation seleniumAutomation)
        {
            if (seleniumAutomation.Headless)
            {
                options.AddArguments("headless");
            }

            if (seleniumAutomation.EagerPageLoadStrategy)
            {
                options.PageLoadStrategy = PageLoadStrategy.Eager;
            }
        }

        public ServiceResponse<ValueResponse<String>> Execute(ISeleniumAutomation seleniumAutomation)
        {
            setOptions(seleniumAutomation);

            var response = new ServiceResponse<ValueResponse<String>>();

            try
            {
                seleniumAutomation.WebDriver = webDriver;
                response = seleniumAutomation.Execute();
            }
            catch
            {
                Screenshot screenshot = WebDriverExtensions.TakeScreenshot(seleniumAutomation.WebDriver);
                screenshot.SaveAsFile($"Screenshot/SS_Error_{seleniumAutomation.GetType().Name}_{DateTime.Now.ToString("yyyyMMddhhmmss")}.png");
                throw;
            }
            finally
            {
                seleniumAutomation.WebDriver.Close();
                seleniumAutomation.WebDriver.Dispose();
            }

            return response;
        }

        public string WebScrape(ISeleniumAutomationWebScrape seleniumAutomationWebScrape)
        {
            string webScrapeString = "";

            try
            {
                seleniumAutomationWebScrape.WebDriver = webDriver;
                webScrapeString = seleniumAutomationWebScrape.WebScrape();
            }
            catch
            {
                throw;
            }
            finally
            {
                seleniumAutomationWebScrape.WebDriver.Close();
                seleniumAutomationWebScrape.WebDriver.Dispose();
            }

            return webScrapeString;
        }
    }
}
