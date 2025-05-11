namespace FlowOrchestrator.Abstractions.Protocols;

/// <summary>
/// Represents the capabilities of a protocol.
/// </summary>
public class ProtocolCapabilities
{
    /// <summary>
    /// Gets or sets whether the protocol supports reading data.
    /// </summary>
    public bool SupportsRead { get; set; }
    
    /// <summary>
    /// Gets or sets whether the protocol supports writing data.
    /// </summary>
    public bool SupportsWrite { get; set; }
    
    /// <summary>
    /// Gets or sets whether the protocol supports deleting data.
    /// </summary>
    public bool SupportsDelete { get; set; }
    
    /// <summary>
    /// Gets or sets whether the protocol supports listing resources.
    /// </summary>
    public bool SupportsList { get; set; }
    
    /// <summary>
    /// Gets or sets whether the protocol supports querying data.
    /// </summary>
    public bool SupportsQuery { get; set; }
    
    /// <summary>
    /// Gets or sets whether the protocol supports streaming data.
    /// </summary>
    public bool SupportsStreaming { get; set; }
    
    /// <summary>
    /// Gets or sets whether the protocol supports batch operations.
    /// </summary>
    public bool SupportsBatch { get; set; }
    
    /// <summary>
    /// Gets or sets whether the protocol supports transactions.
    /// </summary>
    public bool SupportsTransactions { get; set; }
    
    /// <summary>
    /// Gets or sets whether the protocol supports schema discovery.
    /// </summary>
    public bool SupportsSchemaDiscovery { get; set; }
    
    /// <summary>
    /// Gets or sets whether the protocol supports authentication.
    /// </summary>
    public bool SupportsAuthentication { get; set; }
    
    /// <summary>
    /// Gets or sets whether the protocol supports encryption.
    /// </summary>
    public bool SupportsEncryption { get; set; }
    
    /// <summary>
    /// Gets or sets whether the protocol supports compression.
    /// </summary>
    public bool SupportsCompression { get; set; }
    
    /// <summary>
    /// Gets or sets the maximum message size supported by the protocol.
    /// </summary>
    public long? MaxMessageSize { get; set; }
    
    /// <summary>
    /// Gets or sets the maximum concurrent connections supported by the protocol.
    /// </summary>
    public int? MaxConcurrentConnections { get; set; }
}
