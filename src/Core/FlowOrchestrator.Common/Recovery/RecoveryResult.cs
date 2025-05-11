namespace FlowOrchestrator.Common.Recovery;

/// <summary>
/// Represents the result of a recovery operation.
/// </summary>
public class RecoveryResult
{
    /// <summary>
    /// Gets or sets a value indicating whether the recovery was successful.
    /// </summary>
    public bool IsSuccessful { get; set; }
    
    /// <summary>
    /// Gets or sets the name of the recovery strategy that was used.
    /// </summary>
    public string RecoveryStrategy { get; set; } = string.Empty;
    
    /// <summary>
    /// Gets or sets a message describing the result of the recovery operation.
    /// </summary>
    public string Message { get; set; } = string.Empty;
    
    /// <summary>
    /// Gets or sets the duration of the recovery operation.
    /// </summary>
    public TimeSpan? Duration { get; set; }
    
    /// <summary>
    /// Gets or sets additional data associated with the recovery result.
    /// </summary>
    public Dictionary<string, object> AdditionalData { get; set; } = new Dictionary<string, object>();
    
    /// <summary>
    /// Creates a successful recovery result.
    /// </summary>
    /// <param name="strategyName">The name of the recovery strategy that was used.</param>
    /// <param name="message">A message describing the result of the recovery operation.</param>
    /// <param name="duration">The duration of the recovery operation.</param>
    /// <returns>A successful recovery result.</returns>
    public static RecoveryResult Success(string strategyName, string message, TimeSpan? duration = null)
    {
        return new RecoveryResult
        {
            IsSuccessful = true,
            RecoveryStrategy = strategyName,
            Message = message,
            Duration = duration
        };
    }
    
    /// <summary>
    /// Creates a failed recovery result.
    /// </summary>
    /// <param name="strategyName">The name of the recovery strategy that was used.</param>
    /// <param name="message">A message describing the result of the recovery operation.</param>
    /// <param name="duration">The duration of the recovery operation.</param>
    /// <returns>A failed recovery result.</returns>
    public static RecoveryResult Failure(string strategyName, string message, TimeSpan? duration = null)
    {
        return new RecoveryResult
        {
            IsSuccessful = false,
            RecoveryStrategy = strategyName,
            Message = message,
            Duration = duration
        };
    }
}
