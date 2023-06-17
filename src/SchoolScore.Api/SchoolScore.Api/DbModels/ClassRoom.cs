namespace SchoolScore.Api.DbModels
{
    public class Classroom : DbModelBase
    {
        public string Tier { get; set; }
        public string ClassYear { get; set; }
        public string Subclass { get; set; }
        public string SchoolYearId { get; set; }
        public string TeacherId { get; set; }
    }

    public class ClassroomTierPossible
    {
        public const string PreSchool = nameof(PreSchool);
        public const string PrimarySchool = nameof(PrimarySchool);
        public const string JuniorHighSchool = nameof(JuniorHighSchool);
        public const string SeniorHighSchool = nameof(SeniorHighSchool);
    }
}
