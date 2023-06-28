namespace SchoolScore.Api.DbModels
{
    public class ScoringGroup
    {
        public string Id { get; set; }
        public string Name { get; set; }
        /// <summary>
        /// ScoringGroupTypePossible<br />
        /// Evaluate, Exam
        /// </summary>
        public string Type { get; set; }
        public IEnumerable<ScoringSubGroup> ScoringSubGroups { get; set; }
        public IEnumerable<GradingCriteria> GradingCriterias { get; set; }
    }

    public class ScoringGroupTypePossible
    {
        public static readonly string Evaluate = nameof(Evaluate);
        public static readonly string Exam = nameof(Exam);
    }
}
