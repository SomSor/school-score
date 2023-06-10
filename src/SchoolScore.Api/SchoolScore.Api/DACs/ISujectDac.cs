using MongoDB.Driver;
using SchoolScore.Api.DbModels;
using System.Linq.Expressions;

namespace SchoolScore.Api.DACs
{
    public interface ISujectDac<T> : IDataDAC<T>
    {
        Task<Models.Subject> GetWithLearningArea(IMongoCollection<LearningArea> Collection, Expression<Func<Subject, bool>> expression);
    }
}
