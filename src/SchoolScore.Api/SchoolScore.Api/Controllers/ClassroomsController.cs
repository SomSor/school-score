using Mapster;
using Microsoft.AspNetCore.Mvc;
using SchoolScore.Api.DACs;
using SchoolScore.Api.Models;
using System.Linq.Expressions;

namespace SchoolScore.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClassroomsController : ApiControllerBase
    {
        private readonly IClassroomDac<DbModels.Classroom> classRoomDac;
        private readonly IClassroomStudentDac<DbModels.ClassroomStudent> classRoomStudentDac;
        private readonly IStudentDac<DbModels.Student> studentDac;
        private readonly ITeacherDac<DbModels.Teacher> teacherDac;

        public ClassroomsController(
            IClassroomDac<DbModels.Classroom> classRoomDac,
            IClassroomStudentDac<DbModels.ClassroomStudent> classRoomStudentDac,
            IStudentDac<DbModels.Student> studentDac,
            ITeacherDac<DbModels.Teacher> teacherDac
            )
        {
            this.classRoomDac = classRoomDac;
            this.classRoomStudentDac = classRoomStudentDac;
            this.studentDac = studentDac;
            this.teacherDac = teacherDac;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PagingModel<Classroom>>>> Get(string? search, int? page = 1, int? pageSize = 100)
        {
            if (string.IsNullOrWhiteSpace(search))
            {
                var data = page == 0
                    ? await classRoomDac.ListAllWithTeacher(teacherDac.Collection, studentDac.Collection, x => true)
                    : await classRoomDac.ListWithTeacher(teacherDac.Collection, studentDac.Collection, x => true, page ?? 1, pageSize);
                var count = await classRoomDac.Count(x => true);

                return Ok(new PagingModel<Classroom>
                {
                    Data = data,
                    Length = count,
                });
            }
            else
            {
                var txt = search.Trim().ToLower();
                Expression<Func<DbModels.Classroom, bool>> func = x => true && x.ClassYear.ToLower().Contains(txt);

                var data = page == 0
                    ? await classRoomDac.ListAllWithTeacher(teacherDac.Collection, studentDac.Collection, func)
                    : await classRoomDac.ListWithTeacher(teacherDac.Collection, studentDac.Collection, func, page ?? 1, pageSize);
                var count = await classRoomDac.Count(func);

                return Ok(new PagingModel<Classroom>
                {
                    Data = data,
                    Length = count,
                });
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Classroom>> Get(string id)
        {
            var documentDb = await classRoomDac.GetWithTeacherAndStudent(teacherDac.Collection, studentDac.Collection, x => x.Id == id);
            var document = documentDb.Adapt<Classroom>();
            return Ok(document);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ClassroomCreate request)
        {
            var documentDb = request.Adapt<DbModels.Classroom>();
            documentDb.Init(AccountsController.Username);
            documentDb.SchoolYearId = SchoolYearId;
            await classRoomDac.Create(documentDb);
            return Ok();
        }

        [HttpPost("many")]
        public async Task<IActionResult> CreateMany([FromBody] IEnumerable<ClassroomCreate> request)
        {
            var documentDbs = request.Adapt<IEnumerable<DbModels.Classroom>>();
            documentDbs = documentDbs.Select(x =>
            {
                x.Init(AccountsController.Username);
                x.SchoolYearId = SchoolYearId;
                return x;
            }).ToList();
            await classRoomDac.CreateMany(documentDbs);
            return Ok();
        }

        [HttpPost("text")]
        public async Task<IActionResult> ImportByText([FromBody] ClassroomCreate request)
        {
            var rows = request.ClassYear.Trim().Split(Environment.NewLine).Where(x => !string.IsNullOrWhiteSpace(x));
            var rowsSplit = rows.Select(x => x.Split("\t"));
            var documentDbs = rowsSplit.Select(x =>
            {
                var documentDb = new DbModels.Classroom
                {
                    ClassYear = x[0],
                    Subclass = x[1],
                    TeacherId = x[2],
                    SchoolYearId = SchoolYearId,
                };
                documentDb.Init(AccountsController.Username);

                return documentDb;
            });

            await classRoomDac.CreateMany(documentDbs);
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, [FromBody] ClassroomCreate request)
        {
            var documentDb = await classRoomDac.Get(x => x.Id == id);
            documentDb.ClassYear = request.ClassYear;
            documentDb.Subclass = request.Subclass;
            documentDb.TeacherId = request.TeacherId;
            await classRoomDac.ReplaceOne(x => x.Id == id, documentDb);
            return Ok();
        }

        [HttpPut("delete/{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var classRoomStudentCount = await classRoomStudentDac.Count(x => x.ClassroomId == id);
            if (classRoomStudentCount > 0) return Conflict($"ไม่สามารถลบได้ มี {classRoomStudentCount} นักเรียน อยู่ในห้องเรียนรู้นี้");

            await classRoomDac.DeleteOne(x => x.Id == id);
            return Ok();
        }
    }
}
