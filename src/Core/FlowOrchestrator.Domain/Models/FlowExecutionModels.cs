namespace FlowOrchestrator.Domain.Models;

/// <summary>
/// Represents a flow execution result
/// </summary>
public class FlowExecutionResult
{
    /// <summary>
    /// Whether the execution was successful
    /// </summary>
    public bool Success { get; set; }
    
    /// <summary>
    /// Flow ID
    /// </summary>
    public string FlowId { get; set; } = string.Empty;
    
    /// <summary>
    /// Flow version
    /// </summary>
    public string Version { get; set; } = string.Empty;
    
    /// <summary>
    /// Execution ID
    /// </summary>
    public string ExecutionId { get; set; } = string.Empty;
    
    /// <summary>
    /// Execution start time
    /// </summary>
    public DateTime StartTime { get; set; }
    
    /// <summary>
    /// Execution end time
    /// </summary>
    public DateTime? EndTime { get; set; }
    
    /// <summary>
    /// Execution duration
    /// </summary>
    public TimeSpan Duration => EndTime.HasValue ? EndTime.Value - StartTime : TimeSpan.Zero;
    
    /// <summary>
    /// Error message if execution failed
    /// </summary>
    public string? ErrorMessage { get; set; }
    
    /// <summary>
    /// Execution result data
    /// </summary>
    public object? ResultData { get; set; }
    
    /// <summary>
    /// Execution logs
    /// </summary>
    public List<string> Logs { get; set; } = new List<string>();
}

/// <summary>
/// Represents flow execution status
/// </summary>
public class FlowExecutionStatus
{
    /// <summary>
    /// Execution ID
    /// </summary>
    public string ExecutionId { get; set; } = string.Empty;
    
    /// <summary>
    /// Flow ID
    /// </summary>
    public string FlowId { get; set; } = string.Empty;
    
    /// <summary>
    /// Flow version
    /// </summary>
    public string Version { get; set; } = string.Empty;
    
    /// <summary>
    /// Execution status
    /// </summary>
    public ExecutionStatus Status { get; set; }
    
    /// <summary>
    /// Execution start time
    /// </summary>
    public DateTime StartTime { get; set; }
    
    /// <summary>
    /// Execution end time
    /// </summary>
    public DateTime? EndTime { get; set; }
    
    /// <summary>
    /// Execution duration
    /// </summary>
    public TimeSpan Duration => EndTime.HasValue ? EndTime.Value - StartTime : DateTime.UtcNow - StartTime;
    
    /// <summary>
    /// Execution progress percentage
    /// </summary>
    public int Progress { get; set; }
    
    /// <summary>
    /// Current execution step
    /// </summary>
    public string? CurrentStep { get; set; }
    
    /// <summary>
    /// Error message if execution failed
    /// </summary>
    public string? ErrorMessage { get; set; }
    
    /// <summary>
    /// Number of retries
    /// </summary>
    public int RetryCount { get; set; }
    
    /// <summary>
    /// Next retry time
    /// </summary>
    public DateTime? NextRetryTime { get; set; }
}

/// <summary>
/// Represents a flow execution summary
/// </summary>
public class FlowExecutionSummary
{
    /// <summary>
    /// Execution ID
    /// </summary>
    public string ExecutionId { get; set; } = string.Empty;
    
    /// <summary>
    /// Flow ID
    /// </summary>
    public string FlowId { get; set; } = string.Empty;
    
    /// <summary>
    /// Flow version
    /// </summary>
    public string Version { get; set; } = string.Empty;
    
    /// <summary>
    /// Execution status
    /// </summary>
    public ExecutionStatus Status { get; set; }
    
    /// <summary>
    /// Execution start time
    /// </summary>
    public DateTime StartTime { get; set; }
    
    /// <summary>
    /// Execution end time
    /// </summary>
    public DateTime? EndTime { get; set; }
    
    /// <summary>
    /// Execution duration
    /// </summary>
    public TimeSpan Duration { get; set; }
    
    /// <summary>
    /// Error message if execution failed
    /// </summary>
    public string? ErrorMessage { get; set; }
    
    /// <summary>
    /// Number of retries
    /// </summary>
    public int RetryCount { get; set; }
    
    /// <summary>
    /// Execution initiator
    /// </summary>
    public string InitiatedBy { get; set; } = string.Empty;
}

