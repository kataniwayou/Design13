namespace FlowOrchestrator.ValidationProcessor;

/// <summary>
/// Represents the severity of a validation rule.
/// </summary>
public enum ValidationSeverity
{
    /// <summary>
    /// Information level severity.
    /// </summary>
    Info,
    
    /// <summary>
    /// Warning level severity.
    /// </summary>
    Warning,
    
    /// <summary>
    /// Error level severity.
    /// </summary>
    Error,
    
    /// <summary>
    /// Critical level severity.
    /// </summary>
    Critical
}
