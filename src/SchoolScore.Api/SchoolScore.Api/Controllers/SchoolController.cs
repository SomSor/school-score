using Mapster;
using Microsoft.AspNetCore.Mvc;
using SchoolScore.Api.DACs;
using SchoolScore.Api.Models;

namespace SchoolScore.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SchoolController : Controller
    {
        private readonly ISchoolDac<DbModels.School> schoolDac;

        public SchoolController(ISchoolDac<DbModels.School> schoolDac)
        {
            this.schoolDac = schoolDac;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<DbModels.School>>> Get()
        {
            var documentDbs = await schoolDac.List(x => true);
            var documents = documentDbs.Adapt<IEnumerable<DbModels.School>>();
            return Ok(documents);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DbModels.School>> Get(string id)
        {
            var documentDb = await schoolDac.Get(x => x.Id == id);
            var document = documentDb.Adapt<DbModels.School>();
            return Ok(document);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] SchoolCreate request)
        {
            var documentDb = request.Adapt<DbModels.School>();
            documentDb.Init("admin");
            await schoolDac.Create(documentDb);
            return Ok();
        }
    }
}
