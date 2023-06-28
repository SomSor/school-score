namespace SchoolScore.Api.Models
{
    public class SaveSCoreRequest
    {
        public string ClassroomId { get; set; }
        public string OpenSubjectId { get; set; }
        public IEnumerable<ClassroomStudentScore> ClassroomStudentScores { get; set; }
    }

    public class ClassroomStudentScore
    {
        public string StudentId { get; set; }
        public string ScoringSubGroupId { get; set; }
        public string ScoringId { get; set; }
        public decimal? Score { get; set; }
    }
}
