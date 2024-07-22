using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using WLVSTools.Web.Models.AIFS;
using WLVSTools.Web.WebInfrastructure.Extensions;
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
        public void Execute(int maxTimeInSecondsToFindElement = 60)
        {
            bool isThirdParty = false;

            WebDriver.Navigate().GoToUrl(Data.Url);

            if (Data.Type?.ToUpper() == "CONTACT")
            {
                WebDriver.FindElement(By.CssSelector(".rdbApplicantType[value='Host']"), maxTimeInSecondsToFindElement).Click();
            }
            else if (Data.Type?.ToUpper() == "THIRDPARTY")
            {
                WebDriver.FindElement(By.CssSelector(".rdbApplicantType[value='Third Party']"), maxTimeInSecondsToFindElement).Click();
                isThirdParty = true;
            }
            else
            {
                throw new Exception("Invalid Type");
            }

            var personalInfo = Data.Personalnfo;

            if (isThirdParty)
            {

            }
            else
            {
                WebDriver.FindElement(By.Id("AccountName"), maxTimeInSecondsToFindElement).SendKeys(personalInfo.Company.Name);
                WebDriver.FindElement(By.Id("CompanyWebsite"), maxTimeInSecondsToFindElement).SendKeys(personalInfo.Company.Url);
                WebDriver.FindElement(By.Id("PointOfContactFirstName"), maxTimeInSecondsToFindElement).SendKeys(personalInfo.FirstName);
                WebDriver.FindElement(By.Id("PointOfContactLastName"), maxTimeInSecondsToFindElement).SendKeys(personalInfo.LastName);
                WebDriver.FindElement(By.Id("PointOfContactEmail"), maxTimeInSecondsToFindElement).SendKeys(personalInfo.Email);
                WebDriver.FindElement(By.Id("PointOfContactPhone"), maxTimeInSecondsToFindElement).SendKeys(personalInfo.Phone);
                WebDriver.FindElement(By.Id("Password"), maxTimeInSecondsToFindElement).SendKeys(personalInfo.Password);
                WebDriver.FindElement(By.Id("ConfirmPassword"), maxTimeInSecondsToFindElement).SendKeys(personalInfo.Password);

                var elementLeadSource = WebDriver.FindElement(By.Id("ddlLeadSource"), maxTimeInSecondsToFindElement);
                var ddlLeadSource = new SelectElement(elementLeadSource);
                ddlLeadSource.SelectByIndex(1);

                WebDriver.FindElement(By.Id("btnSubmit"), maxTimeInSecondsToFindElement).Click();
            }
        }
    }
}
