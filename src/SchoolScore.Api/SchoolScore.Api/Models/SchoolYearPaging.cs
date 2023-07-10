namespace SchoolScore.Api.Models
{
    public class SchoolYearPaging : PagingModel<DbModels.SchoolYear>
    {
        public DbModels.SchoolYear Current { get; set; }
    }
}
