using FlowOrchestrator.Abstractions.Entities;

namespace FlowOrchestrator.Domain.Entities;

/// <summary>
/// Implementation of the IErrorHandlingConfig interface.
/// </summary>
public class ErrorHandlingConfig : IErrorHandlingConfig
{
    /// <summary>
    /// Gets or sets the default error handling strategy to use for steps that don't specify one.
    /// </summary>
    public ErrorHandlingStrategy DefaultStrategy { get; set; } = ErrorHandlingStrategy.FailImmediately;
    
    /// <summary>
    /// Gets or sets the maximum number of retry attempts for retryable errors.
    /// </summary>
    public int MaxRetryAttempts { get; set; } = 3;
    
    /// <summary>
    /// Gets or sets the retry delay in milliseconds.
    /// </summary>
    public int RetryDelayMs { get; set; } = 1000;
    
    /// <summary>
    /// Gets or sets whether to use exponential backoff for retries.
    /// </summary>
    public bool UseExponentialBackoff { get; set; } = true;
    
    /// <summary>
    /// Gets or sets the step-specific error handling strategy overrides.
    /// </summary>
    public IReadOnlyDictionary<string, ErrorHandlingStrategy> StepErrorHandlingStrategies => _stepErrorHandlingStrategies;
    private readonly Dictionary<string, ErrorHandlingStrategy> _stepErrorHandlingStrategies = new();
    
    /// <summary>
    /// Gets or sets the compensating actions to execute on failure.
    /// </summary>
    public IReadOnlyList<ICompensatingAction> CompensatingActions => _compensatingActions.AsReadOnly();
    private readonly List<ICompensatingAction> _compensatingActions = new();
    
    /// <summary>
    /// Creates a new instance of the ErrorHandlingConfig class.
    /// </summary>
    public ErrorHandlingConfig()
    {
    }
    
    /// <summary>
    /// Creates a new instance of the ErrorHandlingConfig class with the specified properties.
    /// </summary>
    /// <param name="defaultStrategy">The default error handling strategy to use for steps that don't specify one.</param>
    /// <param name="maxRetryAttempts">The maximum number of retry attempts for retryable errors.</param>
    /// <param name="retryDelayMs">The retry delay in milliseconds.</param>
    /// <param name="useExponentialBackoff">Whether to use exponential backoff for retries.</param>
    public ErrorHandlingConfig(
        ErrorHandlingStrategy defaultStrategy,
        int maxRetryAttempts,
        int retryDelayMs,
        bool useExponentialBackoff)
    {
        DefaultStrategy = defaultStrategy;
        MaxRetryAttempts = maxRetryAttempts;
        RetryDelayMs = retryDelayMs;
        UseExponentialBackoff = useExponentialBackoff;
    }
    
    /// <summary>
    /// Adds a step-specific error handling strategy.
    /// </summary>
    /// <param name="stepId">The step ID.</param>
    /// <param name="strategy">The error handling strategy for the step.</param>
    public void AddStepErrorHandlingStrategy(string stepId, ErrorHandlingStrategy strategy)
    {
        _stepErrorHandlingStrategies[stepId] = strategy;
    }
    
    /// <summary>
    /// Adds a compensating action to execute on failure.
    /// </summary>
    /// <param name="compensatingAction">The compensating action to add.</param>
    public void AddCompensatingAction(ICompensatingAction compensatingAction)
    {
        _compensatingActions.Add(compensatingAction);
    }
}
