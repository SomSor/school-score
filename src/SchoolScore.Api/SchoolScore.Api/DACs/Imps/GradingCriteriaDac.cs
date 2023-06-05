using SchoolScore.Api.DbModels;
using SchoolScore.Api.Models.Configuration;

namespace SchoolScore.Api.DACs.Imps
{
    public class GradingCriteriaDac : DataDAC<GradingCriteria>, IGradingCriteriaDac<GradingCriteria>
    {
        public GradingCriteriaDac(MongoDBConfiguration option) : base(option) { }
    }
}
