using System.Linq;
using Microsoft.AspNetCore.Mvc;
using BusinessLogicInterface;
using Model.Out;

namespace WebApi.Controllers
{
    [Route("api/categories")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryLogic CategoryLogic;
        
        public CategoryController(ICategoryLogic categoryLogic)
        {
            CategoryLogic = categoryLogic;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(CategoryLogic.GetAll().Select(x => new CategoryDetailedInfoModel(x)).ToList());
        }
    }
}