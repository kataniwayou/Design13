namespace FlowOrchestrator.TransformationEngine;

/// <summary>
/// Represents a performance profile for a transformation rule.
/// </summary>
public class PerformanceProfile
{
    /// <summary>
    /// Gets or sets the unique identifier for this performance profile.
    /// </summary>
    public string ProfileId { get; set; } = Guid.NewGuid().ToString();
    
    /// <summary>
    /// Gets or sets the transformation rule that was profiled.
    /// </summary>
    public TransformationRule? Rule { get; set; }
    
    /// <summary>
    /// Gets or sets the data sample that was used for profiling.
    /// </summary>
    public DataSample? DataSample { get; set; }
    
    /// <summary>
    /// Gets or sets the average duration of the transformation in milliseconds.
    /// </summary>
    public double AverageDurationMs { get; set; }
    
    /// <summary>
    /// Gets or sets the memory usage in bytes.
    /// </summary>
    public long MemoryUsageBytes { get; set; }
    
    /// <summary>
    /// Gets or sets the CPU usage percentage.
    /// </summary>
    public double CpuUsagePercentage { get; set; }
    
    /// <summary>
    /// Gets or sets the bottlenecks that were identified.
    /// </summary>
    public List<string> Bottlenecks { get; set; } = new List<string>();
    
    /// <summary>
    /// Gets or sets the performance metrics.
    /// </summary>
    public Dictionary<string, double> Metrics { get; set; } = new Dictionary<string, double>();
    
    /// <summary>
    /// Gets or sets the timestamp when this profile was created.
    /// </summary>
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    
    /// <summary>
    /// Gets or sets the additional information about the performance profile.
    /// </summary>
    public Dictionary<string, object> AdditionalInfo { get; set; } = new Dictionary<string, object>();
}
