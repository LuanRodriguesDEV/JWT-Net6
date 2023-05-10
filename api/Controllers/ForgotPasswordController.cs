using api.DTO.ForgotPassword;
using api.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ForgotPasswordController : ControllerBase
    {
        private readonly ForgotPassowordService _service;

        public ForgotPasswordController(ForgotPassowordService service)
        {
            _service = service;
        }
        [HttpPost]
        public async Task<ActionResult> CreateForgot(ForgotDTO forgot)
        {
            await _service.CreateForgot(forgot);
            return Ok();
        }
        [HttpPut]
        public async Task<ActionResult> RecoveryPassword(RecoveryDTO forgot)
        {
            await _service.RecoveryPassword(forgot);
            return Ok();
        }
        [HttpGet("{id}")]
        public async Task<ActionResult> VerifyRecovery(Guid id)
        {
            await _service.VerifyRecovery(id);
            return Ok();
        }
    }
}
