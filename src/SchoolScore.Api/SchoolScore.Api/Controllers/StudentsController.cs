using Mapster;
using Microsoft.AspNetCore.Mvc;
using SchoolScore.Api.DACs;
using SchoolScore.Api.Models;
using System.Linq.Expressions;

namespace SchoolScore.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentsController : ApiControllerBase
    {
        private readonly IClassroomStudentDac<DbModels.ClassroomStudent> classroomStudentDac;
        private readonly IStudentDac<DbModels.Student> studentDac;

        public StudentsController(
            IClassroomStudentDac<DbModels.ClassroomStudent> classroomStudentDac,
            IStudentDac<DbModels.Student> studentDac
            )
        {
            this.classroomStudentDac = classroomStudentDac;
            this.studentDac = studentDac;
        }

        [HttpGet]
        public async Task<ActionResult<PagingModel<DbModels.Student>>> Get(string? search, int? page = 1, int? pageSize = 100)
        {
            if (string.IsNullOrWhiteSpace(search))
            {
                var data = page == 0
                    ? await studentDac.ListAll(x => true)
                    : await studentDac.List(x => true, page ?? 1, pageSize);
                var count = await studentDac.Count(x => true);

                return Ok(new PagingModel<DbModels.Student>
                {
                    Data = data,
                    Length = count,
                });
            }
            else
            {
                var txt = search.Trim().ToLower();
                Expression<Func<DbModels.Student, bool>> func = x => true && x.Name.ToLower().Contains(txt);

                var data = page == 0
                    ? await studentDac.ListAll(func)
                    : await studentDac.List(func, page ?? 1, pageSize);
                var count = await studentDac.Count(func);

                return Ok(new PagingModel<DbModels.Student>
                {
                    Data = data,
                    Length = count,
                });
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Subject>> Get(string id)
        {
            var document = await studentDac.Get(x => x.Id == id);
            return Ok(document);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] StudentCreate request)
        {
            var documentDb = request.Adapt<DbModels.Student>();
            documentDb.Init(AccountsController.Username);
            await studentDac.Create(documentDb);
            return Ok();
        }

        [HttpPost("many")]
        public async Task<IActionResult> CreateMany([FromBody] IEnumerable<StudentCreate> request)
        {
            var documentDbs = request.Adapt<IEnumerable<DbModels.Student>>();
            documentDbs = documentDbs.Select(x =>
            {
                x.Init(AccountsController.Username);
                return x;
            }).ToList();
            await studentDac.CreateMany(documentDbs);
            return Ok();
        }

        [HttpPost("text")]
        public async Task<IActionResult> ImportByText([FromBody] StudentCreate request)
        {
            var rows = request.Name.Trim().Split(Environment.NewLine).Where(x => !string.IsNullOrWhiteSpace(x));
            var rowsSplit = rows.Select(x => x.Split("\t"));
            var documentDbs = rowsSplit.Select(x =>
            {
                var documentDb = new DbModels.Student
                {
                    Code = x[0],
                    Prefix = x[1],
                    Name = x[2],
                    Lastname = x[3],
                    SchoolId = x[4],
                };
                documentDb.Init(AccountsController.Username);

                return documentDb;
            });

            await studentDac.CreateMany(documentDbs);
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, [FromBody] StudentCreate request)
        {
            var documentDb = await studentDac.Get(x => x.Id == id);
            documentDb.Code = request.Code;
            documentDb.Prefix = request.Prefix;
            documentDb.Name = request.Name;
            documentDb.Lastname = request.Lastname;
            documentDb.PID = request.PID;
            await studentDac.ReplaceOne(x => x.Id == id, documentDb);
            return Ok();
        }

        [HttpPut("delete/{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var classroomStudentCount = await classroomStudentDac.Count(x => x.StudentId == id);
            if (classroomStudentCount > 0) return Conflict($"ไม่สามารถลบได้ {classroomStudentCount} ห้องเรียน ที่เปิดอยู่");

            await studentDac.DeleteOne(x => x.Id == id);
            return Ok();
        }
    }
}
