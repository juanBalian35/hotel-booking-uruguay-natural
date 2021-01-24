using Microsoft.AspNetCore.Mvc;
using Model.In;
using Model.Out;
using BusinessLogicInterface;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/sessions")]
    public class SessionController : ControllerBase
    {
        private readonly ISessionLogic SessionLogic;

        public SessionController(ISessionLogic sessionLogic)
        {
            SessionLogic = sessionLogic;
        }

        [HttpPost]
        public IActionResult Post([FromBody]LogInModel model)
        {
            var token = SessionLogic.CreateSession(model.Email, model.Password);
            return Ok(new TokenModel(token));
        }
    }
}
