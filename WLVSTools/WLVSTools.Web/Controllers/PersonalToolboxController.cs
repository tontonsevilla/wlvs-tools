using AutoMapper;
using HtmlAgilityPack;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WLVSTools.Web.Core.Data.PersonalToolsEntities;
using WLVSTools.Web.Core.Models;
using WLVSTools.Web.Infrastructure.Authentication;
using WLVSTools.Web.Infrastructure.PersonalTools;
using WLVSTools.Web.ViewModels.PersonalToolbox;

namespace WLVSTools.Web.Controllers
{
    [Authorize]
    public class PersonalToolboxController : BaseController
    {
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly PersonalToolsDbContext _personalToolsDbContext;

        public PersonalToolboxController(
            IMapper mapper,
            UserManager<ApplicationUser> userManager,
            PersonalToolsDbContext personalToolsDbContext) : base(userManager)
        {
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
            var html = @"https://www.pcso.gov.ph/SearchLottoResult.aspx";

            HtmlWeb web = new HtmlWeb();

            var htmlDoc = web.Load(html);

            var node = htmlDoc.DocumentNode.SelectSingleNode("//*[@id=\"cphContainer_cpContent_GridView1\"]");

            cleanNode(node);

            node.Attributes.Add("class", "table table-striped");

            return View(new HtmlString(node.OuterHtml));
        }

        private void cleanNode(HtmlNode node)
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
