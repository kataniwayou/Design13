using FlowOrchestrator.Abstractions.Entities;

namespace FlowOrchestrator.Domain.Entities;

/// <summary>
/// Represents a step in a processing chain.
/// </summary>
public class ProcessingStepEntity : BaseEntity
{
    /// <summary>
    /// Gets or sets the order of this step in the processing chain.
    /// </summary>
    public int StepOrder { get; set; }
    
    /// <summary>
    /// Gets or sets the processor component ID that implements this step.
    /// </summary>
    public string ProcessorId { get; set; } = string.Empty;
    
    /// <summary>
    /// Gets or sets the configuration for this step.
    /// </summary>
    public string Configuration { get; set; } = string.Empty;
    
    /// <summary>
    /// Gets or sets the input schema for this step.
    /// </summary>
    public string InputSchema { get; set; } = string.Empty;
    
    /// <summary>
    /// Gets or sets the output schema for this step.
    /// </summary>
    public string OutputSchema { get; set; } = string.Empty;
    
    /// <summary>
    /// Gets or sets whether this step is enabled.
    /// </summary>
    public bool IsEnabled { get; set; } = true;
    
    /// <summary>
    /// Gets or sets the error handling strategy for this step.
    /// </summary>
    public ErrorHandlingStrategy ErrorHandlingStrategy { get; set; } = ErrorHandlingStrategy.FailImmediately;
    
    /// <summary>
    /// Creates a new instance of the ProcessingStepEntity class.
    /// </summary>
    public ProcessingStepEntity()
    {
    }
    
    /// <summary>
    /// Creates a new instance of the ProcessingStepEntity class with the specified ID, name, and description.
    /// </summary>
    /// <param name="id">The unique identifier for the processing step.</param>
    /// <param name="name">The name of the processing step.</param>
    /// <param name="description">The description of the processing step.</param>
    public ProcessingStepEntity(string id, string name, string description)
        : base(id, name, description)
    {
    }
    
    /// <summary>
    /// Creates a new instance of the ProcessingStepEntity class with the specified properties.
    /// </summary>
    /// <param name="id">The unique identifier for the processing step.</param>
    /// <param name="name">The name of the processing step.</param>
    /// <param name="description">The description of the processing step.</param>
    /// <param name="stepOrder">The order of this step in the processing chain.</param>
    /// <param name="processorId">The processor component ID that implements this step.</param>
    /// <param name="configuration">The configuration for this step.</param>
    /// <param name="inputSchema">The input schema for this step.</param>
    /// <param name="outputSchema">The output schema for this step.</param>
    /// <param name="isEnabled">Whether this step is enabled.</param>
    /// <param name="errorHandlingStrategy">The error handling strategy for this step.</param>
    public ProcessingStepEntity(
        string id,
        string name,
        string description,
        int stepOrder,
        string processorId,
        string configuration,
        string inputSchema,
        string outputSchema,
        bool isEnabled,
        ErrorHandlingStrategy errorHandlingStrategy)
        : base(id, name, description)
    {
        StepOrder = stepOrder;
        ProcessorId = processorId;
        Configuration = configuration;
        InputSchema = inputSchema;
        OutputSchema = outputSchema;
        IsEnabled = isEnabled;
        ErrorHandlingStrategy = errorHandlingStrategy;
    }
}
