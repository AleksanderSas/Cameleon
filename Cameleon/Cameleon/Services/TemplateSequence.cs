using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cameleon.Services
{
    internal class TemplateSequence : ITemplate
    {
        private List<ITemplate> Templates { get; set; }
        private int InvocationNo { get; set; }

        public TemplateSequence(ITemplate template)
        {
            Templates = new List<ITemplate>();
            Templates.Add(template);
        }

        public void Add(ITemplate teample)
        {
            Templates.Add(teample);
        }

        public async Task Execute(HttpContext context)
        {
            await Templates[InvocationNo].Execute(context);
            if (InvocationNo < Templates.Count - 1) InvocationNo++;
        }
    }
}
