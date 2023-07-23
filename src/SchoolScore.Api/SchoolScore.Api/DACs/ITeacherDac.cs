using MongoDB.Driver;
using SchoolScore.Api.DbModels;
using System.Linq.Expressions;

namespace SchoolScore.Api.DACs
{
    public interface ITeacherDac<T> : IDataDAC<T>
    {
        Task<IEnumerable<Models.Teacher>> ListWithTeacher(IMongoCollection<Account> accountCollection, Expression<Func<Teacher, bool>> expression);
    }
}
