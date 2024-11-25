using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using UserManager.Api.Commands;
using UserManager.Api.Options;

namespace UserManager.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(IOptions<JwtAuthenticationOptions> options) : ControllerBase
    {
        [HttpPost("Login")]
        public IActionResult Login(LoginCommand loginCommand)
        {
            if (!ValidCredentials(loginCommand))
            {
                return Unauthorized();
            }

            var secretKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(options.Value.SecretKey));
            var signingCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, options.Value.Subject),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                new Claim("Email", loginCommand.Email),
            };

            var token = new JwtSecurityToken(
                options.Value.Issuer,
                options.Value.Audience,
                claims,
                DateTime.UtcNow,
                DateTime.UtcNow.AddHours(1),
                signingCredentials);

            return Ok(new JwtSecurityTokenHandler().WriteToken(token));
        }

        private static bool ValidCredentials(LoginCommand loginCommand)
        {
            // TODO : Validate password.
            Console.WriteLine($"Login attempt for: {loginCommand.Email}");
            return true;
        }
    }
}
