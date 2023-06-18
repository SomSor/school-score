using MongoDB.Driver;
using SchoolScore.Api.DbModels;
using System.Linq.Expressions;

namespace SchoolScore.Api.DACs
{
    public interface IClassroomStudentDac<T> : IDataDAC<T>
    {
        Task<IEnumerable<Models.ClassroomStudent>> ListWithClassroom(IMongoCollection<Classroom> classroomCollection, IMongoCollection<Student> studentCollection, Expression<Func<ClassroomStudent, bool>> expression, int page = 1, int? pageSize = null);
        Task<Models.ClassroomStudent> GetWithClassroomAndStudent(IMongoCollection<Classroom> classroomCollection, IMongoCollection<Student> studentCollection, Expression<Func<ClassroomStudent, bool>> expression);
    }
}
