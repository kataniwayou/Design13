namespace FlowOrchestrator.TransformationEngine;

/// <summary>
/// Represents the result of a rule audit operation.
/// </summary>
public class RuleAuditResult
{
    /// <summary>
    /// Gets or sets the rule ID.
    /// </summary>
    public string RuleId { get; set; } = string.Empty;
    
    /// <summary>
    /// Gets or sets the rule name.
    /// </summary>
    public string RuleName { get; set; } = string.Empty;
    
    /// <summary>
    /// Gets or sets the time range for the audit.
    /// </summary>
    public TimeRange TimeRange { get; set; } = TimeRange.LastDays(30);
    
    /// <summary>
    /// Gets or sets the total number of executions.
    /// </summary>
    public int TotalExecutions { get; set; }
    
    /// <summary>
    /// Gets or sets the number of successful executions.
    /// </summary>
    public int SuccessfulExecutions { get; set; }
    
    /// <summary>
    /// Gets or sets the number of failed executions.
    /// </summary>
    public int FailedExecutions { get; set; }
    
    /// <summary>
    /// Gets or sets the average execution duration in milliseconds.
    /// </summary>
    public double AverageExecutionDurationMs { get; set; }
    
    /// <summary>
    /// Gets or sets the users who executed the rule.
    /// </summary>
    public List<string> Users { get; set; } = new List<string>();
    
    /// <summary>
    /// Gets or sets the client applications that executed the rule.
    /// </summary>
    public List<string> ClientApplications { get; set; } = new List<string>();
    
    /// <summary>
    /// Gets or sets the execution frequency by day.
    /// </summary>
    public Dictionary<DateTime, int> ExecutionFrequencyByDay { get; set; } = new Dictionary<DateTime, int>();
    
    /// <summary>
    /// Gets or sets the execution frequency by hour.
    /// </summary>
    public Dictionary<int, int> ExecutionFrequencyByHour { get; set; } = new Dictionary<int, int>();
    
    /// <summary>
    /// Gets or sets the execution frequency by user.
    /// </summary>
    public Dictionary<string, int> ExecutionFrequencyByUser { get; set; } = new Dictionary<string, int>();
    
    /// <summary>
    /// Gets or sets the execution frequency by client application.
    /// </summary>
    public Dictionary<string, int> ExecutionFrequencyByClientApplication { get; set; } = new Dictionary<string, int>();
    
    /// <summary>
    /// Gets or sets the error frequency by error message.
    /// </summary>
    public Dictionary<string, int> ErrorFrequencyByMessage { get; set; } = new Dictionary<string, int>();
    
    /// <summary>
    /// Gets or sets the audit timestamp.
    /// </summary>
    public DateTime AuditTimestamp { get; set; } = DateTime.UtcNow;
    
    /// <summary>
    /// Gets or sets the additional information about the audit.
    /// </summary>
    public Dictionary<string, object> AdditionalInfo { get; set; } = new Dictionary<string, object>();
}
