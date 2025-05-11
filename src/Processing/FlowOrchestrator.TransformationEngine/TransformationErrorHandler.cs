using Microsoft.Extensions.Logging;

namespace FlowOrchestrator.TransformationEngine;

/// <summary>
/// Handles transformation errors.
/// </summary>
public class TransformationErrorHandler : ITransformationErrorHandler
{
    private readonly ILogger<TransformationErrorHandler> _logger;
    private readonly Dictionary<string, List<ErrorRecord>> _errorHistory = new Dictionary<string, List<ErrorRecord>>();

    /// <summary>
    /// Initializes a new instance of the <see cref="TransformationErrorHandler"/> class.
    /// </summary>
    /// <param name="logger">The logger.</param>
    public TransformationErrorHandler(ILogger<TransformationErrorHandler> logger)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    /// <inheritdoc />
    public async Task<RecoveryStrategy?> DetermineRecoveryStrategyAsync(TransformationError error, CancellationToken cancellationToken = default)
    {
        if (error == null) throw new ArgumentNullException(nameof(error));

        _logger.LogInformation("Determining recovery strategy for error {ErrorId}", error.ErrorId);

        // This is a simplified implementation. In a real system, this would analyze the error
        // and determine the best recovery strategy.

        // For demonstration purposes, we'll just create a sample recovery strategy based on the error
        RecoveryStrategy? strategy = null;

        if (error.Exception is ArgumentException)
        {
            // For argument exceptions, use a fallback value
            strategy = new RecoveryStrategy
            {
                StrategyName = "FallbackValue",
                StrategyDescription = "Use a fallback value for the transformation",
                StrategyType = RecoveryStrategyType.FallbackValue,
                FallbackValue = new { Message = "Fallback value due to argument exception" }
            };
        }
        else if (error.Exception is InvalidOperationException)
        {
            // For invalid operation exceptions, use a retry strategy
            strategy = new RecoveryStrategy
            {
                StrategyName = "Retry",
                StrategyDescription = "Retry the transformation with exponential backoff",
                StrategyType = RecoveryStrategyType.Retry,
                MaxRetryAttempts = 3,
                RetryDelayMs = 1000,
                UseExponentialBackoff = true,
                ExponentialBackoffFactor = 2.0
            };
        }
        else if (error.Exception is TimeoutException)
        {
            // For timeout exceptions, use a fallback rule
            strategy = new RecoveryStrategy
            {
                StrategyName = "FallbackRule",
                StrategyDescription = "Use a simpler fallback rule for the transformation",
                StrategyType = RecoveryStrategyType.FallbackRule,
                FallbackRule = new TransformationRule
                {
                    RuleId = Guid.NewGuid().ToString(),
                    Name = "Fallback Rule",
                    Description = "A simpler fallback rule for timeout recovery",
                    RuleType = "Fallback",
                    RuleDefinition = "{ \"type\": \"fallback\" }",
                    InputDataType = error.Rule?.InputDataType ?? "unknown",
                    OutputDataType = error.Rule?.OutputDataType ?? "unknown"
                }
            };
        }
        else
        {
            // For other exceptions, skip the transformation
            strategy = new RecoveryStrategy
            {
                StrategyName = "Skip",
                StrategyDescription = "Skip the transformation and continue",
                StrategyType = RecoveryStrategyType.Skip
            };
        }

        await Task.Delay(50, cancellationToken); // Simulate strategy determination time

        return strategy;
    }

    /// <inheritdoc />
    public async Task<RecoveryResult> ExecuteRecoveryAsync(TransformationError error, RecoveryStrategy strategy, CancellationToken cancellationToken = default)
    {
        if (error == null) throw new ArgumentNullException(nameof(error));
        if (strategy == null) throw new ArgumentNullException(nameof(strategy));

        _logger.LogInformation("Executing recovery strategy {StrategyName} for error {ErrorId}", strategy.StrategyName, error.ErrorId);

        // This is a simplified implementation. In a real system, this would execute the recovery strategy
        // and return the result.

        // For demonstration purposes, we'll just create a sample recovery result based on the strategy
        RecoveryResult result;

        switch (strategy.StrategyType)
        {
            case RecoveryStrategyType.Retry:
                result = await ExecuteRetryStrategyAsync(error, strategy, cancellationToken);
                break;

            case RecoveryStrategyType.FallbackValue:
                result = ExecuteFallbackValueStrategy(error, strategy);
                break;

            case RecoveryStrategyType.FallbackRule:
                result = await ExecuteFallbackRuleStrategyAsync(error, strategy, cancellationToken);
                break;

            case RecoveryStrategyType.Skip:
                result = ExecuteSkipStrategy(error, strategy);
                break;

            default:
                result = RecoveryResult.Failure("Unsupported recovery strategy type", error, strategy);
                break;
        }

        return result;
    }

