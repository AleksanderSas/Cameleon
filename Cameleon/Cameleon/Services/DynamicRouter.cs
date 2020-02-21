using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cameleon.Services
{
    public interface IDynamicRouter
    {
        void AddTemplate(string url, string method, ITemplate template);
        Task Route(HttpContext context);
    }

    public class DynamicRouter : IDynamicRouter
    {
        private readonly IDictionary<(string, string), ITemplate> _templates;
        private readonly ITemplate _defaultTemplate;

        public DynamicRouter(ITemplate defaultTemplate)
        {
            _defaultTemplate = defaultTemplate;
            _templates = new Dictionary<(string, string), ITemplate>();
        }

        public void AddTemplate(string url, string method, ITemplate template)
        {
            url = url.Replace('\\', '/');
            if (url[0] != '/')
            {
                url = "/" + url;
            }

            _templates[(url, method)] = template;
        }

        public Task Route(HttpContext context)
        {
            var key = (context.Request.Path.Value, context.Request.Method);
            var template = _templates.TryGetValue(key, out var value) ? value : _defaultTemplate;
            return template.Execute(context);
        }
    }
}
