using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace WzyCore.Environment.Shell.Abstractions
{
    public interface IShellSettingsConfigurationProvider
    {
        void AddSource(IConfigurationBuilder builder);
        void SaveToSource(string name, IDictionary<string, string> configuration);

        int Order { get; }
    }
}
