using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SchoolScore.Api.Controllers
{
    [Authorize]
    public class ApiControllerBase : Controller
    {
    }
}
