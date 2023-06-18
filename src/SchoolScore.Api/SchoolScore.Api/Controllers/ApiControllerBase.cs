using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SchoolScore.Api.Controllers
{
    //[Authorize]
    public class ApiControllerBase : Controller
    {
        public string SchoolId = "638215692835215387-011673d9";
        public string SchoolYearId = "638215692835215387-011673d9";
    }
}
