namespace FlowOrchestrator.MappingProcessor;

/// <summary>
/// Defines the interface for a factory that creates mapping processors.
/// </summary>
public interface IMappingProcessorFactory
{
    /// <summary>
    /// Creates a new mapping processor.
    /// </summary>
    /// <param name="processorId">The unique identifier for the processor.</param>
    /// <param name="name">The name of the processor.</param>
    /// <param name="description">The description of the processor.</param>
    /// <returns>A new mapping processor.</returns>
    MappingProcessor CreateProcessor(string processorId, string name, string description);
}
