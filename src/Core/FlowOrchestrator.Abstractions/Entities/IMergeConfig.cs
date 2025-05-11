namespace FlowOrchestrator.Abstractions.Entities;

/// <summary>
/// Interface for branch merge configuration.
/// </summary>
public interface IMergeConfig
{
    /// <summary>
    /// Gets the target branch path where this branch merges back.
    /// </summary>
    string TargetBranchPath { get; }
    
    /// <summary>
    /// Gets the step ID in the target branch where this branch merges back.
    /// </summary>
    int TargetStepId { get; }
    
    /// <summary>
    /// Gets the merge strategy to use when merging this branch.
    /// </summary>
    MergeStrategy MergeStrategy { get; }
    
    /// <summary>
    /// Gets the configuration for the merge strategy.
    /// </summary>
    string MergeStrategyConfiguration { get; }
}
