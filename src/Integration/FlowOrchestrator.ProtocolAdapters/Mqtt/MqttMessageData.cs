namespace FlowOrchestrator.ProtocolAdapters.Mqtt;

/// <summary>
/// Represents MQTT message data.
/// </summary>
public class MqttMessageData
{
    /// <summary>
    /// Gets or sets the topic.
    /// </summary>
    public string Topic { get; set; } = string.Empty;
    
    /// <summary>
    /// Gets or sets the payload.
    /// </summary>
    public string Payload { get; set; } = string.Empty;
    
    /// <summary>
    /// Gets or sets the quality of service.
    /// </summary>
    public int QualityOfService { get; set; } = 1;
    
    /// <summary>
    /// Gets or sets a value indicating whether to retain the message.
    /// </summary>
    public bool Retain { get; set; } = false;
    
    /// <summary>
    /// Gets or sets the message ID.
    /// </summary>
    public int MessageId { get; set; }
    
    /// <summary>
    /// Gets or sets a value indicating whether the message is a duplicate.
    /// </summary>
    public bool Duplicate { get; set; } = false;
    
    /// <summary>
    /// Gets or sets the timestamp of the message.
    /// </summary>
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    
    /// <summary>
    /// Gets or sets the user properties.
    /// </summary>
    public Dictionary<string, string> UserProperties { get; set; } = new Dictionary<string, string>();
    
    /// <summary>
    /// Gets or sets the content type.
    /// </summary>
    public string? ContentType { get; set; }
    
    /// <summary>
    /// Gets or sets the response topic.
    /// </summary>
    public string? ResponseTopic { get; set; }
    
    /// <summary>
    /// Gets or sets the correlation data.
    /// </summary>
    public byte[]? CorrelationData { get; set; }
    
    /// <summary>
    /// Gets or sets the message expiry interval in seconds.
    /// </summary>
    public int? MessageExpiryIntervalSeconds { get; set; }
    
    /// <summary>
    /// Gets or sets the payload format indicator.
    /// </summary>
    public int? PayloadFormatIndicator { get; set; }
}
