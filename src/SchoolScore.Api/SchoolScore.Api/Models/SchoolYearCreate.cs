namespace SchoolScore.Api.Models
{
    public class SchoolYearCreate
    {
        public string Year { get; set; }
        public int Semester { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
