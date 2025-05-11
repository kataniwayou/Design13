namespace FlowOrchestrator.Common.Configuration;

/// <summary>
/// Represents the configuration for mapping.
/// </summary>
public class MappingConfiguration
{
    /// <summary>
    /// Gets or sets a value indicating whether mapping is enabled.
    /// </summary>
    public bool IsEnabled { get; set; } = true;
    
    /// <summary>
    /// Gets or sets the mapping rules.
    /// </summary>
    public List<string> MappingRules { get; set; } = new List<string>();
    
    /// <summary>
    /// Gets or sets the mapping rule types.
    /// </summary>
    public List<string> MappingRuleTypes { get; set; } = new List<string>();
    
    /// <summary>
    /// Gets or sets a value indicating whether to validate the source data before mapping.
    /// </summary>
    public bool ValidateSourceBeforeMapping { get; set; } = true;
    
    /// <summary>
    /// Gets or sets a value indicating whether to validate the target data after mapping.
    /// </summary>
    public bool ValidateTargetAfterMapping { get; set; } = true;
    
    /// <summary>
    /// Gets or sets a value indicating whether to use auto-mapping.
    /// </summary>
    public bool UseAutoMapping { get; set; } = true;
    
    /// <summary>
    /// Gets or sets a value indicating whether to optimize the mapping.
    /// </summary>
    public bool OptimizeMapping { get; set; } = true;
    
    /// <summary>
    /// Gets or sets a value indicating whether to use caching for mapping.
    /// </summary>
    public bool UseCache { get; set; } = true;
    
    /// <summary>
    /// Gets or sets the additional parameters for mapping.
    /// </summary>
    public Dictionary<string, object> Parameters { get; set; } = new Dictionary<string, object>();
}
