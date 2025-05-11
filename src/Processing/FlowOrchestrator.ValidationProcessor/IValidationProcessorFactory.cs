namespace FlowOrchestrator.ValidationProcessor;

/// <summary>
/// Defines the interface for a factory that creates validation processors.
/// </summary>
public interface IValidationProcessorFactory
{
    /// <summary>
    /// Creates a new validation processor.
    /// </summary>
    /// <param name="processorId">The unique identifier for the processor.</param>
    /// <param name="name">The name of the processor.</param>
    /// <param name="description">The description of the processor.</param>
    /// <returns>A new validation processor.</returns>
    ValidationProcessor CreateProcessor(string processorId, string name, string description);
}
