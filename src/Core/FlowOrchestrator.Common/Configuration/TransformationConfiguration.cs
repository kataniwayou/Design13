namespace FlowOrchestrator.Common.Configuration;

/// <summary>
/// Represents the configuration for transformation.
/// </summary>
public class TransformationConfiguration
{
    /// <summary>
    /// Gets or sets a value indicating whether transformation is enabled.
    /// </summary>
    public bool IsEnabled { get; set; } = true;
    
    /// <summary>
    /// Gets or sets the transformation rules.
    /// </summary>
    public List<string> TransformationRules { get; set; } = new List<string>();
    
    /// <summary>
    /// Gets or sets the transformation rule types.
    /// </summary>
    public List<string> TransformationRuleTypes { get; set; } = new List<string>();
    
    /// <summary>
    /// Gets or sets a value indicating whether to validate the input data before transformation.
    /// </summary>
    public bool ValidateInputBeforeTransformation { get; set; } = true;
    
    /// <summary>
    /// Gets or sets a value indicating whether to validate the output data after transformation.
    /// </summary>
    public bool ValidateOutputAfterTransformation { get; set; } = true;
    
    /// <summary>
    /// Gets or sets a value indicating whether to optimize the transformation.
    /// </summary>
    public bool OptimizeTransformation { get; set; } = true;
    
    /// <summary>
    /// Gets or sets a value indicating whether to use caching for transformation.
    /// </summary>
    public bool UseCache { get; set; } = true;
    
    /// <summary>
    /// Gets or sets the additional parameters for transformation.
    /// </summary>
    public Dictionary<string, object> Parameters { get; set; } = new Dictionary<string, object>();
}
