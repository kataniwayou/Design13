using System.Collections.Generic;

namespace FlowOrchestrator.Abstractions.Entities;

/// <summary>
/// Interface for flow-level merge strategy configuration.
/// </summary>
public interface IMergeStrategyConfig
{
    /// <summary>
    /// Gets the default merge strategy to use for branches that don't specify one.
    /// </summary>
    MergeStrategy DefaultMergeStrategy { get; }
    
    /// <summary>
    /// Gets the default configuration for the default merge strategy.
    /// </summary>
    string DefaultMergeStrategyConfiguration { get; }
    
    /// <summary>
    /// Gets the branch-specific merge strategy overrides.
    /// </summary>
    IReadOnlyDictionary<string, IMergeConfig> BranchMergeConfigs { get; }
}
