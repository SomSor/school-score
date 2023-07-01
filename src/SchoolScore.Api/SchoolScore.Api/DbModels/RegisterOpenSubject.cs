namespace SchoolScore.Api.DbModels
{
    public class RegisterOpenSubject
    {
        public string Id { get; set; }
        public string OpenSubjectId { get; set; }
        public IEnumerable<ScoringGroupResult> EvaluateResults { get; set; }
        public ScoringGroupResult ExamResult { get; set; }
        public IEnumerable<Attendance> Attendances { get; set; }
        public string GradingResult { get; set; }
    }

    public class Attendance
    {
        /// <summary>
        /// mon-08:00-09:00
        /// </summary>
        public string TimeTableKey { get; set; }
        public DateTime StampDate { get; set; }
    }
}
