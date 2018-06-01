using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace WzyCore.Application.Targets
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddWZY(this IServiceCollection services)
        {
            return AddWZY(services, null);
        }

        public static IServiceCollection AddWZY(this IServiceCollection services, IConfiguration configuration)
        {
            //从文件系统加载站点设置的主机服务。
            services.AddSitesFolder();

            return services;
        }
    }
}
