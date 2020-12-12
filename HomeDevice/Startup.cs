using HomeDevices.Core.Database;
using HomeDevices.Core.Database.Providers;
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

            //using (var serviceScope = app.ApplicationServices.CreateScope())
            //{
            //    var context = serviceScope.ServiceProvider.GetService<DevicesContext>();
            //    context.Database.EnsureCreated();

            //    //// Test data
            //    //var consumerId = System.Guid.NewGuid();

            //    //var consumer = new Consumer()
            //    //{
            //    //    ConsumerId = consumerId,
            //    //    FirstName = "Simone",
            //    //    LastName = "Borda",
            //    //    Address = "Via xxx",
            //    //    Email = "simoneb81@gmail.com"
            //    //};

            //    //context.Consumers.Add(consumer);

            //    //context.Devices.Add(new Device()
            //    //{
            //    //    Consumer = consumer,
            //    //    DeviceId = System.Guid.NewGuid(),
            //    //    Description = "Test",
            //    //    RegisteredOn = System.DateTime.Now
            //    //});

            //    context.SaveChanges();
            //}

            Log.Information("Service started.");

        }
    }
}
