using Mapster;
using MongoDB.Driver;
using SchoolScore.Api.DbModels;
using SchoolScore.Api.Models.Configuration;
using System.Linq.Expressions;

namespace SchoolScore.Api.DACs.Imps
{
    public class SujectDac : DataDAC<Subject>, ISujectDac<Subject>
    {
        public SujectDac(MongoDBConfiguration option) : base(option) { }

        public async Task<Models.Subject> GetWithLearningArea(IMongoCollection<LearningArea> LearningAreaCollection, Expression<Func<Subject, bool>> expression)
        {
            var document = Collection.AsQueryable()
                .Where(expression)
                .Join(LearningAreaCollection.AsQueryable(), o => o.LearningAreaId, i => i.Id, (x, y) => new
                {
                    Subject = x,
                    LearningArea = y,
                })
                .FirstOrDefault();
            var result = document.Subject.Adapt<Models.Subject>();
            result.LearningArea = document.LearningArea;
            return result;
        }
    }
}
