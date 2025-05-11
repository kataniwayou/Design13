namespace FlowOrchestrator.MonitoringFramework;

/// <summary>
/// Represents a monitoring result
/// </summary>
public class MonitoringResult
{
    /// <summary>
    /// Whether the monitoring was successful
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
    /// Monitoring timestamp
    /// </summary>
    public DateTime MonitoringTimestamp { get; set; } = DateTime.UtcNow;
    
    /// <summary>
    /// Status
    /// </summary>
    public string Status { get; set; } = string.Empty;
    
    /// <summary>
    /// Metrics
    /// </summary>
    public Dictionary<string, object> Metrics { get; set; } = new Dictionary<string, object>();
    
    /// <summary>
    /// Error message if monitoring failed
    /// </summary>
    public string? ErrorMessage { get; set; }
}

/// <summary>
/// Represents a health monitoring result
/// </summary>
public class HealthMonitoringResult
{
    /// <summary>
    /// Whether the monitoring was successful
    /// </summary>
    public bool Success { get; set; }
    
    /// <summary>
    /// Monitoring timestamp
    /// </summary>
    public DateTime MonitoringTimestamp { get; set; } = DateTime.UtcNow;
    
    /// <summary>
    /// Overall status
    /// </summary>
    public string OverallStatus { get; set; } = string.Empty;
    
    /// <summary>
    /// Component statuses
    /// </summary>
    public Dictionary<string, string> ComponentStatuses { get; set; } = new Dictionary<string, string>();
    
    /// <summary>
    /// System metrics
    /// </summary>
    public Dictionary<string, object> SystemMetrics { get; set; } = new Dictionary<string, object>();
    
    /// <summary>
    /// Error message if monitoring failed
    /// </summary>
    public string? ErrorMessage { get; set; }
}

/// <summary>
/// Represents a monitoring configuration
/// </summary>
public class MonitoringConfiguration
{
    /// <summary>
    /// Monitoring interval in seconds
    /// </summary>
    public int IntervalSeconds { get; set; } = 60;
    
    /// <summary>
    /// Whether to enable detailed metrics
    /// </summary>
    public bool EnableDetailedMetrics { get; set; } = false;
    
    /// <summary>
    /// Whether to enable alerts
    /// </summary>
    public bool EnableAlerts { get; set; } = true;
    
    /// <summary>
    /// Alert thresholds
    /// </summary>
    public Dictionary<string, object> AlertThresholds { get; set; } = new Dictionary<string, object>();
    
    /// <summary>
    /// Metrics to collect
    /// </summary>
    public List<string> MetricsToCollect { get; set; } = new List<string>();
}

/// <summary>
/// Represents a monitoring configuration result
/// </summary>
public class MonitoringConfigurationResult
{
    /// <summary>
    /// Whether the configuration was successful
    /// </summary>
    public bool Success { get; set; }
    
    /// <summary>
    /// Component ID
    /// </summary>
    public string ComponentId { get; set; } = string.Empty;
    
    /// <summary>
    /// Configuration timestamp
    /// </summary>
    public DateTime ConfigurationTimestamp { get; set; } = DateTime.UtcNow;
    
    /// <summary>
    /// Applied configuration
    /// </summary>
    public MonitoringConfiguration AppliedConfiguration { get; set; } = new MonitoringConfiguration();
    
    /// <summary>
    /// Error message if configuration failed
    /// </summary>
    public string? ErrorMessage { get; set; }
}
