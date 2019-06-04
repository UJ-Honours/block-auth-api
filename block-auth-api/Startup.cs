using block_auth_api.Connection;
using block_auth_api.Models;
using block_auth_api.Orchestration.DeviceContract;
using block_auth_api.Orchestration.UsersContract;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;

namespace block_auth_api
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
            services.AddSingleton<IContractManager, ContractManager>();

            services.AddSingleton<IDeviceContractOrchestration, DeviceContractOrchestration>();
            services.AddSingleton<IUsersContractOrchestration, UsersContractOrchestration>();

            var contract = Configuration.GetSection("Contract")
                .Get<ResourceContractOptions>();
            services.AddSingleton(contract);

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1",
                    new Info { Title = "Block-Auth API", Version = "v1" });
            });

            var MVC_VERSION = CompatibilityVersion.Version_2_2;
            services.AddMvc().SetCompatibilityVersion(MVC_VERSION);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("v1/swagger.json", "Blockchain Voting System API");
            });

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}