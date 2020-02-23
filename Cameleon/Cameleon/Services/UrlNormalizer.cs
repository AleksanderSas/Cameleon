using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cameleon.Services
{
    public interface IUrlNormalizer
    {
        string Normalize(string url);
    }

    public class UrlNormalizer : IUrlNormalizer
    {
        public string Normalize(string url)
        {
            url = url.Replace('\\', '/');
            if (url[0] != '/')
            {
                url = "/" + url;
            }

            return url;
        }
    }
}
