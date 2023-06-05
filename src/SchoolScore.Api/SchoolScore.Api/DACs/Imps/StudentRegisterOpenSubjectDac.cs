using SchoolScore.Api.DbModels;
using SchoolScore.Api.Models.Configuration;

namespace SchoolScore.Api.DACs.Imps
{
    public class StudentRegisterOpenSubjectDac : DataDAC<StudentRegisterOpenSubject>, IStudentRegisterOpenSubjectDac<StudentRegisterOpenSubject>
    {
        public StudentRegisterOpenSubjectDac(MongoDBConfiguration option) : base(option) { }
    }
}
