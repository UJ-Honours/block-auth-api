using block_auth_api.Connection;
using block_auth_api.Models;
using block_auth_api.Orchestration.AccountContract;
using block_auth_api.Orchestration.DeviceContract;
using block_auth_api.Orchestration.TokenOrchestration;
using block_auth_api.Orchestration.UsersContract;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.Swagger;
using System.Text;

namespace block_auth_api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        private void InjectOrchestration(IServiceCollection services)
        {
            services.AddSingleton<IContractManager, ContractManager>();

            services.AddSingleton<IAccountContractOrchestration, AccountContractOrchestration>();
            services.AddSingleton<IDeviceContractOrchestration, DeviceContractOrchestration>();
            services.AddSingleton<IUsersContractOrchestration, UsersContractOrchestration>();
            services.AddSingleton<ITokenOrchestration, TokenOrchestration>();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            InjectOrchestration(services);
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

            services.AddCors(options1 =>
            {
                options1.AddPolicy("MyCorsPolicy", builder => builder
                    .AllowAnyMethod()
                    .AllowCredentials()
                    .AllowAnyOrigin()
                    .AllowAnyHeader());
                //.WithHeaders("Accept", "Content-Type", "Origin", "X-My-Header"));
            });

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = Configuration["Jwt:Issuer"],
            ValidAudience = Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"]))
        };
    });

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
            app.UseCors("MyCorsPolicy");
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("v1/swagger.json", "Blockchain Voting System API");
            });
            app.UseAuthentication();
            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}