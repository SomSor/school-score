namespace SchoolScore.Api.DbModels
{
    public class ClassroomStudent : DbModelBase
    {
        public string ClassroomId { get; set; }
        public string StudentId { get; set; }
        public string SchoolYearId { get; set; }
    }
}
