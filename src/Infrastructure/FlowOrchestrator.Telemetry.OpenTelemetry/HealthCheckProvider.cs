using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Logging;

namespace FlowOrchestrator.Telemetry.OpenTelemetry
{
    /// <summary>
    /// Provider for health checks in the FlowOrchestrator system.
    /// </summary>
    public class HealthCheckProvider : IHealthCheck
    {
        private readonly ILogger<HealthCheckProvider> _logger;
        private readonly Dictionary<string, Func<CancellationToken, Task<HealthCheckResult>>> _healthChecks = new Dictionary<string, Func<CancellationToken, Task<HealthCheckResult>>>();

        /// <summary>
        /// Initializes a new instance of the <see cref="HealthCheckProvider"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        public HealthCheckProvider(ILogger<HealthCheckProvider> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Registers a health check.
        /// </summary>
        /// <param name="name">The name of the health check.</param>
        /// <param name="check">The health check function.</param>
        public void RegisterHealthCheck(string name, Func<CancellationToken, Task<HealthCheckResult>> check)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentException("Health check name cannot be null or empty.", nameof(name));
            
            if (check == null)
                throw new ArgumentNullException(nameof(check));
            
            try
            {
                _logger.LogDebug("Registering health check {HealthCheckName}", name);
                
                _healthChecks[name] = check;
                
                _logger.LogDebug("Health check {HealthCheckName} registered successfully", name);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error registering health check {HealthCheckName}", name);
                throw;
            }
        }

        /// <summary>
        /// Unregisters a health check.
        /// </summary>
        /// <param name="name">The name of the health check.</param>
        public void UnregisterHealthCheck(string name)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentException("Health check name cannot be null or empty.", nameof(name));
            
            try
            {
                _logger.LogDebug("Unregistering health check {HealthCheckName}", name);
                
                _healthChecks.Remove(name);
                
                _logger.LogDebug("Health check {HealthCheckName} unregistered successfully", name);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error unregistering health check {HealthCheckName}", name);
                throw;
            }
        }

        /// <summary>
        /// Checks the health of the system.
        /// </summary>
        /// <param name="context">The health check context.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The health check result.</returns>
        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            try
            {
                _logger.LogDebug("Checking health of the system");
                
                if (_healthChecks.Count == 0)
                {
                    _logger.LogWarning("No health checks registered");
                    return HealthCheckResult.Healthy("No health checks registered");
                }
                
                var results = new Dictionary<string, object>();
                var status = HealthStatus.Healthy;
                var description = "All health checks passed";
                Exception exception = null;
                
                foreach (var healthCheck in _healthChecks)
                {
                    try
                    {
                        var result = await healthCheck.Value(cancellationToken);
                        results[healthCheck.Key] = result;
                        
                        // Update the overall status
                        if (result.Status == HealthStatus.Unhealthy)
                        {
                            status = HealthStatus.Unhealthy;
                            description = $"Health check {healthCheck.Key} failed: {result.Description}";
                            exception = result.Exception;
                        }
                        else if (result.Status == HealthStatus.Degraded && status == HealthStatus.Healthy)
                        {
                            status = HealthStatus.Degraded;
                            description = $"Health check {healthCheck.Key} degraded: {result.Description}";
                        }
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "Error executing health check {HealthCheckName}", healthCheck.Key);
                        results[healthCheck.Key] = new HealthCheckResult(HealthStatus.Unhealthy, $"Exception: {ex.Message}", ex);
                        status = HealthStatus.Unhealthy;
                        description = $"Health check {healthCheck.Key} failed with exception: {ex.Message}";
                        exception = ex;
                    }
                }
                
                _logger.LogDebug("Health check completed with status {Status}", status);
                
                return new HealthCheckResult(status, description, exception, results);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error checking health of the system");
                return new HealthCheckResult(HealthStatus.Unhealthy, "Error checking health of the system", ex);
            }
        }

        /// <summary>
        /// Creates a health check for a database connection.
        /// </summary>
        /// <param name="name">The name of the health check.</param>
        /// <param name="checkConnection">The function to check the database connection.</param>
        /// <returns>The health check function.</returns>
        public static Func<CancellationToken, Task<HealthCheckResult>> CreateDatabaseHealthCheck(string name, Func<CancellationToken, Task<bool>> checkConnection)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentException("Health check name cannot be null or empty.", nameof(name));
            
            if (checkConnection == null)
                throw new ArgumentNullException(nameof(checkConnection));
            
            return async (cancellationToken) =>
            {
                try
                {
                    var isConnected = await checkConnection(cancellationToken);
                    
                    if (isConnected)
                    {
                        return HealthCheckResult.Healthy($"Database {name} is connected");
                    }
                    else
                    {
                        return HealthCheckResult.Unhealthy($"Database {name} is not connected");
                    }
                }
                catch (Exception ex)
                {
                    return HealthCheckResult.Unhealthy($"Database {name} connection check failed", ex);
                }
            };
        }

        /// <summary>
        /// Creates a health check for a service dependency.
        /// </summary>
        /// <param name="name">The name of the health check.</param>
        /// <param name="checkService">The function to check the service.</param>
        /// <returns>The health check function.</returns>
        public static Func<CancellationToken, Task<HealthCheckResult>> CreateServiceHealthCheck(string name, Func<CancellationToken, Task<bool>> checkService)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentException("Health check name cannot be null or empty.", nameof(name));
            
            if (checkService == null)
                throw new ArgumentNullException(nameof(checkService));
            
            return async (cancellationToken) =>
            {
                try
                {
                    var isAvailable = await checkService(cancellationToken);
                    
                    if (isAvailable)
                    {
                        return HealthCheckResult.Healthy($"Service {name} is available");
                    }
                    else
                    {
                        return HealthCheckResult.Unhealthy($"Service {name} is not available");
                    }
                }
                catch (Exception ex)
                {
                    return HealthCheckResult.Unhealthy($"Service {name} availability check failed", ex);
                }
            };
        }

        /// <summary>
        /// Creates a health check for system resources.
        /// </summary>
        /// <param name="name">The name of the health check.</param>
        /// <param name="checkResources">The function to check the resources.</param>
        /// <param name="threshold">The threshold for the resources.</param>
        /// <returns>The health check function.</returns>
        public static Func<CancellationToken, Task<HealthCheckResult>> CreateResourceHealthCheck(string name, Func<CancellationToken, Task<double>> checkResources, double threshold)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentException("Health check name cannot be null or empty.", nameof(name));
            
            if (checkResources == null)
                throw new ArgumentNullException(nameof(checkResources));
            
            return async (cancellationToken) =>
            {
                try
                {
                    var value = await checkResources(cancellationToken);
                    
                    if (value <= threshold)
                    {
                        return HealthCheckResult.Healthy($"Resource {name} is within threshold: {value} <= {threshold}");
                    }
                    else if (value <= threshold * 1.5)
                    {
                        return HealthCheckResult.Degraded($"Resource {name} is approaching threshold: {value} > {threshold}");
                    }
                    else
                    {
                        return HealthCheckResult.Unhealthy($"Resource {name} exceeds threshold: {value} > {threshold}");
                    }
                }
                catch (Exception ex)
                {
                    return HealthCheckResult.Unhealthy($"Resource {name} check failed", ex);
                }
            };
        }
    }
}