    /// <inheritdoc />
    public async Task LogErrorAsync(TransformationError error, CancellationToken cancellationToken = default)
    {
        if (error == null) throw new ArgumentNullException(nameof(error));

        _logger.LogError(error.Exception, "Transformation error: {ErrorMessage}", error.ErrorMessage);

        // This is a simplified implementation. In a real system, this would log the error
        // to a database or other storage.

        // For demonstration purposes, we'll just add the error to our in-memory history
        var ruleId = error.Rule?.RuleId ?? "unknown";

        if (!_errorHistory.TryGetValue(ruleId, out var errors))
        {
            errors = new List<ErrorRecord>();
            _errorHistory[ruleId] = errors;
        }

        var errorRecord = new ErrorRecord
        {
            ErrorId = error.ErrorId,
            RuleId = ruleId,
            ErrorCode = error.ErrorCode,
            ErrorMessage = error.ErrorMessage,
            Severity = error.Severity,
            ErrorTimestamp = error.ErrorTime,
            StackTrace = error.Exception?.StackTrace,
            InputDataType = error.Input?.DataType
        };

        errors.Add(errorRecord);

        await Task.Delay(10, cancellationToken); // Simulate logging time
    }

    /// <inheritdoc />
    public async Task<ErrorAnalysisResult> AnalyzeErrorAsync(TransformationError error, CancellationToken cancellationToken = default)
    {
        if (error == null) throw new ArgumentNullException(nameof(error));

        _logger.LogInformation("Analyzing error {ErrorId}", error.ErrorId);

        // This is a simplified implementation. In a real system, this would analyze the error
        // and provide insights.

        // For demonstration purposes, we'll just create a sample analysis result
        var ruleId = error.Rule?.RuleId ?? "unknown";
        var errorFrequency = 1;

        if (_errorHistory.TryGetValue(ruleId, out var errors))
        {
            errorFrequency = errors.Count(e => e.ErrorMessage == error.ErrorMessage);
        }

        var errorCategory = DetermineErrorCategory(error);
        var errorCause = DetermineErrorCause(error);
        var errorImpact = DetermineErrorImpact(error);

        var recommendedStrategies = new List<RecoveryStrategy>();
        var strategy = await DetermineRecoveryStrategyAsync(error, cancellationToken);
        if (strategy != null)
        {
            recommendedStrategies.Add(strategy);
        }

        var preventionRecommendations = new List<string>
        {
            "Validate input data before transformation",
            "Add error handling for specific exception types",
            "Implement circuit breaker pattern for external dependencies",
            "Add logging for better diagnostics"
        };

        await Task.Delay(100, cancellationToken); // Simulate analysis time

        return ErrorAnalysisResult.Success(
            error,
            errorCategory,
            errorCause,
            errorImpact,
            errorFrequency,
            recommendedStrategies,
            preventionRecommendations);
    }

    /// <inheritdoc />
    public async Task<ErrorHistory> GetErrorHistoryAsync(string ruleId, TimeRange timeRange, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(ruleId)) throw new ArgumentException("Rule ID cannot be empty", nameof(ruleId));
        if (timeRange == null) throw new ArgumentNullException(nameof(timeRange));

        _logger.LogInformation("Getting error history for rule {RuleId} for time range {StartTime} to {EndTime}", ruleId, timeRange.StartTime, timeRange.EndTime);

        // This is a simplified implementation. In a real system, this would query a database
        // or other storage for the error history.

        var history = new ErrorHistory
        {
            RuleId = ruleId,
            TimeRange = timeRange
        };

