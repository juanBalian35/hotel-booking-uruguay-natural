using Microsoft.AspNetCore.Mvc;
using WebApi.Filters;
using Model.In;
using BusinessLogicInterface;

namespace WebApi.Controllers
{
    [Route("api/bulkImports")]
    [ApiController]
    public class ImportController : ControllerBase
    {
        private readonly IImportLogic ImportLogic;
        
        public ImportController(IImportLogic importLogic)
        {
            ImportLogic = importLogic;
        }

        [HttpGet]
        [AuthorizationFilter]
        public IActionResult Get()
        {
            return Ok(ImportLogic.GetFormatNames());
        }
        
        [HttpPost("lodgings")]
        [AuthorizationFilter]
        public IActionResult Post([FromForm] ImportLodgingModel importLodgingModel)
        {
            ImportLogic.Import(importLodgingModel);
            return NoContent();
        }
    }
}