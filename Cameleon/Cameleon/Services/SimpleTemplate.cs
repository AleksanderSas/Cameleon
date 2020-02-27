using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace Cameleon.Services
{
    public interface ITemplate
    {
        Task Execute(HttpContext context);
        string Name { get; }
    }

    internal class SimpleTemplate : ITemplate
    {
        public string Body { get; set; }
        public int StatusCode { get; set; }
        public int ResponsDelay { get; set; }

        public string Name => nameof(SimpleTemplate);

        internal SimpleTemplate() { }

        public SimpleTemplate(string body, int statusCode, int responsDelay = 0)
        {
            Body = body ?? string.Empty;
            StatusCode = statusCode;
            ResponsDelay = responsDelay;
        }

        public async Task Execute(HttpContext context)
        {
            await Task.Delay(ResponsDelay);
            context.Response.StatusCode = StatusCode;
            await context.Response.WriteAsync(Body);
        }
    }
}
