using MongoDB.Driver;
using SchoolScore.Api.DbModels;
using System.Linq.Expressions;

namespace SchoolScore.Api.DACs
{
    public interface ISujectDac<T> : IDataDAC<T>
    {
        Task<IEnumerable<Models.Subject>> ListWithLearningArea(IMongoCollection<LearningArea> learningAreaCollection, Expression<Func<Subject, bool>> expression, int page = 1, int? pageSize = null);
        Task<Models.Subject> GetWithLearningArea(IMongoCollection<LearningArea> learningAreaCollection, Expression<Func<Subject, bool>> expression);
    }
}
