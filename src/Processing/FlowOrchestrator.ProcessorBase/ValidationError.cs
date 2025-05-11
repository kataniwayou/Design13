namespace FlowOrchestrator.ProcessorBase;

/// <summary>
/// Represents a validation error.
/// </summary>
public class ValidationError
{
    /// <summary>
    /// Gets or sets the error code.
    /// </summary>
    public string ErrorCode { get; set; } = string.Empty;
    
    /// <summary>
    /// Gets or sets the error message.
    /// </summary>
    public string ErrorMessage { get; set; } = string.Empty;
    
    /// <summary>
    /// Gets or sets the property name that caused the error.
    /// </summary>
    public string? PropertyName { get; set; }
    
    /// <summary>
    /// Gets or sets the property value that caused the error.
    /// </summary>
    public object? PropertyValue { get; set; }
    
    /// <summary>
    /// Gets or sets the severity of the error.
    /// </summary>
    public ValidationSeverity Severity { get; set; } = ValidationSeverity.Error;
    
    /// <summary>
    /// Gets or sets the additional information about the error.
    /// </summary>
    public Dictionary<string, object> AdditionalInfo { get; set; } = new Dictionary<string, object>();
}
