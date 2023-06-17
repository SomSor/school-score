using Mapster;
using MongoDB.Driver;
using SchoolScore.Api.DbModels;
using SchoolScore.Api.Models.Configuration;
using System.Linq.Expressions;

namespace SchoolScore.Api.DACs.Imps
{
    public class ClassroomDac : DataDAC<Classroom>, IClassroomDac<Classroom>
    {
        public ClassroomDac(MongoDBConfiguration option) : base(option) { }

        public async Task<IEnumerable<Models.Classroom>> ListWithTeacher(IMongoCollection<Teacher> teacherCollection, IMongoCollection<Student> studentCollection, Expression<Func<Classroom, bool>> expression, int page = 1, int? pageSize = null)
        {
            var query = Collection.AsQueryable()
                .Where(expression)
                .Join(teacherCollection.AsQueryable(), o => o.TeacherId, i => i.Id, (x, y) => new
                {
                    Classroom = x,
                    Teacher = y,
                })
                .GroupJoin(studentCollection.AsQueryable(), o => o.Classroom.Id, i => i.Id, (x, y) => new
                {
                    Classroom = x.Classroom,
                    Teacher = x.Teacher,
                    StudentCount = y.Count(),
                });

            if (pageSize is not null && page >= 1)
            {
                var skip = (page - 1) * pageSize.Value;
                query = query.Skip(skip).Take(pageSize.Value);
            }
            var result = query.ToList()
                .Select(x =>
                {
                    var document = x.Classroom.Adapt<Models.Classroom>();
                    document.Teacher = x.Teacher;
                    document.StudentCount = x.StudentCount;
                    return document;
                });
            return result;
        }

        public async Task<IEnumerable<Models.Classroom>> ListAllWithTeacher(IMongoCollection<Teacher> teacherCollection, IMongoCollection<Student> studentCollection, Expression<Func<Classroom, bool>> expression)
        {
            var query = Collection.AsQueryable()
                .Where(expression)
                .Join(teacherCollection.AsQueryable(), o => o.TeacherId, i => i.Id, (x, y) => new
                {
                    Classroom = x,
                    Teacher = y,
                })
                .GroupJoin(studentCollection.AsQueryable(), o => o.Classroom.Id, i => i.Id, (x, y) => new
                {
                    Classroom = x.Classroom,
                    Teacher = x.Teacher,
                    StudentCount = y.Count(),
                });

            var result = query.ToList()
                .Select(x =>
                {
                    var document = x.Classroom.Adapt<Models.Classroom>();
                    document.Teacher = x.Teacher;
                    document.StudentCount = x.StudentCount;
                    return document;
                });
            return result;
        }

        public async Task<Models.Classroom> GetWithTeacherAndStudent(IMongoCollection<Teacher> teacherCollection, IMongoCollection<Student> studentCollection, Expression<Func<Classroom, bool>> expression)
        {
            var document = Collection.AsQueryable()
                .Where(expression)
                .Join(teacherCollection.AsQueryable(), o => o.TeacherId, i => i.Id, (x, y) => new
                {
                    Classroom = x,
                    Teacher = y,
                })
                .GroupJoin(studentCollection.AsQueryable(), o => o.Classroom.Id, i => i.Id, (x, y) => new
                {
                    Classroom = x.Classroom,
                    Teacher = x.Teacher,
                    Students = y,
                })
                .FirstOrDefault();
            var result = document.Classroom.Adapt<Models.Classroom>();
            result.Teacher = document.Teacher;
            result.Students = document.Students;
            result.StudentCount = document.Students.Count();
            return result;
        }
    }
}
