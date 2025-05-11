namespace FlowOrchestrator.ProcessorBase;

/// <summary>
/// Represents a validation warning.
/// </summary>
public class ValidationWarning
{
    /// <summary>
    /// Gets or sets the warning code.
    /// </summary>
    public string WarningCode { get; set; } = string.Empty;
    
    /// <summary>
    /// Gets or sets the warning message.
    /// </summary>
    public string WarningMessage { get; set; } = string.Empty;
    
    /// <summary>
    /// Gets or sets the property name that caused the warning.
    /// </summary>
    public string? PropertyName { get; set; }
    
    /// <summary>
    /// Gets or sets the property value that caused the warning.
    /// </summary>
    public object? PropertyValue { get; set; }
    
    /// <summary>
    /// Gets or sets the additional information about the warning.
    /// </summary>
    public Dictionary<string, object> AdditionalInfo { get; set; } = new Dictionary<string, object>();
}
