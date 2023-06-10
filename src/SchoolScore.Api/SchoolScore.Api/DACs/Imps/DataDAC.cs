using MongoDB.Driver;
using SchoolScore.Api.Models.Configuration;
using System.Linq.Expressions;

namespace SchoolScore.Api.DACs.Imps
{
    public class DataDAC<T> : IDataDAC<T>
        where T : DbModels.DbModelBase
    {
        private readonly IMongoClient client;
        private readonly IMongoDatabase _database;

        public IMongoCollection<T> Collection => _database.GetCollection<T>(typeof(T).Name);

        public DataDAC(MongoDBConfiguration option)
        {
            client = new MongoClient(option.DefaultConnection);
            _database = client.GetDatabase(option.DatabaseName);
        }

        public async Task<T> Get(Expression<Func<T, bool>> expression)
        {
            return await Collection.Find(expression).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<T>> List(Expression<Func<T, bool>> expression, int page = 1, int? pageSize = null)
        {
            var query = Collection.Find(expression);

            if (pageSize is not null && page >= 1)
            {
                var skip = (page - 1) * pageSize.Value;
                query = query.Skip(skip).Limit(pageSize.Value);
            }
            var result = await query.ToListAsync();
            return result;
        }

        public async Task<IEnumerable<T>> ListAll(Expression<Func<T, bool>> expression)
        {
            var query = Collection.Find(expression);
            var result = await query.ToListAsync();
            return result;
        }

        public async Task Create(T document)
        {
            await Collection.InsertOneAsync(document);
        }

        public async Task CreateMany(IEnumerable<T> documents)
        {
            await Collection.InsertManyAsync(documents);
        }

        public async Task DeleteOne(Expression<Func<T, bool>> expression)
        {
            await Collection.DeleteOneAsync(expression);
        }

        public async Task DeleteMany(Expression<Func<T, bool>> expression)
        {
            await Collection.DeleteManyAsync(expression);
        }

        public async Task ReplaceOne(Expression<Func<T, bool>> expression, T document)
        {
            await Collection.ReplaceOneAsync(expression, document);
        }

        public async Task<long> Count(Expression<Func<T, bool>> expression)
        {
            return await Collection.CountDocumentsAsync(expression);
        }
    }
}
