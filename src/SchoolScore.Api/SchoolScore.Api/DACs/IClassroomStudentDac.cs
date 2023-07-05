using MongoDB.Driver;
using SchoolScore.Api.DbModels;
using System.Linq.Expressions;

namespace SchoolScore.Api.DACs
{
    public interface IClassroomStudentDac<T> : IDataDAC<T>
    {
        Task<IEnumerable<Models.StudentInClassroom>> ListWithStudent(
            IMongoCollection<Student> studentCollection,
            string classroomId, int page = 1, int? pageSize = null);
        Task<IEnumerable<Models.ClassroomStudent>> ListWithClassroomAndStudent(
            IMongoCollection<Classroom> classroomCollection,
            IMongoCollection<Student> studentCollection,
            Expression<Func<ClassroomStudent, bool>> expression, int page = 1, int? pageSize = null);
        Task<Models.ClassroomStudent> GetWithClassroomAndStudent(
            IMongoCollection<Classroom> classroomCollection,
            IMongoCollection<Student> studentCollection,
            Expression<Func<ClassroomStudent, bool>> expression);
        Task<Models.RegisteredOpenSubject> ListWithOpenSubject(
            IMongoCollection<Classroom> classroomCollection,
            IMongoCollection<Student> studentCollection,
            IMongoCollection<OpenSubject> openSubjectCollection,
            IMongoCollection<Subject> subjectCollection,
            Expression<Func<ClassroomStudent, bool>> expression, int page = 1, int? pageSize = null);
    }
}
