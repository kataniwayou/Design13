namespace FlowOrchestrator.TransformationEngine;

/// <summary>
/// Represents the execution history for a transformation rule.
/// </summary>
public class RuleExecutionHistory
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
    /// Gets or sets the execution records.
    /// </summary>
    public List<RuleExecutionRecord> ExecutionRecords { get; set; } = new List<RuleExecutionRecord>();
    
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
    /// Gets or sets the minimum execution duration in milliseconds.
    /// </summary>
    public long MinExecutionDurationMs { get; set; }
    
    /// <summary>
    /// Gets or sets the maximum execution duration in milliseconds.
    /// </summary>
    public long MaxExecutionDurationMs { get; set; }
    
    /// <summary>
    /// Gets or sets the first execution timestamp.
    /// </summary>
    public DateTime FirstExecutionTimestamp { get; set; }
    
    /// <summary>
    /// Gets or sets the last execution timestamp.
    /// </summary>
    public DateTime LastExecutionTimestamp { get; set; }
    
    /// <summary>
    /// Gets or sets the additional information about the execution history.
    /// </summary>
    public Dictionary<string, object> AdditionalInfo { get; set; } = new Dictionary<string, object>();
}
