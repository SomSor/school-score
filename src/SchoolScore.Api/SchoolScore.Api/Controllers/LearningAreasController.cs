using Mapster;
using Microsoft.AspNetCore.Mvc;
using SchoolScore.Api.DACs;
using SchoolScore.Api.Models;

namespace SchoolScore.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LearningAreasController : ApiControllerBase
    {
        private readonly ILearningAreaDac<DbModels.LearningArea> learningAreaDac;

        public LearningAreasController(ILearningAreaDac<DbModels.LearningArea> learningAreaDac)
        {
            this.learningAreaDac = learningAreaDac;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<DbModels.LearningArea>>> Get()
        {
            var documentDbs = await learningAreaDac.List(x => true);
            var documents = documentDbs.Adapt<IEnumerable<DbModels.LearningArea>>();
            return Ok(documents);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DbModels.LearningArea>> Get(string id)
        {
            var documentDb = await learningAreaDac.Get(x => x.Id == id);
            var document = documentDb.Adapt<DbModels.LearningArea>();
            return Ok(document);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] LearningAreaCreate request)
        {
            var documentDb = request.Adapt<DbModels.LearningArea>();
            documentDb.Init("admin");
            documentDb.SchoolId = SchoolsController.SchoolId;
            await learningAreaDac.Create(documentDb);
            return Ok();
        }
    }
}
