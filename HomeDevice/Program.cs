using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;

namespace HomeDevices
{
    public class Program
    {
        public static int Main(string[] args)
        {
            try
            {
                // Logger setup:
                string loggingLevel = Environment.GetEnvironmentVariable("LOG_LEVEL");
                HomeDevices.Core.Utils.Logger.CreateLoggerFromEnvironment("svc-home-devices", loggingLevel);

                Log.Logger.Information("Starting service...");
                CreateHostBuilder(args).Build().Run();
                return 0;
            }
            catch (Exception ex)
            {
                Log.Logger.Fatal($"Service terminated unexpectedly - {ex.Message}");
                return 1;
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseSerilog()
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
