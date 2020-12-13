using HomeDevices.Core.Database;
using HomeDevices.Core.Database.Providers;
using HomeDevices.Extensions;
using HomeDevices.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace HomeDevices
{
    public class Startup 
    {

        public void ConfigureServices(IServiceCollection services)
        {
            var appConfiguration = new ConfigurationBuilder()
              .AddEnvironmentVariables()
              .Build();

            var homeDevConfiguration = new HomeDevicesConfiguration()
            {
                // Default service configuration:
                ServiceId = "homde-dev",
                LogLevel = "DEBUG",
                DatabaseServer = System.Environment.MachineName,
                DatabaseName = "homedev",
                DatabaseUsername = "DEVICES",
                DatabasePassword = "homedev"                
            };
            homeDevConfiguration.SetEnvironmentConfiguration(appConfiguration);
            homeDevConfiguration.LogConfiguration();

            services.AddControllers();
            services.AddHealthChecks().AddCheck<HealthCheck>("health-check");
            services.AddDbContext<DevicesContext>(options =>
                options.UseNpgsql(homeDevConfiguration.GetConnectionString()));

            services.AddTransient<IDataProvider, DataProvider>();

        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //if (env.IsDevelopment())
            //{
            //    app.UseDeveloperExceptionPage();
            //}

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHealthChecks("/health");
            });

            app.UseSerilogRequestLogging();

        }
    }
}
