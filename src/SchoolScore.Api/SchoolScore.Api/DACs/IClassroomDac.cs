using MongoDB.Driver;
using SchoolScore.Api.DbModels;
using System.Linq.Expressions;

namespace SchoolScore.Api.DACs
{
    public interface IClassroomDac<T> : IDataDAC<T>
    {
        Task<IEnumerable<Models.Classroom>> ListWithTeacher(
            IMongoCollection<Teacher> teacherCollection,
            IMongoCollection<ClassroomStudent> classroomStudentCollection,
            Expression<Func<Classroom, bool>> expression, string schoolYearId,
            int page = 1, int? pageSize = null);
        Task<Models.Classroom> GetWithTeacher(
            IMongoCollection<Teacher> teacherCollection,
            IMongoCollection<ClassroomStudent> classroomStudentCollection,
            Expression<Func<Classroom, bool>> expression, string schoolYearId);
        Task<Models.Classroom> GetWithTeacherAndStudent(
            IMongoCollection<Teacher> teacherCollection,
            IMongoCollection<ClassroomStudent> classroomStudentCollection,
            IMongoCollection<Student> studentCollection,
            Expression<Func<Classroom, bool>> expression, string schoolYearId);
    }
}
