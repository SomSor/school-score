namespace SchoolScore.Api.DbModels
{
    public class GradingCriteria : DbModelBase
    {
        public decimal Score { get; set; }
        public string Grade { get; set; }
    }
}
