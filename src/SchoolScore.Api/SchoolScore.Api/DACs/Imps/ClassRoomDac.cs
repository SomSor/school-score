using SchoolScore.Api.DbModels;
using SchoolScore.Api.Models.Configuration;

namespace SchoolScore.Api.DACs.Imps
{
    public class ClassRoomDac : DataDAC<ClassRoom>, IClassRoomDac<ClassRoom>
    {
        public ClassRoomDac(MongoDBConfiguration option) : base(option) { }
    }
}
