using SchoolScore.Api.DbModels;
using SchoolScore.Api.Models.Configuration;

namespace SchoolScore.Api.DACs.Imps
{
    public class ScoringGroupDac : DataDAC<ScoringGroup>, IScoringGroupDac<ScoringGroup>
    {
        public ScoringGroupDac(MongoDBConfiguration option) : base(option) { }
    }
}
