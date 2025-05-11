namespace FlowOrchestrator.TransformationEngine;

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
    /// Gets or sets the error message if the recovery failed.
    /// </summary>
    public string? ErrorMessage { get; set; }
    
    /// <summary>
    /// Gets or sets the transformation error that was recovered from.
    /// </summary>
    public TransformationError? Error { get; set; }
    
    /// <summary>
    /// Gets or sets the recovery strategy that was used.
    /// </summary>
    public RecoveryStrategy? Strategy { get; set; }
    
    /// <summary>
    /// Gets or sets the output data from the recovery.
    /// </summary>
    public object? OutputData { get; set; }
    
    /// <summary>
    /// Gets or sets the output data type from the recovery.
    /// </summary>
    public string? OutputDataType { get; set; }
    
    /// <summary>
    /// Gets or sets the number of retry attempts that were made.
    /// </summary>
    public int RetryAttempts { get; set; }
    
    /// <summary>
    /// Gets or sets the recovery timestamp.
    /// </summary>
    public DateTime RecoveryTimestamp { get; set; } = DateTime.UtcNow;
    
    /// <summary>
    /// Gets or sets the additional information about the recovery.
    /// </summary>
    public Dictionary<string, object> AdditionalInfo { get; set; } = new Dictionary<string, object>();
    
    /// <summary>
    /// Creates a successful recovery result.
    /// </summary>
    /// <param name="error">The transformation error that was recovered from.</param>
    /// <param name="strategy">The recovery strategy that was used.</param>
    /// <param name="outputData">The output data from the recovery.</param>
    /// <param name="outputDataType">The output data type from the recovery.</param>
    /// <param name="retryAttempts">The number of retry attempts that were made.</param>
    /// <returns>A successful recovery result.</returns>
    public static RecoveryResult Success(TransformationError error, RecoveryStrategy strategy, object? outputData, string? outputDataType, int retryAttempts = 0)
    {
        return new RecoveryResult
        {
            IsSuccessful = true,
            Error = error,
            Strategy = strategy,
            OutputData = outputData,
            OutputDataType = outputDataType,
            RetryAttempts = retryAttempts
        };
    }
    
    /// <summary>
    /// Creates a failed recovery result.
    /// </summary>
    /// <param name="errorMessage">The error message.</param>
    /// <param name="error">The transformation error that was recovered from.</param>
    /// <param name="strategy">The recovery strategy that was used.</param>
    /// <param name="retryAttempts">The number of retry attempts that were made.</param>
    /// <returns>A failed recovery result.</returns>
    public static RecoveryResult Failure(string errorMessage, TransformationError error, RecoveryStrategy strategy, int retryAttempts = 0)
    {
        return new RecoveryResult
        {
            IsSuccessful = false,
            ErrorMessage = errorMessage,
            Error = error,
            Strategy = strategy,
            RetryAttempts = retryAttempts
        };
    }
}
