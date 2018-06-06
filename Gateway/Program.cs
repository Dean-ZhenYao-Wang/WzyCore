using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
namespace Gateway
{
    public class Program
    {
        public static readonly int port = Common.PortCommon.GetRandAvailablePort();
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseUrls("http://127.0.0.1:" + port)
                .UseStartup<Startup>()
                .ConfigureAppConfiguration((hostingContext, builder) =>
                {
                    builder.SetBasePath(hostingContext.HostingEnvironment.ContentRootPath).AddJsonFile("Ocelot.json")
                    .AddEnvironmentVariables();
                })
                .Build();
    }
}
