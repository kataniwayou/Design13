namespace FlowOrchestrator.TransformationEngine;

/// <summary>
/// Represents a record of an error.
/// </summary>
public class ErrorRecord
{
    /// <summary>
    /// Gets or sets the error ID.
    /// </summary>
    public string ErrorId { get; set; } = Guid.NewGuid().ToString();
    
    /// <summary>
    /// Gets or sets the rule ID.
    /// </summary>
    public string RuleId { get; set; } = string.Empty;
    
    /// <summary>
    /// Gets or sets the error code.
    /// </summary>
    public string ErrorCode { get; set; } = string.Empty;
    
    /// <summary>
    /// Gets or sets the error message.
    /// </summary>
    public string ErrorMessage { get; set; } = string.Empty;
    
    /// <summary>
    /// Gets or sets the error severity.
    /// </summary>
    public ErrorSeverity Severity { get; set; } = ErrorSeverity.Error;
    
    /// <summary>
    /// Gets or sets the error timestamp.
    /// </summary>
    public DateTime ErrorTimestamp { get; set; } = DateTime.UtcNow;
    
    /// <summary>
    /// Gets or sets the stack trace.
    /// </summary>
    public string? StackTrace { get; set; }
    
    /// <summary>
    /// Gets or sets the input data type.
    /// </summary>
    public string? InputDataType { get; set; }
    
    /// <summary>
    /// Gets or sets the user who executed the rule.
    /// </summary>
    public string? User { get; set; }
    
    /// <summary>
    /// Gets or sets the client application that executed the rule.
    /// </summary>
    public string? ClientApplication { get; set; }
    
    /// <summary>
    /// Gets or sets the recovery strategy that was used.
    /// </summary>
    public RecoveryStrategy? RecoveryStrategy { get; set; }
    
    /// <summary>
    /// Gets or sets a value indicating whether the recovery was successful.
    /// </summary>
    public bool RecoverySuccessful { get; set; }
    
    /// <summary>
    /// Gets or sets the additional information about the error.
    /// </summary>
    public Dictionary<string, object> AdditionalInfo { get; set; } = new Dictionary<string, object>();
}
