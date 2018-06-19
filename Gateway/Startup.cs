using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer4.AccessTokenValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
namespace Gateway
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            //指定Identity Server的信息 
            Action<IdentityServerAuthenticationOptions> isaOptMsg = o =>
            {
                o.Authority = "http://localhost:9500";
                o.ApiName = "chatapi";//要连接的应用的名字 
                o.RequireHttpsMetadata = false;
                o.SupportedTokens = SupportedTokens.Both;
                o.ApiSecret = "w.s-bp-2019";//秘钥
            };
            services.AddAuthentication()
            // 对配置文件中使用ChatKey配置了AuthenticationProviderKey = MsgKey的路由规则使用如下的验证方式 
            .AddIdentityServerAuthentication("ChatKey", isaOptMsg);
            services.AddOcelot();
        }
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseOcelot().Wait();
        }
    }
}
