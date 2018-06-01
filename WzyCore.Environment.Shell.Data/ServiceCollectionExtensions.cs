using Microsoft.Extensions.DependencyInjection;
using System;
using WzyCore.Environment.Shell.Abstractions;

namespace WzyCore.Environment.Shell.Data
{
    public static class ServiceCollectionExtensions
    { /// <summary>
      ///  从文件系统加载站点设置的主机服务。
      ///  Host services to load site settings from the file system
      /// </summary>
      /// <param name="services"></param>
      /// <param name="sitesPath"></param>
      /// <returns></returns>
        public static IServiceCollection AddSitesFolder(this IServiceCollection services)
        {
            services.AddSingleton<IShellSettingsConfigurationProvider, ShellSettingsConfigurationProvider>();
            services.AddSingleton<IShellSettingsManager, ShellSettingsManager>();
            services.AddTransient<IConfigureOptions<ShellOptions>, ShellOptionsSetup>();

            return services;
        }
    }
}
