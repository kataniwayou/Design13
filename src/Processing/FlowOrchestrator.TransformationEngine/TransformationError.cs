namespace FlowOrchestrator.TransformationEngine;

/// <summary>
/// Represents an error that occurred during a transformation operation.
/// </summary>
public class TransformationError
{
    /// <summary>
    /// Gets or sets the unique identifier for this error.
    /// </summary>
    public string ErrorId { get; set; } = Guid.NewGuid().ToString();
    
    /// <summary>
    /// Gets or sets the exception that caused the error.
    /// </summary>
    public Exception? Exception { get; set; }
    
    /// <summary>
    /// Gets or sets the error message.
    /// </summary>
    public string ErrorMessage { get; set; } = string.Empty;
    
    /// <summary>
    /// Gets or sets the error code.
    /// </summary>
    public string ErrorCode { get; set; } = string.Empty;
    
    /// <summary>
    /// Gets or sets the error severity.
    /// </summary>
    public ErrorSeverity Severity { get; set; } = ErrorSeverity.Error;
    
    /// <summary>
    /// Gets or sets the input data that caused the error.
    /// </summary>
    public DataPackage? Input { get; set; }
    
    /// <summary>
    /// Gets or sets the transformation rule that caused the error.
    /// </summary>
    public TransformationRule? Rule { get; set; }
    
    /// <summary>
    /// Gets or sets the timestamp when the error occurred.
    /// </summary>
    public DateTime ErrorTime { get; set; } = DateTime.UtcNow;
    
    /// <summary>
    /// Gets or sets the stack trace of the error.
    /// </summary>
    public string? StackTrace { get; set; }
    
    /// <summary>
    /// Gets or sets the additional information about the error.
    /// </summary>
    public Dictionary<string, object> AdditionalInfo { get; set; } = new Dictionary<string, object>();
}
