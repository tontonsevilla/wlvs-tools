
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WLVSTools.Web.Infrastructure.Authentication;
using WLVSTools.Web.ViewModels.Accounts;

namespace WLVSTools.Web.Controllers.api
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    public class SecurityController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly UserManager<ApplicationUser> _userManager;

        public SecurityController(
            IConfiguration configuration,
            UserManager<ApplicationUser> userManager)
        {
            _configuration = configuration;
            _userManager = userManager;
        }

        [HttpPost("Token")]
        public async Task<IActionResult> CreateToken(LoginViewModel user)
        {
            var applicationUser = await _userManager.FindByEmailAsync(user.Email);
            var passwordResult = _userManager.PasswordHasher.VerifyHashedPassword(
                applicationUser,
                applicationUser.PasswordHash,
                user.Password);

            if (passwordResult == PasswordVerificationResult.Success)
            {
                var issuer = _configuration["Jwt:Issuer"];
                var audience = _configuration["Jwt:Audience"];
                var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new[]
                    {
                        new Claim("Id", applicationUser.Id),
                        new Claim(JwtRegisteredClaimNames.Sub, applicationUser.Email),
                        new Claim(JwtRegisteredClaimNames.Email, applicationUser.Email),
                        new Claim(JwtRegisteredClaimNames.Jti,
                        Guid.NewGuid().ToString())
                    }),
                    Expires = DateTime.UtcNow.AddMinutes(1),
                    Issuer = issuer,
                    Audience = audience,
                    SigningCredentials = new SigningCredentials(
                        new SymmetricSecurityKey(key),
                        SecurityAlgorithms.HmacSha512Signature)
                };
                var tokenHandler = new JwtSecurityTokenHandler();
                var token = tokenHandler.CreateToken(tokenDescriptor);
                var jwtToken = tokenHandler.WriteToken(token);

                return Ok(jwtToken);
            }

            return Unauthorized();
        }
    }
}
