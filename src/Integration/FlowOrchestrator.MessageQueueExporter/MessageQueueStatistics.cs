namespace FlowOrchestrator.MessageQueueExporter;

/// <summary>
/// Represents statistics for a message queue.
/// </summary>
public class MessageQueueStatistics
{
    /// <summary>
    /// Gets or sets the name of the queue.
    /// </summary>
    public string QueueName { get; set; } = string.Empty;
    
    /// <summary>
    /// Gets or sets the number of messages in the queue.
    /// </summary>
    public int MessageCount { get; set; }
    
    /// <summary>
    /// Gets or sets the number of consumers for the queue.
    /// </summary>
    public int ConsumerCount { get; set; }
    
    /// <summary>
    /// Gets or sets a value indicating whether the queue is durable.
    /// </summary>
    public bool Durable { get; set; }
    
    /// <summary>
    /// Gets or sets a value indicating whether the queue is exclusive.
    /// </summary>
    public bool Exclusive { get; set; }
    
    /// <summary>
    /// Gets or sets a value indicating whether the queue is auto-deleted.
    /// </summary>
    public bool AutoDelete { get; set; }
    
    /// <summary>
    /// Gets or sets the queue arguments.
    /// </summary>
    public Dictionary<string, object> Arguments { get; set; } = new Dictionary<string, object>();
}
