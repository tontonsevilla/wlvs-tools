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

            WebDriver.FindElement(By.Id("btnRegister")).Click();

            var elementCitizenship = WebDriver.ElementVisible(By.XPath("//select[@id='Citizenship' and not(@disabled)]"), maxTimeInSecondsToFindElement);
            var ddlCitizenship = new SelectElement(elementCitizenship);
            ddlCitizenship.SelectByValue("US");

            WebDriver.FindElement(By.Id("regFirstName"), maxTimeInSecondsToFindElement).SendKeysCustom(Data.Personalnfo.FirstName);
            WebDriver.FindElement(By.Id("regLastName"), maxTimeInSecondsToFindElement).SendKeysCustom(Data.Personalnfo.LastName);

            WebDriver.FindElement(By.Id("regPassword"), maxTimeInSecondsToFindElement).SendKeysCustom(Data.Personalnfo.Password);
            WebDriver.FindElement(By.Id("regConfirmPassword"), maxTimeInSecondsToFindElement).SendKeysCustom(Data.Personalnfo.Password);

            var elementCollegeState = WebDriver.ElementVisible(By.XPath("//select[@id='ddlCollegeState' and not(@disabled)]"), maxTimeInSecondsToFindElement);
            var ddlCollegeState = new SelectElement(elementCollegeState);
            ddlCollegeState.SelectByValue(Data.Personalnfo.Address.State);

            WebDriver.WaitForAjax(maxTimeInSecondsToFindElement);

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

            WebDriver.WaitForAjax(maxTimeInSecondsToFindElement);

            var elementProgram = WebDriver.ElementVisible(By.XPath("//select[@id='ddlProgram' and not(@disabled)]"), maxTimeInSecondsToFindElement);
            var ddlProgram = new SelectElement(elementProgram);
            ddlProgram.SelectByIndex(1);

            WebDriver.WaitForAjax(maxTimeInSecondsToFindElement);

            var elementSemester = WebDriver.ElementVisible(By.XPath("//select[@id='ddlSemester' and not(@disabled)]"), maxTimeInSecondsToFindElement);
            var ddlSemester = new SelectElement(elementSemester);
            ddlSemester.SelectByIndex(1);

            var elementReferral = WebDriver.ElementVisible(By.XPath("//select[@id='ddlReferral' and not(@disabled)]"), maxTimeInSecondsToFindElement);
            var ddlReferral = new SelectElement(elementReferral);
            ddlReferral.SelectByIndex(1);

            var elementPrimiaryCareerField = WebDriver.ElementVisible(By.XPath("//select[@id='ddlPrimaryCareerField' and not(@disabled)]"), maxTimeInSecondsToFindElement);
            var ddlPrimaryCareerField = new SelectElement(elementPrimiaryCareerField);
            ddlPrimaryCareerField.SelectByIndex(1);

            var elementSecondaryCareerField = WebDriver.ElementVisible(By.XPath("//select[@id='ddlSecondaryCareerField' and not(@disabled)]"), maxTimeInSecondsToFindElement);
            var ddlSecondaryCareerField = new SelectElement(elementSecondaryCareerField);
            ddlSecondaryCareerField.SelectByIndex(1);

            WebDriver.FindElementClickable(By.Id("btnRegister"), maxTimeInSecondsToFindElement).Click();

            return response;
        }
    }
}
