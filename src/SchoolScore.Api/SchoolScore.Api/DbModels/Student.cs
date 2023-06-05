namespace SchoolScore.Api.DbModels
{
    public class Student : Person
    {
        public string Code { get; set; }
        public string SchoolId { get; set; }
    }
}
