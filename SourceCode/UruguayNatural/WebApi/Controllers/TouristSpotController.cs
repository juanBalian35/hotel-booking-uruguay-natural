using Microsoft.AspNetCore.Mvc;
using Model.In;
using Model.Out;
using BusinessLogicInterface;
using WebApi.Filters;
using System.Linq;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/touristSpots")]
    public class TouristSpotController : ControllerBase
    {
        private readonly ITouristSpotLogic TouristSpotLogic;

        public TouristSpotController(ITouristSpotLogic touristSpotLogic)
        {
            TouristSpotLogic = touristSpotLogic;
        }
        
        [HttpPost]
        [AuthorizationFilter]
        public IActionResult Post([FromForm] TouristSpotModel touristSpotModel)
        {
            IActionResult result;
            if(touristSpotModel.HasErrors())
            {
                result = BadRequest(new ErrorModel(touristSpotModel.Errors()));
            }
            else
            {
                var touristSpot = TouristSpotLogic.Create(touristSpotModel.ToEntity());
                result = Created("GetTouristSpot", new TouristSpotBasicInfoModel(touristSpot));   
            }

            return result;
        }

        [HttpGet]
        public IActionResult Get([FromQuery] TouristSpotSearchModel touristSpotSearchModel)
        {
            IActionResult result;
            if (touristSpotSearchModel.HasErrors())
            {
                result = BadRequest(new ErrorModel(touristSpotSearchModel.Errors()));
            }
            else
            {
                result = Ok(TouristSpotLogic.Search(touristSpotSearchModel)
                    .Select(x => new TouristSpotBasicInfoModel(x)).ToList());
            }

            return result;
        }
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            return Ok(new TouristSpotDetailedInfoModel(TouristSpotLogic.Get(id)));
        }
    }
}
