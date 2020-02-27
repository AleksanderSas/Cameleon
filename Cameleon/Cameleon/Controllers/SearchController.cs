using Cameleon.Services;
using Microsoft.AspNetCore.Mvc;

namespace Cameleon.Controllers
{
    [Route("search")]
    [ApiController]
    public class SearchController : ControllerBase
    {
        private IRecorder _recorder;

        public SearchController(IRecorder recorder)
        {
            _recorder = recorder;
        }

        [HttpGet("")]
        public IActionResult Get(string method, string pathPatters)
        {
            var records = _recorder.Find(method, pathPatters);
            return Ok(new { Records = records });
        }
    }
}