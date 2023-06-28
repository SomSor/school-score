namespace SchoolScore.Api.Models
{
    public class SubjectCreate
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string LearningAreaId { get; set; }
    }
}
