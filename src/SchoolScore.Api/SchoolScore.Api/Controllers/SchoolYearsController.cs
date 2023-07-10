using Mapster;
using Microsoft.AspNetCore.Mvc;
using SchoolScore.Api.DACs;
using SchoolScore.Api.Models;
using System.Linq.Expressions;

namespace SchoolScore.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SchoolYearsController : ApiControllerBase
    {
        private readonly ISchoolYearDac<DbModels.SchoolYear> schoolYearDac;

        public SchoolYearsController(ISchoolYearDac<DbModels.SchoolYear> schoolYearDac)
        {
            this.schoolYearDac = schoolYearDac;
        }

        [HttpGet]
        public async Task<ActionResult<SchoolYearPaging>> Get(string? search, int? page = 1, int? pageSize = 100)
        {
            if (string.IsNullOrWhiteSpace(search))
            {
                var data = page == 0
                    ? await schoolYearDac.ListAll(x => true)
                    : await schoolYearDac.List(x => true, page ?? 1, pageSize);
                var count = await schoolYearDac.Count(x => true);
                var current = await schoolYearDac.Current();

                return Ok(new SchoolYearPaging
                {
                    Data = data,
                    Length = count,
                    Current = current,
                });
            }
            else
            {
                var txt = search.Trim().ToLower();
                Expression<Func<DbModels.SchoolYear, bool>> func = x => true && x.Year.ToLower().Contains(txt);

                var data = page == 0
                    ? await schoolYearDac.ListAll(func)
                    : await schoolYearDac.List(func, page ?? 1, pageSize);
                var count = await schoolYearDac.Count(func);
                var current = await schoolYearDac.Current();

                return Ok(new SchoolYearPaging
                {
                    Data = data,
                    Length = count,
                    Current = current,
                });
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SchoolYear>> Get(string id)
        {
            var documentDb = await schoolYearDac.Get(x => x.Id == id);
            var document = documentDb.Adapt<SchoolYear>();
            document.Current = await schoolYearDac.Current();
            return Ok(documentDb);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] SchoolYearCreate request)
        {
            var documentDb = request.Adapt<DbModels.SchoolYear>();
            documentDb.Init(AccountsController.Username);
            await schoolYearDac.Create(documentDb);
            return Ok();
        }

        [HttpPost("many")]
        public async Task<IActionResult> CreateMany([FromBody] IEnumerable<SchoolYearCreate> request)
        {
            var documentDbs = request.Adapt<IEnumerable<DbModels.SchoolYear>>();
            documentDbs = documentDbs.Select(x =>
            {
                x.Init(AccountsController.Username);
                return x;
            }).ToList();
            await schoolYearDac.CreateMany(documentDbs);
            return Ok();
        }

        [HttpPost("text")]
        public async Task<IActionResult> ImportByText([FromBody] SchoolYearCreate request)
        {
            var rows = request.Year.Trim().Split(Environment.NewLine).Where(x => !string.IsNullOrWhiteSpace(x));
            var rowsSplit = rows.Select(x => x.Split("\t"));
            var documentDbs = rowsSplit.Select(x =>
            {
                var documentDb = new DbModels.SchoolYear
                {
                    Year = x[0],
                    Semester = int.Parse(x[1]),
                    StartDate = DateTime.Parse(x[2]),
                    EndDate = DateTime.Parse(x[3]),
                    SchoolId = x[4],
                };
                documentDb.Init(AccountsController.Username);

                return documentDb;
            });

            await schoolYearDac.CreateMany(documentDbs);
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, [FromBody] SchoolYearCreate request)
        {
            var documentDb = await schoolYearDac.Get(x => x.Id == id);
            documentDb.Year = request.Year;
            documentDb.Semester = request.Semester;
            documentDb.StartDate = request.StartDate;
            documentDb.EndDate = request.EndDate;
            await schoolYearDac.ReplaceOne(x => x.Id == id, documentDb);
            return Ok();
        }

        [HttpPut("delete/{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            await schoolYearDac.DeleteOne(x => x.Id == id);
            return Ok();
        }
    }
}
