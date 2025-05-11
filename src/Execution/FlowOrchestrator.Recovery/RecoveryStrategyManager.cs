using FlowOrchestrator.Abstractions.Services;
using FlowOrchestrator.Common.Errors;
using FlowOrchestrator.Common.Recovery;
using CorrelatedError = FlowOrchestrator.Common.Recovery.CorrelatedError;
using Microsoft.Extensions.Logging;

namespace FlowOrchestrator.Recovery;

/// <summary>
/// Manages recovery strategies for the recovery framework.
/// </summary>
public class RecoveryStrategyManager
{
    private readonly ILogger<RecoveryStrategyManager> _logger;
    private readonly IEnumerable<IRecoveryStrategy> _strategies;

    /// <summary>
    /// Initializes a new instance of the <see cref="RecoveryStrategyManager"/> class.
    /// </summary>
    /// <param name="logger">The logger.</param>
    /// <param name="strategies">The available recovery strategies.</param>
    public RecoveryStrategyManager(
        ILogger<RecoveryStrategyManager> logger,
        IEnumerable<IRecoveryStrategy> strategies)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _strategies = strategies ?? throw new ArgumentNullException(nameof(strategies));
    }

    /// <summary>
    /// Selects an appropriate recovery strategy for the given error context and correlated errors.
    /// </summary>
    /// <param name="errorContext">The error context.</param>
    /// <param name="correlatedErrors">The correlated errors.</param>
    /// <param name="cancellationToken">A cancellation token to observe while waiting for the task to complete.</param>
    /// <returns>The selected recovery strategy, or null if no suitable strategy is found.</returns>
    public async Task<IRecoveryStrategy?> SelectStrategyAsync(
        ErrorContext errorContext,
        IEnumerable<Common.Recovery.CorrelatedError> correlatedErrors,
        CancellationToken cancellationToken)
    {
        if (errorContext == null) throw new ArgumentNullException(nameof(errorContext));
        if (correlatedErrors == null) throw new ArgumentNullException(nameof(correlatedErrors));

        _logger.LogInformation("Selecting recovery strategy for error {ErrorId} with {CorrelatedErrorCount} correlated errors",
            errorContext.ErrorId, correlatedErrors.Count());

        // Sort strategies by priority (higher priority first)
        var sortedStrategies = _strategies.OrderByDescending(s => s.Priority);

        foreach (var strategy in sortedStrategies)
        {
            try
            {
                var isApplicable = await strategy.IsApplicableAsync(errorContext, correlatedErrors, cancellationToken);

                if (isApplicable)
                {
                    _logger.LogInformation("Selected recovery strategy {StrategyName} for error {ErrorId}",
                        strategy.Name, errorContext.ErrorId);

                    return strategy;
                }
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "Error checking if strategy {StrategyName} is applicable for error {ErrorId}",
                    strategy.Name, errorContext.ErrorId);
            }
        }

        _logger.LogWarning("No suitable recovery strategy found for error {ErrorId}", errorContext.ErrorId);
        return null;
    }

    /// <summary>
    /// Gets a recovery strategy by name.
    /// </summary>
    /// <param name="strategyName">The name of the strategy to get.</param>
    /// <returns>The recovery strategy with the specified name, or null if not found.</returns>
    public IRecoveryStrategy? GetStrategyByName(string strategyName)
    {
        if (string.IsNullOrEmpty(strategyName)) throw new ArgumentNullException(nameof(strategyName));

        var strategy = _strategies.FirstOrDefault(s => s.Name.Equals(strategyName, StringComparison.OrdinalIgnoreCase));

        if (strategy == null)
        {
            _logger.LogWarning("Recovery strategy {StrategyName} not found", strategyName);
        }

        return strategy;
    }

    /// <summary>
    /// Gets all available recovery strategies.
    /// </summary>
    /// <returns>A collection of all available recovery strategies.</returns>
    public IEnumerable<IRecoveryStrategy> GetAllStrategies()
    {
        return _strategies;
    }
}
