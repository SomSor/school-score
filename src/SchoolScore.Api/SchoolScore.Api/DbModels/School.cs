namespace SchoolScore.Api.DbModels
{
    public class School : DbModelBase
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string Area { get; set; }
    }
}
