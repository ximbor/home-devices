using Microsoft.Extensions.Diagnostics.HealthChecks;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace HomeDevices
{
    public class HealthCheck : IHealthCheck
    {

        public Task<HealthCheckResult> CheckHealthAsync(
        HealthCheckContext context,
        CancellationToken cancellationToken = default(CancellationToken))
        {
            bool healthCheckResultHealthy = true;

            if (healthCheckResultHealthy)
            {
                return Task.FromResult(
                    HealthCheckResult.Healthy("Healthy."));
            }

            return Task.FromResult(
                HealthCheckResult.Unhealthy("Unhealthy."));
        }
    }
}