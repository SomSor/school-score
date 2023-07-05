namespace SchoolScore.Api.DbModels
{
    public class OpenSubject : DbModelBase
    {
        public string SchoolYearId { get; set; }
        public string Semester { get; set; }
        public string SubjectId { get; set; }
        public string MainTeacherId { get; set; }
        public string Description { get; set; }
        public IEnumerable<ScoringGroup> Evaluates { get; set; }
        public ScoringGroup Exam { get; set; }
        /// <summary>
        /// 1-08:00-09:00 > monday
        /// </summary>
        public IEnumerable<string> TimeTables { get; set; }
    }
}
