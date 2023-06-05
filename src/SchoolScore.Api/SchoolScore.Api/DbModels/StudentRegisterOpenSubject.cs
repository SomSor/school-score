namespace SchoolScore.Api.DbModels
{
    public class StudentRegisterOpenSubject : DbModelBase
    {
        public string OpenSubjectId { get; set; }
        public string StudentId { get; set; }
        public IEnumerable<ScoringSubGroupResult> ScoringSubGroupResults { get; set; }
        public string GradingResult { get; set; }
        public string Remark { get; set; }
    }
}
