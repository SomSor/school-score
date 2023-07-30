using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchoolScore.Api.DACs;
using SchoolScore.Api.DACs.Imps;
using SchoolScore.Api.Models;
using System.Linq.Expressions;

namespace SchoolScore.Api.Controllers
{
    [Authorize(Roles = "Admin,Mod")]
    [ApiController]
    [Route("api/[controller]")]
    public class SubjectsController : ApiControllerBase
    {
        private readonly ILearningAreaDac<DbModels.LearningArea> learningAreaDac;
        private readonly IOpenSubjectDac<DbModels.OpenSubject> openSubjectDac;
        private readonly ISubjectDac<DbModels.Subject> subjectDac;

        public SubjectsController(
            ILearningAreaDac<DbModels.LearningArea> learningAreaDac,
            IOpenSubjectDac<DbModels.OpenSubject> openSubjectDac,
            ISubjectDac<DbModels.Subject> subjectDac
            )
        {
            this.learningAreaDac = learningAreaDac;
            this.openSubjectDac = openSubjectDac;
            this.subjectDac = subjectDac;
        }

        [HttpGet]
        public async Task<ActionResult<PagingModel<Subject>>> Get(string? search, int? page = 1, int? pageSize = 100)
        {
            if (string.IsNullOrWhiteSpace(search))
            {
                var data = page == 0
                    ? await subjectDac.ListWithLearningArea(learningAreaDac.Collection, x => true)
                    : await subjectDac.ListWithLearningArea(learningAreaDac.Collection, x => true, page ?? 1, pageSize);
                var count = await subjectDac.Count(x => true);

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
                    ? await subjectDac.ListWithLearningArea(learningAreaDac.Collection, func)
                    : await subjectDac.ListWithLearningArea(learningAreaDac.Collection, func, page ?? 1, pageSize);
                var count = await subjectDac.Count(func);

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
            var document = await subjectDac.GetWithLearningArea(learningAreaDac.Collection, x => x.Id == id);
            return Ok(document);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] SubjectCreate request)
        {
            var checkDoc = await subjectDac.Get(x => x.Code == request.Code);
            if (checkDoc != null) return Conflict($"ไม่สำเร็จ มีรหัสวิชา {request.Code} นี้ในระบบแล้วแล้ว");

            var documentDb = request.Adapt<DbModels.Subject>();
            documentDb.Init(UserId);
            await subjectDac.Create(documentDb);
            return Ok();
        }

        [HttpPost("many")]
        public async Task<IActionResult> CreateMany([FromBody] IEnumerable<SubjectCreate> request)
        {
            var documentDbs = request.Adapt<IEnumerable<DbModels.Subject>>();
            documentDbs = documentDbs.Select(x =>
            {
                x.Init(UserId);
                return x;
            }).ToList();
            await subjectDac.CreateMany(documentDbs);
            return Ok();
        }

        [HttpPost("text")]
        public async Task<IActionResult> ImportByText([FromBody] SubjectCreate request)
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
                documentDb.Init(UserId);

                return documentDb;
            });

            await subjectDac.CreateMany(documentDbs);
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, [FromBody] SubjectCreate request)
        {
            var checkDoc = await subjectDac.Get(x => x.Id != id && x.Code == request.Code);
            if (checkDoc != null) return Conflict($"ไม่สำเร็จ มีรหัสวิชา {request.Code} นี้ในระบบแล้วแล้ว");

            var documentDb = await subjectDac.Get(x => x.Id == id);
            documentDb.Code = request.Code;
            documentDb.Name = request.Name;
            documentDb.Description = request.Description;
            await subjectDac.ReplaceOne(x => x.Id == id, documentDb);
            return Ok();
        }

        [HttpPut("delete/{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var documentCount = await openSubjectDac.Count(x => x.SubjectId == id);
            if (documentCount > 0) return Conflict($"ไม่สามารถลบได้ มี {documentCount} วิชา ที่เปิดอยู่");

            await subjectDac.DeleteOne(x => x.Id == id);
            return Ok();
        }
    }
}
