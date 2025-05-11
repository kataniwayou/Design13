namespace FlowOrchestrator.ProtocolAdapters;

/// <summary>
/// Represents the capabilities of a protocol adapter.
/// </summary>
public class ProtocolCapabilities
{
    /// <summary>
    /// Gets or sets a value indicating whether the protocol supports authentication.
    /// </summary>
    public bool SupportsAuthentication { get; set; }
    
    /// <summary>
    /// Gets or sets a value indicating whether the protocol supports encryption.
    /// </summary>
    public bool SupportsEncryption { get; set; }
    
    /// <summary>
    /// Gets or sets a value indicating whether the protocol supports compression.
    /// </summary>
    public bool SupportsCompression { get; set; }
    
    /// <summary>
    /// Gets or sets a value indicating whether the protocol supports streaming.
    /// </summary>
    public bool SupportsStreaming { get; set; }
    
    /// <summary>
    /// Gets or sets a value indicating whether the protocol supports binary data.
    /// </summary>
    public bool SupportsBinaryData { get; set; }
    
    /// <summary>
    /// Gets or sets a value indicating whether the protocol supports text data.
    /// </summary>
    public bool SupportsTextData { get; set; }
    
    /// <summary>
    /// Gets or sets a value indicating whether the protocol supports structured data.
    /// </summary>
    public bool SupportsStructuredData { get; set; }
    
    /// <summary>
    /// Gets or sets a value indicating whether the protocol supports transactions.
    /// </summary>
    public bool SupportsTransactions { get; set; }
    
    /// <summary>
    /// Gets or sets a value indicating whether the protocol supports multiplexing.
    /// </summary>
    public bool SupportsMultiplexing { get; set; }
    
    /// <summary>
    /// Gets or sets a value indicating whether the protocol supports keep-alive.
    /// </summary>
    public bool SupportsKeepAlive { get; set; }
    
    /// <summary>
    /// Gets or sets a value indicating whether the protocol supports reconnection.
    /// </summary>
    public bool SupportsReconnection { get; set; }
    
    /// <summary>
    /// Gets or sets a value indicating whether the protocol supports flow control.
    /// </summary>
    public bool SupportsFlowControl { get; set; }
    
    /// <summary>
    /// Gets or sets a value indicating whether the protocol supports error correction.
    /// </summary>
    public bool SupportsErrorCorrection { get; set; }
    
    /// <summary>
    /// Gets or sets a value indicating whether the protocol supports error detection.
    /// </summary>
    public bool SupportsErrorDetection { get; set; }
    
    /// <summary>
    /// Gets or sets a value indicating whether the protocol supports quality of service.
    /// </summary>
    public bool SupportsQualityOfService { get; set; }
    
    /// <summary>
    /// Gets or sets the supported authentication methods.
    /// </summary>
    public List<string> SupportedAuthenticationMethods { get; set; } = new List<string>();
    
    /// <summary>
    /// Gets or sets the supported encryption methods.
    /// </summary>
    public List<string> SupportedEncryptionMethods { get; set; } = new List<string>();
    
    /// <summary>
    /// Gets or sets the supported compression methods.
    /// </summary>
    public List<string> SupportedCompressionMethods { get; set; } = new List<string>();
    
    /// <summary>
    /// Gets or sets the supported data formats.
    /// </summary>
    public List<string> SupportedDataFormats { get; set; } = new List<string>();
}
