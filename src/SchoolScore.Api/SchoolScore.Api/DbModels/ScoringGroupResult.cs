namespace SchoolScore.Api.DbModels
{
    public class ScoringGroupResult
    {
        public string ScoringGroupId { get; set; }
        public IEnumerable<ScoringSubGroupResult> ScoringSubGroupResults { get; set; }
        public string Remark { get; set; }
    }
}
