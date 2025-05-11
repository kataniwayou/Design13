namespace FlowOrchestrator.TransformationEngine;

/// <summary>
/// Represents the error history for a rule.
/// </summary>
public class ErrorHistory
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
    /// Gets or sets the time range for the history.
    /// </summary>
    public TimeRange TimeRange { get; set; } = TimeRange.LastDays(30);
    
    /// <summary>
    /// Gets or sets the error records.
    /// </summary>
    public List<ErrorRecord> ErrorRecords { get; set; } = new List<ErrorRecord>();
    
    /// <summary>
    /// Gets or sets the total number of errors.
    /// </summary>
    public int TotalErrors { get; set; }
    
    /// <summary>
    /// Gets or sets the error frequency by error code.
    /// </summary>
    public Dictionary<string, int> ErrorFrequencyByCode { get; set; } = new Dictionary<string, int>();
    
    /// <summary>
    /// Gets or sets the error frequency by error message.
    /// </summary>
    public Dictionary<string, int> ErrorFrequencyByMessage { get; set; } = new Dictionary<string, int>();
    
    /// <summary>
    /// Gets or sets the error frequency by day.
    /// </summary>
    public Dictionary<DateTime, int> ErrorFrequencyByDay { get; set; } = new Dictionary<DateTime, int>();
    
    /// <summary>
    /// Gets or sets the error frequency by hour.
    /// </summary>
    public Dictionary<int, int> ErrorFrequencyByHour { get; set; } = new Dictionary<int, int>();
    
    /// <summary>
    /// Gets or sets the error frequency by severity.
    /// </summary>
    public Dictionary<ErrorSeverity, int> ErrorFrequencyBySeverity { get; set; } = new Dictionary<ErrorSeverity, int>();
    
    /// <summary>
    /// Gets or sets the most common error.
    /// </summary>
    public ErrorRecord? MostCommonError { get; set; }
    
    /// <summary>
    /// Gets or sets the most recent error.
    /// </summary>
    public ErrorRecord? MostRecentError { get; set; }
    
    /// <summary>
    /// Gets or sets the most severe error.
    /// </summary>
    public ErrorRecord? MostSevereError { get; set; }
    
    /// <summary>
    /// Gets or sets the history timestamp.
    /// </summary>
    public DateTime HistoryTimestamp { get; set; } = DateTime.UtcNow;
    
    /// <summary>
    /// Gets or sets the additional information about the history.
    /// </summary>
    public Dictionary<string, object> AdditionalInfo { get; set; } = new Dictionary<string, object>();
}
