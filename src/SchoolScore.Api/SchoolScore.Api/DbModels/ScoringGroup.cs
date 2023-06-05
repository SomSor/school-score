namespace SchoolScore.Api.DbModels
{
    public class ScoringGroup : DbModelBase
    {
        public string Name { get; set; }
        /// <summary>
        /// Evaluate, Exam
        /// </summary>
        public string Type { get; set; }
        public IEnumerable<ScoringSubGroup> ScoringSubGroups { get; set; }
        public IEnumerable<GradingCriteria> GradingCriterias { get; set; }
    }
}
