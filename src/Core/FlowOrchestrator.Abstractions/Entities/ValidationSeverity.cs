namespace FlowOrchestrator.Abstractions.Entities;

/// <summary>
/// Represents the severity of a validation rule.
/// </summary>
public enum ValidationSeverity
{
    /// <summary>
    /// Information-level severity. The flow can still be executed.
    /// </summary>
    Information,
    
    /// <summary>
    /// Warning-level severity. The flow can still be executed, but with caution.
    /// </summary>
    Warning,
    
    /// <summary>
    /// Error-level severity. The flow cannot be executed until this is fixed.
    /// </summary>
    Error
}
