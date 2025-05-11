namespace FlowOrchestrator.Abstractions.Protocols;

/// <summary>
/// Represents the status of a connection to an external system.
/// </summary>
public enum ConnectionStatus
{
    /// <summary>
    /// The connection is not initialized.
    /// </summary>
    NotInitialized,
    
    /// <summary>
    /// The connection is initialized but not connected.
    /// </summary>
    Initialized,
    
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
    /// The connection is disconnected.
    /// </summary>
    Disconnected,
    
    /// <summary>
    /// The connection has failed.
    /// </summary>
    Failed
}
