﻿using Cameleon.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Cameleon.DependencyInjection
{
    public static class Module
    {
        public static void AddServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IFallbackMapper, FallbackMapper>();
            services.AddSingleton<IDynamicRouter, DynamicRouter>();
        }
    }
}