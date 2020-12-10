using Serilog;
using Serilog.Events;
using System;

namespace HomeDevices.Core.Utils
{
    public class Logger
    {
        public static void CreateLoggerFromEnvironment(string serviceName, string loggingLevel)
        {

            Func<string, LoggerConfiguration, LoggerConfiguration> setLevel = (level, config) => {
                level = level.ToUpper();

                switch (level)
                {
                    case "INFO": return config.MinimumLevel.Information();
                    case "DEBUG": return config.MinimumLevel.Debug();
                    case "ERROR": return config.MinimumLevel.Error();
                    case "WARNING": return config.MinimumLevel.Warning();
                    case "VERBOSE": return config.MinimumLevel.Verbose();
                    case "FATAL": return config.MinimumLevel.Fatal();
                    default: return config.MinimumLevel.Debug();
                }
            };

            string logTemplate = "{Timestamp:yyyy-MM-dd HH:mm:ss.fff} [" + serviceName + "] [{Level:u3}] {Message:lj}{NewLine}{Exception}";
            var loggerConfiguration = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                .Enrich.FromLogContext()
                .WriteTo.Console(outputTemplate: logTemplate);

            Log.Logger = loggerConfiguration.CreateLogger();

        }

    }
}
