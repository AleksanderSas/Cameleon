using Cameleon.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace Cameleon.Controllers
{
    [Route("config")]
    [ApiController]
    public class ConfigController : ControllerBase
    {
        [HttpGet("")]
        public IActionResult Get()
        {
            return Ok(new { Message = "Hellow" });
        }

        [HttpPost("")]
        public IActionResult Post([FromBody] Configuration config)
        {
            return Ok(new { Message = config.Data, IsValid = ModelState.IsValid });
        }
    }
}