        if (_errorHistory.TryGetValue(ruleId, out var allErrors))
        {
            // Filter errors by time range
            var errors = allErrors
                .Where(e => e.ErrorTimestamp >= timeRange.StartTime && e.ErrorTimestamp <= timeRange.EndTime)
                .ToList();

            history.ErrorRecords = errors;
            history.TotalErrors = errors.Count;

            // Calculate error frequencies
            foreach (var error in errors)
            {
                // By error code
                if (!history.ErrorFrequencyByCode.ContainsKey(error.ErrorCode))
                {
                    history.ErrorFrequencyByCode[error.ErrorCode] = 0;
                }
                history.ErrorFrequencyByCode[error.ErrorCode]++;

                // By error message
                if (!history.ErrorFrequencyByMessage.ContainsKey(error.ErrorMessage))
                {
                    history.ErrorFrequencyByMessage[error.ErrorMessage] = 0;
                }
                history.ErrorFrequencyByMessage[error.ErrorMessage]++;

                // By day
                var day = error.ErrorTimestamp.Date;
                if (!history.ErrorFrequencyByDay.ContainsKey(day))
                {
                    history.ErrorFrequencyByDay[day] = 0;
                }
                history.ErrorFrequencyByDay[day]++;

                // By hour
                var hour = error.ErrorTimestamp.Hour;
                if (!history.ErrorFrequencyByHour.ContainsKey(hour))
                {
                    history.ErrorFrequencyByHour[hour] = 0;
                }
                history.ErrorFrequencyByHour[hour]++;

                // By severity
                if (!history.ErrorFrequencyBySeverity.ContainsKey(error.Severity))
                {
                    history.ErrorFrequencyBySeverity[error.Severity] = 0;
                }
                history.ErrorFrequencyBySeverity[error.Severity]++;
            }

            // Find most common error
            if (errors.Count > 0)
            {
                var mostCommonErrorMessage = history.ErrorFrequencyByMessage
                    .OrderByDescending(kv => kv.Value)
                    .First()
                    .Key;

                history.MostCommonError = errors
                    .First(e => e.ErrorMessage == mostCommonErrorMessage);

                // Find most recent error
                history.MostRecentError = errors
                    .OrderByDescending(e => e.ErrorTimestamp)
                    .First();

                // Find most severe error
                history.MostSevereError = errors
                    .OrderByDescending(e => e.Severity)
                    .First();
            }
        }

        await Task.Delay(100, cancellationToken); // Simulate query time

