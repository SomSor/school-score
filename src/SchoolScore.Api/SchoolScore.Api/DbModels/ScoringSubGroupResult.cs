namespace SchoolScore.Api.DbModels
{
    public class ScoringSubGroupResult
    {
        public string ScoringSubGroupId { get; set; }
        public IEnumerable<ScoringResult> ScoringResults { get; set; }
        public string GradeResult { get; set; }
    }
}
