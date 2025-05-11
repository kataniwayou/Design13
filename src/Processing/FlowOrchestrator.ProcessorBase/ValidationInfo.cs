namespace FlowOrchestrator.ProcessorBase;

/// <summary>
/// Represents a validation information message.
/// </summary>
public class ValidationInfo
{
    /// <summary>
    /// Gets or sets the information code.
    /// </summary>
    public string InfoCode { get; set; } = string.Empty;
    
    /// <summary>
    /// Gets or sets the information message.
    /// </summary>
    public string InfoMessage { get; set; } = string.Empty;
    
    /// <summary>
    /// Gets or sets the property name that the information relates to.
    /// </summary>
    public string? PropertyName { get; set; }
    
    /// <summary>
    /// Gets or sets the property value that the information relates to.
    /// </summary>
    public object? PropertyValue { get; set; }
    
    /// <summary>
    /// Gets or sets the additional information.
    /// </summary>
    public Dictionary<string, object> AdditionalInfo { get; set; } = new Dictionary<string, object>();
}
