using Microsoft.AspNetCore.Http;
using System.Net;
using System.Threading.Tasks;

namespace Cameleon.Services
{
    internal class DefaultTemplate : ITemplate
    {
        public string Name => throw new System.NotImplementedException();

        public Task Execute(HttpContext context)
        {
            context.Response.StatusCode = (int)HttpStatusCode.NotFound;
            return Task.CompletedTask;
        }
    }
}
