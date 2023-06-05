namespace SchoolScore.Api.DbModels
{
    public class SchoolYear : DbModelBase
    {
        public string Year { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string SchoolId { get; set; }
    }
}
