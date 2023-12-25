using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WLVSTools.Web.Core.Models.Common;
using WLVSTools.Web.Infrastructure.PersonalTools;
using WLVSTools.Web.ViewModels.PersonalToolbox;

namespace WLVSTools.Web.Controllers.api
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    public class PersonalToolboxController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly PersonalToolsDbContext _personalToolsDbContext;

        public PersonalToolboxController(
            IMapper mapper,
            PersonalToolsDbContext personalToolsDbContext)
        {
            _mapper = mapper;
            _personalToolsDbContext = personalToolsDbContext;
        }

        public IActionResult Get()
        {
            var response = new ServiceResponse<EmptyResponse>();

            return Ok(response);
        }

        [HttpGet("Accounts")]
        public IActionResult GetAccounts()
        {
            var accounts = _personalToolsDbContext.Accounts.ToList();
            var accountsViewModel = _mapper.Map<List<AccountViewModel>>(accounts);
            var response = new ServiceResponse<List<AccountViewModel>>();

            response.AddModel(accountsViewModel);

            return Ok(response);
        }

        [HttpGet("Account/{id:int}")]
        public async Task<IActionResult> GetAccount(int id)
        {
            var account = await _personalToolsDbContext.Accounts.FindAsync(id);
            var accountViewModel = _mapper.Map<AccountViewModel>(account);
            var response = new ServiceResponse<AccountViewModel>();

            response.AddModel(accountViewModel);

            return Ok(response);
        }

        [HttpDelete("Account/{id:int}")]
        public async Task<IActionResult> DeleteAccount(int id)
        {
            var account = await _personalToolsDbContext.Accounts.FindAsync(id);

            if (account != null)
            {
                _personalToolsDbContext.Accounts.Remove(account);
                _personalToolsDbContext.SaveChanges();
            }

            return Ok();
        }
    }
}
