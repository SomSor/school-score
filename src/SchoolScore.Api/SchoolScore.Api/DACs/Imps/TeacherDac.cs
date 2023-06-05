using SchoolScore.Api.DbModels;
using SchoolScore.Api.Models.Configuration;

namespace SchoolScore.Api.DACs.Imps
{
    public class TeacherDac : DataDAC<Teacher>, ITeacherDac<Teacher>
    {
        public TeacherDac(MongoDBConfiguration option) : base(option) { }
    }
}
