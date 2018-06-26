using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

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
                .UseStartup<Startup>()
                .UseUrls("http://127.0.0.1:" + port)
                .ConfigureAppConfiguration((hostingContext, builder) =>
                {
                    builder.AddJsonFile("Ocelot.json", false, true);
                })
                .Build();
    }
}