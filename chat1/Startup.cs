using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Consul;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace chat1
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            string ip = "127.0.0.1";
            int port = 39532;
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
                    Timeout = TimeSpan.FromSeconds(5)
                }
            });
            app.UseMvc();
        }
        private static void ConfigurationOverview(ConsulClientConfiguration obj)
        {
            obj.Address = new System.Uri("http://127.0.0.1:8500");
            obj.Datacenter = "dc1";
        }
    }
}
