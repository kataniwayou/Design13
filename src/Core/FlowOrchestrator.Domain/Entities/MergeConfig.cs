using FlowOrchestrator.Abstractions.Entities;

namespace FlowOrchestrator.Domain.Entities;

/// <summary>
/// Implementation of the IMergeConfig interface.
/// </summary>
public class MergeConfig : IMergeConfig
{
    /// <summary>
    /// Gets or sets the target branch path where this branch merges back.
    /// </summary>
    public string TargetBranchPath { get; set; } = string.Empty;
    
    /// <summary>
    /// Gets or sets the step ID in the target branch where this branch merges back.
    /// </summary>
    public int TargetStepId { get; set; }
    
    /// <summary>
    /// Gets or sets the merge strategy to use when merging this branch.
    /// </summary>
    public MergeStrategy MergeStrategy { get; set; } = MergeStrategy.WaitForAll;
    
    /// <summary>
    /// Gets or sets the configuration for the merge strategy.
    /// </summary>
    public string MergeStrategyConfiguration { get; set; } = string.Empty;
    
    /// <summary>
    /// Creates a new instance of the MergeConfig class.
    /// </summary>
    public MergeConfig()
    {
    }
    
    /// <summary>
    /// Creates a new instance of the MergeConfig class with the specified properties.
    /// </summary>
    /// <param name="targetBranchPath">The target branch path where this branch merges back.</param>
    /// <param name="targetStepId">The step ID in the target branch where this branch merges back.</param>
    /// <param name="mergeStrategy">The merge strategy to use when merging this branch.</param>
    /// <param name="mergeStrategyConfiguration">The configuration for the merge strategy.</param>
    public MergeConfig(
        string targetBranchPath,
        int targetStepId,
        MergeStrategy mergeStrategy,
        string mergeStrategyConfiguration)
    {
        TargetBranchPath = targetBranchPath;
        TargetStepId = targetStepId;
        MergeStrategy = mergeStrategy;
        MergeStrategyConfiguration = mergeStrategyConfiguration;
    }
}
