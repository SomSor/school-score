﻿using Mapster;
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
    public class RegisterOpenSubjectsController : ApiControllerBase
    {
        private readonly IClassroomStudentDac<DbModels.ClassroomStudent> classroomStudentDac;
        private readonly IOpenSubjectDac<DbModels.OpenSubject> openSubjectDac;
        private readonly IStudentDac<DbModels.Student> studentDac;

        public RegisterOpenSubjectsController(
            IClassroomStudentDac<DbModels.ClassroomStudent> classroomStudentDac,
            IOpenSubjectDac<DbModels.OpenSubject> openSubjectDac,
            IStudentDac<DbModels.Student> studentDac
            )
        {
            this.classroomStudentDac = classroomStudentDac;
            this.openSubjectDac = openSubjectDac;
            this.studentDac = studentDac;
        }

        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<DbModels.Student>>> Get(string? search, int? page = 1, int? pageSize = 100)
        //{
        //    if (string.IsNullOrWhiteSpace(search))
        //    {
        //        var data = page == 0
        //            ? await classroomStudentDac.ListAll(x => true)
        //            : await classroomStudentDac.List(x => true, page ?? 1, pageSize);
        //        var count = await studentDac.Count(x => true);

        //        return Ok(new PagingModel<DbModels.Student>
        //        {
        //            Data = data,
        //            Length = count,
        //        });
        //    }
        //    else
        //    {
        //        var txt = search.Trim().ToLower();
        //        Expression<Func<DbModels.Student, bool>> func = x => true && x.Name.ToLower().Contains(txt);

        //        var data = page == 0
        //            ? await classroomStudentDac.ListAll(func)
        //            : await classroomStudentDac.List(func, page ?? 1, pageSize);
        //        var count = await studentDac.Count(func);

        //        return Ok(new PagingModel<DbModels.Student>
        //        {
        //            Data = data,
        //            Length = count,
        //        });
        //    }
        //}

        //[HttpGet("{id}")]
        //public async Task<ActionResult<Subject>> Get(string id)
        //{
        //    var document = await studentDac.Get(x => x.Id == id);
        //    return Ok(document);
        //}

        //[HttpPost]
        //public async Task<IActionResult> Create([FromBody] StudentCreate request)
        //{
        //    var documentDb = request.Adapt<DbModels.Student>();
        //    documentDb.Init(UserId);
        //    await studentDac.Create(documentDb);
        //    return Ok();
        //}

        //[HttpPut("{id}")]
        //public async Task<IActionResult> Update(string id, [FromBody] StudentCreate request)
        //{
        //    var documentDb = await studentDac.Get(x => x.Id == id);
        //    documentDb.Code = request.Code;
        //    documentDb.Prefix = request.Prefix;
        //    documentDb.Name = request.Name;
        //    documentDb.Lastname = request.Lastname;
        //    documentDb.PID = request.PID;
        //    await studentDac.ReplaceOne(x => x.Id == id, documentDb);
        //    return Ok();
        //}

        //[HttpPut("delete/{id}")]
        //public async Task<IActionResult> Delete(string id)
        //{
        //    var classroomStudentCount = await classroomStudentDac.Count(x => x.StudentId == id);
        //    if (classroomStudentCount > 0) return Conflict($"ไม่สามารถลบได้ {classroomStudentCount} ห้องเรียน ที่เปิดอยู่");

        //    await studentDac.DeleteOne(x => x.Id == id);
        //    return Ok();
        //}
    }
}
