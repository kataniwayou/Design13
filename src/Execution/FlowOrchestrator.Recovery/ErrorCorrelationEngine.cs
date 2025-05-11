using FlowOrchestrator.Common.Errors;
using FlowOrchestrator.Common.Recovery;
using CorrelatedError = FlowOrchestrator.Common.Recovery.CorrelatedError;
using Microsoft.Extensions.Logging;

namespace FlowOrchestrator.Recovery;

/// <summary>
/// Correlates errors to identify patterns and common causes.
/// </summary>
public class ErrorCorrelationEngine
{
    private readonly ILogger<ErrorCorrelationEngine> _logger;
    private readonly Dictionary<string, List<ErrorContext>> _errorHistory = new();
    private readonly int _historyLimit;

    /// <summary>
    /// Initializes a new instance of the <see cref="ErrorCorrelationEngine"/> class.
    /// </summary>
    /// <param name="logger">The logger.</param>
    /// <param name="historyLimit">The maximum number of errors to keep in history per execution.</param>
    public ErrorCorrelationEngine(
        ILogger<ErrorCorrelationEngine> logger,
        int historyLimit = 100)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _historyLimit = historyLimit > 0 ? historyLimit : throw new ArgumentOutOfRangeException(nameof(historyLimit));
    }

    /// <summary>
    /// Correlates errors for the given error context and execution context.
    /// </summary>
    /// <param name="errorContext">The error context.</param>
    /// <param name="executionId">The execution ID.</param>
    /// <param name="cancellationToken">A cancellation token to observe while waiting for the task to complete.</param>
    /// <returns>A collection of correlated errors.</returns>
    public Task<IEnumerable<Common.Recovery.CorrelatedError>> CorrelateErrorsAsync(
        ErrorContext errorContext,
        string executionId,
        CancellationToken cancellationToken)
    {
        if (errorContext == null) throw new ArgumentNullException(nameof(errorContext));
        if (string.IsNullOrEmpty(executionId)) throw new ArgumentNullException(nameof(executionId));

        // Add the current error to history
        AddErrorToHistory(errorContext, executionId);

        // Get the error history for this execution
        var history = GetErrorHistory(executionId);

        _logger.LogInformation("Correlating error {ErrorId} with {HistoryCount} historical errors for execution {ExecutionId}",
            errorContext.ErrorId, history.Count, executionId);

        // Perform correlation analysis
        var correlatedErrors = PerformCorrelationAnalysis(errorContext, history);

        _logger.LogInformation("Found {CorrelatedErrorCount} correlated errors for error {ErrorId} in execution {ExecutionId}",
            correlatedErrors.Count(), errorContext.ErrorId, executionId);

        return Task.FromResult(correlatedErrors);
    }

    /// <summary>
    /// Adds an error to the history for the specified execution.
    /// </summary>
    /// <param name="errorContext">The error context to add.</param>
    /// <param name="executionId">The execution ID.</param>
    private void AddErrorToHistory(ErrorContext errorContext, string executionId)
    {
        lock (_errorHistory)
        {
            if (!_errorHistory.TryGetValue(executionId, out var errors))
            {
                errors = new List<ErrorContext>();
                _errorHistory[executionId] = errors;
            }

            errors.Add(errorContext);

            // Trim history if it exceeds the limit
            if (errors.Count > _historyLimit)
            {
                errors.RemoveAt(0);
            }
        }
    }

    /// <summary>
    /// Gets the error history for the specified execution.
    /// </summary>
    /// <param name="executionId">The execution ID.</param>
    /// <returns>The error history for the specified execution.</returns>
    private List<ErrorContext> GetErrorHistory(string executionId)
    {
        lock (_errorHistory)
        {
            if (!_errorHistory.TryGetValue(executionId, out var errors))
            {
                return new List<ErrorContext>();
            }

            return new List<ErrorContext>(errors);
        }
    }

    /// <summary>
    /// Performs correlation analysis on the given error context and history.
    /// </summary>
    /// <param name="currentError">The current error context.</param>
    /// <param name="history">The error history.</param>
    /// <returns>A collection of correlated errors.</returns>
    private IEnumerable<Common.Recovery.CorrelatedError> PerformCorrelationAnalysis(ErrorContext currentError, List<ErrorContext> history)
    {
        var correlatedErrors = new List<Common.Recovery.CorrelatedError>();

        // Skip the current error in the history
        var historicalErrors = history.Where(e => e.ErrorId != currentError.ErrorId).ToList();

        // Group errors by error code
        var errorsByCode = historicalErrors
            .Where(e => e.ErrorCode == currentError.ErrorCode)
            .ToList();

        if (errorsByCode.Any())
        {
            correlatedErrors.Add(new Common.Recovery.CorrelatedError
            {
                CorrelationType = Common.Recovery.CorrelationType.SameErrorCode,
                CorrelationStrength = 0.8f,
                RelatedErrors = errorsByCode
            });
        }

        // Group errors by component
        var errorsByComponent = historicalErrors
            .Where(e => e.ComponentName == currentError.ComponentName)
            .ToList();

        if (errorsByComponent.Any())
        {
            correlatedErrors.Add(new Common.Recovery.CorrelatedError
            {
                CorrelationType = Common.Recovery.CorrelationType.SameComponent,
                CorrelationStrength = 0.6f,
                RelatedErrors = errorsByComponent
            });
        }

        // Group errors by error type
        var errorsByType = historicalErrors
            .Where(e => e.ErrorType == currentError.ErrorType)
            .ToList();

        if (errorsByType.Any())
        {
            correlatedErrors.Add(new Common.Recovery.CorrelatedError
            {
                CorrelationType = Common.Recovery.CorrelationType.SameErrorType,
                CorrelationStrength = 0.5f,
                RelatedErrors = errorsByType
            });
        }

        // Group errors by time proximity (errors that occurred within a short time window)
        var recentErrors = historicalErrors
            .Where(e => (currentError.Timestamp - e.Timestamp).TotalMinutes <= 5)
            .ToList();

        if (recentErrors.Any())
        {
            correlatedErrors.Add(new Common.Recovery.CorrelatedError
            {
                CorrelationType = Common.Recovery.CorrelationType.TimeProximity,
                CorrelationStrength = 0.4f,
                RelatedErrors = recentErrors
            });
        }

        return correlatedErrors;
    }

    /// <summary>
    /// Clears the error history for the specified execution.
    /// </summary>
    /// <param name="executionId">The execution ID.</param>
    public void ClearErrorHistory(string executionId)
    {
        if (string.IsNullOrEmpty(executionId)) throw new ArgumentNullException(nameof(executionId));

        lock (_errorHistory)
        {
            if (_errorHistory.Remove(executionId))
            {
                _logger.LogInformation("Cleared error history for execution {ExecutionId}", executionId);
            }
        }
    }
}

/// <summary>
/// Represents a group of correlated errors.
/// </summary>
public class CorrelatedError
{
    /// <summary>
    /// Gets or sets the type of correlation.
    /// </summary>
    public CorrelationType CorrelationType { get; set; }

    /// <summary>
    /// Gets or sets the strength of the correlation (0.0 to 1.0).
    /// </summary>
    public float CorrelationStrength { get; set; }

    /// <summary>
    /// Gets or sets the related errors.
    /// </summary>
    public IEnumerable<ErrorContext> RelatedErrors { get; set; } = Enumerable.Empty<ErrorContext>();
}

/// <summary>
/// Represents the type of correlation between errors.
/// </summary>
public enum CorrelationType
{
    /// <summary>
    /// Errors with the same error code.
    /// </summary>
    SameErrorCode,

    /// <summary>
    /// Errors from the same component.
    /// </summary>
    SameComponent,

    /// <summary>
    /// Errors of the same type.
    /// </summary>
    SameErrorType,

    /// <summary>
    /// Errors that occurred within a short time window.
    /// </summary>
    TimeProximity
}
