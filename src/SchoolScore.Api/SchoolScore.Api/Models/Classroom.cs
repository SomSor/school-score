namespace SchoolScore.Api.Models
{
    public class Classroom : DbModels.Classroom
    {
        public DbModels.Teacher Teacher { get; set; }
        public int StudentCount { get; set; }
        public IEnumerable<DbModels.Student> Students { get; set; }
    }
}