/// <summary>
/// Represents flow usage statistics
/// </summary>
public class FlowUsageStatistics
{
    /// <summary>
    /// Flow ID
    /// </summary>
    public string FlowId { get; set; } = string.Empty;
    
    /// <summary>
    /// Flow version
    /// </summary>
    public string Version { get; set; } = string.Empty;
    
    /// <summary>
    /// Time range
    /// </summary>
    public TimeRange TimeRange { get; set; } = new TimeRange();
    
    /// <summary>
    /// Total executions
    /// </summary>
    public int TotalExecutions { get; set; }
    
    /// <summary>
    /// Successful executions
    /// </summary>
    public int SuccessfulExecutions { get; set; }
    
    /// <summary>
    /// Failed executions
    /// </summary>
    public int FailedExecutions { get; set; }
    
    /// <summary>
    /// Cancelled executions
    /// </summary>
    public int CancelledExecutions { get; set; }
    
    /// <summary>
    /// Average execution duration
    /// </summary>
    public TimeSpan AverageExecutionDuration { get; set; }
    
    /// <summary>
    /// Maximum execution duration
    /// </summary>
    public TimeSpan MaxExecutionDuration { get; set; }
    
    /// <summary>
    /// Minimum execution duration
    /// </summary>
    public TimeSpan MinExecutionDuration { get; set; }
    
    /// <summary>
    /// Usage by user
    /// </summary>
    public Dictionary<string, int> UsageByUser { get; set; } = new Dictionary<string, int>();
    
    /// <summary>
    /// Usage by time period
    /// </summary>
    public Dictionary<string, int> UsageByTimePeriod { get; set; } = new Dictionary<string, int>();
}

/// <summary>
/// Represents flow performance metrics
/// </summary>
public class FlowPerformanceMetrics
{
    /// <summary>
    /// Flow ID
    /// </summary>
    public string FlowId { get; set; } = string.Empty;
    
    /// <summary>
    /// Flow version
    /// </summary>
    public string Version { get; set; } = string.Empty;
    
    /// <summary>
    /// Time range
    /// </summary>
    public TimeRange TimeRange { get; set; } = new TimeRange();
    
    /// <summary>
    /// Average CPU usage percentage
    /// </summary>
    public double AverageCpuUsagePercentage { get; set; }
    
    /// <summary>
    /// Average memory usage in megabytes
    /// </summary>
    public double AverageMemoryUsageMb { get; set; }
    
    /// <summary>
    /// Average disk I/O in kilobytes per second
    /// </summary>
    public double AverageDiskIoKbps { get; set; }
    
    /// <summary>
    /// Average network I/O in kilobytes per second
    /// </summary>
    public double AverageNetworkIoKbps { get; set; }
    
    /// <summary>
    /// Component performance metrics
    /// </summary>
    public Dictionary<string, ComponentPerformanceMetrics> ComponentMetrics { get; set; } = new Dictionary<string, ComponentPerformanceMetrics>();
}

/// <summary>
/// Represents component performance metrics
/// </summary>
public class ComponentPerformanceMetrics
{
    /// <summary>
    /// Component ID
    /// </summary>
    public string ComponentId { get; set; } = string.Empty;
    
    /// <summary>
    /// Average execution duration
    /// </summary>
    public TimeSpan AverageExecutionDuration { get; set; }
    
    /// <summary>
    /// Maximum execution duration
    /// </summary>
    public TimeSpan MaxExecutionDuration { get; set; }
    
    /// <summary>
    /// Minimum execution duration
    /// </summary>
    public TimeSpan MinExecutionDuration { get; set; }
    
    /// <summary>
    /// Execution count
    /// </summary>
    public int ExecutionCount { get; set; }
    
    /// <summary>
    /// Error count
    /// </summary>
    public int ErrorCount { get; set; }
}

/// <summary>
/// Represents a flow audit log
/// </summary>
public class FlowAuditLog
{
    /// <summary>
    /// Flow ID
    /// </summary>
    public string FlowId { get; set; } = string.Empty;
    
    /// <summary>
    /// Flow version
    /// </summary>
    public string Version { get; set; } = string.Empty;
    
    /// <summary>
    /// Time range
    /// </summary>
    public TimeRange TimeRange { get; set; } = new TimeRange();
    
    /// <summary>
    /// Audit entries
    /// </summary>
    public List<AuditEntry> Entries { get; set; } = new List<AuditEntry>();
}
