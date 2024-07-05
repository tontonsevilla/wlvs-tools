﻿using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using System.Text;
using System.Web;
using System.Net;
using WLVSTools.Web.WebInfrastructure.Extensions;
using Microsoft.Extensions.Options;
using WLVSTools.Web.WebInfrastructure.Selenium;

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

        protected void AppendParameter(StringBuilder sb, string name, string value)
        {
            string encodedValue = HttpUtility.UrlEncode(value);
            sb.AppendFormat("{0}={1}&", name, encodedValue);
        }

        public string GenerateCompanyName(string keyword, string quantity)
        {
            const int maxTimeInSecondsToFindElement = 60;
            var chromeOptions = new ChromeOptionsWithPrefs();
            var defaultSettings = new Dictionary<string, object>();
            defaultSettings.Add("images", 2);
            chromeOptions.Prefs.Add("profile.default_content_settings", defaultSettings);
            chromeOptions.AddArguments("headless");

            IWebDriver driver = new ChromeDriver(@"C:\WebDrivers", chromeOptions);
            string pageSource = "";

            try
            {
                driver.Navigate().GoToUrl("https://www.coolgenerator.com/company-name-generator");
                var clickGenerate = false;

                if (!string.IsNullOrWhiteSpace(keyword))
                {
                    var keywordTextBox = driver.FindElement(By.Name("keyword"), maxTimeInSecondsToFindElement);
                    keywordTextBox.SendKeysCustom(keyword);
                    clickGenerate = true;
                }

                if (!string.IsNullOrWhiteSpace(quantity))
                {
                    var quantityTextBox = driver.FindElement(By.Name("quantity"), maxTimeInSecondsToFindElement);
                    quantityTextBox.SendKeysCustom(quantity);
                    clickGenerate= true;
                }

                if (clickGenerate)
                {
                    var generateButton = driver.FindElement(By.XPath("//button[.='Generate']"), maxTimeInSecondsToFindElement);
                    generateButton.Click();

                    driver.FindElement(By.ClassName("content-list"), maxTimeInSecondsToFindElement);
                }

                pageSource = driver.PageSource;
            }
            catch 
            {
                throw;
            }
            finally
            {
                driver.Close();
                driver.Dispose();
            }

            return pageSource;
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
