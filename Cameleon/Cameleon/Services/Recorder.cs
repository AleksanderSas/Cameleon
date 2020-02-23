using Cameleon.Model;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Cameleon.Services
{
    public interface IRecorder
    {
        Task Record(HttpRequest request);
        IEnumerable<RequestRecord> Find(string method, string urlPattern);
    }

    internal class Recorder : IRecorder
    {
        private IList<RequestRecord> _requests;

        public Recorder()
        {
            _requests = new List<RequestRecord>();
        }

        public IEnumerable<RequestRecord> Find(string method, string urlPattern)
        {
            method = method.ToUpper();
            return _requests.Where(x => x.Method == method && Regex.IsMatch(x.Url, urlPattern));
        }

        public async Task Record(HttpRequest request)
        {
            var body = new StreamReader(request.Body);
            var requestBody = await body.ReadToEndAsync();

            _requests.Add(new RequestRecord
            {
                Method = request.Method,
                Url = request.Path.Value,
                Body = requestBody
            });
        }
    }
}
