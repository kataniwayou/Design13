using FlowOrchestrator.Domain.Models;

namespace FlowOrchestrator.AnalyticsEngine;

/// <summary>
/// Issue severity enumeration
/// </summary>
public enum IssueSeverity
{
    /// <summary>
    /// Information severity
    /// </summary>
    Information,
    
    /// <summary>
    /// Warning severity
    /// </summary>
    Warning,
    
    /// <summary>
    /// Error severity
    /// </summary>
    Error,
    
    /// <summary>
    /// Critical severity
    /// </summary>
    Critical
}

/// <summary>
/// Represents a performance issue
/// </summary>
public class PerformanceIssue
{
    /// <summary>
    /// Issue type
    /// </summary>
    public string IssueType { get; set; } = string.Empty;
    
    /// <summary>
    /// Issue description
    /// </summary>
    public string Description { get; set; } = string.Empty;
    
    /// <summary>
    /// Issue severity
    /// </summary>
    public IssueSeverity Severity { get; set; }
    
    /// <summary>
    /// Recommended action
    /// </summary>
    public string? RecommendedAction { get; set; }
    
    /// <summary>
    /// Issue details
    /// </summary>
    public Dictionary<string, object> Details { get; set; } = new Dictionary<string, object>();
}

/// <summary>
/// Represents a flow execution analysis result
/// </summary>
public class FlowExecutionAnalysisResult
{
    /// <summary>
    /// Whether the analysis was successful
    /// </summary>
    public bool Success { get; set; }
    
    /// <summary>
    /// Flow ID
    /// </summary>
    public string FlowId { get; set; } = string.Empty;
    
    /// <summary>
    /// Time range
    /// </summary>
    public TimeRange TimeRange { get; set; } = new TimeRange();
    
    /// <summary>
    /// Analysis timestamp
    /// </summary>
    public DateTime AnalysisTimestamp { get; set; } = DateTime.UtcNow;
    
    /// <summary>
    /// Execution count
    /// </summary>
    public int ExecutionCount { get; set; }
    
    /// <summary>
    /// Success rate
    /// </summary>
    public double SuccessRate { get; set; }
    
    /// <summary>
    /// Average execution time
    /// </summary>
    public TimeSpan AverageExecutionTime { get; set; }
    
    /// <summary>
    /// Performance trend
    /// </summary>
    public string PerformanceTrend { get; set; } = string.Empty;
    
    /// <summary>
    /// Recommendations
    /// </summary>
    public List<string> Recommendations { get; set; } = new List<string>();
    
    /// <summary>
    /// Error message if analysis failed
    /// </summary>
    public string? ErrorMessage { get; set; }
}

/// <summary>
/// Represents a component performance analysis result
/// </summary>
public class ComponentPerformanceAnalysisResult
{
    /// <summary>
    /// Whether the analysis was successful
    /// </summary>
    public bool Success { get; set; }
    
    /// <summary>
    /// Component ID
    /// </summary>
    public string ComponentId { get; set; } = string.Empty;
    
    /// <summary>
    /// Time range
    /// </summary>
    public TimeRange TimeRange { get; set; } = new TimeRange();
    
    /// <summary>
    /// Analysis timestamp
    /// </summary>
    public DateTime AnalysisTimestamp { get; set; } = DateTime.UtcNow;
    
    /// <summary>
    /// Request count
    /// </summary>
    public int RequestCount { get; set; }
    
    /// <summary>
    /// Average response time
    /// </summary>
    public TimeSpan AverageResponseTime { get; set; }
    
    /// <summary>
    /// Error rate
    /// </summary>
    public double ErrorRate { get; set; }
    
    /// <summary>
    /// Resource utilization
    /// </summary>
    public Dictionary<string, double> ResourceUtilization { get; set; } = new Dictionary<string, double>();
    
    /// <summary>
    /// Performance issues
    /// </summary>
    public List<PerformanceIssue> PerformanceIssues { get; set; } = new List<PerformanceIssue>();
    
    /// <summary>
    /// Error message if analysis failed
    /// </summary>
    public string? ErrorMessage { get; set; }
}

/// <summary>
/// Represents a system usage analysis result
/// </summary>
public class SystemUsageAnalysisResult
{
    /// <summary>
    /// Whether the analysis was successful
    /// </summary>
    public bool Success { get; set; }
    
    /// <summary>
    /// Time range
    /// </summary>
    public TimeRange TimeRange { get; set; } = new TimeRange();
    
    /// <summary>
    /// Analysis timestamp
    /// </summary>
    public DateTime AnalysisTimestamp { get; set; } = DateTime.UtcNow;
    
    /// <summary>
    /// Total flow executions
    /// </summary>
    public int TotalFlowExecutions { get; set; }
    
    /// <summary>
    /// Unique flows
    /// </summary>
    public int UniqueFlows { get; set; }
    
    /// <summary>
    /// Top flows
    /// </summary>
    public Dictionary<string, int> TopFlows { get; set; } = new Dictionary<string, int>();
    
    /// <summary>
    /// Usage by time of day
    /// </summary>
    public Dictionary<string, int> UsageByTimeOfDay { get; set; } = new Dictionary<string, int>();
    
    /// <summary>
    /// Usage by user
    /// </summary>
    public Dictionary<string, int> UsageByUser { get; set; } = new Dictionary<string, int>();
    
    /// <summary>
    /// Error message if analysis failed
    /// </summary>
    public string? ErrorMessage { get; set; }
}

/// <summary>
/// Represents a predictive analysis request
/// </summary>
public class PredictiveAnalysisRequest
{
    /// <summary>
    /// Prediction type
    /// </summary>
    public string PredictionType { get; set; } = string.Empty;
    
    /// <summary>
    /// Time range
    /// </summary>
    public TimeRange TimeRange { get; set; } = new TimeRange();
    
    /// <summary>
    /// Target entity ID
    /// </summary>
    public string? TargetEntityId { get; set; }
    
    /// <summary>
    /// Target entity type
    /// </summary>
    public string? TargetEntityType { get; set; }
    
    /// <summary>
    /// Analysis parameters
    /// </summary>
    public Dictionary<string, object> Parameters { get; set; } = new Dictionary<string, object>();
}

/// <summary>
/// Represents a predictive analysis result
/// </summary>
public class PredictiveAnalysisResult
{
    /// <summary>
    /// Whether the analysis was successful
    /// </summary>
    public bool Success { get; set; }
    
    /// <summary>
    /// Analysis timestamp
    /// </summary>
    public DateTime AnalysisTimestamp { get; set; } = DateTime.UtcNow;
    
    /// <summary>
    /// Prediction type
    /// </summary>
    public string PredictionType { get; set; } = string.Empty;
    
    /// <summary>
    /// Time range
    /// </summary>
    public TimeRange TimeRange { get; set; } = new TimeRange();
    
    /// <summary>
    /// Predictions
    /// </summary>
    public Dictionary<string, object> Predictions { get; set; } = new Dictionary<string, object>();
    
    /// <summary>
    /// Confidence
    /// </summary>
    public double Confidence { get; set; }
    
    /// <summary>
    /// Recommendations
    /// </summary>
    public List<string> Recommendations { get; set; } = new List<string>();
    
    /// <summary>
    /// Error message if analysis failed
    /// </summary>
    public string? ErrorMessage { get; set; }
}
