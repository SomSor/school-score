using SchoolScore.Api.DbModels;

namespace SchoolScore.Api.DACs
{
    public interface ISchoolYearDac<T> : IDataDAC<T>
    {
        Task<SchoolYear> Current();
    }
}
