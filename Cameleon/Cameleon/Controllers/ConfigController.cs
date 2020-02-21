using Cameleon.Model;
using Cameleon.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace Cameleon.Controllers
{
    [Route("config")]
    [ApiController]
    public class ConfigController : ControllerBase
    {
        private IDynamicRouter _router;

        public ConfigController(IDynamicRouter router)
        {
            _router = router;
        }

        [HttpGet("")]
        public IActionResult Get()
        {
            return Ok(new { Message = "Hellow" });
        }

        [HttpPost("")]
        public IActionResult Post([FromBody] Configuration config)
        {
            var template = new SimpleTemplate(config.Body, config.HttpCode);
            _router.AddTemplate(config.Path, config.Method, template);

            return Ok();
        }
    }
}