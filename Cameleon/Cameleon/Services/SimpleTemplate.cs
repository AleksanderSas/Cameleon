using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace Cameleon.Services
{
    public interface ITemplate
    {
        Task Execute(HttpContext context);
    }

    internal class SimpleTemplate : ITemplate
    {
        private readonly string _body;
        private readonly int _statusCode;
        private readonly int _responsDelay;

        public SimpleTemplate(string body, int statusCode, int responsDelay = 0)
        {
            _body = body ?? string.Empty;
            _statusCode = statusCode;
            _responsDelay = responsDelay;
        }

        public async Task Execute(HttpContext context)
        {
            await Task.Delay(_responsDelay);
            context.Response.StatusCode = _statusCode;
            await context.Response.WriteAsync(_body);
        }
    }
}
