namespace SchoolScore.Api.Models
{
    public class PagingModel<T>
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
        public long Length { get; set; }
        public IEnumerable<T> Data { get; set; }
    }
}
