namespace FlowOrchestrator.Common.Configuration;

/// <summary>
/// Represents the bulk operation configuration.
/// </summary>
public class BulkOperationConfiguration
{
    /// <summary>
    /// Gets or sets a value indicating whether bulk operations are enabled.
    /// </summary>
    public bool Enabled { get; set; }
    
    /// <summary>
    /// Gets or sets the batch size for bulk operations.
    /// </summary>
    public int BatchSize { get; set; } = 1000;
    
    /// <summary>
    /// Gets or sets the timeout for bulk operations in seconds.
    /// </summary>
    public int TimeoutSeconds { get; set; } = 300;
    
    /// <summary>
    /// Gets or sets a value indicating whether to use ordered operations.
    /// </summary>
    public bool OrderedOperations { get; set; }
    
    /// <summary>
    /// Gets or sets a value indicating whether to keep identity values.
    /// </summary>
    public bool KeepIdentity { get; set; }
    
    /// <summary>
    /// Gets or sets a value indicating whether to check constraints.
    /// </summary>
    public bool CheckConstraints { get; set; } = true;
    
    /// <summary>
    /// Gets or sets a value indicating whether to fire triggers.
    /// </summary>
    public bool FireTriggers { get; set; }
    
    /// <summary>
    /// Gets or sets the additional parameters for this configuration.
    /// </summary>
    public Dictionary<string, object> Parameters { get; set; } = new Dictionary<string, object>();
}
