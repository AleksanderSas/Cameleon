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
        private IRecorder _recorder;

        public ConfigController(IDynamicRouter router, IRecorder recorder)
        {
            _router = router;
            _recorder = recorder;
        }

        [HttpGet("")]
        public IActionResult Get(string method, string pathPatters)
        {
            var records = _recorder.Find(method, pathPatters);
            return Ok(new { Records = records });
        }

        [HttpPost("")]
        public IActionResult Post([FromBody] Configuration config)
        {
            var template = new SimpleTemplate(config.Body, config.HttpCode);
            _router.AddTemplate(config.Path, config.Method, template, config.SequenceSuccessor);

            return Ok();
        }

        [HttpDelete("")]
        public IActionResult Delete([FromBody] BasicConfiguration config)
        {
            var result = _router.Delete(config.Path, config.Method);
            return result ? (IActionResult)Ok() : NotFound();
        }
    }
}