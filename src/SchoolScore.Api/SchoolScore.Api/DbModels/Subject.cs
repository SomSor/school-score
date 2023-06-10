namespace SchoolScore.Api.DbModels
{
    public class Subject : DbModelBase
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string LearningAreaId { get; set; }
    }
}
