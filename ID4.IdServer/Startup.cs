using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Consul;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace ID4.IdServer
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddIdentityServer()
                    .AddDeveloperSigningCredential()
                    .AddInMemoryApiResources(Config.GetApiResources())
                    .AddInMemoryClients(Config.GetClients());
        }
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            string ip = "127.0.0.1";
            int port = 20291;
            var client = new ConsulClient(ConfigurationOverview);
            var result = client.Agent.ServiceRegister(new AgentServiceRegistration()
            {
                ID = "GateWay" + Guid.NewGuid(),
                Name = "GateWay",
                Address = ip,
                Port = port,
                Check = new AgentServiceCheck
                {
                    DeregisterCriticalServiceAfter = TimeSpan.FromSeconds(5),
                    Interval = TimeSpan.FromSeconds(10),
                    HTTP = $"http://{ip}:{port}/api/Health",
                    Timeout=TimeSpan.FromSeconds(5)
                }
            });

            app.UseIdentityServer();
        }
        private static void ConfigurationOverview(ConsulClientConfiguration obj)
        {
            obj.Address = new System.Uri("http://127.0.0.1:8500");
            obj.Datacenter = "dc1";
        }
    }
}
