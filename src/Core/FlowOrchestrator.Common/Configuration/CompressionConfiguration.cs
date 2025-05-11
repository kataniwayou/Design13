namespace FlowOrchestrator.Common.Configuration;

/// <summary>
/// Represents the compression configuration.
/// </summary>
public class CompressionConfiguration
{
    /// <summary>
    /// Gets or sets a value indicating whether compression is enabled.
    /// </summary>
    public bool Enabled { get; set; }
    
    /// <summary>
    /// Gets or sets the compression method.
    /// </summary>
    public string Method { get; set; } = string.Empty;
    
    /// <summary>
    /// Gets or sets the compression level.
    /// </summary>
    public int Level { get; set; }
    
    /// <summary>
    /// Gets or sets the additional parameters for this configuration.
    /// </summary>
    public Dictionary<string, object> Parameters { get; set; } = new Dictionary<string, object>();
}
