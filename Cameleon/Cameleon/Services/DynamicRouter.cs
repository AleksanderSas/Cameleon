using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cameleon.Services
{
    public interface IDynamicRouter
    {
        void AddTemplate(string url, string method, ITemplate template, bool sequenceSuccessor);
        bool Delete(string url, string method);
        Task Route(HttpContext context);
    }

    internal class DynamicRouter : IDynamicRouter
    {
        private readonly IDictionary<(string, string), TemplateSequence> _templates;
        private readonly ITemplate _defaultTemplate;
        private readonly IRecorder _recorder;

        public DynamicRouter(ITemplate defaultTemplate, IRecorder recorder)
        {
            _defaultTemplate = defaultTemplate;
            _recorder = recorder;
            _templates = new Dictionary<(string, string), TemplateSequence>();
        }

        public void AddTemplate(string url, string method, ITemplate template, bool sequenceSuccessor)
        {
            url = Normalize(url);
            var key = (url, method);
            if (sequenceSuccessor && _templates.TryGetValue(key, out var value))
            {
                value.Add(template);
            }
            else
            {
                _templates[key] = new TemplateSequence(template);
            }
        }

        private static string Normalize(string url)
        {
            url = url.Replace('\\', '/');
            if (url[0] != '/')
            {
                url = "/" + url;
            }

            return url;
        }

        public bool Delete(string url, string method)
        {
            url = Normalize(url);
            return _templates.Remove((url, method));
        }

        public async Task Route(HttpContext context)
        {
            await _recorder.Record(context.Request);
            var key = (context.Request.Path.Value, context.Request.Method);
            var template = _templates.TryGetValue(key, out var value) ? value : _defaultTemplate;
            await template.Execute(context);
        }
    }
}
