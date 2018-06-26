using IdentityServer4.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;

namespace ID4.IdServer
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            var idResources = new List<IdentityResource>{
                new IdentityResources.OpenId(),//解决报无效的scope错误
                new IdentityResources.Profile()
            };

            services.AddIdentityServer()
                    .AddDeveloperSigningCredential()
                    .AddInMemoryIdentityResources(idResources)
                    .AddInMemoryApiResources(Config.GetApiResources())
                    .AddInMemoryClients(Config.GetClients())
                    .AddResourceOwnerValidator<ResourceOwnerPasswordValidator>()
                    .AddProfileService<ProfileService>();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseIdentityServer();
        }
    }
}