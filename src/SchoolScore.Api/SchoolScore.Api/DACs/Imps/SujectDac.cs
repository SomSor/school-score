using SchoolScore.Api.DbModels;
using SchoolScore.Api.Models.Configuration;

namespace SchoolScore.Api.DACs.Imps
{
    public class SujectDac : DataDAC<Suject>, ISujectDac<Suject>
    {
        public SujectDac(MongoDBConfiguration option) : base(option) { }
    }
}
