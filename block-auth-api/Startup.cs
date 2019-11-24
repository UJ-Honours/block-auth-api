using block_auth_api.Connection;
using block_auth_api.Models;
using block_auth_api.Orchestration;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using OrchestrationLibrary.LogsOrchestration;
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
            services.AddSingleton<ILogsOrchestration, LogsOrchestration>();
            services.AddSingleton<IDeviceContractOrchestration, DeviceContractOrchestration>();
            services.AddSingleton<IUsersContractOrchestration, UsersContractOrchestration>();
            services.AddSingleton<ITokenOrchestration, TokenOrchestration>();
            services.AddSingleton<IOwnerRoleOrchestration, OwnerRoleOrchestration>();
            services.AddSingleton<IGuestRoleOrchestration, GuestRoleOrchestration>();
        }

        private void InjectDependencies(IServiceCollection services)
        {
            InjectOrchestration(services);
            InjectContracts(services);
            InjectContractManagers(services);
            InjectCors(services);
            InjectJWT(services);
        }

        private void InjectContractManagers(IServiceCollection services)
        {
            services.AddSingleton<ILogContractManager, LogContractManager>();
            services.AddSingleton<IGuestRoleContractManager, GuestRoleContractManager>();
            services.AddSingleton<IOwnerRoleContractManager, OwnerRoleContractManager>();
            services.AddSingleton<IDeviceContractManager, DeviceContractManager>();
            services.AddSingleton<IUserContractManager, UserContractManager>();
        }

        private void InjectContracts(IServiceCollection services)
        {
            var LogContract = Configuration.GetSection("Log")
               .Get<LogContractOptions>();
            services.AddSingleton(LogContract);

            var deviceContract = Configuration.GetSection("Device")
                .Get<DeviceContractOptions>();
            services.AddSingleton(deviceContract);

            var userContract = Configuration.GetSection("User")
                .Get<UserContractOptions>();
            services.AddSingleton(userContract);

            var guestRoleContract = Configuration.GetSection("GuestRole")
                .Get<GuestRoleContractOptions>();
            services.AddSingleton(guestRoleContract);

            var ownerRoleContract = Configuration.GetSection("OwnerRole")
                .Get<OwnerRoleContractOptions>();
            services.AddSingleton(ownerRoleContract);

        }

        private void InjectJWT(IServiceCollection services)
        {
            var appSettingsSection = Configuration.GetSection("JWTSettings");
            services.Configure<JWTSettings>(appSettingsSection);

            // configure jwt authentication
            var appSettings = appSettingsSection.Get<JWTSettings>();
            var key = Encoding.ASCII.GetBytes(appSettings.SecretKey);

            var tokenParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false
            };
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = tokenParameters;
            });
        }

        private void InjectCors(IServiceCollection services)
        {
            services.AddCors(options1 =>
            {
                options1.AddPolicy("MyCorsPolicy", builder => builder
                    .AllowAnyMethod()
                    .AllowAnyOrigin()
                    .AllowAnyHeader());
            });
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            InjectDependencies(services);

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
            app.UseCors("MyCorsPolicy");
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("v1/swagger.json", "Blockchain Voting System API");
            });
            app.UseAuthentication();
            //app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}