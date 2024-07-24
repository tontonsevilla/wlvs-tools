using OpenQA.Selenium;

namespace WLVSTools.Web.WebInfrastructure.Extensions.Selenium
{
    public static class WebElementExtensions
    {
        public static string GetHtmlContent(this IWebElement element, IWebDriver driver)
        {
            var innerHtml = string.Empty;
            var js = driver as IJavaScriptExecutor;
            
            if (js != null)
            {
                innerHtml = (string)js.ExecuteScript("return arguments[0].innerHTML;", element);
            }

            return innerHtml;
        }
    }
}
