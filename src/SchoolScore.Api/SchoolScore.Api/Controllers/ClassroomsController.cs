using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchoolScore.Api.DACs;
using SchoolScore.Api.Models;
using System.Linq.Expressions;

namespace SchoolScore.Api.Controllers
{
    [Authorize(Roles = "Admin,Mod")]
    [ApiController]
    [Route("api/[controller]")]
    public class ClassroomsController : ApiControllerBase
    {
        private readonly IDictionary<string, string> Tiers = new Dictionary<string, string>
        {
            { DbModels.ClassroomTierPossible.PreSchool, "อ." },
            { DbModels.ClassroomTierPossible.PrimarySchool, "ป." },
            { DbModels.ClassroomTierPossible.JuniorHighSchool, "ม." },
            { DbModels.ClassroomTierPossible.SeniorHighSchool, "ม." },
        };

        private readonly IClassroomDac<DbModels.Classroom> classroomDac;
        private readonly IClassroomStudentDac<DbModels.ClassroomStudent> classroomStudentDac;
        private readonly ISchoolYearDac<DbModels.SchoolYear> schoolYearDac;
        private readonly IStudentDac<DbModels.Student> studentDac;
        private readonly ITeacherDac<DbModels.Teacher> teacherDac;

        public ClassroomsController(
            IClassroomDac<DbModels.Classroom> classroomDac,
            IClassroomStudentDac<DbModels.ClassroomStudent> classroomStudentDac,
            ISchoolYearDac<DbModels.SchoolYear> schoolYearDac,
            IStudentDac<DbModels.Student> studentDac,
            ITeacherDac<DbModels.Teacher> teacherDac
            )
        {
            this.classroomDac = classroomDac;
            this.classroomStudentDac = classroomStudentDac;
            this.schoolYearDac = schoolYearDac;
            this.studentDac = studentDac;
            this.teacherDac = teacherDac;
        }

        [HttpGet]
        public async Task<ActionResult<PagingModel<Classroom>>> Get(string? search, int? page = 1, int? pageSize = 100)
        {
            var schoolYear = await schoolYearDac.Current();
            if (string.IsNullOrWhiteSpace(search))
            {
                var data = page == 0
                    ? await classroomDac.ListWithTeacher(teacherDac.Collection, classroomStudentDac.Collection, x => true, schoolYear.Id)
                    : await classroomDac.ListWithTeacher(teacherDac.Collection, classroomStudentDac.Collection, x => true, schoolYear.Id, page ?? 1, pageSize);
                var count = await classroomDac.Count(x => true);

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
                    ? await classroomDac.ListWithTeacher(teacherDac.Collection, classroomStudentDac.Collection, func, schoolYear.Id)
                    : await classroomDac.ListWithTeacher(teacherDac.Collection, classroomStudentDac.Collection, func, schoolYear.Id, page ?? 1, pageSize);
                var count = await classroomDac.Count(func);

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
            var schoolYear = await schoolYearDac.Current();
            var documentDb = await classroomDac.GetWithTeacherAndStudent(teacherDac.Collection, classroomStudentDac.Collection, studentDac.Collection, x => x.Id == id, schoolYear.Id);
            var document = documentDb.Adapt<Classroom>();
            return Ok(document);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ClassroomCreate request)
        {
            var schoolYear = await schoolYearDac.Current();
            var checkDoc = await classroomDac.Get(x => x.SchoolYearId == schoolYear.Id && x.Tier == request.Tier && x.ClassYear == request.ClassYear && x.Subclass == request.Subclass);
            if (checkDoc != null) return Conflict($"ไม่สำเร็จ มีห้องเรียน {Tiers[request.Tier]} {request.ClassYear}/{request.Subclass} นี้ในระบบแล้วแล้ว");

            var documentDb = request.Adapt<DbModels.Classroom>();
            documentDb.Init(UserId);
            documentDb.SchoolYearId = schoolYear.Id;
            await classroomDac.Create(documentDb);
            return Ok();
        }

        //[HttpPost("many")]
        //public async Task<IActionResult> CreateMany([FromBody] IEnumerable<ClassroomCreate> request)
        //{
        //    var documentDbs = request.Adapt<IEnumerable<DbModels.Classroom>>();
        //    documentDbs = documentDbs.Select(x =>
        //    {
        //        x.Init(UserId);
        //        x.SchoolYearId = SchoolYearId;
        //        return x;
        //    }).ToList();
        //    await classroomDac.CreateMany(documentDbs);
        //    return Ok();
        //}

        //[HttpPost("text")]
        //public async Task<IActionResult> ImportByText([FromBody] ClassroomCreate request)
        //{
        //    var rows = request.ClassYear.Trim().Split(Environment.NewLine).Where(x => !string.IsNullOrWhiteSpace(x));
        //    var rowsSplit = rows.Select(x => x.Split("\t"));
        //    var documentDbs = rowsSplit.Select(x =>
        //    {
        //        var documentDb = new DbModels.Classroom
        //        {
        //            ClassYear = x[0],
        //            Subclass = x[1],
        //            TeacherId = x[2],
        //            SchoolYearId = SchoolYearId,
        //        };
        //        documentDb.Init(UserId);

        //        return documentDb;
        //    });

        //    await classroomDac.CreateMany(documentDbs);
        //    return Ok();
        //}

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, [FromBody] ClassroomCreate request)
        {
            var schoolYear = await schoolYearDac.Current();
            var checkDoc = await classroomDac.Get(x => x.Id != id && x.SchoolYearId == schoolYear.Id && x.Tier == request.Tier && x.ClassYear == request.ClassYear && x.Subclass == request.Subclass);
            if (checkDoc != null) return Conflict($"ไม่สำเร็จ มีห้องเรียน {Tiers[request.Tier]} {request.ClassYear}/{request.Subclass} นี้ในระบบแล้วแล้ว");

            var documentDb = await classroomDac.Get(x => x.Id == id);
            documentDb.ClassYear = request.ClassYear;
            documentDb.Subclass = request.Subclass;
            documentDb.TeacherId = request.TeacherId;
            await classroomDac.ReplaceOne(x => x.Id == id, documentDb);
            return Ok();
        }

        [HttpPut("delete/{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var classroomStudentCount = await classroomStudentDac.Count(x => x.ClassroomId == id);
            if (classroomStudentCount > 0) return Conflict($"ไม่สามารถลบได้ มี {classroomStudentCount} นักเรียน อยู่ในห้องเรียนรู้นี้");

            await classroomDac.DeleteOne(x => x.Id == id);
            return Ok();
        }
    }
}
