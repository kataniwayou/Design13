using FlowOrchestrator.Abstractions.Entities;

namespace FlowOrchestrator.Abstractions.Strategies;

/// <summary>
/// Interface for recovery strategies that handle error recovery in flows.
/// </summary>
public interface IRecoveryStrategy
{
    /// <summary>
    /// Gets the unique identifier for this recovery strategy.
    /// </summary>
    string Id { get; }
    
    /// <summary>
    /// Gets the name of this recovery strategy.
    /// </summary>
    string Name { get; }
    
    /// <summary>
    /// Gets the description of this recovery strategy.
    /// </summary>
    string Description { get; }
    
    /// <summary>
    /// Gets the strategy type.
    /// </summary>
    string StrategyType { get; }
    
    /// <summary>
    /// Determines whether this strategy can handle the specified error.
    /// </summary>
    /// <param name="error">The error to handle.</param>
    /// <param name="context">The context in which the error occurred.</param>
    /// <returns>True if this strategy can handle the error, false otherwise.</returns>
    bool CanHandle(Exception error, RecoveryContext context);
    
    /// <summary>
    /// Handles the specified error.
    /// </summary>
    /// <param name="error">The error to handle.</param>
    /// <param name="context">The context in which the error occurred.</param>
    /// <returns>The result of the recovery operation.</returns>
    Task<RecoveryResult> HandleAsync(Exception error, RecoveryContext context);
    
    /// <summary>
    /// Validates the configuration for this recovery strategy.
    /// </summary>
    /// <param name="configuration">The configuration to validate.</param>
    /// <returns>True if the configuration is valid, false otherwise.</returns>
    bool ValidateConfiguration(string configuration);
}
