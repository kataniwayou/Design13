using System.Collections.Generic;

namespace FlowOrchestrator.Abstractions.Entities;

/// <summary>
/// Interface for flow-level error handling configuration.
/// </summary>
public interface IErrorHandlingConfig
{
    /// <summary>
    /// Gets the default error handling strategy to use for steps that don't specify one.
    /// </summary>
    ErrorHandlingStrategy DefaultStrategy { get; }
    
    /// <summary>
    /// Gets the maximum number of retry attempts for retryable errors.
    /// </summary>
    int MaxRetryAttempts { get; }
    
    /// <summary>
    /// Gets the retry delay in milliseconds.
    /// </summary>
    int RetryDelayMs { get; }
    
    /// <summary>
    /// Gets whether to use exponential backoff for retries.
    /// </summary>
    bool UseExponentialBackoff { get; }
    
    /// <summary>
    /// Gets the step-specific error handling strategy overrides.
    /// </summary>
    IReadOnlyDictionary<string, ErrorHandlingStrategy> StepErrorHandlingStrategies { get; }
    
    /// <summary>
    /// Gets the compensating actions to execute on failure.
    /// </summary>
    IReadOnlyList<ICompensatingAction> CompensatingActions { get; }
}
