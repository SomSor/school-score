namespace SchoolScore.Api.DbModels
{
    public class ScoringSubGroup
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<Scoring> Scorings { get; set; }
        public IEnumerable<string> Gradings { get; set; }
    }
}
