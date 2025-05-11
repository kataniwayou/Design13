namespace FlowOrchestrator.Common.Configuration;

/// <summary>
/// Represents the configuration for enrichment.
/// </summary>
public class EnrichmentConfiguration
{
    /// <summary>
    /// Gets or sets a value indicating whether enrichment is enabled.
    /// </summary>
    public bool IsEnabled { get; set; } = true;
    
    /// <summary>
    /// Gets or sets the enrichment rules.
    /// </summary>
    public List<string> EnrichmentRules { get; set; } = new List<string>();
    
    /// <summary>
    /// Gets or sets the enrichment rule types.
    /// </summary>
    public List<string> EnrichmentRuleTypes { get; set; } = new List<string>();
    
    /// <summary>
    /// Gets or sets the enrichment sources.
    /// </summary>
    public List<string> EnrichmentSources { get; set; } = new List<string>();
    
    /// <summary>
    /// Gets or sets a value indicating whether to validate the original data before enrichment.
    /// </summary>
    public bool ValidateOriginalBeforeEnrichment { get; set; } = true;
    
    /// <summary>
    /// Gets or sets a value indicating whether to validate the enriched data after enrichment.
    /// </summary>
    public bool ValidateEnrichedAfterEnrichment { get; set; } = true;
    
    /// <summary>
    /// Gets or sets a value indicating whether to optimize the enrichment.
    /// </summary>
    public bool OptimizeEnrichment { get; set; } = true;
    
    /// <summary>
    /// Gets or sets a value indicating whether to use caching for enrichment.
    /// </summary>
    public bool UseCache { get; set; } = true;
    
    /// <summary>
    /// Gets or sets the additional parameters for enrichment.
    /// </summary>
    public Dictionary<string, object> Parameters { get; set; } = new Dictionary<string, object>();
}
