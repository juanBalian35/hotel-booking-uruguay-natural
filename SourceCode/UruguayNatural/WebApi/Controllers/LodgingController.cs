using System.Linq;
using Microsoft.AspNetCore.Mvc;
using WebApi.Filters;
using Model.In;
using BusinessLogicInterface;
using Model.Out;

namespace WebApi.Controllers
{
    [Route("api/lodgings")]
    [ApiController]
    public class LodgingController : ControllerBase
    {
        private readonly ILodgingLogic LodgingLogic;
        private readonly ILodgingReviewLogic LodgingReviewLogic;
        
        public LodgingController(ILodgingLogic lodgingLogic, ILodgingReviewLogic lodgingReviewLogic)
        {
            LodgingLogic = lodgingLogic;
            LodgingReviewLogic = lodgingReviewLogic;
        }

        [HttpPost]
        [AuthorizationFilter]
        public IActionResult Post([FromForm]LodgingModel lodgingModel)
        {
            IActionResult result;
            if (lodgingModel.HasErrors())
            {
                result = BadRequest(new ErrorModel(lodgingModel.Errors()));
            }
            else
            {
                var lodging = LodgingLogic.Create(lodgingModel.ToEntity());
                result =  Created("GetAdministrator", new LodgingBasicInfoModel(lodging));                
            }

            return result;
        }
        
        [HttpPut("{id}")]
        [AuthorizationFilter]
        public IActionResult Put(int id)
        {
            var lodging = LodgingLogic.Update(id);
            return Ok(new LodgingModifiedModel(lodging));
        }

        [HttpGet]
        public IActionResult Get([FromQuery]SearchLodgingModel searchLodgingModel)
        {
            IActionResult result;
            if (searchLodgingModel.HasErrors())
            {
                result = BadRequest(new ErrorModel(searchLodgingModel.Errors()));
            }
            else
            {
                var lodgings = LodgingLogic.Search(searchLodgingModel);
                result = Ok(lodgings.Select(x => new LodgingSearchBasicInfoModel(x)).ToList());   
            }

            return result;
        }
        
        [HttpDelete("{id}")]
        [AuthorizationFilter]
        public IActionResult Delete(int id)
        {
            LodgingLogic.Delete(id);
            return NoContent();
        }
        
        [HttpGet("{id}/Reviews")]
        public IActionResult GetReviews(int id,[FromQuery] int page, [FromQuery] int resultsPerPage)
        {
            var reviews = LodgingReviewLogic.GetAllReviews(id, page, resultsPerPage);
            return Ok(reviews.Select(x => new LodgingReviewBasicInfoModel(x)).ToList());
        }
    }
}
