using MongoDB.Driver;
using SchoolScore.Api.DbModels;
using System.Linq.Expressions;

namespace SchoolScore.Api.DACs
{
    public interface IOpenSubjectDac<T> : IDataDAC<T>
    {
        Task<IEnumerable<Models.OpenSubject>> ListWithSubjectAndTeacher(IMongoCollection<Subject> subjectCollection, IMongoCollection<Teacher> teacherCollection, Expression<Func<OpenSubject, bool>> expression, int page = 1, int? pageSize = null);
        Task<Models.OpenSubject> GetWithSubjectAndTeacher(IMongoCollection<Subject> subjectCollection, IMongoCollection<Teacher> teacherCollection, Expression<Func<OpenSubject, bool>> expression);
    }
}
