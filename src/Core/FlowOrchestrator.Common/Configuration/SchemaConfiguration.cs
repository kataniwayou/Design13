namespace FlowOrchestrator.Common.Configuration;

/// <summary>
/// Represents the schema configuration.
/// </summary>
public class SchemaConfiguration
{
    /// <summary>
    /// Gets or sets a value indicating whether schema validation is enabled.
    /// </summary>
    public bool ValidationEnabled { get; set; }
    
    /// <summary>
    /// Gets or sets a value indicating whether schema discovery is enabled.
    /// </summary>
    public bool DiscoveryEnabled { get; set; }
    
    /// <summary>
    /// Gets or sets the schema path.
    /// </summary>
    public string? SchemaPath { get; set; }
    
    /// <summary>
    /// Gets or sets the schema format.
    /// </summary>
    public string? SchemaFormat { get; set; }
    
    /// <summary>
    /// Gets or sets the additional parameters for this configuration.
    /// </summary>
    public Dictionary<string, object> Parameters { get; set; } = new Dictionary<string, object>();
}
