namespace FlowOrchestrator.Abstractions.Services;

/// <summary>
/// Interface for processor services that process data.
/// </summary>
public interface IProcessorService : IService
{
    /// <summary>
    /// Gets the processor type that this processor implements.
    /// </summary>
    string ProcessorType { get; }
    
    /// <summary>
    /// Gets the supported data formats for this processor.
    /// </summary>
    IEnumerable<string> SupportedDataFormats { get; }
    
    /// <summary>
    /// Processes data from the specified memory address.
    /// </summary>
    /// <param name="inputMemoryAddress">The memory address of the input data.</param>
    /// <param name="processingOptions">The options for the processing operation.</param>
    /// <returns>The result of the processing operation.</returns>
    Task<ProcessingResult> ProcessAsync(string inputMemoryAddress, string processingOptions);
    
    /// <summary>
    /// Validates the processing options.
    /// </summary>
    /// <param name="processingOptions">The options for the processing operation.</param>
    /// <returns>True if the options are valid, false otherwise.</returns>
    bool ValidateOptions(string processingOptions);
    
    /// <summary>
    /// Gets the schema of the output data based on the input schema and processing options.
    /// </summary>
    /// <param name="inputSchema">The schema of the input data.</param>
    /// <param name="processingOptions">The options for the processing operation.</param>
    /// <returns>The schema of the output data.</returns>
    string GetOutputSchema(string inputSchema, string processingOptions);
}
