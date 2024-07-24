using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using WLVSTools.Web.Core.General;
using WLVSTools.Web.Models.AIFS;
using WLVSTools.Web.WebInfrastructure.Extensions.Selenium;
using WLVSTools.Web.WebInfrastructure.Selenium.Interfaces;

namespace WLVSTools.Web.WebInfrastructure.Selenium.Automation.AIFS.ProfessionalPathways
{
    public class HostRegistrationAutomation : ISeleniumAutomation
    {
        public HostRegistrationAutomation(ProfessionalPathwaysRegistration data)
        {
            Data = data;
        }

        public ProfessionalPathwaysRegistration Data { get; private set; }

        public IWebDriver WebDriver { get; set; }
        public bool Headless { get; set; } = true;
        public bool EagerPageLoadStrategy { get; set; } = true;

        public ServiceResponse<ValueResponse<String>> Execute(int maxTimeInSecondsToFindElement = 60)
        {
            var response = new ServiceResponse<ValueResponse<String>>();
            bool isThirdParty = false;

            WebDriver.Navigate().GoToUrl(Data.Url);

            if (Data.Type?.ToUpper() == "CONTACT")
            {
                WebDriver.FindElement(By.XPath("//input[@value='Host' and @class='rdbApplicantType']"), maxTimeInSecondsToFindElement).Click();
            }
            else if (Data.Type?.ToUpper() == "THIRDPARTY")
            {
                WebDriver.FindElement(By.XPath("//input[@value='Third Party' and @class='rdbApplicantType']"), maxTimeInSecondsToFindElement).Click();
                isThirdParty = true;
            }
            else
            {
                response.AddErrorMessage("Invalid type.");
            }

            if (!response.HasError)
            {
                var personalInfo = Data.Personalnfo;

                WebDriver.FindElement(By.XPath("//input[@id='AccountName' and not(@disabled)]"), maxTimeInSecondsToFindElement).SendKeysCustom(personalInfo.Company.Name);
                WebDriver.FindElement(By.XPath("//input[@id='CompanyWebsite' and not(@disabled)]"), maxTimeInSecondsToFindElement).SendKeysCustom(personalInfo.Company.Url);
                WebDriver.FindElement(By.XPath("//input[@id='PointOfContactFirstName' and not(@disabled)]"), maxTimeInSecondsToFindElement).SendKeysCustom(personalInfo.FirstName);
                WebDriver.FindElement(By.XPath("//input[@id='PointOfContactLastName' and not(@disabled)]"), maxTimeInSecondsToFindElement).SendKeysCustom(personalInfo.LastName);
                WebDriver.FindElement(By.XPath("//input[@id='PointOfContactEmail' and not(@disabled)]"), maxTimeInSecondsToFindElement).SendKeysCustom(personalInfo.Email);
                WebDriver.FindElement(By.XPath("//input[@id='PointOfContactPhone' and not(@disabled)]"), maxTimeInSecondsToFindElement).SendKeysCustom(personalInfo.Phone);
                WebDriver.FindElement(By.XPath("//input[@id='Password' and not(@disabled)]"), maxTimeInSecondsToFindElement).SendKeysCustom(personalInfo.Password);
                WebDriver.FindElement(By.XPath("//input[@id='ConfirmPassword' and not(@disabled)]"), maxTimeInSecondsToFindElement).SendKeysCustom(personalInfo.Password);

                if (isThirdParty)
                {
                    WebDriver.FindElement(By.XPath("//input[@id='Title' and not(@disabled)]"), maxTimeInSecondsToFindElement).SendKeysCustom(personalInfo.Title);
                }

                var elementLeadSource = WebDriver.FindElement(By.XPath("//select[@id='ddlLeadSource' and not(@disabled)]"), maxTimeInSecondsToFindElement);
                var ddlLeadSource = new SelectElement(elementLeadSource);
                ddlLeadSource.SelectByIndex(1);

                WebDriver.FindElement(By.Id("btnSubmit"), maxTimeInSecondsToFindElement).Click();
            }

            return response;
        }
    }
}
