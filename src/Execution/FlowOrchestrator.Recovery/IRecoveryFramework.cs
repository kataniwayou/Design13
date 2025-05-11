using FlowOrchestrator.Common.Errors;
using FlowOrchestrator.Common.Recovery;

namespace FlowOrchestrator.Recovery;

/// <summary>
/// Defines the interface for the recovery framework.
/// </summary>
public interface IRecoveryFramework
{
    /// <summary>
    /// Attempts to recover from an error in the flow execution.
    /// </summary>
    /// <param name="errorContext">The error context containing information about the error.</param>
    /// <param name="executionContext">The execution context in which the error occurred.</param>
    /// <param name="cancellationToken">A cancellation token to observe while waiting for the task to complete.</param>
    /// <returns>A <see cref="RecoveryResult"/> indicating the outcome of the recovery attempt.</returns>
    Task<RecoveryResult> RecoverAsync(ErrorContext errorContext, object executionContext, CancellationToken cancellationToken = default);
}
