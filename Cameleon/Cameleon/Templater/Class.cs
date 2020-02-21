using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace Cameleon.Templater
{
    public class Class
    {
        public static Task Do(HttpContext context)
        {
            return Task.CompletedTask;
        }
    }
}
