using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace ID4.IdServer
{
    public class Program
    {
        public static readonly int port = Common.PortCommon.GetRandAvailablePort();

        public static void Main(string[] args)
        {
            BuildWebHost(args).Build().Run();
        }

        public static IWebHostBuilder BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                   .UseUrls("http://127.0.0.1:9500");
    }
}