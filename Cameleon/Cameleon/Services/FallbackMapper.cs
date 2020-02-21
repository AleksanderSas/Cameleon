using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace Cameleon.Services
{
    public interface IFallbackMapper
    {
        Task Do(HttpContext context);
    }

    public class FallbackMapper : IFallbackMapper
    {
        private readonly IDynamicRouter _router;

        public FallbackMapper(IDynamicRouter router)
        {
            _router = router;
        }

        public Task Do(HttpContext context)
        {
            return Task.CompletedTask;
        }
    }
}
