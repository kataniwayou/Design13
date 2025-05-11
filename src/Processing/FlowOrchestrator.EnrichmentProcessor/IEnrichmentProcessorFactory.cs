namespace FlowOrchestrator.EnrichmentProcessor;

/// <summary>
/// Defines the interface for a factory that creates enrichment processors.
/// </summary>
public interface IEnrichmentProcessorFactory
{
    /// <summary>
    /// Creates a new enrichment processor.
    /// </summary>
    /// <param name="processorId">The unique identifier for the processor.</param>
    /// <param name="name">The name of the processor.</param>
    /// <param name="description">The description of the processor.</param>
    /// <returns>A new enrichment processor.</returns>
    EnrichmentProcessor CreateProcessor(string processorId, string name, string description);
}
