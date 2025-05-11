namespace FlowOrchestrator.TransformationEngine;

/// <summary>
/// Represents a data sample for performance profiling.
/// </summary>
public class DataSample
{
    /// <summary>
    /// Gets or sets the unique identifier for this data sample.
    /// </summary>
    public string SampleId { get; set; } = Guid.NewGuid().ToString();
    
    /// <summary>
    /// Gets or sets the sample data.
    /// </summary>
    public object? SampleData { get; set; }
    
    /// <summary>
    /// Gets or sets the sample data type.
    /// </summary>
    public string SampleDataType { get; set; } = string.Empty;
    
    /// <summary>
    /// Gets or sets the sample size in bytes.
    /// </summary>
    public long SampleSize { get; set; }
    
    /// <summary>
    /// Gets or sets the sample complexity.
    /// </summary>
    public int SampleComplexity { get; set; }
    
    /// <summary>
    /// Gets or sets the sample creation timestamp.
    /// </summary>
    public DateTime CreationTimestamp { get; set; } = DateTime.UtcNow;
    
    /// <summary>
    /// Gets or sets the additional metadata for this data sample.
    /// </summary>
    public Dictionary<string, object> Metadata { get; set; } = new Dictionary<string, object>();
}
