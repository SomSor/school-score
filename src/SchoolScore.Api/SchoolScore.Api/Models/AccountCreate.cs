namespace SchoolScore.Api.Models
{
    public class AccountCreate
    {
        public string PersonId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public IEnumerable<string> Roles { get; set; }
    }
}
