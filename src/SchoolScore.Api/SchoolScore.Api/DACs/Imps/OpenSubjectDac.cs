using SchoolScore.Api.DbModels;
using SchoolScore.Api.Models.Configuration;

namespace SchoolScore.Api.DACs.Imps
{
    public class OpenSubjectDac : DataDAC<OpenSubject>, IOpenSubjectDac<OpenSubject>
    {
        public OpenSubjectDac(MongoDBConfiguration option) : base(option) { }
    }
}
