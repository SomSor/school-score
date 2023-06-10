using Microsoft.AspNetCore.Mvc;
using SchoolScore.Api.Helpers;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace SchoolScore.Api.Controllers
{
    [Route("api/[controller]")]
    public class AccountsController : Controller
    {
        public static string Username { get; set; } = "admin";
        public static string Password { get; set; } = "admin";

        private readonly JwtHandler _jwtHandler;
        public AccountsController(JwtHandler jwtHandler)
        {
            _jwtHandler = jwtHandler;
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            if (request.Username != Username && request.Password != Username)
            {
                return BadRequest("Login fail.");
            }

            var claims = new List<Claim>
            {
                new Claim("Id", request.Username),
                new Claim(ClaimTypes.Email, request.Username),
                new Claim(ClaimTypes.Name, request.Username),
                new Claim(ClaimTypes.Role, "Administrator"),
            };

            var signingCredentials = _jwtHandler.GetSigningCredentials();
            var tokenOptions = _jwtHandler.GenerateTokenOptions(signingCredentials, claims);
            var token = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
            return Ok(new { IsAuthSuccessful = true, Token = token });
        }
    }

    public class LoginRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
