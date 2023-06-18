using Mapster;
using Microsoft.AspNetCore.Mvc;
using SchoolScore.Api.DACs;
using SchoolScore.Api.Models;
using System.Linq.Expressions;

namespace SchoolScore.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SubjectsController : ApiControllerBase
    {
        private readonly ILearningAreaDac<DbModels.LearningArea> learningAreaDac;
        private readonly IOpenSubjectDac<DbModels.OpenSubject> openSubjectDac;
        private readonly ISujectDac<DbModels.Subject> sujectDac;

        public SubjectsController(
            ILearningAreaDac<DbModels.LearningArea> learningAreaDac,
            IOpenSubjectDac<DbModels.OpenSubject> openSubjectDac,
            ISujectDac<DbModels.Subject> sujectDac
            )
        {
            this.learningAreaDac = learningAreaDac;
            this.openSubjectDac = openSubjectDac;
            this.sujectDac = sujectDac;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PagingModel<Subject>>>> Get(string? search, int? page = 1, int? pageSize = 100)
        {
            if (string.IsNullOrWhiteSpace(search))
            {
                var data = page == 0
                    ? await sujectDac.ListWithLearningArea(learningAreaDac.Collection, x => true)
                    : await sujectDac.ListWithLearningArea(learningAreaDac.Collection, x => true, page ?? 1, pageSize);
                var count = await sujectDac.Count(x => true);

                return Ok(new PagingModel<Subject>
                {
                    Data = data,
                    Length = count,
                });
            }
            else
            {
                var txt = search.Trim().ToLower();
                Expression<Func<DbModels.Subject, bool>> func = x => true && x.Name.ToLower().Contains(txt);

                var data = page == 0
                    ? await sujectDac.ListWithLearningArea(learningAreaDac.Collection, func)
                    : await sujectDac.ListWithLearningArea(learningAreaDac.Collection, func, page ?? 1, pageSize);
                var count = await sujectDac.Count(func);

                return Ok(new PagingModel<Subject>
                {
                    Data = data,
                    Length = count,
                });
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Subject>> Get(string id)
        {
            var document = await sujectDac.GetWithLearningArea(learningAreaDac.Collection, x => x.Id == id);
            return Ok(document);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] SujectCreate request)
        {
            var documentDb = request.Adapt<DbModels.Subject>();
            documentDb.Init(AccountsController.Username);
            await sujectDac.Create(documentDb);
            return Ok();
        }

        [HttpPost("many")]
        public async Task<IActionResult> CreateMany([FromBody] IEnumerable<SujectCreate> request)
        {
            var documentDbs = request.Adapt<IEnumerable<DbModels.Subject>>();
            documentDbs = documentDbs.Select(x =>
            {
                x.Init(AccountsController.Username);
                return x;
            }).ToList();
            await sujectDac.CreateMany(documentDbs);
            return Ok();
        }

        [HttpPost("text")]
        public async Task<IActionResult> ImportByText([FromBody] SujectCreate request)
        {
            var rows = request.Name.Trim().Split(Environment.NewLine).Where(x => !string.IsNullOrWhiteSpace(x));
            var rowsSplit = rows.Select(x => x.Split("\t"));
            var documentDbs = rowsSplit.Select(x =>
            {
                var documentDb = new DbModels.Subject
                {
                    Code = x[0],
                    Name = x[1],
                    Description = x[2],
                    LearningAreaId = x[3],
                };
                documentDb.Init(AccountsController.Username);

                return documentDb;
            });

            await sujectDac.CreateMany(documentDbs);
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, [FromBody] SujectCreate request)
        {
            var documentDb = await sujectDac.Get(x => x.Id == id);
            documentDb.Code = request.Code;
            documentDb.Name = request.Name;
            documentDb.Description = request.Description;
            await sujectDac.ReplaceOne(x => x.Id == id, documentDb);
            return Ok();
        }

        [HttpPut("delete/{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var documentCount = await openSubjectDac.Count(x => x.SujectId == id);
            if (documentCount > 0) return Conflict($"ไม่สามารถลบได้ มี {documentCount} วิชา ที่เปิดอยู่");

            await sujectDac.DeleteOne(x => x.Id == id);
            return Ok();
        }
    }
}
