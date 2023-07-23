using Mapster;
using MongoDB.Driver;
using SchoolScore.Api.DbModels;
using SchoolScore.Api.Models.Configuration;
using System.Linq.Expressions;

namespace SchoolScore.Api.DACs.Imps
{
    public class TeacherDac : DataDAC<Teacher>, ITeacherDac<Teacher>
    {
        public TeacherDac(MongoDBConfiguration option) : base(option) { }

        public async Task<IEnumerable<Models.Teacher>> ListWithTeacher(IMongoCollection<Account> accountCollection, Expression<Func<Teacher, bool>> expression)
        {
            var query = Collection.AsQueryable()
                .Where(expression)
                .Join(accountCollection.AsQueryable(), o => o.Id, i => i.PersonId, (x, y) => new
                {
                    Teacher = x,
                    Account = y,
                });

            var result = query.ToList()
                .Select(x =>
                {
                    var document = x.Teacher.Adapt<Models.Teacher>();
                    document.Account = x.Account;
                    document.Account.Password = string.Empty;
                    return document;
                });
            return result;
        }
    }
}
