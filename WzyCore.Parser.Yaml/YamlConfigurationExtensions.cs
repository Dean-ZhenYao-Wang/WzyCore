using Microsoft.Extensions.Configuration;
using System;

namespace WzyCore.Parser.Yaml
{
    public static class YamlConfigurationExtensions
    {
        /// <summary>
        /// 将<paramref name="path"/>下的 Yaml 配置提供程序添加到 <paramref name="builder"/>.
        /// Adds the Yaml configuration provider at <paramref name="path"/> to <paramref name="builder"/>.
        /// </summary>
        /// <param name="builder">
        /// 要添加到的 IConfigurationBuilder。
        /// The <see cref="IConfigurationBuilder"/> to add to.
        /// </param>
        /// <param name="path">
        /// 相对于生成器IConfigurationBuilder.Properties中存储的基路径的路径 
        /// Path relative to the base path stored in <see cref="IConfigurationBuilder.Properties"/> of <paramref name="builder"/>.
        /// </param>
        /// <returns>The <see cref="IConfigurationBuilder"/>.</returns>
        public static IConfigurationBuilder AddYamlFile(this IConfigurationBuilder builder, string path)
        {
            return AddYamlFile(builder, provider: null, path: path, optional: false, reloadOnChange: false);
        }
    }
}
