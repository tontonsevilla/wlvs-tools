using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Net.NetworkInformation;
using WebDriverManager.DriverConfigs.Impl;
using WLVSTools.Web.Core.General;
using WLVSTools.Web.WebInfrastructure.Extensions.Selenium;
using WLVSTools.Web.WebInfrastructure.Selenium.Interfaces;

namespace WLVSTools.Web.WebInfrastructure.Managers
{
    public class SeleniumManager
    {
        const int maxmaxTimeInSecondsToFindElement = 60;
        ChromeOptions options;
        IWebDriver webDriver;

        public SeleniumManager()
        {
            new WebDriverManager.DriverManager().SetUpDriver(new ChromeConfig());
            options = GetOptions();
            webDriver = new ChromeDriver(options);
        }

        private void setOptions(ISeleniumAutomation seleniumAutomation)
        {
            if (seleniumAutomation.Headless)
            {
                options.AddArguments("headless=new");
            }

            if (seleniumAutomation.EagerPageLoadStrategy)
            {
                options.PageLoadStrategy = PageLoadStrategy.Eager;
            }
        }

        private ChromeOptions GetOptions()
        {
            ChromeOptions options = new ChromeOptions();
            options.AcceptInsecureCertificates = true;

            options.AddExcludedArgument("enable-automation");
            options.AddArgument("disable-extensions");
            options.AddArgument("disable-infobars");
            options.AddArgument("disable-notifications");
            options.AddArgument("disable-web-security");
            options.AddArgument("dns-prefetch-disable");
            options.AddArgument("disable-browser-side-navigation");
            options.AddArgument("disable-gpu");
            options.AddArgument("always-authorize-plugins");
            options.AddArgument("load-extension=src/main/resources/chrome_load_stopper");
            //options.AddUserProfilePreference("download.default_directory", _globalProperties.datasetlocation);

            options.AddExtension(@"C:\Selenium\Chrome\Extensions\AdBlock_6.9.2.0.crx");

            return options;
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
                seleniumAutomation.WebDriver.TakeScreenShot(seleniumAutomation);
                seleniumAutomation.WebDriver.Quit();
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
                seleniumAutomationWebScrape.WebDriver.Quit();
            }

            return webScrapeString;
        }
    }
}
