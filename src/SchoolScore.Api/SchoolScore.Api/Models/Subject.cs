namespace SchoolScore.Api.Models
{
    public class Subject : DbModels.Subject
    {
        public DbModels.LearningArea LearningArea { get; set; }
    }
}
