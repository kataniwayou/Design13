namespace FlowOrchestrator.TransformationEngine;

/// <summary>
/// Represents the severity of an error.
/// </summary>
public enum ErrorSeverity
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
    Critical,
    
    /// <summary>
    /// Fatal level severity.
    /// </summary>
    Fatal
}
