namespace SchoolScore.Api.DbModels
{
    public class Teacher : Person
    {
        public string Code { get; set; }
        public string SchoolId { get; set; }
    }
}
