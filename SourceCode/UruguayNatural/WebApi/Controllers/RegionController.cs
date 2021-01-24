using System.Linq;
using Microsoft.AspNetCore.Mvc;
using BusinessLogicInterface;
using Model.Out;

namespace WebApi.Controllers
{
    [Route("api/regions")]
    [ApiController]
    public class RegionController : ControllerBase
    {
        private readonly IRegionLogic RegionLogic;
        
        public RegionController(IRegionLogic regionLogic)
        {
            RegionLogic = regionLogic;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(RegionLogic.GetAll().Select(x => new RegionDetailedInfoModel(x)).ToList());
        }
    }
}