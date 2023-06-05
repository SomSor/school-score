namespace SchoolScore.Api.DbModels
{
    public class ClassRoomStudent : DbModelBase
    {
        public string ClassRoomId { get; set; }
        public string StudentId { get; set; }
        public string SchoolYearId { get; set; }
    }
}
