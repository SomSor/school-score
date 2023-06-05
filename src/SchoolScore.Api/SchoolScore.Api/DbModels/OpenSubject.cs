namespace SchoolScore.Api.DbModels
{
    public class OpenSubject : DbModelBase
    {
        public string SchoolYearId { get; set; }
        public string SujectId { get; set; }
        public string MainTeacherId { get; set; }
        public IEnumerable<ScoringGroup> Evaluates { get; set; }
        public ScoringGroup Exaam { get; set; }
    }
}
