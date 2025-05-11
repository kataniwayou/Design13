namespace FlowOrchestrator.Abstractions.Protocols;

/// <summary>
/// Represents the result of a protocol operation.
/// </summary>
public class ProtocolOperationResult
{
    /// <summary>
    /// Gets or sets whether the operation was successful.
    /// </summary>
    public bool Success { get; set; }
    
    /// <summary>
    /// Gets or sets the error message if the operation failed.
    /// </summary>
    public string? ErrorMessage { get; set; }
    
    /// <summary>
    /// Gets or sets the data returned by the operation.
    /// </summary>
    public byte[]? Data { get; set; }
    
    /// <summary>
    /// Gets or sets the metadata associated with the operation.
    /// </summary>
    public Dictionary<string, string> Metadata { get; set; } = new Dictionary<string, string>();
    
    /// <summary>
    /// Gets or sets the timestamp when the operation started.
    /// </summary>
    public DateTime StartTimestamp { get; set; }
    
    /// <summary>
    /// Gets or sets the timestamp when the operation completed.
    /// </summary>
    public DateTime EndTimestamp { get; set; }
    
    /// <summary>
    /// Gets the duration of the operation.
    /// </summary>
    public TimeSpan Duration => EndTimestamp - StartTimestamp;
    
    /// <summary>
    /// Gets or sets the status code returned by the operation.
    /// </summary>
    public int? StatusCode { get; set; }
    
    /// <summary>
    /// Gets or sets the status message returned by the operation.
    /// </summary>
    public string? StatusMessage { get; set; }
    
    /// <summary>
    /// Gets or sets the resource affected by the operation.
    /// </summary>
    public string? Resource { get; set; }
    
    /// <summary>
    /// Gets or sets the operation type.
    /// </summary>
    public string? OperationType { get; set; }
    
    /// <summary>
    /// Gets or sets the number of bytes transferred during the operation.
    /// </summary>
    public long? BytesTransferred { get; set; }
    
    /// <summary>
    /// Gets or sets the transfer rate in bytes per second.
    /// </summary>
    public double? TransferRateBytesPerSecond { get; set; }
}
