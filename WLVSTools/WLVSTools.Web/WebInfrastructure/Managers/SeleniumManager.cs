using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Collections.Generic;
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
            setOptions();
            webDriver = new ChromeDriver(@"C:\WebDrivers", options);
        }

        private void setOptions()
        {
            options.PageLoadStrategy = PageLoadStrategy.Eager;
            options.AddArguments("headless");
        }

        public void Execute(ISeleniumAutomation seleniumAutomation)
        {
            try
            {
                seleniumAutomation.WebDriver = webDriver;
                seleniumAutomation.Execute();
            }
            catch
            {
                throw;
            }
            finally
            {
                seleniumAutomation.WebDriver.Close();
                seleniumAutomation.WebDriver.Dispose();
            }
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
