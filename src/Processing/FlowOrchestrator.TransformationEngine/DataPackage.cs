namespace FlowOrchestrator.TransformationEngine;

/// <summary>
/// Represents a package of data to be transformed.
/// </summary>
public class DataPackage
{
    /// <summary>
    /// Gets or sets the unique identifier for this data package.
    /// </summary>
    public string PackageId { get; set; } = Guid.NewGuid().ToString();
    
    /// <summary>
    /// Gets or sets the data.
    /// </summary>
    public object? Data { get; set; }
    
    /// <summary>
    /// Gets or sets the data type.
    /// </summary>
    public string DataType { get; set; } = string.Empty;
    
    /// <summary>
    /// Gets or sets the data format.
    /// </summary>
    public string DataFormat { get; set; } = string.Empty;
    
    /// <summary>
    /// Gets or sets the data schema.
    /// </summary>
    public string? DataSchema { get; set; }
    
    /// <summary>
    /// Gets or sets the data source.
    /// </summary>
    public string? DataSource { get; set; }
    
    /// <summary>
    /// Gets or sets the data timestamp.
    /// </summary>
    public DateTime DataTimestamp { get; set; } = DateTime.UtcNow;
    
    /// <summary>
    /// Gets or sets the additional metadata for this data package.
    /// </summary>
    public Dictionary<string, object> Metadata { get; set; } = new Dictionary<string, object>();
}
