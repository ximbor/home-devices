using HomeDevices.Models;
using Microsoft.Extensions.Configuration;
using Serilog;
using System;

namespace HomeDevices.Extensions
{
    public static class HomeDevicesExtensions
    {
        public static void SetEnvironmentConfiguration(this HomeDevicesConfiguration config, IConfigurationRoot rootConfig)
        {
            string serviceListeningInterface = rootConfig.GetSection("ASPNETCORE_URLS").Value;
            string serviceId = rootConfig.GetSection("SERVICE_ID").Value;
            string databaseServer = rootConfig.GetSection("DB_SERVER").Value;
            string databaseName = rootConfig.GetSection("DB_NAME").Value;
            string databaseUsername = rootConfig.GetSection("DB_USERNAME").Value;
            string databasePassword = rootConfig.GetSection("DB_PWD").Value;
            string logLevel = rootConfig.GetSection("LOG_LEVEL").Value;

            if (!string.IsNullOrEmpty(serviceId))
            {
                config.ServiceId = serviceId;
            }

            if (!string.IsNullOrEmpty(serviceListeningInterface))
            {
                config.ServiceListeningInterface = serviceListeningInterface;
            }

            if (!string.IsNullOrEmpty(databaseServer))
            {
                config.DatabaseServer = databaseServer;
            }

            if (!string.IsNullOrEmpty(databaseName))
            {
                config.DatabaseName = databaseName;
            }

            if (!string.IsNullOrEmpty(databaseUsername))
            {
                config.DatabaseUsername = databaseUsername;
            }

            if (!string.IsNullOrEmpty(databasePassword))
            {
                config.DatabasePassword = databasePassword;
            }

            if (!string.IsNullOrEmpty(logLevel))
            {
                config.LogLevel = logLevel;
            }

        }

        public static void LogConfiguration(this HomeDevicesConfiguration config)
        {
            Log.Information("----------------------------------");
            Log.Information("Started home-devices");
            Log.Information("----------------------------------");
            Log.Information($"[CFG] Service Id                     = {config.ServiceId ?? ""}");
            Log.Information($"[CFG] Listening on                   = {config.ServiceListeningInterface ?? ""}");
            Log.Information($"[CFG] Database server                = {config.DatabaseServer ?? ""}");
            Log.Information($"[CFG] Database name                  = {config.DatabaseName ?? ""}");
            Log.Information($"[CFG] Database password              = *******");
            Log.Information($"[CFG] Database user name             = {config.DatabaseUsername ?? ""}");
            Log.Information($"[CFG] Log level                      = {config.LogLevel ?? ""}");
            Log.Information("----------------------------------");
        }
    }
}
