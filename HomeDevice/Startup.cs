using HomeDevices.Database;
using HomeDevices.Database.Models;
using HomeDevices.Database.Services;
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

            services.AddControllers();
            services.AddHealthChecks().AddCheck<HealthCheck>("health-check");
            services.AddDbContext<DevicesContext>(options =>
                options.UseNpgsql($"Host={System.Environment.MachineName};Database=DEVICES;Username=homedev;Password=homedev"));

            services.AddTransient<IDevicesService, DevicesService>();
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

            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<DevicesContext>();
                context.Database.EnsureCreated();

                // Test data
                context.Devices.Add(new Device()
                {
                    DeviceId = System.Guid.NewGuid(),
                    Description = "Test",
                    RegisteredOn = System.DateTime.Now
                });

                context.SaveChanges();
            }

            Log.Information("Service started.");

        }
    }
}
