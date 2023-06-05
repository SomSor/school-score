namespace SchoolScore.Api.DbModels
{
    public class ScoringSubGroupResult
    {
        public string ScoringSubGroupId { get; set; }
        public IEnumerable<ScoringResult> ScoringResult { get; set; }
        public string GradeResult { get; set; }
    }
}
