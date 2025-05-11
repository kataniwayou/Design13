namespace FlowOrchestrator.Abstractions.Services;

/// <summary>
/// Represents the status of a managed resource.
/// </summary>
public class ResourceStatus
{
    /// <summary>
    /// Gets or sets the ID of the resource.
    /// </summary>
    public string ResourceId { get; set; } = string.Empty;
    
    /// <summary>
    /// Gets or sets the name of the resource.
    /// </summary>
    public string ResourceName { get; set; } = string.Empty;
    
    /// <summary>
    /// Gets or sets the type of the resource.
    /// </summary>
    public string ResourceType { get; set; } = string.Empty;
    
    /// <summary>
    /// Gets or sets the status of the resource.
    /// </summary>
    public ServiceStatus Status { get; set; }
    
    /// <summary>
    /// Gets or sets the health status of the resource.
    /// </summary>
    public HealthStatus HealthStatus { get; set; }
    
    /// <summary>
    /// Gets or sets the timestamp when the status was last updated.
    /// </summary>
    public DateTime LastUpdated { get; set; }
    
    /// <summary>
    /// Gets or sets the timestamp when the resource was started.
    /// </summary>
    public DateTime? StartTime { get; set; }
    
    /// <summary>
    /// Gets or sets the uptime of the resource.
    /// </summary>
    public TimeSpan? Uptime { get; set; }
    
    /// <summary>
    /// Gets or sets the error message if the resource is in an error state.
    /// </summary>
    public string? ErrorMessage { get; set; }
    
    /// <summary>
    /// Gets or sets the metrics associated with the resource.
    /// </summary>
    public Dictionary<string, string> Metrics { get; set; } = new Dictionary<string, string>();
}
