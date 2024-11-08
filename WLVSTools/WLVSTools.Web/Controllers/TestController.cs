using Microsoft.AspNetCore.Mvc;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using WLVSTools.Web.WebInfrastructure.Selenium.Interfaces;

namespace WLVSTools.Web.Controllers
{
    public class TestController : Controller
    {
        public IActionResult Index()
        {
            ChromeOptions options = new ChromeOptions();
            options.AddExtension(@"C:\Selenium\Chrome\Extensions\AdBlock_6.9.2.0.crx");
            options.AddArguments("headless=new");
            options.PageLoadStrategy = PageLoadStrategy.Eager;
            IWebDriver webDriver = new ChromeDriver(@"C:\Selenium\Chrome\WebDriver", options);

            try
            {
                webDriver.Navigate().GoToUrl($"https://www.coolgenerator.com/fake-name-generator");
            }
            catch (Exception)
            {
                Screenshot screenshot = OpenQA.Selenium.Support.Extensions.WebDriverExtensions.TakeScreenshot(webDriver);
                screenshot.SaveAsFile($"Screenshot/SS_Error_{DateTime.Now.ToString("yyyyMMddhhmmss")}.png");

                throw;
            }
            finally
            {
                webDriver.Close();
                webDriver.Dispose();
            }

            return View();
        }
    }
}
