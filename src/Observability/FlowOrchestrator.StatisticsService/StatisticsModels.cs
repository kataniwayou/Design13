using FlowOrchestrator.Domain.Models;

namespace FlowOrchestrator.StatisticsService;

/// <summary>
/// Represents a statistics collection result
/// </summary>
public class StatisticsCollectionResult
{
    /// <summary>
    /// Whether the collection was successful
    /// </summary>
    public bool Success { get; set; }
    
    /// <summary>
    /// Flow ID
    /// </summary>
    public string? FlowId { get; set; }
    
    /// <summary>
    /// Execution ID
    /// </summary>
    public string? ExecutionId { get; set; }
    
    /// <summary>
    /// Component ID
    /// </summary>
    public string? ComponentId { get; set; }
    
    /// <summary>
    /// Collection timestamp
    /// </summary>
    public DateTime CollectionTimestamp { get; set; } = DateTime.UtcNow;
    
    /// <summary>
    /// Time range
    /// </summary>
    public TimeRange? TimeRange { get; set; }
    
    /// <summary>
    /// Collected statistics
    /// </summary>
    public Dictionary<string, object> Statistics { get; set; } = new Dictionary<string, object>();
    
    /// <summary>
    /// Error message if collection failed
    /// </summary>
    public string? ErrorMessage { get; set; }
}

/// <summary>
/// Represents a statistics aggregation result
/// </summary>
public class StatisticsAggregationResult
{
    /// <summary>
    /// Whether the aggregation was successful
    /// </summary>
    public bool Success { get; set; }
    
    /// <summary>
    /// Flow ID
    /// </summary>
    public string? FlowId { get; set; }
    
    /// <summary>
    /// Component ID
    /// </summary>
    public string? ComponentId { get; set; }
    
    /// <summary>
    /// Time range
    /// </summary>
    public TimeRange TimeRange { get; set; } = new TimeRange();
    
    /// <summary>
    /// Aggregation timestamp
    /// </summary>
    public DateTime AggregationTimestamp { get; set; } = DateTime.UtcNow;
    
    /// <summary>
    /// Aggregated statistics
    /// </summary>
    public Dictionary<string, object> AggregatedStatistics { get; set; } = new Dictionary<string, object>();
    
    /// <summary>
    /// Error message if aggregation failed
    /// </summary>
    public string? ErrorMessage { get; set; }
}

/// <summary>
/// Represents a statistics report request
/// </summary>
public class StatisticsReportRequest
{
    /// <summary>
    /// Report type
    /// </summary>
    public string ReportType { get; set; } = string.Empty;
    
    /// <summary>
    /// Flow ID
    /// </summary>
    public string? FlowId { get; set; }
    
    /// <summary>
    /// Component ID
    /// </summary>
    public string? ComponentId { get; set; }
    
    /// <summary>
    /// Time range
    /// </summary>
    public TimeRange TimeRange { get; set; } = new TimeRange();
    
    /// <summary>
    /// Report format
    /// </summary>
    public string Format { get; set; } = "JSON";
    
    /// <summary>
    /// Report options
    /// </summary>
    public Dictionary<string, object> Options { get; set; } = new Dictionary<string, object>();
}

/// <summary>
/// Represents a statistics report
/// </summary>
public class StatisticsReport
{
    /// <summary>
    /// Report ID
    /// </summary>
    public string ReportId { get; set; } = string.Empty;
    
    /// <summary>
    /// Report type
    /// </summary>
    public string ReportType { get; set; } = string.Empty;
    
    /// <summary>
    /// Time range
    /// </summary>
    public TimeRange TimeRange { get; set; } = new TimeRange();
    
    /// <summary>
    /// Generation timestamp
    /// </summary>
    public DateTime GenerationTimestamp { get; set; } = DateTime.UtcNow;
    
    /// <summary>
    /// Report data
    /// </summary>
    public Dictionary<string, object> ReportData { get; set; } = new Dictionary<string, object>();
}
