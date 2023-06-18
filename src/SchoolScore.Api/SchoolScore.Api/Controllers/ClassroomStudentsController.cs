using Mapster;
using Microsoft.AspNetCore.Mvc;
using SchoolScore.Api.DACs;
using SchoolScore.Api.Models;
using System.Linq.Expressions;

namespace SchoolScore.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClassroomStudentsController : ApiControllerBase
    {
        private readonly IClassroomDac<DbModels.Classroom> classroomDac;
        private readonly IClassroomStudentDac<DbModels.ClassroomStudent> classroomStudentDac;
        private readonly IStudentDac<DbModels.Student> studentDac;

        public ClassroomStudentsController(
            IClassroomDac<DbModels.Classroom> classroomDac,
            IClassroomStudentDac<DbModels.ClassroomStudent> classroomStudentDac,
            IStudentDac<DbModels.Student> studentDac
            )
        {
            this.classroomDac = classroomDac;
            this.classroomStudentDac = classroomStudentDac;
            this.studentDac = studentDac;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PagingModel<ClassroomStudent>>>> Get(string? search, int? page = 1, int? pageSize = 100)
        {
            if (string.IsNullOrWhiteSpace(search))
            {
                var data = page == 0
                    ? await classroomStudentDac.ListWithClassroom(classroomDac.Collection, studentDac.Collection, x => true)
                    : await classroomStudentDac.ListWithClassroom(classroomDac.Collection, studentDac.Collection, x => true, page ?? 1, pageSize);
                var count = await classroomStudentDac.Count(x => true);

                return Ok(new PagingModel<ClassroomStudent>
                {
                    Data = data,
                    Length = count,
                });
            }
            else
            {
                var txt = search.Trim().ToLower();
                Expression<Func<DbModels.ClassroomStudent, bool>> func = x => true && x.ClassroomId.ToLower().Contains(txt);

                var data = page == 0
                    ? await classroomStudentDac.ListWithClassroom(classroomDac.Collection, studentDac.Collection, func)
                    : await classroomStudentDac.ListWithClassroom(classroomDac.Collection, studentDac.Collection, func, page ?? 1, pageSize);
                var count = await classroomStudentDac.Count(func);

                return Ok(new PagingModel<ClassroomStudent>
                {
                    Data = data,
                    Length = count,
                });
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ClassroomStudent>> Get(string id)
        {
            var documentDb = await classroomStudentDac.GetWithClassroomAndStudent(classroomDac.Collection, studentDac.Collection, x => x.Id == id);
            var document = documentDb.Adapt<ClassroomStudent>();
            return Ok(document);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ClassroomStudentCreate request)
        {
            var documentDb = request.Adapt<DbModels.ClassroomStudent>();
            documentDb.Init(AccountsController.Username);
            documentDb.SchoolYearId = SchoolYearId;
            await classroomStudentDac.Create(documentDb);
            return Ok();
        }

        [HttpPost("many")]
        public async Task<IActionResult> CreateMany([FromBody] IEnumerable<ClassroomStudentCreate> request)
        {
            var documentDbs = request.Adapt<IEnumerable<DbModels.ClassroomStudent>>();
            documentDbs = documentDbs.Select(x =>
            {
                x.Init(AccountsController.Username);
                x.SchoolYearId = SchoolYearId;
                return x;
            }).ToList();
            await classroomStudentDac.CreateMany(documentDbs);
            return Ok();
        }

        [HttpPost("text")]
        public async Task<IActionResult> ImportByText([FromBody] ClassroomStudentCreate request)
        {
            var rows = request.ClassroomId.Trim().Split(Environment.NewLine).Where(x => !string.IsNullOrWhiteSpace(x));
            var rowsSplit = rows.Select(x => x.Split("\t"));
            var documentDbs = rowsSplit.Select(x =>
            {
                var documentDb = new DbModels.ClassroomStudent
                {
                    ClassroomId = x[0],
                    StudentId = x[1],
                    SchoolYearId = SchoolYearId,
                };
                documentDb.Init(AccountsController.Username);

                return documentDb;
            });

            await classroomStudentDac.CreateMany(documentDbs);
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, [FromBody] ClassroomStudentCreate request)
        {
            var documentDb = await classroomStudentDac.Get(x => x.Id == id);
            documentDb.ClassroomId = request.ClassroomId;
            documentDb.StudentId = request.StudentId;
            await classroomStudentDac.ReplaceOne(x => x.Id == id, documentDb);
            return Ok();
        }

        [HttpPut("delete/{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            await classroomStudentDac.DeleteOne(x => x.Id == id);
            return Ok();
        }
    }
}
