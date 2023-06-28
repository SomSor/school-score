namespace SchoolScore.Api.Models
{
    public class OpenSubjectCreate
    {
        public string Semester { get; set; }
        public string SubjectId { get; set; }
        public string MainTeacherId { get; set; }
        public string Description { get; set; }

        public decimal MidTermMaxScore { get; set; }
        public decimal FinalMaxScore { get; set; }
    }
}
