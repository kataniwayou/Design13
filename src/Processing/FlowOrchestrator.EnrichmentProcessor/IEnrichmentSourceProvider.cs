namespace FlowOrchestrator.EnrichmentProcessor;

/// <summary>
/// Defines the interface for a provider that provides enrichment sources.
/// </summary>
public interface IEnrichmentSourceProvider
{
    /// <summary>
    /// Gets an enrichment source by name.
    /// </summary>
    /// <param name="sourceName">The name of the enrichment source.</param>
    /// <param name="cancellationToken">A cancellation token to observe while waiting for the task to complete.</param>
    /// <returns>A task representing the asynchronous operation, containing the enrichment source.</returns>
    Task<IEnrichmentSource?> GetEnrichmentSourceAsync(string sourceName, CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Gets all available enrichment sources.
    /// </summary>
    /// <param name="cancellationToken">A cancellation token to observe while waiting for the task to complete.</param>
    /// <returns>A task representing the asynchronous operation, containing the enrichment sources.</returns>
    Task<IEnumerable<IEnrichmentSource>> GetAllEnrichmentSourcesAsync(CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Registers an enrichment source.
    /// </summary>
    /// <param name="source">The enrichment source to register.</param>
    /// <param name="cancellationToken">A cancellation token to observe while waiting for the task to complete.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    Task RegisterEnrichmentSourceAsync(IEnrichmentSource source, CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Unregisters an enrichment source.
    /// </summary>
    /// <param name="sourceName">The name of the enrichment source to unregister.</param>
    /// <param name="cancellationToken">A cancellation token to observe while waiting for the task to complete.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    Task UnregisterEnrichmentSourceAsync(string sourceName, CancellationToken cancellationToken = default);
}
