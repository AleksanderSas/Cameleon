using Cameleon.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Cameleon.DependencyInjection
{
    public static class Module
    {
        public static void AddServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<ITemplate, DefaultTemplate>();
            services.AddSingleton<IDynamicRouter, DynamicRouter>();
            services.AddSingleton<IRecorder, Recorder>();
        }
    }
}
