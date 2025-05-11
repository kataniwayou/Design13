namespace FlowOrchestrator.Common.Configuration;

/// <summary>
/// Represents the configuration for error recovery.
/// </summary>
public class ErrorRecoveryConfiguration
{
    /// <summary>
    /// Gets or sets a value indicating whether error recovery is enabled.
    /// </summary>
    public bool IsEnabled { get; set; } = true;
    
    /// <summary>
    /// Gets or sets the error recovery strategy.
    /// </summary>
    public string ErrorRecoveryStrategy { get; set; } = "Retry";
    
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
    /// Gets or sets the maximum retry delay in milliseconds.
    /// </summary>
    public int MaxRetryDelayMs { get; set; } = 30000;
    
    /// <summary>
    /// Gets or sets a value indicating whether to use circuit breaker.
    /// </summary>
    public bool UseCircuitBreaker { get; set; } = true;
    
    /// <summary>
    /// Gets or sets the circuit breaker failure threshold.
    /// </summary>
    public int CircuitBreakerFailureThreshold { get; set; } = 5;
    
    /// <summary>
    /// Gets or sets the circuit breaker reset timeout in seconds.
    /// </summary>
    public int CircuitBreakerResetTimeoutSeconds { get; set; } = 60;
    
    /// <summary>
    /// Gets or sets a value indicating whether to use fallback.
    /// </summary>
    public bool UseFallback { get; set; } = true;
    
    /// <summary>
    /// Gets or sets the fallback strategy.
    /// </summary>
    public string FallbackStrategy { get; set; } = "DefaultValue";
    
    /// <summary>
    /// Gets or sets the additional parameters for error recovery.
    /// </summary>
    public Dictionary<string, object> Parameters { get; set; } = new Dictionary<string, object>();
}
