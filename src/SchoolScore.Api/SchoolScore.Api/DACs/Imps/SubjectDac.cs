using Mapster;
using MongoDB.Driver;
using SchoolScore.Api.DbModels;
using SchoolScore.Api.Models.Configuration;
using System.Linq.Expressions;

namespace SchoolScore.Api.DACs.Imps
{
    public class SubjectDac : DataDAC<Subject>, ISubjectDac<Subject>
    {
        public SubjectDac(MongoDBConfiguration option) : base(option) { }

        public async Task<IEnumerable<Models.Subject>> ListWithLearningArea(
            IMongoCollection<LearningArea> learningAreaCollection,
            Expression<Func<Subject, bool>> expression, int page = 1, int? pageSize = null)
        {
            var query = Collection.AsQueryable()
                .Where(expression)
                .Join(learningAreaCollection.AsQueryable(), o => o.LearningAreaId, i => i.Id, (x, y) => new
                {
                    Subject = x,
                    LearningArea = y,
                });

            if (pageSize is not null && page >= 1)
            {
                var skip = (page - 1) * pageSize.Value;
                query = query.Skip(skip).Take(pageSize.Value);
            }
            var result = query.ToList()
                .Select(x =>
                {
                    var document = x.Subject.Adapt<Models.Subject>();
                    document.LearningArea = x.LearningArea;
                    return document;
                });
            return result;
        }

        public async Task<Models.Subject> GetWithLearningArea(
            IMongoCollection<LearningArea> learningAreaCollection,
            Expression<Func<Subject, bool>> expression)
        {
            var document = Collection.AsQueryable()
                .Where(expression)
                .Join(learningAreaCollection.AsQueryable(), o => o.LearningAreaId, i => i.Id, (x, y) => new
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
