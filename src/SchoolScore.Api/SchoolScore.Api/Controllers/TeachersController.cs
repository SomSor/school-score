using Mapster;
using Microsoft.AspNetCore.Mvc;
using SchoolScore.Api.DACs;
using SchoolScore.Api.Models;
using System.Linq.Expressions;

namespace SchoolScore.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TeachersController : ApiControllerBase
    {
        private readonly IClassroomDac<DbModels.Classroom> classroomDac;
        private readonly IOpenSubjectDac<DbModels.OpenSubject> openSubjectDac;
        private readonly ITeacherDac<DbModels.Teacher> teacherDac;

        public TeachersController(
            IClassroomDac<DbModels.Classroom> classroomDac,
            IOpenSubjectDac<DbModels.OpenSubject> openSubjectDac,
            ITeacherDac<DbModels.Teacher> teacherDac
            )
        {
            this.teacherDac = teacherDac;
            this.classroomDac = classroomDac;
            this.openSubjectDac = openSubjectDac;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PagingModel<DbModels.Teacher>>>> Get(string? search, int? page = 1, int? pageSize = 100)
        {
            if (string.IsNullOrWhiteSpace(search))
            {
                var data = page == 0
                    ? await teacherDac.ListAll(x => true)
                    : await teacherDac.List(x => true, page ?? 1, pageSize);
                var count = await teacherDac.Count(x => true);

                return Ok(new PagingModel<DbModels.Teacher>
                {
                    Data = data,
                    Length = count,
                });
            }
            else
            {
                var txt = search.Trim().ToLower();
                Expression<Func<DbModels.Teacher, bool>> func = x => true && x.Name.ToLower().Contains(txt);

                var data = page == 0
                    ? await teacherDac.ListAll(func)
                    : await teacherDac.List(func, page ?? 1, pageSize);
                var count = await teacherDac.Count(func);

                return Ok(new PagingModel<DbModels.Teacher>
                {
                    Data = data,
                    Length = count,
                });
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Subject>> Get(string id)
        {
            var document = await teacherDac.Get(x => x.Id == id);
            return Ok(document);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] TeacherCreate request)
        {
            var documentDb = request.Adapt<DbModels.Teacher>();
            documentDb.Init(AccountsController.Username);
            await teacherDac.Create(documentDb);
            return Ok();
        }

        [HttpPost("many")]
        public async Task<IActionResult> CreateMany([FromBody] IEnumerable<TeacherCreate> request)
        {
            var documentDbs = request.Adapt<IEnumerable<DbModels.Teacher>>();
            documentDbs = documentDbs.Select(x =>
            {
                x.Init(AccountsController.Username);
                return x;
            }).ToList();
            await teacherDac.CreateMany(documentDbs);
            return Ok();
        }

        [HttpPost("text")]
        public async Task<IActionResult> ImportByText([FromBody] TeacherCreate request)
        {
            var rows = request.Name.Trim().Split(Environment.NewLine).Where(x => !string.IsNullOrWhiteSpace(x));
            var rowsSplit = rows.Select(x => x.Split("\t"));
            var documentDbs = rowsSplit.Select(x =>
            {
                var documentDb = new DbModels.Teacher
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

            await teacherDac.CreateMany(documentDbs);
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, [FromBody] TeacherCreate request)
        {
            var documentDb = await teacherDac.Get(x => x.Id == id);
            documentDb.Code = request.Code;
            documentDb.Prefix = request.Prefix;
            documentDb.Name = request.Name;
            documentDb.Lastname = request.Lastname;
            documentDb.PID = request.PID;
            await teacherDac.ReplaceOne(x => x.Id == id, documentDb);
            return Ok();
        }

        [HttpPut("delete/{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var openSubjectCount = await openSubjectDac.Count(x => x.MainTeacherId == id);
            var classroomCount = await classroomDac.Count(x => x.TeacherId == id);
            if (openSubjectCount > 0 || classroomCount > 0) return Conflict($"ไม่สามารถลบได้ มี {openSubjectCount} วิชา หรือ {classroomCount} ห้องเรียน ที่เปิดอยู่");

            await teacherDac.DeleteOne(x => x.Id == id);
            return Ok();
        }
    }
}
