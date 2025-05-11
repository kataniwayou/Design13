namespace FlowOrchestrator.TransformationEngine;

/// <summary>
/// Defines the interface for transformation error handling.
/// </summary>
public interface ITransformationErrorHandler
{
    /// <summary>
    /// Determines the recovery strategy for a transformation error.
    /// </summary>
    /// <param name="error">The transformation error.</param>
    /// <param name="cancellationToken">A cancellation token to observe while waiting for the task to complete.</param>
    /// <returns>A task representing the asynchronous operation, containing the recovery strategy.</returns>
    Task<RecoveryStrategy?> DetermineRecoveryStrategyAsync(TransformationError error, CancellationToken cancellationToken = default);

    /// <summary>
    /// Executes a recovery strategy for a transformation error.
    /// </summary>
    /// <param name="error">The transformation error.</param>
    /// <param name="strategy">The recovery strategy to execute.</param>
    /// <param name="cancellationToken">A cancellation token to observe while waiting for the task to complete.</param>
    /// <returns>A task representing the asynchronous operation, containing the recovery result.</returns>
    Task<RecoveryResult> ExecuteRecoveryAsync(TransformationError error, RecoveryStrategy strategy, CancellationToken cancellationToken = default);

    /// <summary>
    /// Logs a transformation error.
    /// </summary>
    /// <param name="error">The transformation error.</param>
    /// <param name="cancellationToken">A cancellation token to observe while waiting for the task to complete.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    Task LogErrorAsync(TransformationError error, CancellationToken cancellationToken = default);

    /// <summary>
    /// Analyzes a transformation error.
    /// </summary>
    /// <param name="error">The transformation error.</param>
    /// <param name="cancellationToken">A cancellation token to observe while waiting for the task to complete.</param>
    /// <returns>A task representing the asynchronous operation, containing the error analysis result.</returns>
    Task<ErrorAnalysisResult> AnalyzeErrorAsync(TransformationError error, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets the error history for a rule.
    /// </summary>
    /// <param name="ruleId">The rule ID.</param>
    /// <param name="timeRange">The time range for the history.</param>
    /// <param name="cancellationToken">A cancellation token to observe while waiting for the task to complete.</param>
    /// <returns>A task representing the asynchronous operation, containing the error history.</returns>
    Task<ErrorHistory> GetErrorHistoryAsync(string ruleId, TimeRange timeRange, CancellationToken cancellationToken = default);

    /// <summary>
    /// Creates a fallback transformation result.
    /// </summary>
    /// <param name="error">The transformation error.</param>
    /// <param name="cancellationToken">A cancellation token to observe while waiting for the task to complete.</param>
    /// <returns>A task representing the asynchronous operation, containing the fallback transformation result.</returns>
    Task<ProcessorBase.TransformationResult> CreateFallbackResultAsync(TransformationError error, CancellationToken cancellationToken = default);
}
