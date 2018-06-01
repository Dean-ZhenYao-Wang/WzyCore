using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using WzyCore.Environment.Shell.Abstractions;
using WzyCore.Parser.Yaml;

namespace WzyCore.Environment.Shell
{
    public class ShellSettingsConfigurationProvider : IShellSettingsConfigurationProvider
    {
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly IOptions<ShellOptions> _optionsAccessor;
        public ShellSettingsConfigurationProvider(
            IHostingEnvironment hostingEnvironment,
            IOptions<ShellOptions> optionsAccessor)
        {
            _hostingEnvironment = hostingEnvironment;
            _optionsAccessor = optionsAccessor;
        }
        public int Order => 1000;

        public void AddSource(IConfigurationBuilder builder)
        {
            //将两个字符串组合到一个路径中
            var tenantSettingsPath = Path.Combine(
                        _optionsAccessor.Value.ShellsApplicationDataPath,
                        _optionsAccessor.Value.ShellsContainerName);
            //如果路径不存在
            if (!Directory.Exists(tenantSettingsPath))
            {
                return;
            }
            //指定目录的子目录 (包括其路径) 的名称
            var tenants = Directory.GetDirectories(tenantSettingsPath);
            foreach (var tenant in tenants)
            {
                builder.AddYamlFile(GetSettingsFilePath(tenant));
            }
        }

        public void SaveToSource(string name, IDictionary<string, string> configuration)
        {
            var settingsFile = GetSettingsFilePath(Path.Combine(
                        _optionsAccessor.Value.ShellsApplicationDataPath,
                        _optionsAccessor.Value.ShellsContainerName,
                        name));

            var configurationProvider = new YamlConfigurationProvider(new YamlConfigurationSource
            {
                Path = settingsFile,
                Optional = false
            });

            configurationProvider.Set(name, null);
            configurationProvider.Set($"{name}:RequestUrlHost", ObtainValue(configuration, $"{name}:RequestUrlHost"));
            configurationProvider.Set($"{name}:RequestUrlPrefix", ObtainValue(configuration, $"{name}:RequestUrlPrefix"));
            configurationProvider.Set($"{name}:DatabaseProvider", ObtainValue(configuration, $"{name}:DatabaseProvider"));
            configurationProvider.Set($"{name}:TablePrefix", ObtainValue(configuration, $"{name}:TablePrefix"));
            configurationProvider.Set($"{name}:ConnectionString", ObtainValue(configuration, $"{name}:ConnectionString"));
            configurationProvider.Set($"{name}:State", ObtainValue(configuration, $"{name}:State"));

            configurationProvider.Commit();
        }
        private string GetSettingsFilePath(string tenantFolderPath) => Path.Combine(tenantFolderPath, "Settings.txt");
    }
}
