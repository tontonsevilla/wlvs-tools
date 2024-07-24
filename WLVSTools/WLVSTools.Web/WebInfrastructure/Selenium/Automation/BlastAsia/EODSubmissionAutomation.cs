using OpenQA.Selenium;
using WLVSTools.Web.Core.General;
using WLVSTools.Web.Models.BlastAsia;
using WLVSTools.Web.WebInfrastructure.Extensions;
using WLVSTools.Web.WebInfrastructure.Extensions.Selenium;
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
        public bool Headless { get; set; } = true;
        public bool EagerPageLoadStrategy { get; set; } = true;

        public ServiceResponse<ValueResponse<String>> Execute(int maxTimeInSecondsToFindElement = 60)
        {
            var response = new ServiceResponse<ValueResponse<String>>();

            WebDriver.Navigate().GoToUrl("https://live.quickreach.co/");

            //LOGIN
            WebDriver.FindElement(By.Id("inputEmail1"), maxTimeInSecondsToFindElement).SendKeysCustom(Data.Email);
            WebDriver.FindElement(By.Id("inputPassword")).SendKeysCustom(Data.Password);
            WebDriver.FindElement(By.CssSelector("button[data-cy='lg-submit-btn']")).Click();

            IWebElement validationMessageElement = WebDriver.ElementVisible(By.ClassName("validation-summary-errors"), maxTimeInSecondsToFindElement);

            if (validationMessageElement == null)
            {
                //CREATE REQUEST
                WebDriver.FindElementClickable(By.CssSelector("button[mattooltip='File New Request']"), maxTimeInSecondsToFindElement).Click();
                if (WebDriver.IsElementNotVisible(By.Id("spinner"), maxTimeInSecondsToFindElement))
                {
                    WebDriver.FindElement(By.XPath("//*[@id=\"mat-dialog-0\"]/app-workflow-selection-wizard/section/div[2]/div/div[3]/section[2]/div/div/mat-card[4]"), maxTimeInSecondsToFindElement).Click();

                    if (WebDriver.IsElementNotVisible(By.Id("spinner"), maxTimeInSecondsToFindElement))
                    {
                        WebDriver.FindElement(By.CssSelector("input[placeholder='Subject']"), maxTimeInSecondsToFindElement).SendKeysCustom(Data.Subject);
                        WebDriver.FindElementClickable(By.CssSelector("mat-datepicker-toggle[data-cy='datepicker-toggle-EODDate']"), maxTimeInSecondsToFindElement).Click();
                        WebDriver.FindElementClickable(By.CssSelector($"td[class*='mat-calendar-body-cell'][aria-label='{Data.EODDate?.ToString("dd-MM-yyyy")}']"), maxTimeInSecondsToFindElement).Click();
                        WebDriver.FindElementClickable(By.XPath("//mat-label[contains(text(),'Account')]/ancestor::mat-form-field"), maxTimeInSecondsToFindElement).Click();
                        WebDriver.FindElementClickable(By.CssSelector($"mat-option[data-cy='{Data.EODAccount}']"), maxTimeInSecondsToFindElement).Click();

                        //ADD ITEM
                        foreach (var item in Data.TaskItems)
                        {
                            WebDriver.FindElementClickable(By.XPath("//span[@mattooltip='Add New Record']/ancestor::button"), maxTimeInSecondsToFindElement).Click();
                            WebDriver.FindElement(By.CssSelector("textarea[data-cy='DescriptionEOD']"), maxTimeInSecondsToFindElement).SendKeysCustom(item.Description);
                            WebDriver.FindElement(By.CssSelector("input[data-cy='NoofHoursEOD']"), maxTimeInSecondsToFindElement).SendKeysCustom(item.NoOfHours.ToSafeString());
                            WebDriver.FindElement(By.CssSelector("input[data-cy='NoofMinutesEOD']"), maxTimeInSecondsToFindElement).SendKeysCustom(item.NoOfMinutes.ToSafeString());
                            WebDriver.FindElementClickable(By.XPath("//span[@mattooltip='Save']/ancestor::button"), maxTimeInSecondsToFindElement).Click();
                        }

                        //FOR DEBUGGING PURPOSES ONLY (Make sure to disable headless)
                        //WebDriver.ScrollToBottom();
                        //WebDriver.TakeScreenShot(this);
                    }
                }
            }
            else
            {
                var validationMessage = WebDriver
                    .FindElement(By.ClassName("validation-summary-errors"), maxTimeInSecondsToFindElement)
                    .GetHtmlContent(WebDriver);

                response.AddModel(new ValueResponse<string>(validationMessage));
            }

            return response;
        }
    }
}
