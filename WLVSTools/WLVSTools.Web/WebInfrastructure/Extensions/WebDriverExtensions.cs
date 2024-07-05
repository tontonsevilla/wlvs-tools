using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;

namespace WLVSTools.Web.WebInfrastructure.Extensions
{
    public static class WebDriverExtensions
    {
        public static IWebElement FindElement(this IWebDriver driver, By by, int timeoutInSeconds)
        {
            if (timeoutInSeconds > 0)
            {
                var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutInSeconds));
                return wait.Until(drv => drv.FindElement(by));
            }
            return driver.FindElement(by);
        }

        public static void SendKeysCustom(this IWebElement element, string value)
        {
            element.Click();
            element.Clear();
            element.SendKeys(value);
        }
    }
}
