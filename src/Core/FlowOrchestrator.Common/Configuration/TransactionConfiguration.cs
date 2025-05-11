namespace FlowOrchestrator.Common.Configuration;

/// <summary>
/// Represents the transaction configuration.
/// </summary>
public class TransactionConfiguration
{
    /// <summary>
    /// Gets or sets a value indicating whether transactions are enabled.
    /// </summary>
    public bool Enabled { get; set; }
    
    /// <summary>
    /// Gets or sets the isolation level.
    /// </summary>
    public string IsolationLevel { get; set; } = "ReadCommitted";
    
    /// <summary>
    /// Gets or sets the transaction timeout in seconds.
    /// </summary>
    public int TimeoutSeconds { get; set; } = 60;
    
    /// <summary>
    /// Gets or sets a value indicating whether to use distributed transactions.
    /// </summary>
    public bool UseDistributedTransactions { get; set; }
    
    /// <summary>
    /// Gets or sets the additional parameters for this configuration.
    /// </summary>
    public Dictionary<string, object> Parameters { get; set; } = new Dictionary<string, object>();
}
