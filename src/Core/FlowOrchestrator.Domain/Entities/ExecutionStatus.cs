namespace FlowOrchestrator.Domain.Entities;

/// <summary>
/// Represents the status of a flow execution.
/// </summary>
public enum ExecutionStatus
{
    /// <summary>
    /// The execution has been created but not started.
    /// </summary>
    Created,
    
    /// <summary>
    /// The execution is scheduled to run in the future.
    /// </summary>
    Scheduled,
    
    /// <summary>
    /// The execution is waiting for a dependency.
    /// </summary>
    Waiting,
    
    /// <summary>
    /// The execution is currently running.
    /// </summary>
    Running,
    
    /// <summary>
    /// The execution is paused.
    /// </summary>
    Paused,
    
    /// <summary>
    /// The execution has been cancelled.
    /// </summary>
    Cancelled,
    
    /// <summary>
    /// The execution has failed.
    /// </summary>
    Failed,
    
    /// <summary>
    /// The execution has completed successfully.
    /// </summary>
    Completed
}
