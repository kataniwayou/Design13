namespace FlowOrchestrator.JsonProcessor;

/// <summary>
/// Defines the interface for a factory that creates JSON processors.
/// </summary>
public interface IJsonProcessorFactory
{
    /// <summary>
    /// Creates a new JSON processor.
    /// </summary>
    /// <param name="processorId">The unique identifier for the processor.</param>
    /// <param name="name">The name of the processor.</param>
    /// <param name="description">The description of the processor.</param>
    /// <returns>A new JSON processor.</returns>
    JsonProcessor CreateProcessor(string processorId, string name, string description);
}
