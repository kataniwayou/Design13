using FlowOrchestrator.Abstractions.Entities;

namespace FlowOrchestrator.Domain.Entities;

/// <summary>
/// Represents the execution context for a step in a flow.
/// </summary>
public class StepExecutionContext : BaseEntity
{
    /// <summary>
    /// Gets or sets the branch execution context ID.
    /// </summary>
    public string BranchExecutionContextId { get; set; } = string.Empty;
    
    /// <summary>
    /// Gets or sets the step ID.
    /// </summary>
    public string StepId { get; set; } = string.Empty;
    
    /// <summary>
    /// Gets or sets the step type.
    /// </summary>
    public StepType StepType { get; set; }
    
    /// <summary>
    /// Gets or sets the component ID that implements this step.
    /// </summary>
    public string ComponentId { get; set; } = string.Empty;
    
    /// <summary>
    /// Gets or sets the execution start time.
    /// </summary>
    public DateTime StartTime { get; set; }
    
    /// <summary>
    /// Gets or sets the execution end time.
    /// </summary>
    public DateTime? EndTime { get; set; }
    
    /// <summary>
    /// Gets or sets the execution status.
    /// </summary>
    public ExecutionStatus Status { get; set; } = ExecutionStatus.Created;
    
    /// <summary>
    /// Gets or sets the error message if the execution failed.
    /// </summary>
    public string? ErrorMessage { get; set; }
    
    /// <summary>
    /// Gets or sets the number of retry attempts.
    /// </summary>
    public int RetryAttempts { get; set; }
    
    /// <summary>
    /// Gets or sets the input memory address.
    /// </summary>
    public string? InputMemoryAddress { get; set; }
    
    /// <summary>
    /// Gets or sets the output memory address.
    /// </summary>
    public string? OutputMemoryAddress { get; set; }
    
    /// <summary>
    /// Gets or sets the step execution metrics.
    /// </summary>
    public Dictionary<string, double> Metrics { get; set; } = new Dictionary<string, double>();
    
    /// <summary>
    /// Gets or sets the step execution logs.
    /// </summary>
    public List<string> Logs { get; set; } = new List<string>();
    
    /// <summary>
    /// Creates a new instance of the StepExecutionContext class.
    /// </summary>
    public StepExecutionContext()
    {
    }
    
    /// <summary>
    /// Creates a new instance of the StepExecutionContext class with the specified ID, name, and description.
    /// </summary>
    /// <param name="id">The unique identifier for the step execution context.</param>
    /// <param name="name">The name of the step execution context.</param>
    /// <param name="description">The description of the step execution context.</param>
    public StepExecutionContext(string id, string name, string description)
        : base(id, name, description)
    {
    }
    
    /// <summary>
    /// Creates a new instance of the StepExecutionContext class with the specified properties.
    /// </summary>
    /// <param name="id">The unique identifier for the step execution context.</param>
    /// <param name="name">The name of the step execution context.</param>
    /// <param name="description">The description of the step execution context.</param>
    /// <param name="branchExecutionContextId">The branch execution context ID.</param>
    /// <param name="stepId">The step ID.</param>
    /// <param name="stepType">The step type.</param>
    /// <param name="componentId">The component ID that implements this step.</param>
    /// <param name="startTime">The execution start time.</param>
    /// <param name="status">The execution status.</param>
    /// <param name="inputMemoryAddress">The input memory address.</param>
    public StepExecutionContext(
        string id,
        string name,
        string description,
        string branchExecutionContextId,
        string stepId,
        StepType stepType,
        string componentId,
        DateTime startTime,
        ExecutionStatus status,
        string? inputMemoryAddress)
        : base(id, name, description)
    {
        BranchExecutionContextId = branchExecutionContextId;
        StepId = stepId;
        StepType = stepType;
        ComponentId = componentId;
        StartTime = startTime;
        Status = status;
        InputMemoryAddress = inputMemoryAddress;
    }
    
    /// <summary>
    /// Updates the execution status.
    /// </summary>
    /// <param name="status">The new execution status.</param>
    /// <param name="errorMessage">The error message if the execution failed.</param>
    public void UpdateStatus(ExecutionStatus status, string? errorMessage = null)
    {
        Status = status;
        ErrorMessage = errorMessage;
        
        if (status == ExecutionStatus.Completed || status == ExecutionStatus.Failed || status == ExecutionStatus.Cancelled)
        {
            EndTime = DateTime.UtcNow;
        }
    }
    
    /// <summary>
    /// Increments the retry attempts counter.
    /// </summary>
    public void IncrementRetryAttempts()
    {
        RetryAttempts++;
    }
    
    /// <summary>
    /// Sets the output memory address.
    /// </summary>
    /// <param name="outputMemoryAddress">The output memory address.</param>
    public void SetOutputMemoryAddress(string outputMemoryAddress)
    {
        OutputMemoryAddress = outputMemoryAddress;
    }
    
    /// <summary>
    /// Adds a metric to the step execution context.
    /// </summary>
    /// <param name="key">The metric key.</param>
    /// <param name="value">The metric value.</param>
    public void AddMetric(string key, double value)
    {
        Metrics[key] = value;
    }
    
    /// <summary>
    /// Adds a log entry to the step execution context.
    /// </summary>
    /// <param name="logEntry">The log entry to add.</param>
    public void AddLogEntry(string logEntry)
    {
        Logs.Add(logEntry);
    }
}
