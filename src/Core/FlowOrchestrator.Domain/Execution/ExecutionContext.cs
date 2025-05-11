namespace FlowOrchestrator.Domain.Execution;

/// <summary>
/// Provides context information for a flow execution.
/// </summary>
public class ExecutionContext
{
    /// <summary>
    /// Gets or sets the unique identifier for this execution.
    /// </summary>
    public string ExecutionId { get; set; } = Guid.NewGuid().ToString();
    
    /// <summary>
    /// Gets or sets the flow ID.
    /// </summary>
    public string FlowId { get; set; } = string.Empty;
    
    /// <summary>
    /// Gets or sets the flow version.
    /// </summary>
    public string FlowVersion { get; set; } = string.Empty;
    
    /// <summary>
    /// Gets or sets the flow name.
    /// </summary>
    public string FlowName { get; set; } = string.Empty;
    
    /// <summary>
    /// Gets or sets the execution start time.
    /// </summary>
    public DateTime StartTime { get; set; } = DateTime.UtcNow;
    
    /// <summary>
    /// Gets or sets the execution end time.
    /// </summary>
    public DateTime? EndTime { get; set; }
    
    /// <summary>
    /// Gets or sets the execution status.
    /// </summary>
    public ExecutionStatus Status { get; set; } = ExecutionStatus.Created;
    
    /// <summary>
    /// Gets or sets the parent execution ID, if any.
    /// </summary>
    public string? ParentExecutionId { get; set; }
    
    /// <summary>
    /// Gets or sets the branch ID, if any.
    /// </summary>
    public string? BranchId { get; set; }
    
    /// <summary>
    /// Gets or sets the memory address for this execution.
    /// </summary>
    public string MemoryAddress { get; set; } = string.Empty;
    
    /// <summary>
    /// Gets or sets the execution parameters.
    /// </summary>
    public Dictionary<string, object> Parameters { get; set; } = new Dictionary<string, object>();
    
    /// <summary>
    /// Gets or sets the execution metadata.
    /// </summary>
    public Dictionary<string, object> Metadata { get; set; } = new Dictionary<string, object>();
    
    /// <summary>
    /// Gets or sets the execution tags.
    /// </summary>
    public List<string> Tags { get; set; } = new List<string>();
    
    /// <summary>
    /// Gets or sets the execution priority.
    /// </summary>
    public int Priority { get; set; } = 0;
    
    /// <summary>
    /// Gets or sets the execution timeout in seconds.
    /// </summary>
    public int TimeoutSeconds { get; set; } = 3600; // Default: 1 hour
    
    /// <summary>
    /// Gets or sets the maximum number of retry attempts.
    /// </summary>
    public int MaxRetryAttempts { get; set; } = 3;
    
    /// <summary>
    /// Gets or sets the current retry attempt.
    /// </summary>
    public int CurrentRetryAttempt { get; set; } = 0;
    
    /// <summary>
    /// Gets or sets the user ID who initiated the execution.
    /// </summary>
    public string? UserId { get; set; }
    
    /// <summary>
    /// Gets or sets the tenant ID for the execution.
    /// </summary>
    public string? TenantId { get; set; }
    
    /// <summary>
    /// Gets or sets the correlation ID for the execution.
    /// </summary>
    public string? CorrelationId { get; set; }
    
    /// <summary>
    /// Creates a child execution context from this execution context.
    /// </summary>
    /// <param name="branchId">The branch ID for the child execution.</param>
    /// <returns>A new execution context with this execution as its parent.</returns>
    public ExecutionContext CreateChildContext(string branchId)
    {
        return new ExecutionContext
        {
            FlowId = this.FlowId,
            FlowVersion = this.FlowVersion,
            FlowName = this.FlowName,
            ParentExecutionId = this.ExecutionId,
            BranchId = branchId,
            UserId = this.UserId,
            TenantId = this.TenantId,
            CorrelationId = this.CorrelationId,
            Priority = this.Priority,
            TimeoutSeconds = this.TimeoutSeconds,
            MaxRetryAttempts = this.MaxRetryAttempts,
            Tags = new List<string>(this.Tags)
        };
    }
}

/// <summary>
/// Represents the status of a flow execution.
/// </summary>
public enum ExecutionStatus
{
    /// <summary>
    /// The execution has been created but not yet started.
    /// </summary>
    Created,
    
    /// <summary>
    /// The execution is currently running.
    /// </summary>
    Running,
    
    /// <summary>
    /// The execution has been paused.
    /// </summary>
    Paused,
    
    /// <summary>
    /// The execution has been completed successfully.
    /// </summary>
    Completed,
    
    /// <summary>
    /// The execution has failed.
    /// </summary>
    Failed,
    
    /// <summary>
    /// The execution has been cancelled.
    /// </summary>
    Cancelled,
    
    /// <summary>
    /// The execution has timed out.
    /// </summary>
    TimedOut
}
