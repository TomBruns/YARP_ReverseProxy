using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Worldpay.US.Express.Utilities;


/// <summary>
/// This class implements custom service Health Checks
/// </summary>
public class HealthChecks : IHealthCheck
{
    public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
    {
        var isHealthy = true;
        Dictionary<string, object> healthCheckTests = new Dictionary<string, object>();

        // create health checks for other dependencies here;

        return isHealthy
            ? Task.FromResult(HealthCheckResult.Healthy(@"Healthy"))
            : Task.FromResult(new HealthCheckResult(context.Registration.FailureStatus, @"", null, healthCheckTests));
    }
}
