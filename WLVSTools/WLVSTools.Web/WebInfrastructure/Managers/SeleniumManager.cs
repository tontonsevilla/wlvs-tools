using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.Extensions;
using System.Collections.Generic;
using WLVSTools.Web.Core.General;
using WLVSTools.Web.WebInfrastructure.Extensions.Selenium;
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
            webDriver = new ChromeDriver(@"C:\Selenium\Chrome\WebDriver", options);
        }

        private void setOptions(ISeleniumAutomation seleniumAutomation)
        {
            options.AddExtension(@"C:\Selenium\Chrome\Extensions\AdBlock_6.9.2.0.crx");

            if (seleniumAutomation.Headless)
            {
                options.AddArguments("headless=new");
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
                seleniumAutomation.WebDriver.TakeScreenShot(seleniumAutomation);
                throw;
            }
            finally
            {
                seleniumAutomation.WebDriver.Close();
                seleniumAutomation.WebDriver.Dispose();
            }

            return response;
        }

        private void setOptions(ISeleniumAutomationWebScrape seleniumAutomationWebScrape)
        {
            if (seleniumAutomationWebScrape.Headless)
            {
                options.AddArguments("headless=new");
            }

            if (seleniumAutomationWebScrape.EagerPageLoadStrategy)
            {
                options.PageLoadStrategy = PageLoadStrategy.Eager;
            }
        }

        public string WebScrape(ISeleniumAutomationWebScrape seleniumAutomationWebScrape)
        {
            setOptions(seleniumAutomationWebScrape);

            string webScrapeString = "";

            try
            {
                seleniumAutomationWebScrape.WebDriver = webDriver;
                webScrapeString = seleniumAutomationWebScrape.WebScrape();
            }
            catch
            {
                seleniumAutomationWebScrape.WebDriver.TakeScreenShot(seleniumAutomationWebScrape);
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
