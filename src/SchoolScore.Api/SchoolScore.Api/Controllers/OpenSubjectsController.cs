using Mapster;
using Microsoft.AspNetCore.Mvc;
using SchoolScore.Api.DACs;
using SchoolScore.Api.Models;
using System.Linq.Expressions;

namespace SchoolScore.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OpenSubjectsController : ApiControllerBase
    {
        private readonly IClassroomStudentDac<DbModels.ClassroomStudent> classroomStudentDac;
        private readonly IOpenSubjectDac<DbModels.OpenSubject> openSubjectDac;
        private readonly ISubjectDac<DbModels.Subject> subjectDac;
        private readonly ITeacherDac<DbModels.Teacher> teacherDac;

        public OpenSubjectsController(
            IClassroomStudentDac<DbModels.ClassroomStudent> classroomStudentDac,
            IOpenSubjectDac<DbModels.OpenSubject> openSubjectDac,
            ISubjectDac<DbModels.Subject> subjectDac,
            ITeacherDac<DbModels.Teacher> teacherDac
            )
        {
            this.classroomStudentDac = classroomStudentDac;
            this.openSubjectDac = openSubjectDac;
            this.subjectDac = subjectDac;
            this.teacherDac = teacherDac;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PagingModel<OpenSubject>>>> Get(string? search, int? page = 1, int? pageSize = 100)
        {
            if (string.IsNullOrWhiteSpace(search))
            {
                var data = page == 0
                    ? await openSubjectDac.ListWithSubjectAndTeacher(subjectDac.Collection, teacherDac.Collection, x => true)
                    : await openSubjectDac.ListWithSubjectAndTeacher(subjectDac.Collection, teacherDac.Collection, x => true, page ?? 1, pageSize);
                var count = await openSubjectDac.Count(x => true);

                return Ok(new PagingModel<OpenSubject>
                {
                    Data = data,
                    Length = count,
                });
            }
            else
            {
                var txt = search.Trim().ToLower();
                Expression<Func<DbModels.OpenSubject, bool>> func = x => true && x.SubjectId.ToLower().Contains(txt);

                var data = page == 0
                    ? await openSubjectDac.ListWithSubjectAndTeacher(subjectDac.Collection, teacherDac.Collection, func)
                    : await openSubjectDac.ListWithSubjectAndTeacher(subjectDac.Collection, teacherDac.Collection, func, page ?? 1, pageSize);
                var count = await openSubjectDac.Count(func);

                return Ok(new PagingModel<OpenSubject>
                {
                    Data = data,
                    Length = count,
                });
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OpenSubject>> Get(string id)
        {
            var document = await openSubjectDac.GetWithSubjectAndTeacher(subjectDac.Collection, teacherDac.Collection, x => x.Id == id);
            return Ok(document);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] OpenSubjectCreate request)
        {
            var documentDb = request.Adapt<DbModels.OpenSubject>();
            documentDb.Init(AccountsController.Username);
            documentDb.SchoolYearId = SchoolYearId;


            var scorings_eval1 = delegate ()
            {
                return new List<DbModels.Scoring>
                {
                    new DbModels.Scoring
                    {
                        Id = DbModels.DbModelBase.RunId(),
                        Name = "ประเมิน",
                        MaxScore = 4,
                    },
                };
            };
            var scorings_eval2 = delegate ()
            {
                return new List<DbModels.Scoring>
                {
                    new DbModels.Scoring
                    {
                        Id = DbModels.DbModelBase.RunId(),
                        Name = "ข้อ 1 เต็ม 3",
                        MaxScore = 3,
                    },
                    new DbModels.Scoring
                    {
                        Id = DbModels.DbModelBase.RunId(),
                        Name = "ข้อ 2 เต็ม 3",
                        MaxScore = 3,
                    },
                    new DbModels.Scoring
                    {
                        Id = DbModels.DbModelBase.RunId(),
                        Name = "ข้อ 3 เต็ม 3",
                        MaxScore = 3,
                    },
                    new DbModels.Scoring
                    {
                        Id = DbModels.DbModelBase.RunId(),
                        Name = "ข้อ 4 เต็ม 3",
                        MaxScore = 3,
                    },
                };
            };
            var criterias = delegate ()
            {
                return new List<DbModels.GradingCriteria>
                {
                    new DbModels.GradingCriteria
                    {
                        Id = DbModels.DbModelBase.RunId(),
                        Grade = "ดีเยี่ยม",
                        Score = 13,
                    },
                    new DbModels.GradingCriteria
                    {
                        Id = DbModels.DbModelBase.RunId(),
                        Grade = "ดี",
                        Score = 9,
                    },
                    new DbModels.GradingCriteria
                    {
                        Id = DbModels.DbModelBase.RunId(),
                        Grade = "ผ่านเกณฑ์",
                        Score = 5,
                    },
                    new DbModels.GradingCriteria
                    {
                        Id = DbModels.DbModelBase.RunId(),
                        Grade = "ไม่ผ่านเกณฑ์",
                        Score = 0,
                    },
                };
            };

            documentDb.Evaluates = new List<DbModels.ScoringGroup>
            {
                new DbModels.ScoringGroup
                {
                    Id = DbModels.DbModelBase.RunId(),
                    Name = "การประเมินคุณลักษณะอันพึงประสงค์",
                    Type = DbModels.ScoringGroupTypePossible.Evaluate,
                    ScoringSubGroups = new List<DbModels.ScoringSubGroup>
                    {
                        new DbModels.ScoringSubGroup
                        {
                            Id = DbModels.DbModelBase.RunId(),
                            Name = "ข้อ 1. รักชาติ ศาสน์ กษัตริย์",
                            Scorings = scorings_eval1().Adapt<IEnumerable<DbModels.Scoring>>(),
                        },
                        new DbModels.ScoringSubGroup
                        {
                            Id = DbModels.DbModelBase.RunId(),
                            Name = "ข้อ 2. ซื่อสัตย์ สุจริต",
                            Scorings = scorings_eval1().Adapt<IEnumerable<DbModels.Scoring>>(),
                        },
                        new DbModels.ScoringSubGroup
                        {
                            Id = DbModels.DbModelBase.RunId(),
                            Name = "ข้อ 3. มีวินัย",
                            Scorings = scorings_eval1().Adapt<IEnumerable<DbModels.Scoring>>(),
                        },
                        new DbModels.ScoringSubGroup
                        {
                            Id = DbModels.DbModelBase.RunId(),
                            Name = "ข้อ 4. ใฝ่เรียนรู้",
                            Scorings = scorings_eval1().Adapt<IEnumerable<DbModels.Scoring>>(),
                        },
                        new DbModels.ScoringSubGroup
                        {
                            Id = DbModels.DbModelBase.RunId(),
                            Name = "ข้อ 5. อยู่อย่างพอเพียง",
                            Scorings = scorings_eval1().Adapt<IEnumerable<DbModels.Scoring>>(),
                        },
                        new DbModels.ScoringSubGroup
                        {
                            Id = DbModels.DbModelBase.RunId(),
                            Name = "ข้อ 6. มุ่งมั่นในการทำงาน",
                            Scorings = scorings_eval1().Adapt<IEnumerable<DbModels.Scoring>>(),
                        },
                        new DbModels.ScoringSubGroup
                        {
                            Id = DbModels.DbModelBase.RunId(),
                            Name = "ข้อ 7. รักความเป็นไทย",
                            Scorings = scorings_eval1().Adapt<IEnumerable<DbModels.Scoring>>(),
                        },
                        new DbModels.ScoringSubGroup
                        {
                            Id = DbModels.DbModelBase.RunId(),
                            Name = "ข้อ 8. มีจิตสาธารณะ",
                            Scorings = scorings_eval1().Adapt<IEnumerable<DbModels.Scoring>>(),
                        },
                    },
                    GradingCriterias = criterias(),
                },
                new DbModels.ScoringGroup
                {
                    Id = DbModels.DbModelBase.RunId(),
                    Name = "การประเมินการอ่าน",
                    Type = DbModels.ScoringGroupTypePossible.Evaluate,
                    ScoringSubGroups = new List<DbModels.ScoringSubGroup>
                    {
                        new DbModels.ScoringSubGroup
                        {
                            Id = DbModels.DbModelBase.RunId(),
                            Name = "การอ่าน",
                            Scorings = scorings_eval2().Adapt<IEnumerable<DbModels.Scoring>>(),
                        },
                    },
                    GradingCriterias = new List<DbModels.GradingCriteria>
                {
                    new DbModels.GradingCriteria
                    {
                        Id = DbModels.DbModelBase.RunId(),
                        Grade = "ดีเยี่ยม",
                        Score = 25,
                    },
                    new DbModels.GradingCriteria
                    {
                        Id = DbModels.DbModelBase.RunId(),
                        Grade = "ดี",
                        Score = 17,
                    },
                    new DbModels.GradingCriteria
                    {
                        Id = DbModels.DbModelBase.RunId(),
                        Grade = "ผ่านเกณฑ์",
                        Score = 9,
                    },
                    new DbModels.GradingCriteria
                    {
                        Id = DbModels.DbModelBase.RunId(),
                        Grade = "ไม่ผ่านเกณฑ์",
                        Score = 0,
                    },
                },
                },
                new DbModels.ScoringGroup
                {
                    Id = DbModels.DbModelBase.RunId(),
                    Name = "การประเมินการคิดวิเคราะห์",
                    Type = DbModels.ScoringGroupTypePossible.Evaluate,
                    ScoringSubGroups = new List<DbModels.ScoringSubGroup>
                    {
                        new DbModels.ScoringSubGroup
                        {
                            Id = DbModels.DbModelBase.RunId(),
                            Name = "การคิดวิเคราะห์",
                            Scorings = scorings_eval2().Adapt<IEnumerable<DbModels.Scoring>>(),
                        },
                    },
                    GradingCriterias = criterias(),
                },
                new DbModels.ScoringGroup
                {
                    Id = DbModels.DbModelBase.RunId(),
                    Name = "การประเมินการเขียน",
                    Type = DbModels.ScoringGroupTypePossible.Evaluate,
                    ScoringSubGroups = new List<DbModels.ScoringSubGroup>
                    {
                        new DbModels.ScoringSubGroup
                        {
                            Id = DbModels.DbModelBase.RunId(),
                            Name = "การเขียน",
                            Scorings = scorings_eval2().Adapt<IEnumerable<DbModels.Scoring>>(),
                        },
                    },
                    GradingCriterias = criterias(),
                },
            };
            documentDb.Exam = new DbModels.ScoringGroup
            {
                Id = DbModels.DbModelBase.RunId(),
                Name = "การประเมินตัวชี้วัด",
                Type = DbModels.ScoringGroupTypePossible.Exam,
                ScoringSubGroups = new List<DbModels.ScoringSubGroup>
                {
                    new DbModels.ScoringSubGroup
                    {
                        Id = DbModels.DbModelBase.RunId(),
                        Name = $"ภาคเรียนที่ {request.Semester}",
                        Scorings = new List<DbModels.Scoring>
                        {
                            new DbModels.Scoring
                            {
                                Id = DbModels.DbModelBase.RunId(),
                                Name = $"ระหว่างภาค เต็ม {request.MidTermMaxScore}",
                                MaxScore = request.MidTermMaxScore,
                            },
                            new DbModels.Scoring
                            {
                                Id = DbModels.DbModelBase.RunId(),
                                Name = $"ปลายภาค เต็ม {request.FinalMaxScore}",
                                MaxScore = request.FinalMaxScore,
                            },
                        },
                    },
                    new DbModels.ScoringSubGroup
                    {
                        Id = DbModels.DbModelBase.RunId(),
                        Name = "ผลการแก้ไข เต็ม 100",
                        Scorings = new List<DbModels.Scoring>
                        {
                            new DbModels.Scoring
                            {
                                Id = DbModels.DbModelBase.RunId(),
                                Name = "คะแนนแก้ไข",
                                MaxScore = 100,
                            },
                        },
                    },
                },
                GradingCriterias = new List<DbModels.GradingCriteria>
                {
                    new DbModels.GradingCriteria
                    {
                        Id = DbModels.DbModelBase.RunId(),
                        Grade = "4",
                        Score = 80,
                    },
                    new DbModels.GradingCriteria
                    {
                        Id = DbModels.DbModelBase.RunId(),
                        Grade = "3.5",
                        Score = 75,
                    },
                    new DbModels.GradingCriteria
                    {
                        Id = DbModels.DbModelBase.RunId(),
                        Grade = "3",
                        Score = 70,
                    },
                    new DbModels.GradingCriteria
                    {
                        Id = DbModels.DbModelBase.RunId(),
                        Grade = "2.5",
                        Score = 65,
                    },
                    new DbModels.GradingCriteria
                    {
                        Id = DbModels.DbModelBase.RunId(),
                        Grade = "2",
                        Score = 60,
                    },
                    new DbModels.GradingCriteria
                    {
                        Id = DbModels.DbModelBase.RunId(),
                        Grade = "1.5",
                        Score = 55,
                    },
                    new DbModels.GradingCriteria
                    {
                        Id = DbModels.DbModelBase.RunId(),
                        Grade = "1",
                        Score = 50,
                    },
                    new DbModels.GradingCriteria
                    {
                        Id = DbModels.DbModelBase.RunId(),
                        Grade = "0",
                        Score = 0,
                    },
                },
            };

            await openSubjectDac.Create(documentDb);
            return Ok();
        }

        [HttpPost("many")]
        public async Task<IActionResult> CreateMany([FromBody] IEnumerable<OpenSubjectCreate> request)
        {
            var documentDbs = request.Adapt<IEnumerable<DbModels.OpenSubject>>();
            documentDbs = documentDbs.Select(x =>
            {
                x.Init(AccountsController.Username);
                return x;
            }).ToList();
            await openSubjectDac.CreateMany(documentDbs);
            return Ok();
        }

        [HttpPost("text")]
        public async Task<IActionResult> ImportByText([FromBody] OpenSubjectCreate request)
        {
            var rows = request.SubjectId.Trim().Split(Environment.NewLine).Where(x => !string.IsNullOrWhiteSpace(x));
            var rowsSplit = rows.Select(x => x.Split("\t"));
            var documentDbs = rowsSplit.Select(x =>
            {
                var documentDb = new DbModels.OpenSubject
                {
                    SubjectId = x[0],
                    MainTeacherId = x[1],
                    Description = x[2],
                };
                documentDb.Init(AccountsController.Username);

                return documentDb;
            });

            await openSubjectDac.CreateMany(documentDbs);
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, [FromBody] OpenSubjectCreate request)
        {
            var documentDb = await openSubjectDac.Get(x => x.Id == id);
            documentDb.Semester = request.Semester;
            documentDb.SubjectId = request.SubjectId;
            documentDb.MainTeacherId = request.MainTeacherId;
            documentDb.Description = request.Description;

            var mid = documentDb.Exam.ScoringSubGroups.ElementAt(0).Scorings.ElementAt(0);
            mid.Name = $"ระหว่างภาค เต็ม {request.MidTermMaxScore}";
            mid.MaxScore = request.MidTermMaxScore;
            var final = documentDb.Exam.ScoringSubGroups.ElementAt(0).Scorings.ElementAt(1);
            final.Name = $"ปลายภาค เต็ม {request.FinalMaxScore}";
            final.MaxScore = request.FinalMaxScore;

            await openSubjectDac.ReplaceOne(x => x.Id == id, documentDb);
            return Ok();
        }

        [HttpPut("delete/{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var documentCount = await classroomStudentDac.Count(x => x.RegisterOpenSubjects.Any(y => y.OpenSubjectId == id));
            if (documentCount > 0) return Conflict($"ไม่สามารถลบได้ มี {documentCount} นักเรียน ลงทะเบียนอยู่");

            await openSubjectDac.DeleteOne(x => x.Id == id);
            return Ok();
        }
    }
}
