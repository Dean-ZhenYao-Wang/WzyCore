using IdentityServer4.AccessTokenValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
namespace Gateway
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        } 
        public IConfiguration Configuration { get; }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAuthentication()
            //对配置文件中使用ChatKey配置了AuthenticationProviderKey=ChatKey的路由规则使用如下的验证方式
                    .AddIdentityServerAuthentication("ChatKey", o=> {
                        //IdentityService认证服务的地址 
                        o.Authority = "http://localhost:20291"; 
                        o.ApiName = "chatapi";//要连接的应用的名字 
                        o.RequireHttpsMetadata = false;
                        o.SupportedTokens =SupportedTokens.Both;
                        o.ApiSecret = "123321";//秘钥
                    });
            services.AddOcelot(Configuration);
        }
    public void Configure(IApplicationBuilder app, IHostingEnvironment env)
    {
        app.UseOcelot().Wait();
    }
    }
}
