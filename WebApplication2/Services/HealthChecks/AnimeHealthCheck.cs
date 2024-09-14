using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace WebApplication2.Services.HealthChecker
{
    public class AnimeHealthCheck : IHealthCheck
    {
        private readonly string _param;

        public AnimeHealthCheck(string param)
        {
            _param = param;
        }

        public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            return Task.FromResult(HealthCheckResult.Healthy(description: _param));
        }

        public static Task WriteResponse(HttpContext httpContext, HealthReport healthReport)
        {
            var healthEntry = healthReport.Entries.Values.FirstOrDefault();
            return httpContext.Response.WriteAsync($"{healthReport.Status}: {healthEntry.Description}, {httpContext.Request.Headers["Authoriaztion"]}");
        }
    }
}
