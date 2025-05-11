using FlowOrchestrator.Abstractions.Entities;

namespace FlowOrchestrator.Domain.Entities;

/// <summary>
/// Implementation of the IMergeStrategyConfig interface.
/// </summary>
public class MergeStrategyConfig : IMergeStrategyConfig
{
    /// <summary>
    /// Gets or sets the default merge strategy to use for branches that don't specify one.
    /// </summary>
    public MergeStrategy DefaultMergeStrategy { get; set; } = MergeStrategy.WaitForAll;
    
    /// <summary>
    /// Gets or sets the default configuration for the default merge strategy.
    /// </summary>
    public string DefaultMergeStrategyConfiguration { get; set; } = string.Empty;
    
    /// <summary>
    /// Gets or sets the branch-specific merge strategy overrides.
    /// </summary>
    public IReadOnlyDictionary<string, IMergeConfig> BranchMergeConfigs => _branchMergeConfigs;
    private readonly Dictionary<string, IMergeConfig> _branchMergeConfigs = new();
    
    /// <summary>
    /// Creates a new instance of the MergeStrategyConfig class.
    /// </summary>
    public MergeStrategyConfig()
    {
    }
    
    /// <summary>
    /// Creates a new instance of the MergeStrategyConfig class with the specified properties.
    /// </summary>
    /// <param name="defaultMergeStrategy">The default merge strategy to use for branches that don't specify one.</param>
    /// <param name="defaultMergeStrategyConfiguration">The default configuration for the default merge strategy.</param>
    public MergeStrategyConfig(
        MergeStrategy defaultMergeStrategy,
        string defaultMergeStrategyConfiguration)
    {
        DefaultMergeStrategy = defaultMergeStrategy;
        DefaultMergeStrategyConfiguration = defaultMergeStrategyConfiguration;
    }
    
    /// <summary>
    /// Adds a branch-specific merge configuration.
    /// </summary>
    /// <param name="branchPath">The branch path.</param>
    /// <param name="mergeConfig">The merge configuration for the branch.</param>
    public void AddBranchMergeConfig(string branchPath, IMergeConfig mergeConfig)
    {
        _branchMergeConfigs[branchPath] = mergeConfig;
    }
}
