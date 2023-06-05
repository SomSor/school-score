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

        public async Task<List<T>> List(Expression<Func<T, bool>> expression)
        {
            return await Collection.Find(expression).ToListAsync();
        }

        public async Task Create(T document)
        {
            await Collection.InsertOneAsync(document);
        }

        public async Task CreateMany(List<T> documents)
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
    }
}
