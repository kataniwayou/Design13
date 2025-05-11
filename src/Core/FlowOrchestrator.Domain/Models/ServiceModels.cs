namespace FlowOrchestrator.Domain.Models;

/// <summary>
/// Represents a service status update
/// </summary>
public class ServiceStatusUpdate
{
    /// <summary>
    /// Service ID
    /// </summary>
    public string ServiceId { get; set; } = string.Empty;
    
    /// <summary>
    /// Service version
    /// </summary>
    public string Version { get; set; } = string.Empty;
    
    /// <summary>
    /// New service status
    /// </summary>
    public ServiceStatus Status { get; set; }
    
    /// <summary>
    /// Status update reason
    /// </summary>
    public string? Reason { get; set; }
    
    /// <summary>
    /// Status update timestamp
    /// </summary>
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    
    /// <summary>
    /// Status update initiator
    /// </summary>
    public string UpdatedBy { get; set; } = string.Empty;
}

/// <summary>
/// Represents a service lifecycle operation result
/// </summary>
public class ServiceLifecycleResult
{
    /// <summary>
    /// Whether the operation was successful
    /// </summary>
    public bool Success { get; set; }
    
    /// <summary>
    /// Service ID
    /// </summary>
    public string ServiceId { get; set; } = string.Empty;
    
    /// <summary>
    /// Service version
    /// </summary>
    public string Version { get; set; } = string.Empty;
    
    /// <summary>
    /// Operation type
    /// </summary>
    public string OperationType { get; set; } = string.Empty;
    
    /// <summary>
    /// Error message if operation failed
    /// </summary>
    public string? ErrorMessage { get; set; }
    
    /// <summary>
    /// Operation timestamp
    /// </summary>
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;
}

/// <summary>
/// Represents a dependency validation result
/// </summary>
public class DependencyValidationResult
{
    /// <summary>
    /// Whether the dependencies are valid
    /// </summary>
    public bool IsValid { get; set; }
    
    /// <summary>
    /// Service ID
    /// </summary>
    public string ServiceId { get; set; } = string.Empty;
    
    /// <summary>
    /// Service version
    /// </summary>
    public string Version { get; set; } = string.Empty;
    
    /// <summary>
    /// Validation issues
    /// </summary>
    public List<ValidationIssue> ValidationIssues { get; set; } = new List<ValidationIssue>();
}

/// <summary>
/// Represents a dependency resolution result
/// </summary>
public class DependencyResolutionResult
{
    /// <summary>
    /// Whether the resolution was successful
    /// </summary>
    public bool Success { get; set; }
    
    /// <summary>
    /// Service ID
    /// </summary>
    public string ServiceId { get; set; } = string.Empty;
    
    /// <summary>
    /// Service version
    /// </summary>
    public string Version { get; set; } = string.Empty;
    
    /// <summary>
    /// Resolved dependencies
    /// </summary>
    public List<ResolvedDependency> ResolvedDependencies { get; set; } = new List<ResolvedDependency>();
    
    /// <summary>
    /// Unresolved dependencies
    /// </summary>
    public List<UnresolvedDependency> UnresolvedDependencies { get; set; } = new List<UnresolvedDependency>();
}

/// <summary>
/// Represents a resolved dependency
/// </summary>
public class ResolvedDependency
{
    /// <summary>
    /// Dependency service ID
    /// </summary>
    public string ServiceId { get; set; } = string.Empty;
    
    /// <summary>
    /// Dependency version
    /// </summary>
    public string Version { get; set; } = string.Empty;
    
    /// <summary>
    /// Dependency endpoint
    /// </summary>
    public string Endpoint { get; set; } = string.Empty;
}

/// <summary>
/// Represents an unresolved dependency
/// </summary>
public class UnresolvedDependency
{
    /// <summary>
    /// Dependency service ID
    /// </summary>
    public string ServiceId { get; set; } = string.Empty;
    
