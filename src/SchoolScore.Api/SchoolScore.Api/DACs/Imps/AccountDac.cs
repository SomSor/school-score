using Mapster;
using MongoDB.Driver;
using SchoolScore.Api.DbModels;
using SchoolScore.Api.Models.Configuration;
using System.Linq.Expressions;

namespace SchoolScore.Api.DACs.Imps
{
    public class AccountDac : DataDAC<Account>, IAccountDac<Account>
    {
        public AccountDac(MongoDBConfiguration option) : base(option) { }

        public async Task<Models.Account> GetWithTeacher(IMongoCollection<Teacher> teacherCollection, Expression<Func<Account, bool>> expression)
        {
            var document = Collection.AsQueryable()
                .Where(expression)
                .Join(teacherCollection.AsQueryable(), o => o.PersonId, i => i.Id, (x, y) => new
                {
                    Account = x,
                    Teacher = y,
                })
                .FirstOrDefault();
            if (document == null) return null;
            var result = document.Account?.Adapt<Models.Account>();
            result.Teacher = document.Teacher;
            return result;
        }
    }
}
