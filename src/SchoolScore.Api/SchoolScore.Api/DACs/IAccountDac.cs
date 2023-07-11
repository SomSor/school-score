using MongoDB.Driver;
using SchoolScore.Api.DbModels;
using System.Linq.Expressions;

namespace SchoolScore.Api.DACs
{
    public interface IAccountDac<T> : IDataDAC<T>
    {
        Task<Models.Account> GetWithTeacher(
            IMongoCollection<Teacher> teacherCollection,
            Expression<Func<Account, bool>> expression);
    }
}
