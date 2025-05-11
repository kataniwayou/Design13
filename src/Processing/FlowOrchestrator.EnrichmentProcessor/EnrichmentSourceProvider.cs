using Microsoft.Extensions.Logging;

namespace FlowOrchestrator.EnrichmentProcessor;

/// <summary>
/// Provider for enrichment sources.
/// </summary>
public class EnrichmentSourceProvider : IEnrichmentSourceProvider
{
    private readonly ILogger<EnrichmentSourceProvider> _logger;
    private readonly Dictionary<string, IEnrichmentSource> _sources = new Dictionary<string, IEnrichmentSource>();
    
    /// <summary>
    /// Initializes a new instance of the <see cref="EnrichmentSourceProvider"/> class.
    /// </summary>
    /// <param name="logger">The logger.</param>
    public EnrichmentSourceProvider(ILogger<EnrichmentSourceProvider> logger)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }
    
    /// <inheritdoc />
    public Task<IEnrichmentSource?> GetEnrichmentSourceAsync(string sourceName, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(sourceName)) throw new ArgumentException("Source name cannot be empty", nameof(sourceName));
        
        _logger.LogInformation("Getting enrichment source {SourceName}", sourceName);
        
        _sources.TryGetValue(sourceName, out var source);
        
        return Task.FromResult(source);
    }
    
    /// <inheritdoc />
    public Task<IEnumerable<IEnrichmentSource>> GetAllEnrichmentSourcesAsync(CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Getting all enrichment sources");
        
        return Task.FromResult<IEnumerable<IEnrichmentSource>>(_sources.Values.ToList());
    }
    
    /// <inheritdoc />
    public Task RegisterEnrichmentSourceAsync(IEnrichmentSource source, CancellationToken cancellationToken = default)
    {
        if (source == null) throw new ArgumentNullException(nameof(source));
        
        _logger.LogInformation("Registering enrichment source {SourceName}", source.Name);
        
        _sources[source.Name] = source;
        
        return Task.CompletedTask;
    }
    
    /// <inheritdoc />
    public Task UnregisterEnrichmentSourceAsync(string sourceName, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(sourceName)) throw new ArgumentException("Source name cannot be empty", nameof(sourceName));
        
        _logger.LogInformation("Unregistering enrichment source {SourceName}", sourceName);
        
        _sources.Remove(sourceName);
        
        return Task.CompletedTask;
    }
}
