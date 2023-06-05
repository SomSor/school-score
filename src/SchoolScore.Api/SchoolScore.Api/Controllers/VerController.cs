using Microsoft.AspNetCore.Mvc;

namespace SchoolScore.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VerController : Controller
    {
        [HttpGet]
        public async Task<IActionResult> Ver()
        {
            return Ok("0.8.0");
        }
    }
}
