namespace FlowOrchestrator.EnrichmentProcessor;

/// <summary>
/// Represents an enrichment rule.
/// </summary>
public class EnrichmentRule
{
    /// <summary>
    /// Gets or sets the name of the enrichment source.
    /// </summary>
    public string SourceName { get; set; } = string.Empty;
    
    /// <summary>
    /// Gets or sets the target property to enrich.
    /// </summary>
    public string TargetProperty { get; set; } = string.Empty;
    
    /// <summary>
    /// Gets or sets the source property to use for enrichment.
    /// </summary>
    public string SourceProperty { get; set; } = string.Empty;
    
    /// <summary>
    /// Gets or sets the lookup key property.
    /// </summary>
    public string LookupKeyProperty { get; set; } = string.Empty;
    
    /// <summary>
    /// Gets or sets the lookup value property.
    /// </summary>
    public string LookupValueProperty { get; set; } = string.Empty;
    
    /// <summary>
    /// Gets or sets a value indicating whether to create the target property if it doesn't exist.
    /// </summary>
    public bool CreateIfNotExists { get; set; } = true;
    
    /// <summary>
    /// Gets or sets a value indicating whether to overwrite the target property if it already exists.
    /// </summary>
    public bool OverwriteIfExists { get; set; } = true;
    
    /// <summary>
    /// Gets or sets a value indicating whether to skip this rule if the lookup key is not found.
    /// </summary>
    public bool SkipIfLookupKeyNotFound { get; set; } = true;
    
    /// <summary>
    /// Gets or sets the default value to use if the lookup key is not found.
    /// </summary>
    public string? DefaultValue { get; set; }
    
    /// <summary>
    /// Gets or sets the transformation to apply to the enriched value.
    /// </summary>
    public string? Transformation { get; set; }
    
    /// <summary>
    /// Gets or sets the additional parameters for this rule.
    /// </summary>
    public Dictionary<string, object> Parameters { get; set; } = new Dictionary<string, object>();
}
