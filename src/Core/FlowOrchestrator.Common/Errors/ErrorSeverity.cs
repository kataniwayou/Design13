namespace FlowOrchestrator.Common.Errors;

/// <summary>
/// Represents the severity of an error.
/// </summary>
public enum ErrorSeverity
{
    /// <summary>
    /// The error is informational.
    /// </summary>
    Information,
    
    /// <summary>
    /// The error is a warning.
    /// </summary>
    Warning,
    
    /// <summary>
    /// The error is an error.
    /// </summary>
    Error,
    
    /// <summary>
    /// The error is critical.
    /// </summary>
    Critical,
    
    /// <summary>
    /// The error is fatal.
    /// </summary>
    Fatal
}
