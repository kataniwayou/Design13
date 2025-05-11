namespace FlowOrchestrator.TransformationEngine;

/// <summary>
/// Represents the type of a recovery strategy.
/// </summary>
public enum RecoveryStrategyType
{
    /// <summary>
    /// Retry the operation.
    /// </summary>
    Retry,
    
    /// <summary>
    /// Use a fallback value.
    /// </summary>
    FallbackValue,
    
    /// <summary>
    /// Use a fallback rule.
    /// </summary>
    FallbackRule,
    
    /// <summary>
    /// Skip the operation.
    /// </summary>
    Skip,
    
    /// <summary>
    /// Abort the operation.
    /// </summary>
    Abort,
    
    /// <summary>
    /// Other recovery strategy.
    /// </summary>
    Other
}