    /// <summary>
    /// Dependency version range
    /// </summary>
    public string VersionRange { get; set; } = string.Empty;
    
    /// <summary>
    /// Reason for not resolving
    /// </summary>
    public string Reason { get; set; } = string.Empty;
}

/// <summary>
/// Represents service metrics
/// </summary>
public class ServiceMetrics
{
    /// <summary>
    /// Service ID
    /// </summary>
    public string ServiceId { get; set; } = string.Empty;
    
    /// <summary>
    /// Service version
    /// </summary>
    public string Version { get; set; } = string.Empty;
    
    /// <summary>
    /// Time range
    /// </summary>
    public TimeRange TimeRange { get; set; } = new TimeRange();
    
    /// <summary>
    /// Request count
    /// </summary>
    public int RequestCount { get; set; }
    
    /// <summary>
    /// Success count
    /// </summary>
    public int SuccessCount { get; set; }
    
    /// <summary>
    /// Error count
    /// </summary>
    public int ErrorCount { get; set; }
    
    /// <summary>
    /// Average response time in milliseconds
    /// </summary>
    public double AverageResponseTimeMs { get; set; }
    
    /// <summary>
    /// CPU usage percentage
    /// </summary>
    public double CpuUsagePercentage { get; set; }
    
    /// <summary>
    /// Memory usage in megabytes
    /// </summary>
    public double MemoryUsageMb { get; set; }
    
    /// <summary>
    /// Detailed metrics
    /// </summary>
    public Dictionary<string, object> DetailedMetrics { get; set; } = new Dictionary<string, object>();
}

/// <summary>
/// Represents a service usage report
/// </summary>
public class ServiceUsageReport
{
    /// <summary>
    /// Service ID
    /// </summary>
    public string ServiceId { get; set; } = string.Empty;
    
    /// <summary>
    /// Service version
    /// </summary>
    public string Version { get; set; } = string.Empty;
    
    /// <summary>
    /// Time range
    /// </summary>
    public TimeRange TimeRange { get; set; } = new TimeRange();
    
    /// <summary>
    /// Total requests
    /// </summary>
    public int TotalRequests { get; set; }
    
    /// <summary>
    /// Unique clients
    /// </summary>
    public int UniqueClients { get; set; }
    
    /// <summary>
    /// Usage by client
    /// </summary>
    public Dictionary<string, int> UsageByClient { get; set; } = new Dictionary<string, int>();
    
    /// <summary>
    /// Usage by operation
    /// </summary>
    public Dictionary<string, int> UsageByOperation { get; set; } = new Dictionary<string, int>();
    
    /// <summary>
    /// Usage by time period
    /// </summary>
    public Dictionary<string, int> UsageByTimePeriod { get; set; } = new Dictionary<string, int>();
}

/// <summary>
/// Represents a service audit log
/// </summary>
public class ServiceAuditLog
{
    /// <summary>
    /// Service ID
    /// </summary>
    public string ServiceId { get; set; } = string.Empty;
    
    /// <summary>
    /// Service version
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

/// <summary>
/// Represents an audit entry
/// </summary>
public class AuditEntry
{
    /// <summary>
    /// Entry ID
    /// </summary>
    public string EntryId { get; set; } = string.Empty;
    
    /// <summary>
    /// Timestamp
    /// </summary>
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    
    /// <summary>
    /// User
    /// </summary>
    public string User { get; set; } = string.Empty;
    
    /// <summary>
    /// Action
    /// </summary>
    public string Action { get; set; } = string.Empty;
    
    /// <summary>
    /// Resource
    /// </summary>
    public string Resource { get; set; } = string.Empty;
    
    /// <summary>
    /// Details
    /// </summary>
    public string Details { get; set; } = string.Empty;
    
    /// <summary>
    /// IP address
    /// </summary>
    public string IpAddress { get; set; } = string.Empty;
    
    /// <summary>
    /// User agent
    /// </summary>
    public string UserAgent { get; set; } = string.Empty;
}
