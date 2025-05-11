namespace FlowOrchestrator.TransformationEngine;

/// <summary>
/// Represents a transformation rule.
/// </summary>
public class TransformationRule
{
    /// <summary>
    /// Gets or sets the unique identifier for this rule.
    /// </summary>
    public string RuleId { get; set; } = Guid.NewGuid().ToString();
    
    /// <summary>
    /// Gets or sets the name of this rule.
    /// </summary>
    public string Name { get; set; } = string.Empty;
    
    /// <summary>
    /// Gets or sets the description of this rule.
    /// </summary>
    public string Description { get; set; } = string.Empty;
    
    /// <summary>
    /// Gets or sets the type of this rule.
    /// </summary>
    public string RuleType { get; set; } = string.Empty;
    
    /// <summary>
    /// Gets or sets the rule definition.
    /// </summary>
    public string RuleDefinition { get; set; } = string.Empty;
    
    /// <summary>
    /// Gets or sets the rule language.
    /// </summary>
    public string RuleLanguage { get; set; } = "JSON";
    
    /// <summary>
    /// Gets or sets the input data type.
    /// </summary>
    public string InputDataType { get; set; } = string.Empty;
    
    /// <summary>
    /// Gets or sets the output data type.
    /// </summary>
    public string OutputDataType { get; set; } = string.Empty;
    
    /// <summary>
    /// Gets or sets the version of this rule.
    /// </summary>
    public string Version { get; set; } = "1.0.0";
    
    /// <summary>
    /// Gets or sets the author of this rule.
    /// </summary>
    public string Author { get; set; } = string.Empty;
    
    /// <summary>
    /// Gets or sets the creation date of this rule.
    /// </summary>
    public DateTime CreationDate { get; set; } = DateTime.UtcNow;
    
    /// <summary>
    /// Gets or sets the last modified date of this rule.
    /// </summary>
    public DateTime LastModifiedDate { get; set; } = DateTime.UtcNow;
    
    /// <summary>
    /// Gets or sets a value indicating whether optimization is enabled for this rule.
    /// </summary>
    public bool OptimizationEnabled { get; set; } = true;
    
    /// <summary>
    /// Gets or sets a value indicating whether caching is enabled for this rule.
    /// </summary>
    public bool CachingEnabled { get; set; } = true;
    
    /// <summary>
    /// Gets or sets a value indicating whether validation is enabled for this rule.
    /// </summary>
    public bool ValidationEnabled { get; set; } = true;
    
    /// <summary>
    /// Gets or sets the additional metadata for this rule.
    /// </summary>
    public Dictionary<string, object> Metadata { get; set; } = new Dictionary<string, object>();
}
