namespace FlowOrchestrator.Abstractions.Protocols;

/// <summary>
/// Represents the security level of a protocol.
/// </summary>
public enum SecurityLevel
{
    /// <summary>
    /// No security.
    /// </summary>
    None,
    
    /// <summary>
    /// Low security.
    /// </summary>
    Low,
    
    /// <summary>
    /// Medium security.
    /// </summary>
    Medium,
    
    /// <summary>
    /// High security.
    /// </summary>
    High,
    
    /// <summary>
    /// Very high security.
    /// </summary>
    VeryHigh
}
