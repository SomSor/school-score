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

        public bool Sunday { get; set; }
        public bool Monday { get; set; }
        public bool Tuesday { get; set; }
        public bool Wednesday { get; set; }
        public bool Thursday { get; set; }
        public bool Friday { get; set; }
        public bool Saturday { get; set; }
    }
}
