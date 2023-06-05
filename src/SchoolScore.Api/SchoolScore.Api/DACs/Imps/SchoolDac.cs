using SchoolScore.Api.DbModels;
using SchoolScore.Api.Models.Configuration;

namespace SchoolScore.Api.DACs.Imps
{
    public class SchoolDac : DataDAC<School>, ISchoolDac<School>
    {
        public SchoolDac(MongoDBConfiguration option) : base(option) { }
    }
}
