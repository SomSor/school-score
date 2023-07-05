using Mapster;
using MongoDB.Driver;
using SchoolScore.Api.DbModels;
using SchoolScore.Api.Models.Configuration;
using System.Linq.Expressions;

namespace SchoolScore.Api.DACs.Imps
{
    public class ClassroomStudentDac : DataDAC<ClassroomStudent>, IClassroomStudentDac<ClassroomStudent>
    {
        public ClassroomStudentDac(MongoDBConfiguration option) : base(option) { }

        public async Task<IEnumerable<Models.StudentInClassroom>> ListWithStudent(
            IMongoCollection<Student> studentCollection,
            string classroomId, int page = 1, int? pageSize = null)
        {
            var query = Collection.AsQueryable()
                .Where(x => x.ClassroomId == classroomId)
                .Join(studentCollection.AsQueryable(), o => o.StudentId, i => i.Id, (x, y) => new
                {
                    ClassroomStudent = x,
                    Student = y,
                });

            if (pageSize is not null && page >= 1)
            {
                var skip = (page - 1) * pageSize.Value;
                query = query.Skip(skip).Take(pageSize.Value);
            }
            return query.ToList()
                .Select(x =>
                {
                    var y = x.ClassroomStudent.Adapt<Models.StudentInClassroom>();
                    y.Student = x.Student;
                    return y;
                });
        }

        public async Task<IEnumerable<Models.ClassroomStudent>> ListWithClassroomAndStudent(
            IMongoCollection<Classroom> classroomCollection,
            IMongoCollection<Student> studentCollection,
            Expression<Func<ClassroomStudent, bool>> expression, int page = 1, int? pageSize = null)
        {
            var query = Collection.AsQueryable()
                .Where(expression)
                .Join(classroomCollection.AsQueryable(), o => o.ClassroomId, i => i.Id, (x, y) => new
                {
                    ClassroomStudent = x,
                    Classroom = y,
                })
                .Join(studentCollection.AsQueryable(), o => o.ClassroomStudent.StudentId, i => i.Id, (x, y) => new
                {
                    ClassroomStudent = x.ClassroomStudent,
                    Classroom = x.Classroom,
                    Student = y,
                });

            if (pageSize is not null && page >= 1)
            {
                var skip = (page - 1) * pageSize.Value;
                query = query.Skip(skip).Take(pageSize.Value);
            }
            var result = query.ToList()
                .Select(x =>
                {
                    var document = x.ClassroomStudent.Adapt<Models.ClassroomStudent>();
                    document.Classroom = x.Classroom;
                    document.Student = x.Student;
                    return document;
                });
            return result;
        }

        public async Task<Models.ClassroomStudent> GetWithClassroomAndStudent(
            IMongoCollection<Classroom> classroomCollection,
            IMongoCollection<Student> studentCollection,
            Expression<Func<ClassroomStudent, bool>> expression)
        {
            var document = Collection.AsQueryable()
                .Where(expression)
                .Join(classroomCollection.AsQueryable(), o => o.ClassroomId, i => i.Id, (x, y) => new
                {
                    ClassroomStudent = x,
                    Classroom = y,
                })
                .Join(studentCollection.AsQueryable(), o => o.ClassroomStudent.StudentId, i => i.Id, (x, y) => new
                {
                    ClassroomStudent = x.ClassroomStudent,
                    Classroom = x.Classroom,
                    Student = y,
                })
                .FirstOrDefault();
            var result = document.ClassroomStudent.Adapt<Models.ClassroomStudent>();
            result.Classroom = document.Classroom;
            result.Student = document.Student;
            return result;
        }

        public async Task<Models.RegisteredOpenSubject> ListWithOpenSubject(
            IMongoCollection<Classroom> classroomCollection,
            IMongoCollection<Student> studentCollection,
            IMongoCollection<OpenSubject> openSubjectCollection,
            IMongoCollection<Subject> subjectCollection,
            Expression<Func<ClassroomStudent, bool>> expression, int page = 1, int? pageSize = null)
        {
            var query = Collection.AsQueryable()
                .Where(expression)
                .Join(classroomCollection.AsQueryable(), o => o.ClassroomId, i => i.Id, (x, y) => new
                {
                    ClassroomStudent = x,
                    Classroom = y,
                })
                .Join(studentCollection.AsQueryable(), o => o.ClassroomStudent.StudentId, i => i.Id, (x, y) => new
                {
                    ClassroomStudent = x.ClassroomStudent,
                    Classroom = x.Classroom,
                    Student = y,
                });

            if (pageSize is not null && page >= 1)
            {
                var skip = (page - 1) * pageSize.Value;
                query = query.Skip(skip).Take(pageSize.Value);
            }
            var queryResult = query.ToList();

            var openSubjectIds = queryResult.SelectMany(x => x.ClassroomStudent.RegisterOpenSubjects.Select(y => y.OpenSubjectId)).GroupBy(x => x).Select(x => x.Key).ToList();
            var openSubjects = await openSubjectCollection.Find(x => openSubjectIds.Contains(x.Id)).ToListAsync();

            var subjectIds = openSubjects.Select(x => x.SubjectId).ToList();
            var subjects = await subjectCollection.Find(x => subjectIds.Contains(x.Id)).ToListAsync();

            return new Models.RegisteredOpenSubject
            {
                Data = queryResult.GroupBy(x => x.Classroom.Id).Select(x => new Models.ClassroomOpenSubject
                {
                    Classroom = x.First().Classroom,
                    OpenSubjectIds = openSubjectIds,
                    ClassroomStudents = x.Select(y => y.ClassroomStudent),
                }),
                Subjects = subjects,
                OpenSubjects = openSubjects,
            };
        }
    }
}
