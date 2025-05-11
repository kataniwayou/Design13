namespace FlowOrchestrator.Common.Configuration;

/// <summary>
/// Represents the configuration for a processor.
/// </summary>
public class ProcessorConfiguration
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
    /// Gets or sets the validation configuration.
    /// </summary>
    public ValidationConfiguration? Validation { get; set; }
    
    /// <summary>
    /// Gets or sets the transformation configuration.
    /// </summary>
    public TransformationConfiguration? Transformation { get; set; }
    
    /// <summary>
    /// Gets or sets the mapping configuration.
    /// </summary>
    public MappingConfiguration? Mapping { get; set; }
    
    /// <summary>
    /// Gets or sets the enrichment configuration.
    /// </summary>
    public EnrichmentConfiguration? Enrichment { get; set; }
    
    /// <summary>
    /// Gets or sets the caching configuration.
    /// </summary>
    public CachingConfiguration? Caching { get; set; }
    
    /// <summary>
    /// Gets or sets the error recovery configuration.
    /// </summary>
    public ErrorRecoveryConfiguration? ErrorRecovery { get; set; }
    
    /// <summary>
    /// Gets or sets the performance configuration.
    /// </summary>
    public PerformanceConfiguration? Performance { get; set; }
    
    /// <summary>
    /// Gets or sets the additional parameters for this configuration.
    /// </summary>
    public Dictionary<string, object> Parameters { get; set; } = new Dictionary<string, object>();
}
