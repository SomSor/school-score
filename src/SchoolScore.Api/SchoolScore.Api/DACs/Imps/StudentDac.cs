using SchoolScore.Api.DbModels;
using SchoolScore.Api.Models.Configuration;

namespace SchoolScore.Api.DACs.Imps
{
    public class StudentDac : DataDAC<Student>, IStudentDac<Student>
    {
        public StudentDac(MongoDBConfiguration option) : base(option) { }
    }
}
