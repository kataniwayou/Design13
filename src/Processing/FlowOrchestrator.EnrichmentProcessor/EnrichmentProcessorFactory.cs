using Microsoft.Extensions.Logging;

namespace FlowOrchestrator.EnrichmentProcessor;

/// <summary>
/// Factory for creating enrichment processors.
/// </summary>
public class EnrichmentProcessorFactory : IEnrichmentProcessorFactory
{
    private readonly IEnrichmentSourceProvider _enrichmentSourceProvider;
    private readonly ILoggerFactory _loggerFactory;
    
    /// <summary>
    /// Initializes a new instance of the <see cref="EnrichmentProcessorFactory"/> class.
    /// </summary>
    /// <param name="enrichmentSourceProvider">The enrichment source provider.</param>
    /// <param name="loggerFactory">The logger factory.</param>
    public EnrichmentProcessorFactory(
        IEnrichmentSourceProvider enrichmentSourceProvider,
        ILoggerFactory loggerFactory)
    {
        _enrichmentSourceProvider = enrichmentSourceProvider ?? throw new ArgumentNullException(nameof(enrichmentSourceProvider));
        _loggerFactory = loggerFactory ?? throw new ArgumentNullException(nameof(loggerFactory));
    }
    
    /// <inheritdoc />
    public EnrichmentProcessor CreateProcessor(string processorId, string name, string description)
    {
        var logger = _loggerFactory.CreateLogger<EnrichmentProcessor>();
        return new EnrichmentProcessor(processorId, name, description, _enrichmentSourceProvider, logger);
    }
}
