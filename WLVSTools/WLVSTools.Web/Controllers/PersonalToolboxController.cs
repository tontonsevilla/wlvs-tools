using AutoMapper;
using HtmlAgilityPack;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using WLVSTools.Web.Core.Data.PersonalToolsEntities;
using WLVSTools.Web.Core.General;
using WLVSTools.Web.Infrastructure.Authentication;
using WLVSTools.Web.Infrastructure.PersonalTools;
using WLVSTools.Web.ViewModels.PersonalToolbox;

namespace WLVSTools.Web.Controllers
{
    [Authorize]
    public class PersonalToolboxController : BaseController
    {
        private readonly IWebDriver webDriver;
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly PersonalToolsDbContext _personalToolsDbContext;

        public PersonalToolboxController(
            IMapper mapper,
            UserManager<ApplicationUser> userManager,
            PersonalToolsDbContext personalToolsDbContext) : base(userManager)
        {
            webDriver = new ChromeDriver();
            _mapper = mapper;
            _userManager = userManager;
            _personalToolsDbContext = personalToolsDbContext;
        }

        public IActionResult CreateAccount()
        {
            return View(new AccountViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> CreateAccount(AccountViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var account = new Account();
                    account.Create(viewModel.Name, viewModel.Description, viewModel.UserId, viewModel.Password, await GetUserId());

                    _personalToolsDbContext.Accounts.Add(account);
                    var result = await _personalToolsDbContext.SaveChangesAsync();

                    if (result > 0)
                    {
                        return RedirectToAction("AccountList");
                    }
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                    GetInnerException(ex);
                }

                return View(viewModel);
            }

            ModelState.AddModelError("", string.Join("; ", ModelState.Values
                                        .SelectMany(x => x.Errors)
                                        .Select(x => x.ErrorMessage)));

            return View(viewModel);
        }

        public IActionResult AccountList()
        {
            var accounts = _mapper.Map<IEnumerable<AccountViewModel>>(_personalToolsDbContext.Accounts.AsEnumerable());
            return View(accounts);
        }

        public IActionResult ContactList()
        {
            return View();
        }

        public IActionResult Lotto()
        {
            return loadLotto(new LottoViewModel());
        }

        [HttpPost]
        public IActionResult Lotto(LottoViewModel viewModel)
        {
            viewModel.HasFilter = true;
            return loadLotto(viewModel);
        }

        private IActionResult loadLotto(LottoViewModel viewModel)
        {
            getPCSOLottoResultHtmlString(viewModel);

            var htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(webDriver.PageSource);

            while (!htmlDoc.DocumentNode.HasChildNodes)
            {
                htmlDoc.LoadHtml(webDriver.PageSource);
            }

            disposeWebDriver();

            var node = htmlDoc.DocumentNode.SelectSingleNode("//*[@id=\"cphContainer_cpContent_GridView1\"]");

            cleanNode(node);

            node.Attributes.Add("class", "table table-striped");

            viewModel.HtmlStringOutput = new HtmlString(node.OuterHtml);

            return View("Lotto", viewModel);
        }

        private void getPCSOLottoResultHtmlString(LottoViewModel viewModel)
        {
            var wait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(5));
            webDriver.Navigate().GoToUrl(@"https://www.pcso.gov.ph/SearchLottoResult.aspx");

            var html = webDriver.FindElement(By.TagName("html"), 5);

            var elementSelectGame = webDriver.FindElement(By.Id("cphContainer_cpContent_ddlSelectGame"), 5);
            var ddlSelectGame = new SelectElement(elementSelectGame);
            ddlSelectGame.SelectByValue(viewModel.LottoGame);

            if (viewModel.HasFilter)
            {
                //Start Date
                var elementStartMonth = webDriver.FindElement(By.Id("cphContainer_cpContent_ddlStartMonth"), 5);
                var ddlStartMonth = new SelectElement(elementStartMonth);
                ddlStartMonth.SelectByValue(viewModel.StartMonth);

                var elementStartDate = webDriver.FindElement(By.Id("cphContainer_cpContent_ddlStartDate"), 5);
                var ddlStartDate = new SelectElement(elementStartDate);
                ddlStartDate.SelectByValue(viewModel.StartDate);

                var elementStartYear = webDriver.FindElement(By.Id("cphContainer_cpContent_ddlStartYear"), 5);
                var ddlStartYear = new SelectElement(elementStartYear);
                ddlStartYear.SelectByValue(viewModel.StartYear);

                //End Date
                var elementEndMonth = webDriver.FindElement(By.Id("cphContainer_cpContent_ddlEndMonth"), 5);
                var ddlEndMonth = new SelectElement(elementEndMonth);
                ddlEndMonth.SelectByValue(viewModel.EndMonth);

                var elementEndDate = webDriver.FindElement(By.Id("cphContainer_cpContent_ddlEndDay"), 5);
                var ddlEndDate = new SelectElement(elementEndDate);
                ddlEndDate.SelectByValue(viewModel.EndDate);

                var elementEndYear = webDriver.FindElement(By.Id("cphContainer_cpContent_ddlEndYear"), 5);
                var ddlEndYear = new SelectElement(elementEndYear);
                ddlEndYear.SelectByValue(viewModel.EndYear);
            }

            var btnSearchLotto = webDriver.FindElement(By.Id("cphContainer_cpContent_btnSearch"), 5);
            btnSearchLotto.Click();
        }

        private void disposeWebDriver()
        {
            webDriver.Quit();
            webDriver.Dispose();
        }

        private void cleanNode(HtmlNode node)
        {
            if (node != null)
            {
                if (node.HasChildNodes)
                {
                    foreach (var childNode in node.ChildNodes)
                    {
                        cleanNode(childNode);
                        node.Attributes.Remove();
                    }
                }
                else
                {
                    node.Attributes.Remove();
                }
            }
        }
    }
}