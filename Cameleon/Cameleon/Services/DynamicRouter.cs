using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

[assembly: InternalsVisibleTo("CameleonTests")]
namespace Cameleon.Services
{
    public interface IDynamicRouter
    {
        void AddTemplate(string url, string method, ITemplate template, bool sequenceSuccessor);
        bool Delete(string url, string method);
        Task Route(HttpContext context);
        void Get();
    }

    internal class DynamicRouter : IDynamicRouter
    {
        private readonly IDictionary<(string, string), TemplateSequence> _templates;
        private readonly ITemplate _defaultTemplate;
        private readonly IRecorder _recorder;
        private readonly IUrlNormalizer _normalizer;

        public DynamicRouter(ITemplate defaultTemplate, IRecorder recorder, IUrlNormalizer normalizer)
        {
            _defaultTemplate = defaultTemplate;
            _recorder = recorder;
            _normalizer = normalizer;
            _templates = new Dictionary<(string, string), TemplateSequence>();
        }

        public void AddTemplate(string url, string method, ITemplate template, bool sequenceSuccessor)
        {
            url = _normalizer.Normalize(url);
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

        public bool Delete(string url, string method)
        {
            url = _normalizer.Normalize(url);
            return _templates.Remove((url, method));
        }

        public void Get()
        {
            throw new System.NotImplementedException();
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
