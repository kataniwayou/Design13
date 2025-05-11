namespace FlowOrchestrator.Common.Configuration;

/// <summary>
/// Represents the timeout configuration.
/// </summary>
public class TimeoutConfiguration
{
    /// <summary>
    /// Gets or sets the connection timeout in seconds.
    /// </summary>
    public int ConnectionTimeoutSeconds { get; set; } = 30;
    
    /// <summary>
    /// Gets or sets the operation timeout in seconds.
    /// </summary>
    public int OperationTimeoutSeconds { get; set; } = 3600;
    
    /// <summary>
    /// Gets or sets the idle timeout in seconds.
    /// </summary>
    public int IdleTimeoutSeconds { get; set; } = 300;
    
    /// <summary>
    /// Gets or sets the additional parameters for this configuration.
    /// </summary>
    public Dictionary<string, object> Parameters { get; set; } = new Dictionary<string, object>();
}
