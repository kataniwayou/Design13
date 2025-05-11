namespace FlowOrchestrator.Abstractions.Entities;

/// <summary>
/// Represents the strategy to use when merging branches.
/// </summary>
public enum MergeStrategy
{
    /// <summary>
    /// Wait for all branches to complete before merging.
    /// </summary>
    WaitForAll,
    
    /// <summary>
    /// Merge as soon as any branch completes.
    /// </summary>
    FirstComplete,
    
    /// <summary>
    /// Merge based on a custom condition.
    /// </summary>
    Conditional,
    
    /// <summary>
    /// Merge based on a priority order.
    /// </summary>
    PriorityBased
}
