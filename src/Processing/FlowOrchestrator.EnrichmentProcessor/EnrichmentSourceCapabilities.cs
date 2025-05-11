namespace FlowOrchestrator.EnrichmentProcessor;

/// <summary>
/// Represents the capabilities of an enrichment source.
/// </summary>
public class EnrichmentSourceCapabilities
{
    /// <summary>
    /// Gets or sets a value indicating whether the enrichment source supports lookup by key.
    /// </summary>
    public bool SupportsLookupByKey { get; set; }
    
    /// <summary>
    /// Gets or sets a value indicating whether the enrichment source supports lookup by value.
    /// </summary>
    public bool SupportsLookupByValue { get; set; }
    
    /// <summary>
    /// Gets or sets a value indicating whether the enrichment source supports batch lookup.
    /// </summary>
    public bool SupportsBatchLookup { get; set; }
    
    /// <summary>
    /// Gets or sets a value indicating whether the enrichment source supports caching.
    /// </summary>
    public bool SupportsCache { get; set; }
    
    /// <summary>
    /// Gets or sets a value indicating whether the enrichment source supports transformation.
    /// </summary>
    public bool SupportsTransformation { get; set; }
    
    /// <summary>
    /// Gets or sets a value indicating whether the enrichment source supports streaming.
    /// </summary>
    public bool SupportsStreaming { get; set; }
    
    /// <summary>
    /// Gets or sets the maximum batch size supported by the enrichment source.
    /// </summary>
    public int MaxBatchSize { get; set; }
    
    /// <summary>
    /// Gets or sets the supported lookup key types.
    /// </summary>
    public List<string> SupportedLookupKeyTypes { get; set; } = new List<string>();
    
    /// <summary>
    /// Gets or sets the supported lookup value types.
    /// </summary>
    public List<string> SupportedLookupValueTypes { get; set; } = new List<string>();
    
    /// <summary>
    /// Gets or sets the supported transformation types.
    /// </summary>
    public List<string> SupportedTransformationTypes { get; set; } = new List<string>();
}
