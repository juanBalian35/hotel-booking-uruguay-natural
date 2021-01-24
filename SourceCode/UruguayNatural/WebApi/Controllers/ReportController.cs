using Microsoft.AspNetCore.Mvc;
using WebApi.Filters;
using Model.In;
using BusinessLogicInterface;
using Model.Out;

namespace WebApi.Controllers
{
    [Route("api/reports")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        private readonly IReportLogic ReportLogic;
        
        public ReportController(IReportLogic reportLogic)
        {
            ReportLogic = reportLogic;
        }

        [HttpGet]
        [AuthorizationFilter]
        public IActionResult Get([FromQuery]ReportModel reportModel)
        {
            IActionResult result;
            if (reportModel.HasErrors())
            {
                result = BadRequest(new ErrorModel(reportModel.Errors()));
            }
            else
            {
                result = Ok(ReportLogic.Search(reportModel));
            }

            return result;
        }
    }
}