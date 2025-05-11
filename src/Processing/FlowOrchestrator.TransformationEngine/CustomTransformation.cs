namespace FlowOrchestrator.TransformationEngine;

/// <summary>
/// Represents a custom transformation.
/// </summary>
public class CustomTransformation
{
    /// <summary>
    /// Gets or sets the unique identifier for this transformation.
    /// </summary>
    public string TransformationId { get; set; } = Guid.NewGuid().ToString();
    
    /// <summary>
    /// Gets or sets the name of this transformation.
    /// </summary>
    public string Name { get; set; } = string.Empty;
    
    /// <summary>
    /// Gets or sets the description of this transformation.
    /// </summary>
    public string Description { get; set; } = string.Empty;
    
    /// <summary>
    /// Gets or sets the transformation definition.
    /// </summary>
    public string TransformationDefinition { get; set; } = string.Empty;
    
    /// <summary>
    /// Gets or sets the transformation language.
    /// </summary>
    public string TransformationLanguage { get; set; } = "JSON";
    
    /// <summary>
    /// Gets or sets the input data type.
    /// </summary>
    public string InputDataType { get; set; } = string.Empty;
    
    /// <summary>
    /// Gets or sets the output data type.
    /// </summary>
    public string OutputDataType { get; set; } = string.Empty;
    
    /// <summary>
    /// Gets or sets the version of this transformation.
    /// </summary>
    public string Version { get; set; } = "1.0.0";
    
    /// <summary>
    /// Gets or sets the author of this transformation.
    /// </summary>
    public string Author { get; set; } = string.Empty;
    
    /// <summary>
    /// Gets or sets the creation date of this transformation.
    /// </summary>
    public DateTime CreationDate { get; set; } = DateTime.UtcNow;
    
    /// <summary>
    /// Gets or sets the last modified date of this transformation.
    /// </summary>
    public DateTime LastModifiedDate { get; set; } = DateTime.UtcNow;
    
    /// <summary>
    /// Gets or sets a value indicating whether optimization is enabled for this transformation.
    /// </summary>
    public bool OptimizationEnabled { get; set; } = true;
    
    /// <summary>
    /// Gets or sets the additional metadata for this transformation.
    /// </summary>
    public Dictionary<string, object> Metadata { get; set; } = new Dictionary<string, object>();
}
