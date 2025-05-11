using FlowOrchestrator.Abstractions.Services;
using FlowOrchestrator.Common.Errors;
using FlowOrchestrator.Common.Recovery;
using Microsoft.Extensions.Logging;

namespace FlowOrchestrator.Recovery;

/// <summary>
/// Provides a framework for error recovery in the flow orchestration process.
/// </summary>
public class RecoveryFramework : IRecoveryFramework
{
    private readonly ILogger<RecoveryFramework> _logger;
    private readonly RecoveryStrategyManager _recoveryStrategyManager;
    private readonly ErrorCorrelationEngine _errorCorrelationEngine;
    private readonly CompensatingActionManager _compensatingActionManager;
    private readonly CircuitBreakerImplementation _circuitBreaker;
    private readonly RecoveryMetricsCollector _metricsCollector;

    /// <summary>
    /// Initializes a new instance of the <see cref="RecoveryFramework"/> class.
    /// </summary>
    /// <param name="logger">The logger.</param>
    /// <param name="recoveryStrategyManager">The recovery strategy manager.</param>
    /// <param name="errorCorrelationEngine">The error correlation engine.</param>
    /// <param name="compensatingActionManager">The compensating action manager.</param>
    /// <param name="circuitBreaker">The circuit breaker implementation.</param>
    /// <param name="metricsCollector">The recovery metrics collector.</param>
    public RecoveryFramework(
        ILogger<RecoveryFramework> logger,
        RecoveryStrategyManager recoveryStrategyManager,
        ErrorCorrelationEngine errorCorrelationEngine,
        CompensatingActionManager compensatingActionManager,
        CircuitBreakerImplementation circuitBreaker,
        RecoveryMetricsCollector metricsCollector)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _recoveryStrategyManager = recoveryStrategyManager ?? throw new ArgumentNullException(nameof(recoveryStrategyManager));
        _errorCorrelationEngine = errorCorrelationEngine ?? throw new ArgumentNullException(nameof(errorCorrelationEngine));
        _compensatingActionManager = compensatingActionManager ?? throw new ArgumentNullException(nameof(compensatingActionManager));
        _circuitBreaker = circuitBreaker ?? throw new ArgumentNullException(nameof(circuitBreaker));
        _metricsCollector = metricsCollector ?? throw new ArgumentNullException(nameof(metricsCollector));
    }

    /// <inheritdoc />
    public async Task<RecoveryResult> RecoverAsync(ErrorContext errorContext, object executionContext, CancellationToken cancellationToken = default)
    {
        if (errorContext == null) throw new ArgumentNullException(nameof(errorContext));
        if (executionContext == null) throw new ArgumentNullException(nameof(executionContext));

        _logger.LogInformation("Starting recovery process for execution with error {ErrorId}",
            errorContext.ErrorId);

        _metricsCollector.RecordRecoveryAttempt(errorContext, executionContext);

        try
        {
            // Check if circuit breaker is open
            string executionId = executionContext.ToString() ?? "unknown";
            if (_circuitBreaker.IsOpen(executionId))
            {
                _logger.LogWarning("Circuit breaker is open for execution {ExecutionId}. Recovery aborted.",
                    executionId);

                return new RecoveryResult
                {
                    IsSuccessful = false,
                    RecoveryStrategy = "CircuitBreakerOpen",
                    Message = "Circuit breaker is open. Too many failures detected."
                };
            }

            // Correlate errors to identify patterns
            var correlatedErrors = await _errorCorrelationEngine.CorrelateErrorsAsync(errorContext, executionId, cancellationToken);

            // Select appropriate recovery strategy
            var strategy = await _recoveryStrategyManager.SelectStrategyAsync(errorContext, correlatedErrors, cancellationToken);

            if (strategy == null)
            {
                _logger.LogWarning("No suitable recovery strategy found for error {ErrorId} in execution {ExecutionId}",
                    errorContext.ErrorId, executionId);

                return new RecoveryResult
                {
                    IsSuccessful = false,
                    RecoveryStrategy = "NoStrategyFound",
                    Message = "No suitable recovery strategy found for the error."
                };
            }

            _logger.LogInformation("Selected recovery strategy {StrategyName} for error {ErrorId} in execution {ExecutionId}",
                strategy.Name, errorContext.ErrorId, executionId);

            // Apply recovery strategy
            var result = await strategy.ApplyAsync(errorContext, executionContext, cancellationToken);

            if (!result.IsSuccessful)
            {
                _logger.LogWarning("Recovery strategy {StrategyName} failed for error {ErrorId} in execution {ExecutionId}: {Message}",
                    strategy.Name, errorContext.ErrorId, executionId, result.Message);

                // Record failure in circuit breaker
                _circuitBreaker.RecordFailure(executionId);

                // Apply compensating actions if needed
                await _compensatingActionManager.ApplyCompensatingActionsAsync(errorContext, executionContext, cancellationToken);
            }
            else
            {
                _logger.LogInformation("Recovery strategy {StrategyName} succeeded for error {ErrorId} in execution {ExecutionId}",
                    strategy.Name, errorContext.ErrorId, executionId);

                // Record success in circuit breaker
                _circuitBreaker.RecordSuccess(executionId);
            }

            _metricsCollector.RecordRecoveryResult(result, strategy.Name, errorContext, executionContext);

            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error during recovery process for error {ErrorId}",
                errorContext.ErrorId);

            string id = executionContext.ToString() ?? "unknown";
            _circuitBreaker.RecordFailure(id);

            _metricsCollector.RecordRecoveryException(ex, errorContext, executionContext);

            return new RecoveryResult
            {
                IsSuccessful = false,
                RecoveryStrategy = "RecoveryFrameworkError",
                Message = $"Error in recovery framework: {ex.Message}"
            };
        }
    }
}
