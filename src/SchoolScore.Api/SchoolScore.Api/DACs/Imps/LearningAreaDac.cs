using SchoolScore.Api.DbModels;
using SchoolScore.Api.Models.Configuration;

namespace SchoolScore.Api.DACs.Imps
{
    public class LearningAreaDac : DataDAC<LearningArea>, ILearningAreaDac<LearningArea>
    {
        public LearningAreaDac(MongoDBConfiguration option) : base(option) { }
    }
}
