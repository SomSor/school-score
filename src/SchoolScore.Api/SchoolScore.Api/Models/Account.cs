namespace SchoolScore.Api.Models
{
    public class Account : DbModels.Account
    {
        public DbModels.Teacher Teacher { get; set; }
    }
}
