using Mapster;
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
            Id = RunId();
            CreateBy = createBy;
        }

        public T Init<T>(string createBy)
        {
            CreateDate = DateTime.UtcNow;
            Id = RunId();
            CreateBy = createBy;

            return this.Adapt<T>();
        }

        public static string RunId() => $"{DateTime.UtcNow.Ticks}-{Guid.NewGuid().ToString().Substring(0, 8)}";

    }
}