        return history;
    }

    /// <inheritdoc />
    public async Task<ProcessorBase.TransformationResult> CreateFallbackResultAsync(TransformationError error, CancellationToken cancellationToken = default)
    {
        if (error == null) throw new ArgumentNullException(nameof(error));

        _logger.LogInformation("Creating fallback result for error {ErrorId}", error.ErrorId);

        // This is a simplified implementation. In a real system, this would create a fallback result
        // based on the error and the rule.

        // For demonstration purposes, we'll just create a simple fallback result
        var fallbackData = new { Message = "Fallback data due to error", Error = error.ErrorMessage };
        var outputDataType = error.Rule?.OutputDataType ?? "unknown";

        await Task.Delay(50, cancellationToken); // Simulate fallback creation time

        return ProcessorBase.TransformationResult.Success(
            error.Input?.Data,
            error.Input?.DataType,
            fallbackData,
            outputDataType,
            error.Rule?.RuleId,
            error.Rule?.RuleType);
    }

    private async Task<RecoveryResult> ExecuteRetryStrategyAsync(TransformationError error, RecoveryStrategy strategy, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Executing retry strategy for error {ErrorId}", error.ErrorId);

        // This is a simplified implementation. In a real system, this would retry the transformation
        // with exponential backoff.

        // For demonstration purposes, we'll just simulate a successful retry after a delay
        var retryAttempts = 0;
        var delay = strategy.RetryDelayMs;

        for (retryAttempts = 0; retryAttempts < strategy.MaxRetryAttempts; retryAttempts++)
        {
            await Task.Delay(delay, cancellationToken);

            // Simulate a successful retry with 80% probability
            if (new Random().NextDouble() < 0.8)
            {
                var outputData = new { Message = "Data from successful retry", AttemptNumber = retryAttempts + 1 };
                var outputDataType = error.Rule?.OutputDataType ?? "unknown";

                return RecoveryResult.Success(
                    error,
                    strategy,
                    outputData,
                    outputDataType,
                    retryAttempts + 1);
            }

            // Increase delay for next retry if using exponential backoff
            if (strategy.UseExponentialBackoff)
            {
                delay = (int)(delay * strategy.ExponentialBackoffFactor);
            }
        }

        return RecoveryResult.Failure(
            "Maximum retry attempts exceeded",
            error,
            strategy,
            retryAttempts);
    }

    private RecoveryResult ExecuteFallbackValueStrategy(TransformationError error, RecoveryStrategy strategy)
    {
        _logger.LogInformation("Executing fallback value strategy for error {ErrorId}", error.ErrorId);

        // This is a simplified implementation. In a real system, this would use a fallback value
        // for the transformation.

        // For demonstration purposes, we'll just use the fallback value from the strategy
        var outputDataType = error.Rule?.OutputDataType ?? "unknown";

        return RecoveryResult.Success(
            error,
            strategy,
            strategy.FallbackValue,
            outputDataType,
            0);
    }

    private async Task<RecoveryResult> ExecuteFallbackRuleStrategyAsync(TransformationError error, RecoveryStrategy strategy, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Executing fallback rule strategy for error {ErrorId}", error.ErrorId);

        // This is a simplified implementation. In a real system, this would use a fallback rule
        // for the transformation.

        // For demonstration purposes, we'll just simulate a successful transformation with the fallback rule
        await Task.Delay(100, cancellationToken); // Simulate transformation time

        var outputData = new { Message = "Data from fallback rule", RuleId = strategy.FallbackRule?.RuleId };
        var outputDataType = strategy.FallbackRule?.OutputDataType ?? error.Rule?.OutputDataType ?? "unknown";

        return RecoveryResult.Success(
            error,
            strategy,
            outputData,
            outputDataType,
            0);
    }

    private RecoveryResult ExecuteSkipStrategy(TransformationError error, RecoveryStrategy strategy)
    {
        _logger.LogInformation("Executing skip strategy for error {ErrorId}", error.ErrorId);

        // This is a simplified implementation. In a real system, this would skip the transformation
        // and continue with the next one.

        // For demonstration purposes, we'll just return a success result with null output data
        return RecoveryResult.Success(
            error,
            strategy,
            null,
            null,
            0);
    }

    private string DetermineErrorCategory(TransformationError error)
    {
        // This is a simplified implementation. In a real system, this would analyze the error
        // and determine its category.

        if (error.Exception is ArgumentException)
        {
            return "InputValidation";
        }
        else if (error.Exception is InvalidOperationException)
        {
            return "OperationSequence";
        }
        else if (error.Exception is TimeoutException)
        {
            return "Performance";
        }
        else if (error.Exception is OutOfMemoryException)
        {
            return "Resource";
        }
        else
        {
            return "Unknown";
        }
    }

    private string DetermineErrorCause(TransformationError error)
    {
        // This is a simplified implementation. In a real system, this would analyze the error
        // and determine its cause.

        if (error.Exception is ArgumentException)
        {
            return "Invalid input data";
        }
        else if (error.Exception is InvalidOperationException)
        {
            return "Operation called in invalid state";
        }
        else if (error.Exception is TimeoutException)
        {
            return "Operation took too long to complete";
        }
        else if (error.Exception is OutOfMemoryException)
        {
            return "Insufficient memory for operation";
        }
        else
        {
            return "Unknown cause";
        }
    }

    private string DetermineErrorImpact(TransformationError error)
    {
        // This is a simplified implementation. In a real system, this would analyze the error
        // and determine its impact.

        switch (error.Severity)
        {
            case ErrorSeverity.Info:
                return "Informational only, no impact";

            case ErrorSeverity.Warning:
                return "Minor impact, operation can continue";

            case ErrorSeverity.Error:
                return "Significant impact, operation failed but system can continue";

            case ErrorSeverity.Critical:
                return "Major impact, system functionality compromised";

            case ErrorSeverity.Fatal:
                return "Severe impact, system cannot continue";

            default:
                return "Unknown impact";
        }
    }
}
