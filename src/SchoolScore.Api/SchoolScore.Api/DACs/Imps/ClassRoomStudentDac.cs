using SchoolScore.Api.DbModels;
using SchoolScore.Api.Models.Configuration;

namespace SchoolScore.Api.DACs.Imps
{
    public class ClassroomStudentDac : DataDAC<ClassroomStudent>, IClassroomStudentDac<ClassroomStudent>
    {
        public ClassroomStudentDac(MongoDBConfiguration option) : base(option) { }
    }
}
