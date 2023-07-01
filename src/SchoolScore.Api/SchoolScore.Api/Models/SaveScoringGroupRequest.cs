namespace SchoolScore.Api.Models
{
    public class SaveScoringGroupRequest
    {
        public string ClassroomId { get; set; }
        public string OpenSubjectId { get; set; }
        public IEnumerable<ClassroomStudentScore> ClassroomStudentScores { get; set; }
        public IEnumerable<ClassroomStudentRemark> ClassroomStudentRemarks { get; set; }
    }

    public class ClassroomStudentScore
    {
        public string StudentId { get; set; }
        public string ScoringSubGroupId { get; set; }
        public string ScoringId { get; set; }
        public decimal? Score { get; set; }
    }

    public class ClassroomStudentRemark
    {
        public string StudentId { get; set; }
        public string ScoringGroupId { get; set; }
        public string Remark { get; set; }
    }
}
