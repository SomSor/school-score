using Mapster;
using Microsoft.AspNetCore.Mvc;
using SchoolScore.Api.DACs;
using SchoolScore.Api.Models;
using System.Linq.Expressions;

namespace SchoolScore.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LearningAreasController : ApiControllerBase
    {
        private readonly ILearningAreaDac<DbModels.LearningArea> learningAreaDac;
        private readonly ISubjectDac<DbModels.Subject> subjectDac;

        public LearningAreasController(
            ILearningAreaDac<DbModels.LearningArea> learningAreaDac,
            ISubjectDac<DbModels.Subject> subjectDac
            )
        {
            this.learningAreaDac = learningAreaDac;
            this.subjectDac = subjectDac;
        }

        [HttpGet]
        public async Task<ActionResult<PagingModel<DbModels.LearningArea>>> Get(string? search, int? page = 1, int? pageSize = 100)
        {
            if (string.IsNullOrWhiteSpace(search))
            {
                var data = page == 0
                    ? await learningAreaDac.ListAll(x => true)
                    : await learningAreaDac.List(x => true, page ?? 1, pageSize);
                var count = await learningAreaDac.Count(x => true);

                return Ok(new PagingModel<DbModels.LearningArea>
                {
                    Data = data,
                    Length = count,
                });
            }
            else
            {
                var txt = search.Trim().ToLower();
                Expression<Func<DbModels.LearningArea, bool>> func = x => true && x.Name.ToLower().Contains(txt);

                var data = page == 0
                    ? await learningAreaDac.ListAll(func)
                    : await learningAreaDac.List(func, page ?? 1, pageSize);
                var count = await learningAreaDac.Count(func);

                return Ok(new PagingModel<DbModels.LearningArea>
                {
                    Data = data,
                    Length = count,
                });
            }
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
            documentDb.Init(AccountsController.Username);
            documentDb.SchoolId = SchoolId;
            await learningAreaDac.Create(documentDb);
            return Ok();
        }

        [HttpPost("many")]
        public async Task<IActionResult> CreateMany([FromBody] IEnumerable<LearningAreaCreate> request)
        {
            var documentDbs = request.Adapt<IEnumerable<DbModels.LearningArea>>();
            documentDbs = documentDbs.Select(x =>
            {
                x.Init(AccountsController.Username);
                x.SchoolId = SchoolId;
                return x;
            }).ToList();
            await learningAreaDac.CreateMany(documentDbs);
            return Ok();
        }

        [HttpPost("text")]
        public async Task<IActionResult> ImportByText([FromBody] LearningAreaCreate request)
        {
            var rows = request.Name.Trim().Split(Environment.NewLine).Where(x => !string.IsNullOrWhiteSpace(x));
            var rowsSplit = rows.Select(x => x.Split("\t"));
            var documentDbs = rowsSplit.Select(x =>
            {
                var documentDb = new DbModels.LearningArea
                {
                    Name = x[0],
                    Description = x[1],
                    SchoolId = SchoolId,
                };
                documentDb.Init(AccountsController.Username);

                return documentDb;
            });

            await learningAreaDac.CreateMany(documentDbs);
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, [FromBody] LearningAreaCreate request)
        {
            var documentDb = await learningAreaDac.Get(x => x.Id == id);
            documentDb.Name = request.Name;
            documentDb.Description = request.Description;
            await learningAreaDac.ReplaceOne(x => x.Id == id, documentDb);
            return Ok();
        }

        [HttpPut("delete/{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var subjectCount = await subjectDac.Count(x => x.LearningAreaId == id);
            if (subjectCount > 0) return Conflict($"ไม่สามารถลบได้ มี {subjectCount} วิชา อยู่ในกลุ่มสาระการเรียนรู้นี้");

            await learningAreaDac.DeleteOne(x => x.Id == id);
            return Ok();
        }
    }
}
