using Microsoft.AspNetCore.Mvc;
using WLVSTools.Web.Infrastructure.PersonalTools;
using WLVSTools.Web.ViewModels.Common;

namespace WLVSTools.Web.Controllers.api
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    public class PersonalToolboxController : ControllerBase
    {
        private readonly PersonalToolsDbContext _personalToolsDbContext;

        public PersonalToolboxController(PersonalToolsDbContext personalToolsDbContext)
        {
            _personalToolsDbContext = personalToolsDbContext;
        }

        public IActionResult Get()
        {
            return Ok(new { Message = "Hello" });
        }

        [HttpDelete("Account/{id:int}")]
        public async Task<IActionResult> Account(int id)
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
