namespace FlowOrchestrator.MessageQueueImporter;

/// <summary>
/// Represents a message from a message queue.
/// </summary>
public class MessageQueueMessage
{
    /// <summary>
    /// Gets or sets the message ID.
    /// </summary>
    public string MessageId { get; set; } = string.Empty;
    
    /// <summary>
    /// Gets or sets the message content.
    /// </summary>
    public string? Content { get; set; }
    
    /// <summary>
    /// Gets or sets the message timestamp.
    /// </summary>
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    
    /// <summary>
    /// Gets or sets the message properties.
    /// </summary>
    public Dictionary<string, object> Properties { get; set; } = new Dictionary<string, object>();
    
    /// <summary>
    /// Gets or sets the message headers.
    /// </summary>
    public Dictionary<string, object> Headers { get; set; } = new Dictionary<string, object>();
    
    /// <summary>
    /// Gets or sets the message delivery tag.
    /// </summary>
    public ulong DeliveryTag { get; set; }
    
    /// <summary>
    /// Gets or sets a value indicating whether the message is redelivered.
    /// </summary>
    public bool Redelivered { get; set; }
    
    /// <summary>
    /// Gets or sets the message exchange.
    /// </summary>
    public string Exchange { get; set; } = string.Empty;
    
    /// <summary>
    /// Gets or sets the message routing key.
    /// </summary>
    public string RoutingKey { get; set; } = string.Empty;
    
    /// <summary>
    /// Gets or sets the message consumer tag.
    /// </summary>
    public string ConsumerTag { get; set; } = string.Empty;
}
