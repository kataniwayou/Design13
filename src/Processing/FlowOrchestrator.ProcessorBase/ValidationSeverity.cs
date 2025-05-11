namespace FlowOrchestrator.ProcessorBase;

/// <summary>
/// Represents the severity of a validation error.
/// </summary>
public enum ValidationSeverity
{
    /// <summary>
    /// The validation error is informational.
    /// </summary>
    Info,
    
    /// <summary>
    /// The validation error is a warning.
    /// </summary>
    Warning,
    
    /// <summary>
    /// The validation error is an error.
    /// </summary>
    Error,
    
    /// <summary>
    /// The validation error is critical.
    /// </summary>
    Critical
}
