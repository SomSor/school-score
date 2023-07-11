using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchoolScore.Api.DACs;
using SchoolScore.Api.Helpers;
using SchoolScore.Api.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace SchoolScore.Api.Controllers
{
    [Route("api/[controller]")]
    public class AccountsController : Controller
    {
        private readonly IAccountDac<DbModels.Account> accountDac;
        private readonly ITeacherDac<DbModels.Teacher> teacherDac;
        private readonly JwtHandler _jwtHandler;

        public AccountsController(
            IAccountDac<DbModels.Account> accountDac,
            ITeacherDac<DbModels.Teacher> teacherDac,
            JwtHandler jwtHandler
            )
        {
            this.accountDac = accountDac;
            this.teacherDac = teacherDac;
            _jwtHandler = jwtHandler;
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var account = await accountDac.GetWithTeacher(teacherDac.Collection, x => x.Username == request.Username && x.Password == request.Password);
            if (account == null)
            {
                return BadRequest("Login fail.");
            }

            var claims = new List<Claim>
            {
                new Claim("UserId", request.Username),
                new Claim("Name", $"{account.Teacher.Prefix}{account.Teacher.Name} {account.Teacher.Lastname}"),
            };
            claims.AddRange(account.Roles.Select(x => new Claim(ClaimTypes.Role, x)));

            var signingCredentials = _jwtHandler.GetSigningCredentials();
            var tokenOptions = _jwtHandler.GenerateTokenOptions(signingCredentials, claims);
            var token = new JwtSecurityTokenHandler().WriteToken(tokenOptions);

            return Ok(new { IsAuthSuccessful = true, Token = token });
        }
    }
}
