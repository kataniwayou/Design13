using FlowOrchestrator.Abstractions.Entities;

namespace FlowOrchestrator.Abstractions.Strategies;

/// <summary>
/// Represents the context in which an error occurred and recovery is attempted.
/// </summary>
public class RecoveryContext
{
    /// <summary>
    /// Gets or sets the flow in which the error occurred.
    /// </summary>
    public IFlow Flow { get; set; } = null!;
    
    /// <summary>
    /// Gets or sets the step in which the error occurred.
    /// </summary>
    public IFlowStep? Step { get; set; }
    
    /// <summary>
    /// Gets or sets the branch in which the error occurred.
    /// </summary>
    public IFlowBranch? Branch { get; set; }
    
    /// <summary>
    /// Gets or sets the error handling configuration for the flow.
    /// </summary>
    public IErrorHandlingConfig ErrorHandlingConfig { get; set; } = null!;
    
    /// <summary>
    /// Gets or sets the number of retry attempts that have been made.
    /// </summary>
    public int RetryAttempts { get; set; }
    
    /// <summary>
    /// Gets or sets the timestamp when the error occurred.
    /// </summary>
    public DateTime ErrorTimestamp { get; set; }
    
    /// <summary>
    /// Gets or sets the memory address of the data being processed when the error occurred.
    /// </summary>
    public string? DataMemoryAddress { get; set; }
    
    /// <summary>
    /// Gets or sets the execution context in which the error occurred.
    /// </summary>
    public string? ExecutionContext { get; set; }
    
    /// <summary>
    /// Gets or sets the metadata associated with the error.
    /// </summary>
    public Dictionary<string, string> Metadata { get; set; } = new Dictionary<string, string>();
}
