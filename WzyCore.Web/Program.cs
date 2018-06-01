using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace WzyCore.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = new WebHostBuilder()
                ///在AspNetCoreModule后面运行时，配置服务器应该监听的端口和基本路径。
                ///该应用程序还将被配置为捕获启动错误。
                .UseIISIntegration()
                //指定Kestrel作为web主机要使用的服务器。
                .UseKestrel()
                //指定web主机要使用的内容根目录。
                .UseContentRoot(Directory.GetCurrentDirectory())
                //指定web主机要使用的启动类型。
                .UseStartup<Startup>()
                .Build();
            host.Run();
        }
    }
}
