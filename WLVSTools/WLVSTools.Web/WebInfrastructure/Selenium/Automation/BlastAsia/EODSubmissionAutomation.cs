using OpenQA.Selenium;
using WLVSTools.Web.Models.BlastAsia;
using WLVSTools.Web.WebInfrastructure.Extensions;
using WLVSTools.Web.WebInfrastructure.Selenium.Interfaces;

namespace WLVSTools.Web.WebInfrastructure.Selenium.Automation.BlastAsia
{
    public class EODSubmissionAutomation : ISeleniumAutomation
    {
        public EODSubmissionAutomation(EndOfDay data)
        {
            Data = data;
        }

        public EndOfDay Data { get; private set; }
        public IWebDriver WebDriver { get; set; }

        public void Execute(int maxTimeInSecondsToFindElement = 60)
        {
            WebDriver.Navigate().GoToUrl("https://live.quickreach.co/");

            //LOGIN
            WebDriver.FindElement(By.Id("inputEmail1"), maxTimeInSecondsToFindElement).SendKeysCustom(Data.Email);
            WebDriver.FindElement(By.Id("inputPassword")).SendKeysCustom(Data.Password);
            WebDriver.FindElement(By.CssSelector("button[data-cy='lg-submit-btn']")).Click();

            //CREATE REQUEST
            WebDriver.FindElementClickable(By.CssSelector("button[mattooltip='File New Request']"), maxTimeInSecondsToFindElement).Click();
            if (WebDriver.SpinnerChecker(By.Id("spinner"), maxTimeInSecondsToFindElement))
            {
                WebDriver.FindElement(By.XPath("//*[@id=\"mat-dialog-0\"]/app-workflow-selection-wizard/section/div[2]/div/div[3]/section[2]/div/div/mat-card[4]"), maxTimeInSecondsToFindElement).Click();
            }            
        }
    }
}
