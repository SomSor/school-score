using MongoDB.Driver;
using SchoolScore.Api.DbModels;
using SchoolScore.Api.Models.Configuration;

namespace SchoolScore.Api.DACs.Imps
{
    public class SchoolYearDac : DataDAC<SchoolYear>, ISchoolYearDac<SchoolYear>
    {
        public SchoolYearDac(MongoDBConfiguration option) : base(option) { }

        public async Task<SchoolYear> Current()
        {
            var document = await Collection.Find(x => true).SortByDescending(x => x.ActivatedDate).FirstOrDefaultAsync();
            if (document.ActivatedDate.HasValue) return document;
            else return null;
        }
    }
}
