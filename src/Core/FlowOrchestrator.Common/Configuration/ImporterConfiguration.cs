namespace FlowOrchestrator.Common.Configuration;

/// <summary>
/// Represents the configuration for an importer.
/// </summary>
public class ImporterConfiguration
{
    /// <summary>
    /// Gets or sets the unique identifier for this configuration.
    /// </summary>
    public string ConfigurationId { get; set; } = Guid.NewGuid().ToString();
    
    /// <summary>
    /// Gets or sets the name of this configuration.
    /// </summary>
    public string Name { get; set; } = string.Empty;
    
    /// <summary>
    /// Gets or sets the description of this configuration.
    /// </summary>
    public string Description { get; set; } = string.Empty;
    
    /// <summary>
    /// Gets or sets the connection string for the data source.
    /// </summary>
    public string? ConnectionString { get; set; }
    
    /// <summary>
    /// Gets or sets the authentication configuration.
    /// </summary>
    public AuthenticationConfiguration? Authentication { get; set; }
    
    /// <summary>
    /// Gets or sets the encryption configuration.
    /// </summary>
    public EncryptionConfiguration? Encryption { get; set; }
    
    /// <summary>
    /// Gets or sets the compression configuration.
    /// </summary>
    public CompressionConfiguration? Compression { get; set; }
    
    /// <summary>
    /// Gets or sets the retry configuration.
    /// </summary>
    public RetryConfiguration? Retry { get; set; }
    
    /// <summary>
    /// Gets or sets the timeout configuration.
    /// </summary>
    public TimeoutConfiguration? Timeout { get; set; }
    
    /// <summary>
    /// Gets or sets the batch configuration.
    /// </summary>
    public BatchConfiguration? Batch { get; set; }
    
    /// <summary>
    /// Gets or sets the schema configuration.
    /// </summary>
    public SchemaConfiguration? Schema { get; set; }
    
    /// <summary>
    /// Gets or sets the additional parameters for this configuration.
    /// </summary>
    public Dictionary<string, object> Parameters { get; set; } = new Dictionary<string, object>();
}
