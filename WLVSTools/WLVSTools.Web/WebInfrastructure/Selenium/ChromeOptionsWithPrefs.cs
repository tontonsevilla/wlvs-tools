using OpenQA.Selenium.Chrome;

namespace WLVSTools.Web.WebInfrastructure.Selenium
{
    public class ChromeOptionsWithPrefs : ChromeOptions
    {
        public Dictionary<string, object> Prefs { get; set; } = new Dictionary<string, object>();
    }
}
