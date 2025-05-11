namespace FlowOrchestrator.Domain.Models;

/// <summary>
/// Service status enumeration
/// </summary>
public enum ServiceStatus
{
    /// <summary>
    /// Unknown status
    /// </summary>
    Unknown,
    
    /// <summary>
    /// Service is starting
    /// </summary>
    Starting,
    
    /// <summary>
    /// Service is running
    /// </summary>
    Running,
    
    /// <summary>
    /// Service is stopping
    /// </summary>
    Stopping,
    
    /// <summary>
    /// Service is stopped
    /// </summary>
    Stopped,
    
    /// <summary>
    /// Service is degraded
    /// </summary>
    Degraded,
    
    /// <summary>
    /// Service has failed
    /// </summary>
    Failed
}

/// <summary>
/// Dependency type enumeration
/// </summary>
public enum DependencyType
{
    /// <summary>
    /// Runtime dependency
    /// </summary>
    Runtime,
    
    /// <summary>
    /// Build-time dependency
    /// </summary>
    BuildTime,
    
    /// <summary>
    /// Optional dependency
    /// </summary>
    Optional
}

/// <summary>
/// Service type enumeration
/// </summary>
public enum ServiceType
{
    /// <summary>
    /// Core service
    /// </summary>
    Core,
    
    /// <summary>
    /// Infrastructure service
    /// </summary>
    Infrastructure,
    
    /// <summary>
    /// Processing service
    /// </summary>
    Processing,
    
    /// <summary>
    /// Integration service
    /// </summary>
    Integration,
    
    /// <summary>
    /// Management service
    /// </summary>
    Management,
    
    /// <summary>
    /// Observability service
    /// </summary>
    Observability
}

/// <summary>
/// Deployment status enumeration
/// </summary>
public enum DeploymentStatus
{
    /// <summary>
    /// Pending status
    /// </summary>
    Pending,
    
    /// <summary>
    /// Deploying status
    /// </summary>
    Deploying,
    
    /// <summary>
    /// Deployed status
    /// </summary>
    Deployed,
    
    /// <summary>
    /// Failed status
    /// </summary>
    Failed,
    
    /// <summary>
    /// Undeploying status
    /// </summary>
    Undeploying,
    
    /// <summary>
    /// Undeployed status
    /// </summary>
    Undeployed
}

/// <summary>
/// Compatibility level enumeration
/// </summary>
public enum CompatibilityLevel
{
    /// <summary>
    /// Unknown compatibility
    /// </summary>
    Unknown,
    
    /// <summary>
    /// Incompatible
    /// </summary>
    Incompatible,
    
    /// <summary>
    /// Partially compatible
    /// </summary>
    PartiallyCompatible,
    
    /// <summary>
    /// Fully compatible
    /// </summary>
    FullyCompatible
}

/// <summary>
/// Version status enumeration
/// </summary>
public enum VersionStatus
{
    /// <summary>
    /// Draft status
    /// </summary>
    Draft,
    
    /// <summary>
    /// Active status
    /// </summary>
    Active,
    
    /// <summary>
    /// Deprecated status
    /// </summary>
    Deprecated,
    
    /// <summary>
    /// Archived status
    /// </summary>
    Archived
}

/// <summary>
/// Version retention policy enumeration
/// </summary>
public enum VersionRetentionPolicy
{
    /// <summary>
    /// Keep all versions
    /// </summary>
    KeepAllVersions,
    
    /// <summary>
    /// Keep only latest versions
    /// </summary>
    KeepLatestVersions,
    
    /// <summary>
    /// Keep only major versions
    /// </summary>
    KeepMajorVersions,
    
    /// <summary>
    /// Keep only major and minor versions
    /// </summary>
    KeepMajorMinorVersions
}

/// <summary>
/// Task priority enumeration
/// </summary>
public enum TaskPriority
{
    /// <summary>
    /// Low priority
    /// </summary>
    Low,
    
    /// <summary>
    /// Normal priority
    /// </summary>
    Normal,
    
    /// <summary>
    /// High priority
    /// </summary>
    High,
    
    /// <summary>
    /// Critical priority
    /// </summary>
    Critical
}

/// <summary>
/// Task status enumeration
/// </summary>
public enum TaskStatus
{
    /// <summary>
    /// Scheduled status
    /// </summary>
    Scheduled,
    
    /// <summary>
    /// Running status
    /// </summary>
    Running,
    
    /// <summary>
    /// Completed status
    /// </summary>
    Completed,
    
    /// <summary>
    /// Failed status
    /// </summary>
    Failed,
    
    /// <summary>
    /// Cancelled status
    /// </summary>
    Cancelled,
    
    /// <summary>
    /// Paused status
    /// </summary>
    Paused,
    
    /// <summary>
    /// Waiting for retry status
    /// </summary>
    WaitingForRetry
}

/// <summary>
/// Execution status enumeration
/// </summary>
public enum ExecutionStatus
{
    /// <summary>
    /// Pending status
    /// </summary>
    Pending,
    
    /// <summary>
    /// Running status
    /// </summary>
    Running,
    
    /// <summary>
    /// Completed status
    /// </summary>
    Completed,
    
    /// <summary>
    /// Failed status
    /// </summary>
    Failed,
    
    /// <summary>
    /// Cancelled status
    /// </summary>
    Cancelled,
    
    /// <summary>
    /// Timed out status
    /// </summary>
    TimedOut,
    
    /// <summary>
    /// Waiting for retry status
    /// </summary>
    WaitingForRetry
}

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
/// Configuration change type enumeration
/// </summary>
public enum ConfigurationChangeType
{
    /// <summary>
    /// Create operation
    /// </summary>
    Create,
    
    /// <summary>
    /// Update operation
    /// </summary>
    Update,
    
    /// <summary>
    /// Delete operation
    /// </summary>
    Delete,
    
    /// <summary>
    /// Revert operation
    /// </summary>
    Revert
}
