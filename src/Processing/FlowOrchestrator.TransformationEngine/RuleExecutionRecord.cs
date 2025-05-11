namespace FlowOrchestrator.TransformationEngine;

/// <summary>
/// Represents a record of a rule execution.
/// </summary>
public class RuleExecutionRecord
{
    /// <summary>
    /// Gets or sets the execution ID.
    /// </summary>
    public string ExecutionId { get; set; } = Guid.NewGuid().ToString();
    
    /// <summary>
    /// Gets or sets the rule ID.
    /// </summary>
    public string RuleId { get; set; } = string.Empty;
    
    /// <summary>
    /// Gets or sets a value indicating whether the execution was successful.
    /// </summary>
    public bool IsSuccessful { get; set; }
    
    /// <summary>
    /// Gets or sets the error message if the execution failed.
    /// </summary>
    public string? ErrorMessage { get; set; }
    
    /// <summary>
    /// Gets or sets the execution timestamp.
    /// </summary>
    public DateTime ExecutionTimestamp { get; set; } = DateTime.UtcNow;
    
    /// <summary>
    /// Gets or sets the execution duration in milliseconds.
    /// </summary>
    public long ExecutionDurationMs { get; set; }
    
    /// <summary>
    /// Gets or sets the input data type.
    /// </summary>
    public string InputDataType { get; set; } = string.Empty;
    
    /// <summary>
    /// Gets or sets the output data type.
    /// </summary>
    public string OutputDataType { get; set; } = string.Empty;
    
    /// <summary>
    /// Gets or sets the input data size in bytes.
    /// </summary>
    public long InputDataSizeBytes { get; set; }
    
    /// <summary>
    /// Gets or sets the output data size in bytes.
    /// </summary>
    public long OutputDataSizeBytes { get; set; }
    
    /// <summary>
    /// Gets or sets the memory usage in bytes.
    /// </summary>
    public long MemoryUsageBytes { get; set; }
    
    /// <summary>
    /// Gets or sets the CPU usage percentage.
    /// </summary>
    public double CpuUsagePercentage { get; set; }
    
    /// <summary>
    /// Gets or sets the user who executed the rule.
    /// </summary>
    public string? User { get; set; }
    
    /// <summary>
    /// Gets or sets the client application that executed the rule.
    /// </summary>
    public string? ClientApplication { get; set; }
    
    /// <summary>
    /// Gets or sets the additional information about the execution.
    /// </summary>
    public Dictionary<string, object> AdditionalInfo { get; set; } = new Dictionary<string, object>();
}
