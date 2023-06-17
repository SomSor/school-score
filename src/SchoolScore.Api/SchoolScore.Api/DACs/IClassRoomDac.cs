using MongoDB.Driver;
using SchoolScore.Api.DbModels;
using System.Linq.Expressions;

namespace SchoolScore.Api.DACs
{
    public interface IClassroomDac<T> : IDataDAC<T>
    {
        Task<IEnumerable<Models.Classroom>> ListWithTeacher(IMongoCollection<Teacher> teacherCollection, IMongoCollection<Student> studentCollection, Expression<Func<Classroom, bool>> expression, int page = 1, int? pageSize = null);
        Task<IEnumerable<Models.Classroom>> ListAllWithTeacher(IMongoCollection<Teacher> teacherCollection, IMongoCollection<Student> studentCollection, Expression<Func<Classroom, bool>> expression);
        Task<Models.Classroom> GetWithTeacherAndStudent(IMongoCollection<Teacher> teacherCollection, IMongoCollection<Student> studentCollection, Expression<Func<Classroom, bool>> expression);
    }
}
