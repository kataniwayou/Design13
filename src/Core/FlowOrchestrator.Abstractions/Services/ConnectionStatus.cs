namespace FlowOrchestrator.Abstractions.Services;

/// <summary>
/// Represents the status of a connection.
/// </summary>
public enum ConnectionStatus
{
    /// <summary>
    /// The connection is disconnected.
    /// </summary>
    Disconnected,
    
    /// <summary>
    /// The connection is connecting.
    /// </summary>
    Connecting,
    
    /// <summary>
    /// The connection is connected.
    /// </summary>
    Connected,
    
    /// <summary>
    /// The connection is disconnecting.
    /// </summary>
    Disconnecting,
    
    /// <summary>
    /// The connection has failed.
    /// </summary>
    Failed,
    
    /// <summary>
    /// The connection status is unknown.
    /// </summary>
    Unknown
}
