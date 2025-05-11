namespace FlowOrchestrator.Abstractions.Entities;

/// <summary>
/// Represents the strategy to use when handling errors.
/// </summary>
public enum ErrorHandlingStrategy
{
    /// <summary>
    /// Fail the entire flow immediately.
    /// </summary>
    FailImmediately,
    
    /// <summary>
    /// Retry the step a specified number of times before failing.
    /// </summary>
    Retry,
    
    /// <summary>
    /// Skip the step and continue with the next step.
    /// </summary>
    SkipAndContinue,
    
    /// <summary>
    /// Execute a compensating action and then fail.
    /// </summary>
    CompensateAndFail,
    
    /// <summary>
    /// Execute a compensating action and then continue.
    /// </summary>
    CompensateAndContinue
}
