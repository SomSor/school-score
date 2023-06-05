namespace SchoolScore.Api.DbModels
{
    public abstract class Person :DbModelBase
    {
        public string Prefix { get; set; }
        public string Name { get; set; }
        public string Lastname { get; set; }
    }
}
