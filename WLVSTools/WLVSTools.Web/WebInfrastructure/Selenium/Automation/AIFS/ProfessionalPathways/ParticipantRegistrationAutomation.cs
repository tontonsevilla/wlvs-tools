using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using WLVSTools.Web.Core.General;
using WLVSTools.Web.Models.AIFS;
using WLVSTools.Web.Models.AIFS.ProfessionalPathways;
using WLVSTools.Web.WebInfrastructure.Extensions.Selenium;
using WLVSTools.Web.WebInfrastructure.Selenium.Interfaces;

namespace WLVSTools.Web.WebInfrastructure.Selenium.Automation.AIFS.ProfessionalPathways
{
    public class ParticipantRegistrationAutomation : ISeleniumAutomation
    {
        public ParticipantRegistrationAutomation(ParticipatRegistration data)
        {
            Data = data;
        }

        public ParticipatRegistration Data { get; private set; }

        public IWebDriver WebDriver { get; set; }
        public bool Headless { get; set; } = true;
        public bool EagerPageLoadStrategy { get; set; } = true;

        public ServiceResponse<ValueResponse<String>> Execute(int maxTimeInSecondsToFindElement = 60)
        {
            var response = new ServiceResponse<ValueResponse<String>>();

            WebDriver.Navigate().GoToUrl(Data.Url);

            WebDriver.FindElement(By.XPath("//input[@value='True' and @class='HaveUSProfessionalTrainingOpportunity']"), maxTimeInSecondsToFindElement).Click();

            var personalInfo = Data.Personalnfo;

            WebDriver.FindElement(By.XPath("//input[@id='FirstName' and not(@disabled)]"), maxTimeInSecondsToFindElement).SendKeysCustom(personalInfo.FirstName);
            WebDriver.FindElement(By.XPath("//input[@id='LastName' and not(@disabled)]"), maxTimeInSecondsToFindElement).SendKeysCustom(personalInfo.LastName);
            WebDriver.FindElement(By.XPath("//input[@id='HomePhone' and not(@disabled)]"), maxTimeInSecondsToFindElement).SendKeysCustom(personalInfo.Phone);
            WebDriver.FindElement(By.XPath("//input[@id='MobilePhone' and not(@disabled)]"), maxTimeInSecondsToFindElement).SendKeysCustom(personalInfo.MobilePhone);
            WebDriver.FindElement(By.XPath("//input[@id='Email' and not(@disabled)]"), maxTimeInSecondsToFindElement).SendKeysCustom(personalInfo.Email);
            WebDriver.FindElement(By.XPath("//input[@id='Password' and not(@disabled)]"), maxTimeInSecondsToFindElement).SendKeysCustom(personalInfo.Password);
            WebDriver.FindElement(By.XPath("//input[@id='ConfirmPassword' and not(@disabled)]"), maxTimeInSecondsToFindElement).SendKeysCustom(personalInfo.Password);
            WebDriver.FindElement(By.XPath("//input[@id='USHostCompany' and not(@disabled)]"), maxTimeInSecondsToFindElement).SendKeysCustom(personalInfo.Company.Name);

            var elementCitizenship = WebDriver.FindElement(By.XPath("//select[@id='ddlCountry' and not(@disabled)]"), maxTimeInSecondsToFindElement);
            var ddlCitizenship = new SelectElement(elementCitizenship);
            ddlCitizenship.SelectByValue(personalInfo.Address.SevisCountry);

            var elementLeadSource = WebDriver.FindElement(By.XPath("//select[@id='ddlLeadSource' and not(@disabled)]"), maxTimeInSecondsToFindElement);
            var ddlLeadSource = new SelectElement(elementLeadSource);
            ddlLeadSource.SelectByIndex(1);

            WebDriver.FindElement(By.XPath("//button[@type='submit']"), maxTimeInSecondsToFindElement).Click();

            return response;
        }
    }
}
