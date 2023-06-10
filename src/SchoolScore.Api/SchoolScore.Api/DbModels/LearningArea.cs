namespace SchoolScore.Api.DbModels
{
    public class LearningArea : DbModelBase
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string SchoolId { get; set; }
    }
}
