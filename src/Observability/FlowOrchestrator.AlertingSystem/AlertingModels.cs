namespace FlowOrchestrator.AlertingSystem;

/// <summary>
/// Alert severity enumeration
/// </summary>
public enum AlertSeverity
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
/// Alert status enumeration
/// </summary>
public enum AlertStatus
{
    /// <summary>
    /// Active status
    /// </summary>
    Active,
    
    /// <summary>
    /// Acknowledged status
    /// </summary>
    Acknowledged,
    
    /// <summary>
    /// Resolved status
    /// </summary>
    Resolved,
    
    /// <summary>
    /// Closed status
    /// </summary>
    Closed
}

/// <summary>
/// Represents an alert
/// </summary>
public class Alert
{
    /// <summary>
    /// Alert ID
    /// </summary>
    public string AlertId { get; set; } = string.Empty;
    
    /// <summary>
    /// Alert severity
    /// </summary>
    public AlertSeverity Severity { get; set; }
    
    /// <summary>
    /// Alert source
    /// </summary>
    public string Source { get; set; } = string.Empty;
    
    /// <summary>
    /// Alert message
    /// </summary>
    public string Message { get; set; } = string.Empty;
    
    /// <summary>
    /// Alert timestamp
    /// </summary>
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    
    /// <summary>
    /// Alert status
    /// </summary>
    public AlertStatus Status { get; set; } = AlertStatus.Active;
    
    /// <summary>
    /// Alert details
    /// </summary>
    public Dictionary<string, object> Details { get; set; } = new Dictionary<string, object>();
    
    /// <summary>
    /// Related entity ID
    /// </summary>
    public string? RelatedEntityId { get; set; }
    
    /// <summary>
    /// Related entity type
    /// </summary>
    public string? RelatedEntityType { get; set; }
}

/// <summary>
/// Represents an alert update
/// </summary>
public class AlertUpdate
{
    /// <summary>
    /// New alert severity
    /// </summary>
    public AlertSeverity? Severity { get; set; }
    
    /// <summary>
    /// New alert message
    /// </summary>
    public string? Message { get; set; }
    
    /// <summary>
    /// New alert status
    /// </summary>
    public AlertStatus? Status { get; set; }
    
    /// <summary>
    /// Details to add or update
    /// </summary>
    public Dictionary<string, object> Details { get; set; } = new Dictionary<string, object>();
    
    /// <summary>
    /// Update reason
    /// </summary>
    public string? Reason { get; set; }
    
    /// <summary>
    /// Update user
    /// </summary>
    public string? UpdatedBy { get; set; }
}

/// <summary>
/// Represents an alert acknowledgement
/// </summary>
public class AlertAcknowledgement
{
    /// <summary>
    /// Acknowledgement user
    /// </summary>
    public string AcknowledgedBy { get; set; } = string.Empty;
    
    /// <summary>
    /// Acknowledgement notes
    /// </summary>
    public string? Notes { get; set; }
    
    /// <summary>
    /// Estimated resolution time
    /// </summary>
    public DateTime? EstimatedResolutionTime { get; set; }
}

/// <summary>
/// Represents an alert resolution
/// </summary>
public class AlertResolution
{
    /// <summary>
    /// Resolution user
    /// </summary>
    public string ResolvedBy { get; set; } = string.Empty;
    
    /// <summary>
    /// Resolution notes
    /// </summary>
    public string? Notes { get; set; }
    
    /// <summary>
    /// Resolution action
    /// </summary>
    public string? Action { get; set; }
    
    /// <summary>
    /// Root cause
    /// </summary>
    public string? RootCause { get; set; }
}

/// <summary>
/// Represents an alert query
/// </summary>
public class AlertQuery
{
    /// <summary>
    /// Alert severity filter
    /// </summary>
    public AlertSeverity? Severity { get; set; }
    
    /// <summary>
    /// Alert status filter
    /// </summary>
    public AlertStatus? Status { get; set; }
    
    /// <summary>
    /// Alert source filter
    /// </summary>
    public string? Source { get; set; }
    
    /// <summary>
    /// Start time filter
    /// </summary>
    public DateTime? StartTime { get; set; }
    
    /// <summary>
    /// End time filter
    /// </summary>
    public DateTime? EndTime { get; set; }
    
    /// <summary>
    /// Related entity ID filter
    /// </summary>
    public string? RelatedEntityId { get; set; }
    
    /// <summary>
    /// Related entity type filter
    /// </summary>
    public string? RelatedEntityType { get; set; }
    
    /// <summary>
    /// Maximum number of results to return
    /// </summary>
    public int MaxResults { get; set; } = 100;
}

/// <summary>
/// Represents an alerting configuration
/// </summary>
public class AlertingConfiguration
{
    /// <summary>
    /// Whether to enable alerting
    /// </summary>
    public bool EnableAlerting { get; set; } = true;
    
    /// <summary>
    /// Minimum severity to alert on
    /// </summary>
    public AlertSeverity MinimumSeverity { get; set; } = AlertSeverity.Warning;
    
    /// <summary>
    /// Alert thresholds
    /// </summary>
    public Dictionary<string, object> AlertThresholds { get; set; } = new Dictionary<string, object>();
    
    /// <summary>
    /// Notification channels
    /// </summary>
    public List<string> NotificationChannels { get; set; } = new List<string>();
}
