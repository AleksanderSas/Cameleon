using Cameleon.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Cameleon.DependencyInjection
{
    public static class Module
    {
        public static void AddServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IUrlNormalizer, UrlNormalizer>();

            services.AddSingleton<ITemplate, DefaultTemplate>();
            services.AddSingleton<IDynamicRouter, DynamicRouter>();
            services.AddSingleton<IRecorder, Recorder>();
        }
    }
}
