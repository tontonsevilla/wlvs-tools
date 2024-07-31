using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using WLVSTools.Web.Core.General;
using WLVSTools.Web.Models.AIFS.StudyAbroad;
using WLVSTools.Web.WebInfrastructure.Extensions.Selenium;
using WLVSTools.Web.WebInfrastructure.Selenium.Interfaces;

namespace WLVSTools.Web.WebInfrastructure.Selenium.Automation.AIFS.StudyAbroad
{
    public class CollegeRegistrationAutomation : ISeleniumAutomation
    {
        public CollegeRegistrationAutomation(Registration data)
        {
            Data = data;
        }

        public Registration Data { get; private set; }

        public bool Headless { get; set; } = true;
        public bool EagerPageLoadStrategy { get; set; } = true;
        public IWebDriver WebDriver { get; set; }

        public ServiceResponse<ValueResponse<string>> Execute(int maxTimeInSecondsToFindElement = 60)
        {
            var response = new ServiceResponse<ValueResponse<String>>();

            WebDriver.Navigate().GoToUrl(Data.Url);

            WebDriver.FindElement(By.Id("regEmail"), maxTimeInSecondsToFindElement).SendKeysCustom(Data.Personalnfo.Email);

            var elementCitizenship = WebDriver.ElementVisible(By.XPath("//select[@id='Citizenship' and not(@disabled)]"), maxTimeInSecondsToFindElement);
            var ddlCitizenship = new SelectElement(elementCitizenship);
            ddlCitizenship.SelectByValue("US");

            WebDriver.FindElement(By.Id("regFirstName"), maxTimeInSecondsToFindElement).SendKeysCustom(Data.Personalnfo.FirstName);
            WebDriver.FindElement(By.Id("regLastName"), maxTimeInSecondsToFindElement).SendKeysCustom(Data.Personalnfo.LastName);
            WebDriver.FindElement(By.Id("regEmail"), maxTimeInSecondsToFindElement).SendKeysCustom(Data.Personalnfo.Email);

            WebDriver.FindElement(By.Id("regPassword"), maxTimeInSecondsToFindElement).SendKeysCustom(Data.Personalnfo.Password);
            WebDriver.FindElement(By.Id("regConfirmPassword"), maxTimeInSecondsToFindElement).SendKeysCustom(Data.Personalnfo.Password);

            var elementCollegeState = WebDriver.ElementVisible(By.XPath("//select[@id='ddlCollegeState' and not(@disabled)]"), maxTimeInSecondsToFindElement);
            var ddlCollegeState = new SelectElement(elementCollegeState);
            ddlCollegeState.SelectByValue(Data.Personalnfo.Address.State);

            var elementUniversity = WebDriver.ElementVisible(By.XPath("//select[@id='ddlCollegeId' and not(@disabled)]"), maxTimeInSecondsToFindElement);
            var ddlUniversity = new SelectElement(elementUniversity);
            ddlUniversity.SelectByIndex(1);

            var elementExpectedYearOfGraduation = WebDriver.ElementVisible(By.XPath("//select[@id='ddlExpectedYearOfGraduation' and not(@disabled)]"), maxTimeInSecondsToFindElement);
            var ddlExpectedYearOfGraduation = new SelectElement(elementExpectedYearOfGraduation);
            ddlExpectedYearOfGraduation.SelectByIndex(1);

            var elementMajor = WebDriver.ElementVisible(By.XPath("//select[@id='ddlMajor' and not(@disabled)]"), maxTimeInSecondsToFindElement);
            var ddlMajor = new SelectElement(elementMajor);
            ddlMajor.SelectByIndex(1);

            var elementProgramOptions = WebDriver.ElementVisible(By.XPath("//select[@id='ddlProgramOptions' and not(@disabled)]"), maxTimeInSecondsToFindElement);
            var ddlProgramOptions = new SelectElement(elementProgramOptions);
            ddlProgramOptions.SelectByIndex(1);

            return response;
        }
    }
}
