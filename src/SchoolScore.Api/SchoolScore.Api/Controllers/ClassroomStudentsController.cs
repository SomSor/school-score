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
        private readonly ILearningAreaDac<DbModels.LearningArea> learningAreaDac;
        private readonly IOpenSubjectDac<DbModels.OpenSubject> openSubjectDac;
        private readonly IStudentDac<DbModels.Student> studentDac;
        private readonly ISubjectDac<DbModels.Subject> subjectDac;
        private readonly ITeacherDac<DbModels.Teacher> teacherDac;

        public ClassroomStudentsController(
            IClassroomDac<DbModels.Classroom> classroomDac,
            IClassroomStudentDac<DbModels.ClassroomStudent> classroomStudentDac,
            ILearningAreaDac<DbModels.LearningArea> learningAreaDac,
            IOpenSubjectDac<DbModels.OpenSubject> openSubjectDac,
            IStudentDac<DbModels.Student> studentDac,
            ISubjectDac<DbModels.Subject> subjectDac,
            ITeacherDac<DbModels.Teacher> teacherDac
            )
        {
            this.classroomDac = classroomDac;
            this.classroomStudentDac = classroomStudentDac;
            this.learningAreaDac = learningAreaDac;
            this.openSubjectDac = openSubjectDac;
            this.studentDac = studentDac;
            this.subjectDac = subjectDac;
            this.teacherDac = teacherDac;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PagingModel<ClassroomStudent>>>> Get(string? search, int? page = 1, int? pageSize = 100)
        {
            if (string.IsNullOrWhiteSpace(search))
            {
                var data = page == 0
                    ? await classroomStudentDac.ListWithClassroomAndStudent(classroomDac.Collection, studentDac.Collection, x => true)
                    : await classroomStudentDac.ListWithClassroomAndStudent(classroomDac.Collection, studentDac.Collection, x => true, page ?? 1, pageSize);
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
                    ? await classroomStudentDac.ListWithClassroomAndStudent(classroomDac.Collection, studentDac.Collection, func)
                    : await classroomStudentDac.ListWithClassroomAndStudent(classroomDac.Collection, studentDac.Collection, func, page ?? 1, pageSize);
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
            documentDb.RegisterOpenSubjects = Enumerable.Empty<DbModels.RegisterOpenSubject>();
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

        [HttpGet("opensubjects")]
        public async Task<ActionResult<RegisteredOpenSubject>> GetOpenSubjects(string? search, int? page = 1, int? pageSize = 100)
        {
            if (string.IsNullOrWhiteSpace(search))
            {
                var data = page == 0
                    ? await classroomStudentDac.ListWithOpenSubject(classroomDac.Collection, studentDac.Collection, openSubjectDac.Collection, subjectDac.Collection, x => true)
                    : await classroomStudentDac.ListWithOpenSubject(classroomDac.Collection, studentDac.Collection, openSubjectDac.Collection, subjectDac.Collection, x => true, page ?? 1, pageSize);
                data.Length = await classroomStudentDac.Count(x => true);

                return Ok(data);
            }
            else
            {
                var txt = search.Trim().ToLower();
                Expression<Func<DbModels.ClassroomStudent, bool>> func = x => true && x.ClassroomId.ToLower().Contains(txt);

                var data = page == 0
                    ? await classroomStudentDac.ListWithOpenSubject(classroomDac.Collection, studentDac.Collection, openSubjectDac.Collection, subjectDac.Collection, func)
                    : await classroomStudentDac.ListWithOpenSubject(classroomDac.Collection, studentDac.Collection, openSubjectDac.Collection, subjectDac.Collection, func, page ?? 1, pageSize);
                data.Length = await classroomStudentDac.Count(x => true);

                return Ok(data);
            }
        }

        [HttpGet("classrooms/{classroomid}/opensubjects/{opensubjectid}")]
        public async Task<ActionResult<ClassroomOpenSubjectDetails>> GetClassroomOpenSubjects(string classroomid, string opensubjectid, string? search, int? page = 1, int? pageSize = 100)
        {
            var classroom = await classroomDac.GetWithTeacher(teacherDac.Collection, classroomStudentDac.Collection, x => x.Id == classroomid);
            var openSubject = await openSubjectDac.GetWithSubjectAndTeacher(subjectDac.Collection, teacherDac.Collection, x => x.Id == opensubjectid);
            var learningArea = await learningAreaDac.Get(x => x.Id == openSubject.Subject.LearningAreaId);

            var response = new ClassroomOpenSubjectDetails
            {
                Classroom = classroom,
                Teacher = classroom.Teacher,
                OpenSubject = openSubject,
                Subject = openSubject.Subject,
                LearningArea = learningArea,
            };

            if (string.IsNullOrWhiteSpace(search))
            {
                var data = page == 0
                    ? await classroomStudentDac.ListWithStudent(classroomid, studentDac.Collection)
                    : await classroomStudentDac.ListWithStudent(classroomid, studentDac.Collection, page ?? 1, pageSize);
                var count = await classroomStudentDac.Count(x => true);

                response.Data = data;
                response.Length = count;
            }
            else
            {
                var txt = search.Trim().ToLower();
                Expression<Func<DbModels.ClassroomStudent, bool>> func = x => true && x.ClassroomId.ToLower().Contains(txt);

                var data = page == 0
                    ? await classroomStudentDac.ListWithStudent(classroomid, studentDac.Collection)
                    : await classroomStudentDac.ListWithStudent(classroomid, studentDac.Collection, page ?? 1, pageSize);
                var count = await classroomStudentDac.Count(func);

                response.Data = data;
                response.Length = count;
            }

            return response;
        }

        [HttpPost("opensubjects")]
        public async Task<IActionResult> RegisterOpenSubjects([FromBody] RegisterOpenSubjectCreate request)
        {
            var openSubject = await openSubjectDac.Get(x => x.Id == request.OpenSubjectId);
            var classRoomStudents = await classroomStudentDac.List(x => x.ClassroomId == request.ClassroomId);

            foreach (var classRoomStudent in classRoomStudents)
            {
                var registerOpenSubjects = classRoomStudent.RegisterOpenSubjects.ToList();
                registerOpenSubjects.Add(new DbModels.RegisterOpenSubject
                {
                    Id = DbModels.DbModelBase.RunId(),
                    OpenSubjectId = openSubject.Id,
                    EvaluateResults = openSubject.Evaluates.Select(sg => new DbModels.ScoringGroupResult
                    {
                        ScoringGroupId = sg.Id,
                        ScoringSubGroupResults = sg.ScoringSubGroups.Select(ssg => new DbModels.ScoringSubGroupResult
                        {
                            ScoringSubGroupId = ssg.Id,
                            ScoringResults = ssg.Scorings.Select(s => new DbModels.ScoringResult
                            {
                                ScoringId = s.Id,
                                Score = null,
                            }),
                            GradeResult = null,
                        }),
                    }),
                    ExamResult = new DbModels.ScoringGroupResult
                    {
                        ScoringGroupId = openSubject.Exam.Id,
                        ScoringSubGroupResults = openSubject.Exam.ScoringSubGroups.Select(ssg => new DbModels.ScoringSubGroupResult
                        {
                            ScoringSubGroupId = ssg.Id,
                            ScoringResults = ssg.Scorings.Select(s => new DbModels.ScoringResult
                            {
                                ScoringId = s.Id,
                                Score = null,
                            }),
                            GradeResult = null,
                        }),
                    },
                });
                classRoomStudent.RegisterOpenSubjects = registerOpenSubjects;
                await classroomStudentDac.ReplaceOne(x => x.Id == classRoomStudent.Id, classRoomStudent);
            }

            return Ok();
        }

        [HttpPut("exam")]
        public async Task<IActionResult> Exam([FromBody] SaveScoringGroupRequest request)
        {
            var classRoomStudents = await classroomStudentDac.List(x => x.ClassroomId == request.ClassroomId);

            foreach (var classRoomStudent in classRoomStudents)
            {
                var registerOpenSubjects = classRoomStudent.RegisterOpenSubjects.ToList();
                foreach (var registerOpenSubject in registerOpenSubjects.Where(x => x.OpenSubjectId == request.OpenSubjectId))
                {
                    foreach (var scoringSubGroupResult in registerOpenSubject.ExamResult.ScoringSubGroupResults)
                    {
                        foreach (var scoringResult in scoringSubGroupResult.ScoringResults)
                        {
                            var oldScore = scoringResult.Score;
                            scoringResult.Score = request.ClassroomStudentScores.FirstOrDefault(x => x.StudentId == classRoomStudent.StudentId
                                && x.ScoringSubGroupId == scoringSubGroupResult.ScoringSubGroupId
                                && x.ScoringId == scoringResult.ScoringId)?.Score ?? oldScore;
                        }
                    }
                    var remark = request.ClassroomStudentRemarks.FirstOrDefault(x => x.StudentId == classRoomStudent.StudentId && x.ScoringGroupId == registerOpenSubject.ExamResult.ScoringGroupId)?.Remark;
                    registerOpenSubject.ExamResult.Remark = remark;
                }
                classRoomStudent.RegisterOpenSubjects = registerOpenSubjects;
                await classroomStudentDac.ReplaceOne(x => x.Id == classRoomStudent.Id, classRoomStudent);
            }
            return Ok();
        }

        [HttpPut("evaluate")]
        public async Task<IActionResult> Evaluate([FromBody] SaveScoringGroupRequest request)
        {
            var classRoomStudents = await classroomStudentDac.List(x => x.ClassroomId == request.ClassroomId);

            foreach (var classRoomStudent in classRoomStudents)
            {
                var registerOpenSubjects = classRoomStudent.RegisterOpenSubjects.ToList();
                foreach (var registerOpenSubject in registerOpenSubjects.Where(x => x.OpenSubjectId == request.OpenSubjectId))
                {
                    foreach (var evaluateResult in registerOpenSubject.EvaluateResults)
                    {
                        foreach (var scoringSubGroupResult in evaluateResult.ScoringSubGroupResults)
                        {
                            foreach (var scoringResult in scoringSubGroupResult.ScoringResults)
                            {
                                var oldScore = scoringResult.Score;
                                scoringResult.Score = request.ClassroomStudentScores.FirstOrDefault(x => x.StudentId == classRoomStudent.StudentId
                                    && x.ScoringSubGroupId == scoringSubGroupResult.ScoringSubGroupId
                                    && x.ScoringId == scoringResult.ScoringId)?.Score ?? oldScore;
                            }
                        }
                        var remark = request.ClassroomStudentRemarks.FirstOrDefault(x => x.StudentId == classRoomStudent.StudentId && x.ScoringGroupId == evaluateResult.ScoringGroupId)?.Remark;
                        evaluateResult.Remark = remark;
                    }
                }
                classRoomStudent.RegisterOpenSubjects = registerOpenSubjects;
                await classroomStudentDac.ReplaceOne(x => x.Id == classRoomStudent.Id, classRoomStudent);
            }
            return Ok();
        }
    }
}
