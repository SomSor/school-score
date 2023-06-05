namespace SchoolScore.Api.DbModels
{
    public class ClassRoom : DbModelBase
    {
        public string ClassYear { get; set; }
        public string Subclass { get; set; }
        public string SchoolYearId { get; set; }
        public string TeacherId { get; set; }
    }
}
