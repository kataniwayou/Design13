namespace FlowOrchestrator.Abstractions.Strategies;

/// <summary>
/// Represents the action taken during a recovery operation.
/// </summary>
public enum RecoveryAction
{
    /// <summary>
    /// No action was taken.
    /// </summary>
    None,
    
    /// <summary>
    /// The operation was retried.
    /// </summary>
    Retry,
    
    /// <summary>
    /// The step was skipped.
    /// </summary>
    Skip,
    
    /// <summary>
    /// The branch was aborted.
    /// </summary>
    AbortBranch,
    
    /// <summary>
    /// The flow was aborted.
    /// </summary>
    AbortFlow,
    
    /// <summary>
    /// Compensating actions were executed.
    /// </summary>
    Compensate,
    
    /// <summary>
    /// The flow was redirected to a different step.
    /// </summary>
    Redirect,
    
    /// <summary>
    /// The flow was paused for manual intervention.
    /// </summary>
    Pause,
    
    /// <summary>
    /// The data was repaired.
    /// </summary>
    RepairData,
    
    /// <summary>
    /// An alternative path was taken.
    /// </summary>
    AlternativePath
}
