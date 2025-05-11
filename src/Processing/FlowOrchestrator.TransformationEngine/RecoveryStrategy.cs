namespace FlowOrchestrator.TransformationEngine;

/// <summary>
/// Represents a recovery strategy for a transformation error.
/// </summary>
public class RecoveryStrategy
{
    /// <summary>
    /// Gets or sets the strategy ID.
    /// </summary>
    public string StrategyId { get; set; } = Guid.NewGuid().ToString();
    
    /// <summary>
    /// Gets or sets the strategy name.
    /// </summary>
    public string StrategyName { get; set; } = string.Empty;
    
    /// <summary>
    /// Gets or sets the strategy description.
    /// </summary>
    public string StrategyDescription { get; set; } = string.Empty;
    
    /// <summary>
    /// Gets or sets the strategy type.
    /// </summary>
    public RecoveryStrategyType StrategyType { get; set; } = RecoveryStrategyType.Other;
    
    /// <summary>
    /// Gets or sets the maximum number of retry attempts.
    /// </summary>
    public int MaxRetryAttempts { get; set; } = 3;
    
    /// <summary>
    /// Gets or sets the retry delay in milliseconds.
    /// </summary>
    public int RetryDelayMs { get; set; } = 1000;
    
    /// <summary>
    /// Gets or sets a value indicating whether to use exponential backoff for retries.
    /// </summary>
    public bool UseExponentialBackoff { get; set; } = true;
    
    /// <summary>
    /// Gets or sets the exponential backoff factor.
    /// </summary>
    public double ExponentialBackoffFactor { get; set; } = 2.0;
    
    /// <summary>
    /// Gets or sets the fallback value.
    /// </summary>
    public object? FallbackValue { get; set; }
    
    /// <summary>
    /// Gets or sets the fallback rule.
    /// </summary>
    public TransformationRule? FallbackRule { get; set; }
    
    /// <summary>
    /// Gets or sets the additional parameters for this strategy.
    /// </summary>
    public Dictionary<string, object> Parameters { get; set; } = new Dictionary<string, object>();
}
