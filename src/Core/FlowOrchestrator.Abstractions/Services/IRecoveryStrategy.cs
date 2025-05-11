using FlowOrchestrator.Common.Errors;
using FlowOrchestrator.Common.Recovery;

namespace FlowOrchestrator.Abstractions.Services;

/// <summary>
/// Defines the interface for a recovery strategy.
/// </summary>
public interface IRecoveryStrategy
{
    /// <summary>
    /// Gets the name of the recovery strategy.
    /// </summary>
    string Name { get; }

    /// <summary>
    /// Gets the description of the recovery strategy.
    /// </summary>
    string Description { get; }

    /// <summary>
    /// Gets the priority of the recovery strategy. Higher values indicate higher priority.
    /// </summary>
    int Priority { get; }

    /// <summary>
    /// Determines whether this recovery strategy is applicable for the given error context and correlated errors.
    /// </summary>
    /// <param name="errorContext">The error context.</param>
    /// <param name="correlatedErrors">The correlated errors.</param>
    /// <param name="cancellationToken">A cancellation token to observe while waiting for the task to complete.</param>
    /// <returns><c>true</c> if this recovery strategy is applicable; otherwise, <c>false</c>.</returns>
    Task<bool> IsApplicableAsync(ErrorContext errorContext, IEnumerable<CorrelatedError> correlatedErrors, CancellationToken cancellationToken);

    /// <summary>
    /// Applies the recovery strategy to the given error context and execution context.
    /// </summary>
    /// <param name="errorContext">The error context.</param>
    /// <param name="executionContext">The execution context.</param>
    /// <param name="cancellationToken">A cancellation token to observe while waiting for the task to complete.</param>
    /// <returns>A <see cref="RecoveryResult"/> indicating the outcome of the recovery attempt.</returns>
    Task<RecoveryResult> ApplyAsync(ErrorContext errorContext, object executionContext, CancellationToken cancellationToken);
}
