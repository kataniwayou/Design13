namespace FlowOrchestrator.Domain.Entities;

/// <summary>
/// Represents the execution context for a branch in a flow.
/// </summary>
public class BranchExecutionContext : BaseEntity
{
    /// <summary>
    /// Gets or sets the execution context ID.
    /// </summary>
    public string ExecutionContextId { get; set; } = string.Empty;
    
    /// <summary>
    /// Gets or sets the branch path.
    /// </summary>
    public string BranchPath { get; set; } = string.Empty;
    
    /// <summary>
    /// Gets or sets the parent branch path.
    /// </summary>
    public string? ParentBranchPath { get; set; }
    
    /// <summary>
    /// Gets or sets the parent step ID where this branch splits off.
    /// </summary>
    public string? ParentStepId { get; set; }
    
    /// <summary>
    /// Gets or sets the current step ID being executed.
    /// </summary>
    public string? CurrentStepId { get; set; }
    
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
    /// Gets or sets the list of step execution contexts.
    /// </summary>
    public IReadOnlyList<StepExecutionContext> StepExecutionContexts => _stepExecutionContexts.AsReadOnly();
    private readonly List<StepExecutionContext> _stepExecutionContexts = new();
    
    /// <summary>
    /// Gets or sets the memory address for the branch data.
    /// </summary>
    public string? MemoryAddress { get; set; }
    
    /// <summary>
    /// Gets or sets the branch execution metrics.
    /// </summary>
    public Dictionary<string, double> Metrics { get; set; } = new Dictionary<string, double>();
    
    /// <summary>
    /// Creates a new instance of the BranchExecutionContext class.
    /// </summary>
    public BranchExecutionContext()
    {
    }
    
    /// <summary>
    /// Creates a new instance of the BranchExecutionContext class with the specified ID, name, and description.
    /// </summary>
    /// <param name="id">The unique identifier for the branch execution context.</param>
    /// <param name="name">The name of the branch execution context.</param>
    /// <param name="description">The description of the branch execution context.</param>
    public BranchExecutionContext(string id, string name, string description)
        : base(id, name, description)
    {
    }
    
    /// <summary>
    /// Creates a new instance of the BranchExecutionContext class with the specified properties.
    /// </summary>
    /// <param name="id">The unique identifier for the branch execution context.</param>
    /// <param name="name">The name of the branch execution context.</param>
    /// <param name="description">The description of the branch execution context.</param>
    /// <param name="executionContextId">The execution context ID.</param>
    /// <param name="branchPath">The branch path.</param>
    /// <param name="parentBranchPath">The parent branch path.</param>
    /// <param name="parentStepId">The parent step ID where this branch splits off.</param>
    /// <param name="startTime">The execution start time.</param>
    /// <param name="status">The execution status.</param>
    /// <param name="memoryAddress">The memory address for the branch data.</param>
    public BranchExecutionContext(
        string id,
        string name,
        string description,
        string executionContextId,
        string branchPath,
        string? parentBranchPath,
        string? parentStepId,
        DateTime startTime,
        ExecutionStatus status,
        string? memoryAddress)
        : base(id, name, description)
    {
        ExecutionContextId = executionContextId;
        BranchPath = branchPath;
        ParentBranchPath = parentBranchPath;
        ParentStepId = parentStepId;
        StartTime = startTime;
        Status = status;
        MemoryAddress = memoryAddress;
    }
    
    /// <summary>
    /// Adds a step execution context to this branch execution context.
    /// </summary>
    /// <param name="stepExecutionContext">The step execution context to add.</param>
    public void AddStepExecutionContext(StepExecutionContext stepExecutionContext)
    {
        _stepExecutionContexts.Add(stepExecutionContext);
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
    /// Updates the current step ID.
    /// </summary>
    /// <param name="stepId">The new current step ID.</param>
    public void UpdateCurrentStep(string stepId)
    {
        CurrentStepId = stepId;
    }
    
    /// <summary>
    /// Adds a metric to the branch execution context.
    /// </summary>
    /// <param name="key">The metric key.</param>
    /// <param name="value">The metric value.</param>
    public void AddMetric(string key, double value)
    {
        Metrics[key] = value;
    }
}
