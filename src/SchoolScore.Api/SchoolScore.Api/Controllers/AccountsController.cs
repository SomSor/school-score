using Mapster;
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
    public class AccountsController : ApiControllerBase
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

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AccountCreate request)
        {
            var checkDoc = await accountDac.Get(x => x.Username == request.Username);
            if (checkDoc != null) return Conflict(new { Message = $"ไม่สำเร็จ มีรหัสผู้ใช้งาน {request.Username} นี้ในระบบแล้ว" });

            var documentDb = request.Adapt<DbModels.Account>();
            documentDb.Init(UserId);
            await accountDac.Create(documentDb);
            return Ok();
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var account = await accountDac.GetWithTeacher(teacherDac.Collection, x => x.Username == request.Username);

            if (!BCrypt.Net.BCrypt.Verify(request.Password, account.Password)) return BadRequest("เข้าสู้ระบบไม่สำเร็จ.");
            //if (!account.ActivateDate.HasValue) return BadRequest("ยังไม่ยืนยันการเปิดใช้งาน.");
            if (account.SuspendDate.HasValue) return BadRequest("รหัสผู้ใช้งานนี้ถูกระงับการใช้งาน.");

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

        [HttpGet("teachers/{teacherid}")]
        public async Task<ActionResult<Account>> GetAccount(string teacherid)
        {
            var account = await accountDac.GetWithTeacher(teacherDac.Collection, x => x.PersonId == teacherid);
            if (account == null) return null;
            account.Password = string.Empty;
            return account;
        }

        [HttpGet("teachers/{teacherid}/create")]
        public async Task<ActionResult<Account>> GetAccountForCreate(string teacherid)
        {
            var account = await accountDac.GetWithTeacher(teacherDac.Collection, x => x.PersonId == teacherid);
            if (account == null)
            {

                return new Account
                {
                    Username = $"teacher-{GenNo()}",
                    Password = GenNo(),
                    Roles = new[] { "Teacher" },
                    Teacher = await teacherDac.Get(x => x.Id == teacherid),
                };
            }
            else
            {
                account.Password = string.Empty;
                return account;
            }
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        private string GenNo(int selectCount = 3, int length = 4)
        {
            var random = new Random();
            var sampleSpace = new List<int> { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };

            var selectedNos = Enumerable.Empty<int>().ToList();
            for (int i = 0; i <= selectCount; i++)
            {
                var index = random.Next(sampleSpace.Count);
                selectedNos.Add(sampleSpace[index]);
                sampleSpace.Remove(sampleSpace[index]);
            }

            string generatedNombers = string.Empty;
            for (int i = 0; i < length; i++)
            {
                generatedNombers += selectedNos[random.Next(selectedNos.Count)];
            }

            return generatedNombers;
        }
    }
}
