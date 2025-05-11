using FlowOrchestrator.Common.Errors;
using FlowOrchestrator.Common.Recovery;
using FlowOrchestrator.Telemetry.OpenTelemetry;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Diagnostics.Metrics;

namespace FlowOrchestrator.Recovery;

/// <summary>
/// Collects metrics related to recovery operations.
/// </summary>
public class RecoveryMetricsCollector
{
    private readonly ILogger<RecoveryMetricsCollector> _logger;
    private readonly Meter _meter;
    private readonly Counter<long> _recoveryAttemptsCounter;
    private readonly Counter<long> _recoverySuccessCounter;
    private readonly Counter<long> _recoveryFailureCounter;
    private readonly Counter<long> _recoveryExceptionCounter;
    private readonly Histogram<double> _recoveryDurationHistogram;

    /// <summary>
    /// Initializes a new instance of the <see cref="RecoveryMetricsCollector"/> class.
    /// </summary>
    /// <param name="logger">The logger.</param>
    /// <param name="telemetryProvider">The telemetry provider.</param>
    public RecoveryMetricsCollector(
        ILogger<RecoveryMetricsCollector> logger,
        OpenTelemetryProvider telemetryProvider)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));

        if (telemetryProvider == null) throw new ArgumentNullException(nameof(telemetryProvider));

        _meter = new Meter("FlowOrchestrator.Recovery", "1.0.0");

        _recoveryAttemptsCounter = _meter.CreateCounter<long>("recovery.attempts", "count", "Number of recovery attempts");
        _recoverySuccessCounter = _meter.CreateCounter<long>("recovery.success", "count", "Number of successful recoveries");
        _recoveryFailureCounter = _meter.CreateCounter<long>("recovery.failure", "count", "Number of failed recoveries");
        _recoveryExceptionCounter = _meter.CreateCounter<long>("recovery.exceptions", "count", "Number of exceptions during recovery");
        _recoveryDurationHistogram = _meter.CreateHistogram<double>("recovery.duration", "ms", "Duration of recovery operations");
    }

    /// <summary>
    /// Records a recovery attempt.
    /// </summary>
    /// <param name="errorContext">The error context.</param>
    /// <param name="executionContext">The execution context.</param>
    public void RecordRecoveryAttempt(ErrorContext errorContext, object executionContext)
    {
        if (errorContext == null) throw new ArgumentNullException(nameof(errorContext));
        if (executionContext == null) throw new ArgumentNullException(nameof(executionContext));

        _logger.LogDebug("Recording recovery attempt for error {ErrorId}",
            errorContext.ErrorId);

        var executionId = executionContext.ToString() ?? "unknown";

        var tags = new KeyValuePair<string, object>[]
        {
            new("error_type", errorContext.ErrorType),
            new("component", errorContext.ComponentName),
            new("execution_id", executionId)
        };

        _recoveryAttemptsCounter.Add(1, tags);
    }

    /// <summary>
    /// Records the result of a recovery operation.
    /// </summary>
    /// <param name="result">The recovery result.</param>
    /// <param name="strategyName">The name of the recovery strategy used.</param>
    /// <param name="errorContext">The error context.</param>
    /// <param name="executionContext">The execution context.</param>
    public void RecordRecoveryResult(RecoveryResult result, string strategyName, ErrorContext errorContext, object executionContext)
    {
        if (result == null) throw new ArgumentNullException(nameof(result));
        if (string.IsNullOrEmpty(strategyName)) throw new ArgumentNullException(nameof(strategyName));
        if (errorContext == null) throw new ArgumentNullException(nameof(errorContext));
        if (executionContext == null) throw new ArgumentNullException(nameof(executionContext));

        var executionId = executionContext.ToString() ?? "unknown";

        var tags = new KeyValuePair<string, object>[]
        {
            new("error_type", errorContext.ErrorType),
            new("component", errorContext.ComponentName),
            new("execution_id", executionId),
            new("strategy", strategyName)
        };

        if (result.IsSuccessful)
        {
            _logger.LogDebug("Recording successful recovery for error {ErrorId} in execution {ExecutionId} using strategy {StrategyName}",
                errorContext.ErrorId, executionId, strategyName);

            _recoverySuccessCounter.Add(1, tags);
        }
        else
        {
            _logger.LogDebug("Recording failed recovery for error {ErrorId} in execution {ExecutionId} using strategy {StrategyName}: {Message}",
                errorContext.ErrorId, executionId, strategyName, result.Message);

            _recoveryFailureCounter.Add(1, tags);
        }

        if (result.Duration.HasValue)
        {
            _recoveryDurationHistogram.Record(result.Duration.Value.TotalMilliseconds, tags);
        }
    }

    /// <summary>
    /// Records an exception that occurred during recovery.
    /// </summary>
    /// <param name="exception">The exception that occurred.</param>
    /// <param name="errorContext">The error context.</param>
    /// <param name="executionContext">The execution context.</param>
    public void RecordRecoveryException(Exception exception, ErrorContext errorContext, object executionContext)
    {
        if (exception == null) throw new ArgumentNullException(nameof(exception));
        if (errorContext == null) throw new ArgumentNullException(nameof(errorContext));
        if (executionContext == null) throw new ArgumentNullException(nameof(executionContext));

        var executionId = executionContext.ToString() ?? "unknown";

        _logger.LogDebug(exception, "Recording recovery exception for error {ErrorId} in execution {ExecutionId}: {ExceptionType}",
            errorContext.ErrorId, executionId, exception.GetType().Name);

        var tags = new KeyValuePair<string, object>[]
        {
            new("error_type", errorContext.ErrorType),
            new("component", errorContext.ComponentName),
            new("execution_id", executionId),
            new("exception_type", exception.GetType().Name)
        };

        _recoveryExceptionCounter.Add(1, tags);
    }
}
