using FlowOrchestrator.Domain.Models;

namespace FlowOrchestrator.TaskScheduler;

/// <summary>
/// Represents a scheduled task
/// </summary>
public class ScheduledTask
{
    /// <summary>
    /// Task ID
    /// </summary>
    public string TaskId { get; set; } = string.Empty;
    
    /// <summary>
    /// Task name
    /// </summary>
    public string Name { get; set; } = string.Empty;
    
    /// <summary>
    /// Task description
    /// </summary>
    public string Description { get; set; } = string.Empty;
    
    /// <summary>
    /// Task type
    /// </summary>
    public string TaskType { get; set; } = string.Empty;
    
    /// <summary>
    /// Task parameters
    /// </summary>
    public Dictionary<string, object> Parameters { get; set; } = new Dictionary<string, object>();
    
    /// <summary>
    /// Scheduled execution time
    /// </summary>
    public DateTime ScheduledTime { get; set; }
    
    /// <summary>
    /// Task priority
    /// </summary>
    public TaskPriority Priority { get; set; } = TaskPriority.Normal;
    
    /// <summary>
    /// Task status
    /// </summary>
    public TaskStatus Status { get; set; } = TaskStatus.Scheduled;
    
    /// <summary>
    /// Task creation timestamp
    /// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    /// <summary>
    /// Task creator
    /// </summary>
    public string CreatedBy { get; set; } = string.Empty;
    
    /// <summary>
    /// Task last modified timestamp
    /// </summary>
    public DateTime LastModifiedAt { get; set; } = DateTime.UtcNow;
    
    /// <summary>
    /// Task last modifier
    /// </summary>
    public string LastModifiedBy { get; set; } = string.Empty;
    
    /// <summary>
    /// Task execution timeout in seconds
    /// </summary>
    public int TimeoutSeconds { get; set; } = 300;
    
    /// <summary>
    /// Maximum number of retries
    /// </summary>
    public int MaxRetries { get; set; } = 3;
    
    /// <summary>
    /// Retry delay in seconds
    /// </summary>
    public int RetryDelaySeconds { get; set; } = 60;
    
    /// <summary>
    /// Whether to use exponential backoff for retries
    /// </summary>
    public bool UseExponentialBackoff { get; set; } = true;
    
    /// <summary>
    /// Task tags
    /// </summary>
    public List<string> Tags { get; set; } = new List<string>();
    
    /// <summary>
    /// Task metadata
    /// </summary>
    public Dictionary<string, string> Metadata { get; set; } = new Dictionary<string, string>();
    
    /// <summary>
    /// ID of the recurring task definition that created this task
    /// </summary>
    public string? RecurringTaskDefinitionId { get; set; }
}

/// <summary>
/// Represents the result of a task scheduling operation
/// </summary>
public class TaskScheduleResult
{
    /// <summary>
    /// Whether the operation was successful
    /// </summary>
    public bool Success { get; set; }
    
    /// <summary>
    /// Task ID
    /// </summary>
    public string TaskId { get; set; } = string.Empty;
    
    /// <summary>
    /// Scheduled execution time
    /// </summary>
    public DateTime ScheduledTime { get; set; }
    
    /// <summary>
    /// Error message if operation failed
    /// </summary>
    public string? ErrorMessage { get; set; }
    
    /// <summary>
    /// Validation issues found during scheduling
    /// </summary>
    public List<ValidationIssue> ValidationIssues { get; set; } = new List<ValidationIssue>();
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
/// Represents a recurring task definition
/// </summary>
public class RecurringTaskDefinition
{
    /// <summary>
    /// Definition ID
    /// </summary>
    public string DefinitionId { get; set; } = string.Empty;
    
    /// <summary>
    /// Definition name
    /// </summary>
    public string Name { get; set; } = string.Empty;
    
    /// <summary>
    /// Definition description
    /// </summary>
    public string Description { get; set; } = string.Empty;
    
    /// <summary>
    /// Task type
    /// </summary>
    public string TaskType { get; set; } = string.Empty;
    
    /// <summary>
    /// Task parameters
    /// </summary>
    public Dictionary<string, object> Parameters { get; set; } = new Dictionary<string, object>();
    
    /// <summary>
    /// Schedule expression (cron format)
    /// </summary>
    public string ScheduleExpression { get; set; } = string.Empty;
    
    /// <summary>
    /// Task priority
    /// </summary>
    public TaskPriority Priority { get; set; } = TaskPriority.Normal;
    
    /// <summary>
    /// Whether the definition is enabled
    /// </summary>
    public bool IsEnabled { get; set; } = true;
    
    /// <summary>
    /// Start date
    /// </summary>
    public DateTime? StartDate { get; set; }
    
    /// <summary>
    /// End date
    /// </summary>
    public DateTime? EndDate { get; set; }
    
    /// <summary>
    /// Task execution timeout in seconds
    /// </summary>
    public int TimeoutSeconds { get; set; } = 300;
    
    /// <summary>
    /// Maximum number of retries
    /// </summary>
    public int MaxRetries { get; set; } = 3;
    
    /// <summary>
    /// Retry delay in seconds
    /// </summary>
    public int RetryDelaySeconds { get; set; } = 60;
    
    /// <summary>
    /// Whether to use exponential backoff for retries
    /// </summary>
    public bool UseExponentialBackoff { get; set; } = true;
    
    /// <summary>
    /// Task tags
    /// </summary>
    public List<string> Tags { get; set; } = new List<string>();
    
    /// <summary>
    /// Task metadata
    /// </summary>
    public Dictionary<string, string> Metadata { get; set; } = new Dictionary<string, string>();
    
    /// <summary>
    /// Definition creation timestamp
    /// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    /// <summary>
    /// Definition creator
    /// </summary>
    public string CreatedBy { get; set; } = string.Empty;
    
    /// <summary>
    /// Definition last modified timestamp
    /// </summary>
    public DateTime LastModifiedAt { get; set; } = DateTime.UtcNow;
    
    /// <summary>
    /// Definition last modifier
    /// </summary>
    public string LastModifiedBy { get; set; } = string.Empty;
}
