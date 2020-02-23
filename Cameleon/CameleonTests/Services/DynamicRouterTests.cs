using Cameleon.Services;
using Microsoft.AspNetCore.Http;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CameleonTests.Services
{
    public class DynamicRouterTests
    {
        private DynamicRouter _target;
        private Mock<ITemplate> _defaultTemplate;
        private Mock<IRecorder> _recorder;
        private Mock<IUrlNormalizer> _normalizer;

        public DynamicRouterTests()
        {
            _defaultTemplate = new Mock<ITemplate>();
            _recorder = new Mock<IRecorder>();
            _normalizer = new Mock<IUrlNormalizer>();
            _target = new DynamicRouter(_defaultTemplate.Object, _recorder.Object, _normalizer.Object);
        }

        [Fact]
        public async Task ThereIsSomeTemplate_AddNewNoneSequencialTemplate_TemplateIsOverriden()
        {
            string url = "/url";
            string method = "POST";

            Mock<ITemplate> template1 = new Mock<ITemplate>();
            Mock<ITemplate> template2 = new Mock<ITemplate>();

            _normalizer.Setup(x => x.Normalize(url)).Returns(url);

            _target.AddTemplate(url, method, template1.Object, false);
            _target.AddTemplate(url, method, template2.Object, false);

            var ctx = MockContext(url, method);

            await _target.Route(ctx);
            await _target.Route(ctx);
            template1.Verify(x => x.Execute(It.IsAny<HttpContext>()), Times.Never);
            template2.Verify(x => x.Execute(ctx), Times.Exactly(2));
        }

        [Fact]
        public async Task ThereIsSomeTemplate_AddNewSequencialTemplate_BothTemplatesAreAvailable()
        {
            string url = "/url";
            string method = "POST";

            Mock<ITemplate> template1 = new Mock<ITemplate>();
            Mock<ITemplate> template2 = new Mock<ITemplate>();

            _normalizer.Setup(x => x.Normalize(url)).Returns(url);

            _target.AddTemplate(url, method, template1.Object, true);
            _target.AddTemplate(url, method, template2.Object, true);

            var ctx = MockContext(url, method);

            await _target.Route(ctx);
            template1.Verify(x => x.Execute(ctx), Times.Exactly(1));

            await _target.Route(ctx);
            template2.Verify(x => x.Execute(ctx), Times.Exactly(1));
        }

        private HttpContext MockContext(string url, string method)
        {
            var request = new Mock<HttpRequest>();
            request.Setup(x => x.Path).Returns(url);
            request.Setup(x => x.Method).Returns(method);
            var ctx = new Mock<HttpContext>();
            ctx.Setup(x => x.Request).Returns(request.Object);
            return ctx.Object;
        }
    }
}
