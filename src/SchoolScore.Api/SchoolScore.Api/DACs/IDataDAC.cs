using MongoDB.Driver;
using System.Linq.Expressions;

namespace SchoolScore.Api.DACs
{
    public interface IDataDAC<T>
    {
        IMongoCollection<T> Collection { get; }

        Task<T> Get(Expression<Func<T, bool>> expression);
        Task<IEnumerable<T>> List(Expression<Func<T, bool>> expression, int page = 1, int? pageSize = null);
        Task<IEnumerable<T>> ListAll(Expression<Func<T, bool>> expression);
        Task Create(T document);
        Task CreateMany(IEnumerable<T> documents);
        Task DeleteOne(Expression<Func<T, bool>> expression);
        Task DeleteMany(Expression<Func<T, bool>> expression);
        Task ReplaceOne(Expression<Func<T, bool>> expression, T document);
        Task<long> Count(Expression<Func<T, bool>> expression);
    }
}
