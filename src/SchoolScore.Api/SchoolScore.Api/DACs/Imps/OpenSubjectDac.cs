using Mapster;
using MongoDB.Driver;
using SchoolScore.Api.DbModels;
using SchoolScore.Api.Models.Configuration;
using System.Linq.Expressions;

namespace SchoolScore.Api.DACs.Imps
{
    public class OpenSubjectDac : DataDAC<OpenSubject>, IOpenSubjectDac<OpenSubject>
    {
        public OpenSubjectDac(MongoDBConfiguration option) : base(option) { }

        public async Task<IEnumerable<Models.OpenSubject>> ListWithSubjectAndTeacher(
            IMongoCollection<Subject> subjectCollection,
            IMongoCollection<Teacher> teacherCollection,
            Expression<Func<OpenSubject, bool>> expression, string schoolYearId,
            int page = 1, int? pageSize = null)
        {
            var query = Collection.AsQueryable()
                .Where(expression)
                .Where(x => x.SchoolYearId == schoolYearId)
                .Join(subjectCollection.AsQueryable(), o => o.SubjectId, i => i.Id, (x, y) => new
                {
                    OpenSubject = x,
                    Subject = y,
                })
                .Join(teacherCollection.AsQueryable(), o => o.OpenSubject.MainTeacherId, i => i.Id, (x, y) => new
                {
                    OpenSubject = x.OpenSubject,
                    Subject = x.Subject,
                    Teacher = y,
                });

            if (pageSize is not null && page >= 1)
            {
                var skip = (page - 1) * pageSize.Value;
                query = query.Skip(skip).Take(pageSize.Value);
            }
            var result = query.ToList()
                .Select(x =>
                {
                    var document = x.OpenSubject.Adapt<Models.OpenSubject>();
                    document.Subject = x.Subject;
                    document.Teacher = x.Teacher;
                    return document;
                });
            return result;
        }

        public async Task<Models.OpenSubject> GetWithSubjectAndTeacher(
            IMongoCollection<Subject> subjectCollection,
            IMongoCollection<Teacher> teacherCollection,
            Expression<Func<OpenSubject, bool>> expression)
        {
            var document = Collection.AsQueryable()
                .Where(expression)
                .Join(subjectCollection.AsQueryable(), o => o.SubjectId, i => i.Id, (x, y) => new
                {
                    OpenSubject = x,
                    Subject = y,
                })
                .Join(teacherCollection.AsQueryable(), o => o.OpenSubject.MainTeacherId, i => i.Id, (x, y) => new
                {
                    OpenSubject = x.OpenSubject,
                    Subject = x.Subject,
                    Teacher = y,
                })
                .FirstOrDefault();
            var result = document.OpenSubject.Adapt<Models.OpenSubject>();
            result.Subject = document.Subject;
            result.Teacher = document.Teacher;
            result.MidTermMaxScore = result.Exam.ScoringSubGroups.ElementAt(0).Scorings.ElementAt(0).MaxScore;
            result.FinalMaxScore = result.Exam.ScoringSubGroups.ElementAt(0).Scorings.ElementAt(1).MaxScore;
            return result;
        }
    }
}
