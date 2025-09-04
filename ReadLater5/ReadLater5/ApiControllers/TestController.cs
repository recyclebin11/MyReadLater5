using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ReadLater5.ApiControllers
{
    [ApiController]
    [Route("api/test")]
    public class TestController: ControllerBase
    {
        [HttpGet]
        [Route("ping")]
        [Authorize(AuthenticationSchemes = "JWT")]
        public async Task<IActionResult> Ping()
        {
            return Ok(await Task.FromResult("pong"));
        }
    }
}
