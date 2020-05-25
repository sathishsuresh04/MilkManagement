using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace MilkManagement.Web.HealthCheck
{
    public class IndexPageHealthCheck: IHealthCheck
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public IndexPageHealthCheck(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            var request = _httpContextAccessor.HttpContext.Request;
            var myUrl = request.Scheme + "://" + request.Host.ToString();

            var client = new HttpClient();
            var response = await client.GetAsync(myUrl, cancellationToken);
            var pageContents = await response.Content.ReadAsStringAsync();
            return pageContents.Contains("product1") ? HealthCheckResult.Healthy("The check indicates a healthy result.") : HealthCheckResult.Unhealthy("The check indicates an unhealthy result.");
        }
    }
}
