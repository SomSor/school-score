namespace SchoolScore.Api.Models
{
    public class Teacher : DbModels.Teacher
    {
        public DbModels.Account Account { get; set; }
    }
}
