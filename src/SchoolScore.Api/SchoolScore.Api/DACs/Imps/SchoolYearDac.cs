using SchoolScore.Api.DbModels;
using SchoolScore.Api.Models.Configuration;

namespace SchoolScore.Api.DACs.Imps
{
    public class SchoolYearDac : DataDAC<SchoolYear>, ISchoolYearDac<SchoolYear>
    {
        public SchoolYearDac(MongoDBConfiguration option) : base(option) { }
    }
}
