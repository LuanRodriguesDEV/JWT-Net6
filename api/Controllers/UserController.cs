using api.Atribute;
using api.DTO.User;
using api.Model;
using api.Services;
using api.VOs.User;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserService _service;
        
        public UserController(UserService service)
        {
            _service = service;
        }

        [HttpPost("Auth")]
        public async Task<ActionResult<string>> Auth(AuthDTO auth)
            =>  await _service.Auth(auth);

        [HttpPost]
        public async Task<ActionResult<string>> Register(UserVOEnter user)
            => await _service.Register(user);
        [Authorize]
        [HttpGet]
        public string TestToken()
            => _service.TestToken(HttpContext);
    }
}
