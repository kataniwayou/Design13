namespace FlowOrchestrator.TransformationEngine;

/// <summary>
/// Represents the severity of a bottleneck.
/// </summary>
public enum BottleneckSeverity
{
    /// <summary>
    /// Low severity bottleneck.
    /// </summary>
    Low,
    
    /// <summary>
    /// Medium severity bottleneck.
    /// </summary>
    Medium,
    
    /// <summary>
    /// High severity bottleneck.
    /// </summary>
    High,
    
    /// <summary>
    /// Critical severity bottleneck.
    /// </summary>
    Critical
}
