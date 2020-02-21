using Microsoft.AspNetCore.Mvc;

namespace Cameleon.Controllers
{
    [ApiController]
    //[Route("*")]
    public class DefaultController : ControllerBase
    {
        [HttpGet("")]
        public IActionResult Get()
        {
            return Ok();
        }

        [HttpPost("")]
        public IActionResult Post()
        {
            return Ok();
        }
    }
}