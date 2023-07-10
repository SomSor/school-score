using Mapster;
using Microsoft.AspNetCore.Mvc;
using SchoolScore.Api.DACs;
using SchoolScore.Api.DACs.Imps;
using SchoolScore.Api.Models;

namespace SchoolScore.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SchoolsController : ApiControllerBase
    {
        private readonly ISchoolDac<DbModels.School> schoolDac;

        public SchoolsController(ISchoolDac<DbModels.School> schoolDac)
        {
            this.schoolDac = schoolDac;
        }

        [HttpGet]
        public async Task<ActionResult<DbModels.School>> Get()
        {
            var documentDb = await schoolDac.Get(x => x.Id == SchoolId);
            var document = documentDb.Adapt<DbModels.School>();
            return Ok(document);
        }

        [HttpPut]
        public async Task<IActionResult> Update(string id, [FromBody] SchoolCreate request)
        {
            var documentDb = await schoolDac.Get(x => x.Id == SchoolId);
            documentDb.Name = request.Name;
            documentDb.Address = request.Address;
            documentDb.Area = request.Area;
            await schoolDac.ReplaceOne(x => x.Id == id, documentDb);
            return Ok();
        }
    }
}
