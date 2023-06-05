using MongoDB.Bson.Serialization.Attributes;

namespace SchoolScore.Api.DbModels
{
    public abstract class DbModelBase
    {
        [BsonId]
        public string Id { get; set; }
        public DateTime CreateDate { get; set; }
        public string CreateBy { get; set; }
        public DateTime? UpdateDate { get; set; }
        public string? UpdateBy { get; set; }
        public DateTime? DeleteDate { get; set; }
        public string? DeleteBy { get; set; }

        public void Init(string createBy)
        {
            CreateDate = DateTime.UtcNow;
            Id = $"{CreateDate.Ticks}-{Guid.NewGuid().ToString().Substring(0, 8)}";
            CreateBy = createBy;
        }
    }
}
