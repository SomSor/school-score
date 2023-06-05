using SchoolScore.Api.DbModels;
using SchoolScore.Api.Models.Configuration;

namespace SchoolScore.Api.DACs.Imps
{
    public class ClassRoomStudentDac : DataDAC<ClassRoomStudent>, IClassRoomStudentDac<ClassRoomStudent>
    {
        public ClassRoomStudentDac(MongoDBConfiguration option) : base(option) { }
    }
}
