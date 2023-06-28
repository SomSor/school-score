namespace SchoolScore.Api.Models
{
    public class OpenSubject : DbModels.OpenSubject
    {
        public DbModels.Subject Subject { get; set; }
        public DbModels.Teacher Teacher { get; set; }

        public decimal MidTermMaxScore { get; set; }
        public decimal FinalMaxScore { get; set; }
    }
}
