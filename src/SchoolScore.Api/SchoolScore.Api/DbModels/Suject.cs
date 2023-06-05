namespace SchoolScore.Api.DbModels
{
    public class Suject : DbModelBase
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string LearningAreaId { get; set; }
    }
}
