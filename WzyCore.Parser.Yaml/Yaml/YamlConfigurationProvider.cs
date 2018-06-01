using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace WzyCore.Parser.Yaml.Yaml
{
    public class YamlConfigurationProvider : FileConfigurationProvider
    {
        public const char Separator = ':';
        public const string EmptyValue = "null";
        public const char ThemesSeparator = ';';
        public YamlConfigurationProvider(FileConfigurationSource source) : base(source)
        {
        }
        public override void Load(Stream stream)
        {
            var parser = new YamlConfigurationFileParser();
            try
            {
                Data = parser.Parse(stream);
            }
            catch (InvalidCastException e)
            {
                throw new FormatException("FormatError_YAMLparseError", e);
            }
        }
    }
}
