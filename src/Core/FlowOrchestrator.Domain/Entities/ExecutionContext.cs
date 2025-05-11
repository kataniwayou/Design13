using FlowOrchestrator.Abstractions.Entities;

namespace FlowOrchestrator.Domain.Entities;

/// <summary>
/// Represents the execution context for a flow.
/// </summary>
public class ExecutionContext : BaseEntity
{
    /// <summary>
    /// Gets or sets the flow ID.
    /// </summary>
    public string FlowId { get; set; } = string.Empty;
    
    /// <summary>
    /// Gets or sets the flow version.
    /// </summary>
    public string FlowVersion { get; set; } = string.Empty;
    
    /// <summary>
    /// Gets or sets the execution ID.
    /// </summary>
    public string ExecutionId { get; set; } = string.Empty;
    
    /// <summary>
    /// Gets or sets the parent execution ID (if this is a sub-flow execution).
    /// </summary>
    public string? ParentExecutionId { get; set; }
    
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
    /// Gets or sets the list of branch execution contexts.
    /// </summary>
    public IReadOnlyList<BranchExecutionContext> BranchExecutionContexts => _branchExecutionContexts.AsReadOnly();
    private readonly List<BranchExecutionContext> _branchExecutionContexts = new();
    
    /// <summary>
    /// Gets or sets the execution parameters.
    /// </summary>
    public Dictionary<string, string> Parameters { get; set; } = new Dictionary<string, string>();
    
    /// <summary>
    /// Gets or sets the execution metrics.
    /// </summary>
    public Dictionary<string, double> Metrics { get; set; } = new Dictionary<string, double>();
    
    /// <summary>
    /// Gets or sets the execution tags.
    /// </summary>
    public List<string> Tags { get; set; } = new List<string>();
    
    /// <summary>
    /// Gets or sets the user ID who initiated the execution.
    /// </summary>
    public string? UserId { get; set; }
    
    /// <summary>
    /// Gets or sets the correlation ID for distributed tracing.
    /// </summary>
    public string? CorrelationId { get; set; }
    
    /// <summary>
    /// Creates a new instance of the ExecutionContext class.
    /// </summary>
    public ExecutionContext()
    {
    }
    
    /// <summary>
    /// Creates a new instance of the ExecutionContext class with the specified ID, name, and description.
    /// </summary>
    /// <param name="id">The unique identifier for the execution context.</param>
    /// <param name="name">The name of the execution context.</param>
    /// <param name="description">The description of the execution context.</param>
    public ExecutionContext(string id, string name, string description)
        : base(id, name, description)
    {
    }
    
    /// <summary>
    /// Creates a new instance of the ExecutionContext class with the specified properties.
    /// </summary>
    /// <param name="id">The unique identifier for the execution context.</param>
    /// <param name="name">The name of the execution context.</param>
    /// <param name="description">The description of the execution context.</param>
    /// <param name="flowId">The flow ID.</param>
    /// <param name="flowVersion">The flow version.</param>
    /// <param name="executionId">The execution ID.</param>
    /// <param name="parentExecutionId">The parent execution ID (if this is a sub-flow execution).</param>
    /// <param name="startTime">The execution start time.</param>
    /// <param name="status">The execution status.</param>
    /// <param name="userId">The user ID who initiated the execution.</param>
    /// <param name="correlationId">The correlation ID for distributed tracing.</param>
    public ExecutionContext(
        string id,
        string name,
        string description,
        string flowId,
        string flowVersion,
        string executionId,
        string? parentExecutionId,
        DateTime startTime,
        ExecutionStatus status,
        string? userId,
        string? correlationId)
        : base(id, name, description)
    {
        FlowId = flowId;
        FlowVersion = flowVersion;
        ExecutionId = executionId;
        ParentExecutionId = parentExecutionId;
        StartTime = startTime;
        Status = status;
        UserId = userId;
        CorrelationId = correlationId;
    }
    
    /// <summary>
    /// Adds a branch execution context to this execution context.
    /// </summary>
    /// <param name="branchExecutionContext">The branch execution context to add.</param>
    public void AddBranchExecutionContext(BranchExecutionContext branchExecutionContext)
    {
        _branchExecutionContexts.Add(branchExecutionContext);
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
    /// Adds a parameter to the execution context.
    /// </summary>
    /// <param name="key">The parameter key.</param>
    /// <param name="value">The parameter value.</param>
    public void AddParameter(string key, string value)
    {
        Parameters[key] = value;
    }
    
    /// <summary>
    /// Adds a metric to the execution context.
    /// </summary>
    /// <param name="key">The metric key.</param>
    /// <param name="value">The metric value.</param>
    public void AddMetric(string key, double value)
    {
        Metrics[key] = value;
    }
    
    /// <summary>
    /// Adds a tag to the execution context.
    /// </summary>
    /// <param name="tag">The tag to add.</param>
    public void AddTag(string tag)
    {
        if (!Tags.Contains(tag))
        {
            Tags.Add(tag);
        }
    }
}
