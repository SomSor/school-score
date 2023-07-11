using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace SchoolScore.Api.Controllers
{
    [Authorize]
    public class ApiControllerBase : Controller
    {
        public string SchoolId = "638215692835215387-011673d9";

        private LoginContext _LoginContext { get; set; }
        public LoginContext LoginContext { get { return _LoginContext ??= GetLoginContext(); } }
        public string UserId { get { return (_LoginContext ??= GetLoginContext()).UserId; } }

        [ApiExplorerSettings(IgnoreApi = true)]
        public LoginContext GetLoginContext()
        {
            return new LoginContext
            {
                UserId = User?.FindFirst("UserId")?.Value,
                Name = User?.FindFirst("Name")?.Value,
                Roles = User?.FindAll(ClaimTypes.Role)
                    .Where(x => !string.IsNullOrWhiteSpace(x?.Value))?
                    .Select(x => x.Value)?
                    .ToList(),
            };
        }
    }

    public class LoginContext
    {
        /// <summary>
        /// account id
        /// </summary>
        public string? UserId { get; set; }
        /// <summary>
        /// name
        /// </summary>
        public string? Name { get; set; }
        /// <summary>
        /// admin, mod, teacher, student
        /// </summary>
        public IEnumerable<string>? Roles { get; set; }
    }
}
