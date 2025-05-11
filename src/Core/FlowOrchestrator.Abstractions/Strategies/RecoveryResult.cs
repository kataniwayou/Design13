namespace FlowOrchestrator.Abstractions.Strategies;

/// <summary>
/// Represents the result of a recovery operation.
/// </summary>
public class RecoveryResult
{
    /// <summary>
    /// Gets or sets whether the recovery was successful.
    /// </summary>
    public bool Success { get; set; }
    
    /// <summary>
    /// Gets or sets the error message if the recovery failed.
    /// </summary>
    public string? ErrorMessage { get; set; }
    
    /// <summary>
    /// Gets or sets the recovery action that was taken.
    /// </summary>
    public RecoveryAction Action { get; set; }
    
    /// <summary>
    /// Gets or sets the timestamp when the recovery started.
    /// </summary>
    public DateTime StartTimestamp { get; set; }
    
    /// <summary>
    /// Gets or sets the timestamp when the recovery completed.
    /// </summary>
    public DateTime EndTimestamp { get; set; }
    
    /// <summary>
    /// Gets the duration of the recovery operation.
    /// </summary>
    public TimeSpan Duration => EndTimestamp - StartTimestamp;
    
    /// <summary>
    /// Gets or sets the metadata associated with the recovery.
    /// </summary>
    public Dictionary<string, string> Metadata { get; set; } = new Dictionary<string, string>();
    
    /// <summary>
    /// Gets or sets the memory address of the recovered data.
    /// </summary>
    public string? RecoveredDataMemoryAddress { get; set; }
    
    /// <summary>
    /// Gets or sets the next step ID to execute after recovery.
    /// </summary>
    public string? NextStepId { get; set; }
    
    /// <summary>
    /// Gets or sets the next branch path to execute after recovery.
    /// </summary>
    public string? NextBranchPath { get; set; }
}
