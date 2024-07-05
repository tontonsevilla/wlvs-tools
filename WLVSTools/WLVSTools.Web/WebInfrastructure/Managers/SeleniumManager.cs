using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
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
            seleniumAutomation.WebDriver = webDriver;
            seleniumAutomation.Execute();
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
