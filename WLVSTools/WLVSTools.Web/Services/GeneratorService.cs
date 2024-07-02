using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;

namespace WLVSTools.Web.Services
{
    public class GeneratorService
    {
        public async Task<HttpResponseMessage> GeneratePersonalInfo(string countryCode)
        {
            var httpClient = new HttpClient();
            return await httpClient.GetAsync($"https://www.coolgenerator.com/{countryCode}fake-name-generator");
        }

        public string GeneratePersonalInfo(string countryCode, string state)
        {
            ChromeOptions options = new ChromeOptions();
            options.AddArguments("headless");

            IWebDriver driver = new ChromeDriver(@"C:\WebDrivers");
            driver.Url = "https://www.coolgenerator.com/fake-name-generator";

            var pageSourceBefore = driver.PageSource;

            SelectElement ddlState = new SelectElement(driver.FindElement(By.Name("state")));
            ddlState.SelectByValue(state);

            IWebElement btnGenerate = driver.FindElement(By.XPath("//button[.='Generate']"));
            btnGenerate.Click();

            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(120));

            wait.Until(drv => drv.FindElement(By.XPath("//b[.='Basic information']")));

            var htmlString = driver.PageSource;

            driver.Quit();
            driver.Dispose();

            return htmlString;
        }

        public async Task<HttpResponseMessage> GenerateCompanyName(string keyword, string quantity)
        {
            var formContent = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("keyword", keyword),
                new KeyValuePair<string, string>("quantity", quantity)
            });
            var httpClient = new HttpClient();
            return await httpClient.PostAsync("https://www.coolgenerator.com/company-name-generator", formContent);
        }

        public string GenerateEIN()
        {
            var length = 9;
            var random = new Random();
            string s = string.Empty;
            for (int i = 0; i < length; i++)
                s = String.Concat(s, random.Next(10).ToString());
            return s;
        }
    }
}
