namespace FlowOrchestrator.EnrichmentProcessor;

/// <summary>
/// Defines the interface for an enrichment source.
/// </summary>
public interface IEnrichmentSource
{
    /// <summary>
    /// Gets the name of this enrichment source.
    /// </summary>
    string Name { get; }
    
    /// <summary>
    /// Gets the description of this enrichment source.
    /// </summary>
    string Description { get; }
    
    /// <summary>
    /// Gets the type of this enrichment source.
    /// </summary>
    string SourceType { get; }
    
    /// <summary>
    /// Gets the version of this enrichment source.
    /// </summary>
    string Version { get; }
    
    /// <summary>
    /// Enriches the specified data using the specified rule.
    /// </summary>
    /// <param name="data">The data to enrich.</param>
    /// <param name="rule">The enrichment rule to apply.</param>
    /// <param name="cancellationToken">A cancellation token to observe while waiting for the task to complete.</param>
    /// <returns>A task representing the asynchronous operation, containing the enrichment result.</returns>
    Task<EnrichmentSourceResult> EnrichAsync(object data, EnrichmentRule rule, CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Gets the capabilities of this enrichment source.
    /// </summary>
    /// <returns>The capabilities of this enrichment source.</returns>
    EnrichmentSourceCapabilities GetCapabilities();
}
