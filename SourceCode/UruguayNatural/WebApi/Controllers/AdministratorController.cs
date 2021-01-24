using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Model.In;
using Model.Out;
using BusinessLogicInterface;
using WebApi.Filters;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/administrators")]
    public class AdministratorController : ControllerBase
    {
        private readonly IAdministratorLogic AdministratorLogic;

        public AdministratorController(IAdministratorLogic administratorLogic)
        {
            AdministratorLogic = administratorLogic;
        }

        [HttpGet]
        [AuthorizationFilter]
        public IActionResult Get()
        {
            return Ok(AdministratorLogic.GetAll().Select(adm => new AdministratorBasicInfoModel(adm)).ToList());
        }

        [HttpPost]
        [AuthorizationFilter]
        public IActionResult Post(AdministratorModel administratorModel)
        {
            IActionResult result;
            if(administratorModel.HasErrors())
            {
                result = BadRequest(new ErrorModel(administratorModel.Errors()));
            }
            else
            {
                var administrator = AdministratorLogic.Create(administratorModel.ToEntity());
                result = Created("GetAdministrator", new AdministratorBasicInfoModel(administrator));
            }
            
            return result;
        }

        [HttpPut("{id}")]
        [AuthorizationFilter]
        public IActionResult Put(int id, AdministratorModel administratorModel)
        {
            var administrator = AdministratorLogic.Update(id, administratorModel.ToEntity());
            return Ok(new AdministratorBasicInfoModel(administrator));
        }
        
        [HttpDelete("{id}")]
        [AuthorizationFilter]
        public IActionResult Delete(int id)
        {
            AdministratorLogic.Delete(id);
            return NoContent();
        }
    }
}
