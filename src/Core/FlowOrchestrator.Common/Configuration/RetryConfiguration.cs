namespace FlowOrchestrator.Common.Configuration;

/// <summary>
/// Represents the retry configuration.
/// </summary>
public class RetryConfiguration
{
    /// <summary>
    /// Gets or sets a value indicating whether retry is enabled.
    /// </summary>
    public bool Enabled { get; set; }
    
    /// <summary>
    /// Gets or sets the maximum number of retry attempts.
    /// </summary>
    public int MaxAttempts { get; set; } = 3;
    
    /// <summary>
    /// Gets or sets the initial delay between retry attempts in milliseconds.
    /// </summary>
    public int InitialDelayMs { get; set; } = 1000;
    
    /// <summary>
    /// Gets or sets the maximum delay between retry attempts in milliseconds.
    /// </summary>
    public int MaxDelayMs { get; set; } = 30000;
    
    /// <summary>
    /// Gets or sets the delay factor for exponential backoff.
    /// </summary>
    public double DelayFactor { get; set; } = 2.0;
    
    /// <summary>
    /// Gets or sets the additional parameters for this configuration.
    /// </summary>
    public Dictionary<string, object> Parameters { get; set; } = new Dictionary<string, object>();
}
