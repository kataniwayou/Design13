namespace FlowOrchestrator.MessageQueueExporter;

/// <summary>
/// Represents a message for a message queue.
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
    /// Gets or sets the message type.
    /// </summary>
    public string? Type { get; set; }
    
    /// <summary>
    /// Gets or sets the message content type.
    /// </summary>
    public string? ContentType { get; set; }
    
    /// <summary>
    /// Gets or sets the message content encoding.
    /// </summary>
    public string? ContentEncoding { get; set; }
    
    /// <summary>
    /// Gets or sets the message priority.
    /// </summary>
    public byte? Priority { get; set; }
    
    /// <summary>
    /// Gets or sets the message correlation ID.
    /// </summary>
    public string? CorrelationId { get; set; }
    
    /// <summary>
    /// Gets or sets the message reply to.
    /// </summary>
    public string? ReplyTo { get; set; }
    
    /// <summary>
    /// Gets or sets the message expiration.
    /// </summary>
    public string? Expiration { get; set; }
    
    /// <summary>
    /// Gets or sets the message app ID.
    /// </summary>
    public string? AppId { get; set; }
    
    /// <summary>
    /// Gets or sets the message user ID.
    /// </summary>
    public string? UserId { get; set; }
}
