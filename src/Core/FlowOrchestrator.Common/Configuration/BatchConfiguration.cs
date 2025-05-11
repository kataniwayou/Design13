namespace FlowOrchestrator.Common.Configuration;

/// <summary>
/// Represents the batch configuration.
/// </summary>
public class BatchConfiguration
{
    /// <summary>
    /// Gets or sets a value indicating whether batching is enabled.
    /// </summary>
    public bool Enabled { get; set; }
    
    /// <summary>
    /// Gets or sets the batch size.
    /// </summary>
    public int BatchSize { get; set; } = 1000;
    
    /// <summary>
    /// Gets or sets the batch timeout in seconds.
    /// </summary>
    public int BatchTimeoutSeconds { get; set; } = 60;
    
    /// <summary>
    /// Gets or sets the additional parameters for this configuration.
    /// </summary>
    public Dictionary<string, object> Parameters { get; set; } = new Dictionary<string, object>();
}
