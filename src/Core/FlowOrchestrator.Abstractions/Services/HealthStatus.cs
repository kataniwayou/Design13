namespace FlowOrchestrator.Abstractions.Services;

/// <summary>
/// Represents the health status of a service.
/// </summary>
public enum HealthStatus
{
    /// <summary>
    /// The service is healthy.
    /// </summary>
    Healthy,
    
    /// <summary>
    /// The service is degraded but still functioning.
    /// </summary>
    Degraded,
    
    /// <summary>
    /// The service is unhealthy and not functioning properly.
    /// </summary>
    Unhealthy,
    
    /// <summary>
    /// The health status of the service is unknown.
    /// </summary>
    Unknown
}
