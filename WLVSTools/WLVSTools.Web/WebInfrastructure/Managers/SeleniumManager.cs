using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using WLVSTools.Web.WebInfrastructure.Selenium;
using WLVSTools.Web.WebInfrastructure.Selenium.Interfaces;

namespace WLVSTools.Web.WebInfrastructure.Managers
{
    public class SeleniumManager
    {
        const int maxmaxTimeInSecondsToFindElement = 60;
        ChromeOptionsWithPrefs options = new ChromeOptionsWithPrefs();
        IWebDriver webDriver;

        public SeleniumManager()
        {
            setOptions();
            webDriver = new ChromeDriver(@"C:\WebDrivers", options);
        }

        private void setOptions()
        {
            var defaultSettings = new Dictionary<string, object>();
            defaultSettings.Add("images", 2);
            options.Prefs.Add("profile.default_content_settings", defaultSettings);
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
