using FlowOrchestrator.Common.Configuration;

namespace FlowOrchestrator.ProcessorBase;

/// <summary>
/// Defines the interface for all processors in the system.
/// </summary>
public interface IProcessor : IDisposable
{
    /// <summary>
    /// Gets the unique identifier for this processor.
    /// </summary>
    string ProcessorId { get; }
    
    /// <summary>
    /// Gets the name of this processor.
    /// </summary>
    string Name { get; }
    
    /// <summary>
    /// Gets the description of this processor.
    /// </summary>
    string Description { get; }
    
    /// <summary>
    /// Gets the type of this processor.
    /// </summary>
    string ProcessorType { get; }
    
    /// <summary>
    /// Gets the version of this processor.
    /// </summary>
    string Version { get; }
    
    /// <summary>
    /// Gets the status of this processor.
    /// </summary>
    ProcessorStatus Status { get; }
    
    /// <summary>
    /// Gets the configuration for this processor.
    /// </summary>
    ProcessorConfiguration Configuration { get; }
    
    /// <summary>
    /// Initializes the processor with the specified configuration.
    /// </summary>
    /// <param name="configuration">The configuration to use.</param>
    /// <param name="cancellationToken">A cancellation token to observe while waiting for the task to complete.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    Task InitializeAsync(ProcessorConfiguration configuration, CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Processes data using the specified processing context.
    /// </summary>
    /// <param name="processingContext">The processing context.</param>
    /// <param name="cancellationToken">A cancellation token to observe while waiting for the task to complete.</param>
    /// <returns>A task representing the asynchronous operation, containing the processing result.</returns>
    Task<ProcessingResult> ProcessAsync(ProcessingContext processingContext, CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Gets the capabilities of this processor.
    /// </summary>
    /// <returns>The capabilities of this processor.</returns>
    ProcessorCapabilities GetCapabilities();
}
