using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchoolScore.Api.DACs;

namespace SchoolScore.Api.Controllers
{
    //[Authorize]
    public class ApiControllerBase : Controller
    {
        public string SchoolId = "638215692835215387-011673d9";

        [ApiExplorerSettings(IgnoreApi = true)]
        public string SchoolYearId(ISchoolYearDac<DbModels.SchoolYear> schoolYearDac)
        {
            var schoolYear = schoolYearDac.Current().Result;
            return schoolYear.Id;
        }
    }
}
