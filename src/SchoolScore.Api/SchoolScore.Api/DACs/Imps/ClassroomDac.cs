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

        public async Task<IEnumerable<Models.Classroom>> ListWithTeacher(
            IMongoCollection<Teacher> teacherCollection,
            IMongoCollection<ClassroomStudent> classroomStudentCollection,
            Expression<Func<Classroom, bool>> expression, string schoolYearId,
            int page = 1, int? pageSize = null)
        {
            var query = Collection.AsQueryable()
                .Where(expression)
                .Where(x => x.SchoolYearId == schoolYearId)
                .Join(teacherCollection.AsQueryable(), o => o.TeacherId, i => i.Id, (x, y) => new
                {
                    Classroom = x,
                    Teacher = y,
                })
                .GroupJoin(classroomStudentCollection.AsQueryable(), o => o.Classroom.Id, i => i.ClassroomId, (x, y) => new
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

        public async Task<Models.Classroom> GetWithTeacher(
            IMongoCollection<Teacher> teacherCollection,
            IMongoCollection<ClassroomStudent> classroomStudentCollection,
            Expression<Func<Classroom, bool>> expression, string schoolYearId)
        {
            var query = Collection.AsQueryable()
                .Where(expression)
                .Where(x => x.SchoolYearId == schoolYearId)
                .Join(teacherCollection.AsQueryable(), o => o.TeacherId, i => i.Id, (x, y) => new
                {
                    Classroom = x,
                    Teacher = y,
                })
                .GroupJoin(classroomStudentCollection.AsQueryable(), o => o.Classroom.Id, i => i.ClassroomId, (x, y) => new
                {
                    Classroom = x.Classroom,
                    Teacher = x.Teacher,
                    StudentCount = y.Count(),
                }).FirstOrDefault();

            var document = query.Classroom.Adapt<Models.Classroom>();
            document.Teacher = query.Teacher;
            document.StudentCount = query.StudentCount;
            return document;
        }

        public async Task<Models.Classroom> GetWithTeacherAndStudent(
            IMongoCollection<Teacher> teacherCollection,
            IMongoCollection<ClassroomStudent> classroomStudentCollection,
            IMongoCollection<Student> studentCollection,
            Expression<Func<Classroom, bool>> expression, string schoolYearId)
        {
            var document = Collection.AsQueryable()
                .Where(expression)
                .Where(x => x.SchoolYearId == schoolYearId)
                .Join(teacherCollection.AsQueryable(), o => o.TeacherId, i => i.Id, (x, y) => new
                {
                    Classroom = x,
                    Teacher = y,
                })
                .FirstOrDefault();
            var classroomStudents = classroomStudentCollection.AsQueryable()
                .Where(x => x.ClassroomId == document.Classroom.Id)
                .Join(studentCollection.AsQueryable(), o => o.StudentId, i => i.Id, (x, y) => new
                {
                    ClassroomStudent = x,
                    Student = y,
                }).ToList();
            var result = document.Classroom.Adapt<Models.Classroom>();
            result.Teacher = document.Teacher;
            result.Students = classroomStudents.Select(x => x.Student);
            result.StudentCount = classroomStudents.Count();
            return result;
        }
    }
}
